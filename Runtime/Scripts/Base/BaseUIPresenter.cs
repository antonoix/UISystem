using System;
using UnityEngine;

namespace Plugins.Antonoix.UISystem.Base
{
    public abstract class BaseUIPresenter<TView, TModel> : IBasePresenter
        where TView : BaseUIView
        where TModel : BaseUIModel
    {
        public TView _view;
        public TModel _model;

        public Type View => typeof(TView);
        public Type Model => typeof(TModel);
        public string GetName => ToString();
        public abstract string UIPrefabAddressablesName { get; }


        public virtual void Show() 
            => _view.Show();

        public virtual void Hide() 
            => _view.Hide();

        public virtual void Initialize(GameObject view, BaseUIModel model)
        {
            _view = view.GetComponent<TView>();
            _view.Initialize();

            _model = model as TModel;
            _model.Initialize();
            
            Hide();
        }

        public void Dispose()
        {
            _view.Dispose();
            _model.Dispose();
        }
    }
}