using UnityEngine;

namespace Plugins.Antonoix.UISystem.Base
{
    public abstract class BaseUIPresenter<T> : IBasePresenter where T : BaseUIView
    {
        protected T _view;

        public virtual void Show() 
            => _view.Show();

        public virtual void Hide() 
            => _view.Hide();

        public string GetName 
            => ToString();
        
        public abstract string UIPrefabAddressablesName { get; }

        public virtual void Initialize(GameObject view)
        {
            _view = view.GetComponent<T>();
            Hide();

            _view.Initialize();
        }
    }
}