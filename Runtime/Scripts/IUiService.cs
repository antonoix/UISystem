using Cysharp.Threading.Tasks;
using Plugins.Antonoix.UISystem.Base;

namespace Plugins.Antonoix.UISystem
{
    public interface IUiService
    {
        UniTask<T> GetPresenter<T>() where T : IBasePresenter;
    }
}