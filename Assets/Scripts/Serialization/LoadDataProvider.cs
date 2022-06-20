using System.IO;
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

            string data = save.Serialize().ToString();
            if (string.IsNullOrEmpty(data)) return;

            // Write to file
            byte[] bytes = Constants.DefaultEncoding.GetBytes(data);
            await File.WriteAllBytesAsync(ConfigPath + fileName, bytes);
        }
        public static JObject Load(string slotName)
        {
            JObject obj = new JObject();
            return obj;
        }
    }
}