using System;
using UnityEngine;

namespace Plugins.Antonoix.UISystem.Base
{
    public interface IBasePresenter
    {
        Type ViewType { get; }  
        Type ModelType { get; }    
        string UIPrefabAddressablesName { get; } 
        void Initialize(GameObject view, BaseUIModel model);
        void Show();
        void Hide();
        void Dispose();
    }
}