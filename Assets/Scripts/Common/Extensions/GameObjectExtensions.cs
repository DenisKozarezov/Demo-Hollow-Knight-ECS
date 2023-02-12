using UnityEngine;

namespace Core
{
    public static class GameObjectExtensions
    {
        public static bool Matches(this Collider2D collider, LayerMask layerMask) =>
            ((1 << collider.gameObject.layer) & layerMask) != 0;
        public static void SetLayer(this GameObject parent, string layer, bool includeChildren = false)
        {
            int unmaskedLayer = LayerMask.NameToLayer(layer);
            parent.layer = unmaskedLayer;
            if (includeChildren)
            {
                //for some reasons this is faster than direct transform enumeration
                foreach (Transform trans in parent.transform.GetComponentsInChildren<Transform>(includeInactive: true))
                    trans.gameObject.layer = unmaskedLayer;
            }
        }
        public static void SetLayer(this GameObject parent, int layer, bool includeChildren = false)
        {
            parent.layer = layer;
            if (includeChildren)
            {
                //for some reasons this is faster than direct transform enumeration
                foreach (Transform trans in parent.transform.GetComponentsInChildren<Transform>(includeInactive: true))
                    trans.gameObject.layer = layer;
            }
        }
    }
}
