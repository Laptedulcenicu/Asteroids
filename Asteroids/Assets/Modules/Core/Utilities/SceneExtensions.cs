using UnityEngine.SceneManagement;

namespace Modules.Core.Scripts.Utilities
{
    public static class SceneExtensions
    {
        public static T GetComponent<T>(this Scene scene) where T : class
        {
            foreach (var gameObject in scene.GetRootGameObjects())
            {
                var component = gameObject.GetComponent<T>();
                if (component != null)
                {
                    return component;
                }
            }

            return default;
        }
    }
}