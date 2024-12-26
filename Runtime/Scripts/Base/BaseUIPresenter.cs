using System;
using UnityEngine;
using Zenject;

namespace Plugins.Antonoix.UISystem.Base
{
    public abstract class BaseUIPresenter<TView, TModel> : IBasePresenter, IUpdatable
        where TView : BaseUIView
        where TModel : BaseUIModel
    {
        public TView View;
        public TModel Model;

        public Type ViewType => typeof(TView);
        public Type ModelType => typeof(TModel);
        public string GetName => ToString();
        public abstract string UIPrefabAddressablesName { get; }


        public virtual void Show() 
            => View.Show();

        public virtual void Hide() 
            => View.Hide();

        public virtual void Initialize(GameObject view, BaseUIModel model)
        {
            View = view.GetComponent<TView>();
            View.Initialize();
            View.gameObject.SetActive(false);

            Model = model as TModel;
            Model.Initialize();
        }

        public void Dispose()
        {
            View.Dispose();
            Model.Dispose();
        }

        void IUpdatable.Update()
        {
            Update();
        }
        
        protected virtual void Update() {}
    }
}