using Plugins.Antonoix.UISystem.Base;
using UnityEngine;
using Zenject;

namespace Plugins.Antonoix.UISystem
{
    [CreateAssetMenu(fileName = "UiServiceInstaller", menuName = "Installers/UiServiceInstaller")]
    public class UiServiceInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private UiConfig config;
        
        public override void InstallBindings()
        {
            Container.BindInstance(config);
            Container.BindInterfacesAndSelfTo<UIService>().AsSingle();
        }
    }
}