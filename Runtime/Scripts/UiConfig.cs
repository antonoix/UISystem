using Plugins.Antonoix.UISystem.Editor.Scripts;
using UnityEngine;

namespace Plugins.Antonoix.UISystem
{
    [CreateAssetMenu(fileName = "UiConfig", menuName = "Configs/UiConfig")]
    public class UiConfig : ScriptableObject
    {
        [field: SerializeField] public Canvas UIRootPrefab { get; internal set; }
        [field: SerializeField] public bool ForcePresentersInit { get; internal set; }

#if UNITY_EDITOR
        private static UiConfig _instance;

        public static UiConfig Instance => LoadUIConfig();

        private static UiConfig LoadUIConfig()
        {
            if (_instance != null)
                return _instance;

            _instance = UnityEditor.AssetDatabase
                .LoadAssetAtPath<UiConfig>(PathConstants.PATH_TO_CONFIG);

            if (_instance != null)
                return _instance;

            _instance = CreateInstance<UiConfig>();
            UnityEditor.AssetDatabase.CreateAsset(_instance, PathConstants.PATH_TO_CONFIG);

            return _instance;
        }
#endif
    }
}