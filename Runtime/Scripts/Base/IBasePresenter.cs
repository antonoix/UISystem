using UnityEngine;

namespace Plugins.Antonoix.UISystem.Base
{
    public interface IBasePresenter
    {
        public string UIPrefabAddressablesName { get; }
        public void Initialize(GameObject view);
        public void Show();
        public void Hide();
    }
}