using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Cysharp.Threading.Tasks;
using Plugins.Antonoix.UISystem.Base;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;
using Object = UnityEngine.Object;

namespace Plugins.Antonoix.UISystem
{
    public class UIService : IUiService, IInitializable
    {
        private readonly UiConfig             _config;
        private readonly DiContainer          _container;
        private readonly List<IBasePresenter> _presenters         = new();
        private readonly List<Type>           _allPresentersTypes = new();

        private bool             _isInitialized;
        private Transform        _root;
        private MonoEventsHelper _monoEventsHelper;

        public UIService(UiConfig config, DiContainer container)
        {
            _config = config;
            _container = container;
        }

        public void Initialize()
        {
            InitializePresenters().Forget();
        }

        public async UniTask<T> GetPresenter<T>() where T : IBasePresenter
        {
            await UniTask.WaitUntil(() => _isInitialized);
            
            var presenter = _presenters.FirstOrDefault(x => x is T);
            presenter ??= await CreatePresenter(typeof(T));
            
            return (T)presenter;
        }

        private async UniTaskVoid InitializePresenters()
        {
            _root             = Object.Instantiate(_config.UIRootPrefab).transform;
            _monoEventsHelper = _root.gameObject.AddComponent<MonoEventsHelper>();
            GameObject.DontDestroyOnLoad(_root);

            if (_config.ForcePresentersInit)
            {
                foreach (var presenterType in UiClassesReflection.FindAllPresenters())
                {
                    await CreatePresenter(presenterType);
                }
            }
            
            _allPresentersTypes.AddRange(UiClassesReflection.FindAllPresenters());

            _isInitialized = true;
        }

        private async UniTask<IBasePresenter> CreatePresenter(Type presenterType)
        {
            var presenter = _container.Instantiate(presenterType) as IBasePresenter;
            
            if (presenter == null)
            {
                Debug.LogError($"Can not create {presenterType}");
                return default;
            }

            _monoEventsHelper.RegisterUpdatable(presenter as IUpdatable);
                
            var viewLoad = Addressables.LoadAssetAsync<GameObject>(presenter.UIPrefabAddressablesName);
            await UniTask.WaitWhile(() => viewLoad.Status == AsyncOperationStatus.None);
            var view = _container.InstantiatePrefab(viewLoad.Result, _root);
            var model = _container.Instantiate(presenter.ModelType) as BaseUIModel;
            
            presenter.Initialize(view, model);
            _presenters.Add(presenter);
            
            return presenter;
        }
    }
}