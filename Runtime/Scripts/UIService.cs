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
        private readonly UiConfig _config;
        private readonly IInstantiator _instantiator;
        private readonly List<IBasePresenter> _presenters = new();
        private readonly List<Type> _allPresentersTypes = new();
        
        //private FirstEnterUIPresenter _firstEnterUiPresenter;
        //private MenuUIPresenter _menuUiPresenter;
        // private ShopUIPresenter _shopUiPresenter;

        private Transform _root;

        public UIService(UiConfig config, IInstantiator instantiator)
        {
            _config = config;
            _instantiator = instantiator;
        }

        public void Initialize()
        {
            _root = Object.Instantiate(_config.UIRootPrefab).transform;
            GameObject.DontDestroyOnLoad(_root);

            if (_config.ForcePresentersInit)
            {
                foreach (var presenterType in UiClassesReflection.FindAllPresenters())
                {
                    CreatePresenter(presenterType).Forget();
                }
            }
            
            _allPresentersTypes.AddRange(UiClassesReflection.FindAllPresenters());
        }

        public async UniTask<T> GetPresenter<T>() where T : IBasePresenter
        {
            var presenter = _presenters.FirstOrDefault(x => x is T);
            presenter ??= await CreatePresenter(typeof(T));
            
            return (T)presenter;
        }

        private async UniTask<IBasePresenter> CreatePresenter(Type presenterType)
        {
            var presenter = _instantiator.Instantiate(presenterType) as IBasePresenter;
            if (presenter == null)
            {
                Debug.LogError($"Can not create {presenterType}");
                return default;
            }
                
            var viewLoad = Addressables.LoadAssetAsync<GameObject>(presenter.UIPrefabAddressablesName);
            await UniTask.WaitWhile(() => viewLoad.Status == AsyncOperationStatus.None);
            var view = _instantiator.InstantiatePrefab(viewLoad.Result, _root);
            var model = _instantiator.Instantiate(presenter.Model) as BaseUIModel;
            presenter.Initialize(view, model);
                
            _presenters.Add(presenter);
            return presenter;
        }
    }
}