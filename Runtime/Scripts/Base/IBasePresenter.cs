using System;
using UnityEngine;

namespace Plugins.Antonoix.UISystem.Base
{
    public interface IBasePresenter
    {
        BaseUIView BaseUIView { get; } 
        Type ViewType { get; }  
        Type ModelType { get; }    
        string UIPrefabAddressablesName { get; } 
        void Initialize(GameObject view, BaseUIModel model);
        void Show(bool withAnimation = true);
        void Hide(bool withAnimation = true);
        void Dispose();
    }
}