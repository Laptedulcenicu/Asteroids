#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Modules.Services.DataService
{
    public class CacheTool : Editor
    {
        //(Ctrl+Shift+Del) is the shortcut to delete the cash
        [MenuItem("Tools/Delete Cache %#DEL")]
        private static void DeleteCash()
        {
            string rootFolder = CacheConfig.PersistentDataPath + ".json";

            if (Application.isPlaying)
            {
                Debug.LogWarning("Cache can't be deleted in play mode!");
                return;
            }

            if (!File.Exists(rootFolder))
            {
                Debug.LogWarning("File does not exist!");
                return;
            }

            File.Delete(rootFolder);
            Debug.Log("Cache deleted");
        }
    }
}
#endif