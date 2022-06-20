using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Core.Serialization
{
    internal static class LoadDataProvider
    {
        private const string ConfigPath = "Assets\\Config\\";

        private static bool IsExists(string fileName)
        {
            return File.Exists(ConfigPath + fileName);
        }
        public static async void Save(ISerializableObject save, string fileName = null)
        {
            if (!IsExists(fileName))
            {
                File.CreateText(ConfigPath + fileName);

#if UNITY_EDITOR
                AssetDatabase.Refresh();
#endif
            }

            // Clear file
            var file = File.OpenWrite(ConfigPath + fileName);
            file.Flush();

            // Write to file
            byte[] bytes = Encoding.Default.GetBytes(save.Serialize().ToString());
            await file.WriteAsync(bytes, 0, bytes.Length);
            file.Close();
        }
        public static JObject Load(string slotName)
        {
            JObject obj = new JObject();
            return obj;
        }
    }
}