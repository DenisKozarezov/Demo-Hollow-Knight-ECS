using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Editor
{
    public sealed class TrackballUIAttribute : PropertyAttribute
    {
        public float MouseSensivity { get; set; } = 0.2f;
        public TrackballUIAttribute()
        {

        }
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(TrackballUIAttribute))]
    internal class TrackballUIAttributeDrawer : PropertyDrawer
    {
        private readonly int _thumbHash = "colorWheelThumb".GetHashCode();
        private const string ColorWheelPath = "Assets/Scripts/Editor/2D/ColorWheelTexture.png";
        private const string ThumbPath = "Assets/Scripts/Editor/2D/Thumb.png";
        private const float WheelThumbRadius = 0.2f;

        private GUIStyle _wheelThumb = new GUIStyle();
        private GUIStyle _colorWheel = new GUIStyle();
        private Texture2D _colorWheelTexture = null;
        private Texture2D _wheelThumbTexture = null;

        private Vector2 WheelSize => new Vector2(150, 150);
        private Vector2 _wheelThumbPos;
        private Vector2 _cursorPos;   
        private float _mouseSensivity;       

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label) * 11.5f;
        }
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SerializedPropertyType.Vector4)
            {
                Debug.LogWarning("TrackballUIAttribute requires a Vector4 property");
                return;
            }

            var trackballAttribute = (TrackballUIAttribute)attribute;
            _mouseSensivity = trackballAttribute.MouseSensivity;
            Vector4 value = property.vector4Value;

            position.width = WheelSize.x;
            EditorGUI.BeginProperty(position, label, property);

            DrawLabel(position, label);
            position.y += EditorGUIUtility.singleLineHeight;
            DrawWheel(position);
            DrawThumb(position);

            // Slider
            position.y += WheelSize.y;
            value.w = DrawSlider(position, ref value.w);
            EditorGUI.EndProperty();

            // Reset button
            position.height = EditorGUIUtility.singleLineHeight;
            position.x += EditorGUIUtility.labelWidth;
            position.width = 50f;
            if (GUI.Button(position, new GUIContent("Reset", "Immediately resets all values of Vector4 property to default.")))
            {
                Reset(ref value);
            }

            property.vector4Value = value;
        }
        private void InitStyles()
        {
            if (_colorWheelTexture == null)
            {
                _colorWheelTexture = AssetDatabase.LoadAssetAtPath<Texture2D>(ColorWheelPath);
            }
            if (_wheelThumbTexture == null)
            {
                _wheelThumbTexture = AssetDatabase.LoadAssetAtPath<Texture2D>(ThumbPath);
            }

            _colorWheel.normal.background = _colorWheelTexture;
            _wheelThumb.normal.background = _wheelThumbTexture;
        }   

        private void DrawLabel(Rect position, GUIContent label)
        {
            GUIStyle style = new GUIStyle(EditorStyles.centeredGreyMiniLabel);
            style.fontSize = 11;
            style.normal.textColor = Color.white;
            style.alignment = TextAnchor.UpperCenter;
            EditorGUI.LabelField(position, label, style);
        }
        private void DrawWheel(Rect position)
        {
            if (Event.current.type == EventType.Repaint)
            {
                // Init all styles and textures
                InitStyles();

                // Retina support
                float scale = EditorGUIUtility.pixelsPerPoint;

                // Wheel texture
                _colorWheel.Draw(new Rect(position.position, WheelSize), false, false, false, false);
            }
        }
        private void DrawThumb(Rect position)
        {
            if (Event.current.type == EventType.Repaint)
            {
                Vector2 size = new Vector2(_wheelThumb.normal.background.width, _wheelThumb.normal.background.height) * WheelThumbRadius;

                position.x += WheelSize.x / 2 - size.x / 2;
                position.y += WheelSize.y / 2 - size.y / 2;
                position.position += _wheelThumbPos;

                _wheelThumb.Draw(new Rect(position.position, size), false, false, false, false);
            }
        }
        private float DrawSlider(Rect rect, ref float value)
        {
            rect.x += 10f;
            rect.width *= 0.9f;
            rect.height = EditorGUIUtility.singleLineHeight;
            value = GUI.HorizontalSlider(rect, value, -1f, 1f);
            rect.y += EditorGUIUtility.singleLineHeight;

            GUIStyle style = new GUIStyle(EditorStyles.centeredGreyMiniLabel);
            style.alignment = TextAnchor.MiddleLeft;
            EditorGUI.LabelField(rect, "-1.0", style);
            style.alignment = TextAnchor.MiddleCenter;
            EditorGUI.LabelField(rect, value.ToString("F2"), style);
            style.alignment = TextAnchor.MiddleRight;
            EditorGUI.LabelField(rect, "1.0", style);
            return value;
        }
        private Vector3 GetInput(Rect bounds, Vector3 hsv, Vector2 thumbPos, float radius)
        {
            Event e = Event.current;
            int id = GUIUtility.GetControlID(_thumbHash, FocusType.Passive, bounds);
            Vector2 mousePos = e.mousePosition;

            if (e.type == EventType.MouseDown && GUIUtility.hotControl == 0 && bounds.Contains(mousePos))
            {
                if (e.button == 0)
                {
                    var center = new Vector2(bounds.x + radius, bounds.y + radius);
                    float dist = Vector2.Distance(center, mousePos);

                    if (dist <= radius)
                    {
                        e.Use();
                        _cursorPos = new Vector2(thumbPos.x + radius, thumbPos.y + radius);
                        GUIUtility.hotControl = id;
                        GUI.changed = true;
                    }
                }
                else if (e.button == 1)
                {
                    e.Use();
                    GUI.changed = true;
                }
            }
            else if (e.type == EventType.MouseDrag && e.button == 0 && GUIUtility.hotControl == id)
            {
                e.Use();
                GUI.changed = true;
                _cursorPos += e.delta * _mouseSensivity; // Sensitivity
                GetWheelHueSaturation(_cursorPos.x, _cursorPos.y, radius, out hsv.x, out hsv.y);
            }
            else if (e.rawType == EventType.MouseUp && e.button == 0 && GUIUtility.hotControl == id)
            {
                e.Use();
                GUIUtility.hotControl = 0;
            }

            return hsv;
        }
        private void GetWheelHueSaturation(float x, float y, float radius, out float hue, out float saturation)
        {
            float dx = (x - radius) / radius;
            float dy = (y - radius) / radius;
            float d = Mathf.Sqrt(dx * dx + dy * dy);
            hue = Mathf.Atan2(dx, -dy);
            hue = 1f - ((hue > 0) ? hue : (Mathf.PI * 2f) + hue) / (Mathf.PI * 2f);
            saturation = Mathf.Clamp01(d);
        }
        private void Reset(ref Vector4 value)
        {
            value = Vector4.zero;
            _wheelThumbPos = Vector2.zero;
        }
    }
#endif
}