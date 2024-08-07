#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Plugins.Antonoix.UISystem.Editor.Scripts
{
    public class UiConfigWindow : UnityEditor.EditorWindow
    {
        private const string ANTONOIX_UI_SYSTEM = "Antonoix UI system";
        private const string PRESENTERS_LIST_STYLE = "presenterListElement";
        private const string UI_ROOT_FIELD = "UIRootPrefab";
        private const string FORCE_INIT_PRESENTERS = "ForceInitPresenters";
        private const string PRESENTERS_LIST_FIELD = "PresentersList";

        [UnityEditor.MenuItem("Tools/Antonoix/UI system")]
        public static void Open()
        {
            UiConfigWindow window = GetWindow<UiConfigWindow>();
            window.titleContent = new GUIContent(ANTONOIX_UI_SYSTEM);
            window.Show();
        }

        private void CreateGUI()
        {
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(PathConstants.PATH_TO_DOCUMENT);
            VisualElement tree = visualTree.Instantiate();
        
            rootVisualElement.Add(tree);
        
            DrawPresenters();
            BindUIRootPrefab();
            BindForceInitPresenters();
        }

        private void OnFocus()
        {
            DrawPresenters();
            BindUIRootPrefab();
            BindForceInitPresenters();
        }

        private void DrawPresenters()
        {
            GroupBox presentersList = rootVisualElement.Q<GroupBox>(PRESENTERS_LIST_FIELD);

            var presenters = UiClassesReflection.FindAllPresenters();

            var presentersListElementStyle = AssetDatabase.LoadAssetAtPath<StyleSheet>(PathConstants.BASE_STYLES_PATH);

            presentersList?.Clear();
            
            foreach (var presenter in presenters)
            {
                var element = new VisualElement();
                element.styleSheets.Add(presentersListElementStyle);
                element.AddToClassList(PRESENTERS_LIST_STYLE);

                var label = new Label();
                label.text = presenter.Name;
            
                element.Add(label);
                presentersList?.Add(element);
            }
        }

        private void BindUIRootPrefab()
        {
            ObjectField uiRootPrefab = rootVisualElement.Q<ObjectField>(UI_ROOT_FIELD);
            if (uiRootPrefab == null)
                return;
            uiRootPrefab.value = UiConfig.Instance.UIRootPrefab;
            uiRootPrefab.RegisterValueChangedCallback((evt) => 
                { UiConfig.Instance.UIRootPrefab = (Canvas)evt.newValue; });
        }

        private void BindForceInitPresenters()
        {
            Toggle forceInitPresenters = rootVisualElement.Q<Toggle>(FORCE_INIT_PRESENTERS);
            if (forceInitPresenters == null)
                return;
            forceInitPresenters.value = UiConfig.Instance.ForcePresentersInit;
            forceInitPresenters.RegisterValueChangedCallback((evt) => 
                { UiConfig.Instance.ForcePresentersInit = evt.newValue; });
        }
    }
}
#endif
