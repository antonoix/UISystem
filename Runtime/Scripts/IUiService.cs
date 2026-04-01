using Cysharp.Threading.Tasks;
using Plugins.Antonoix.UISystem.Base;

namespace Plugins.Antonoix.UISystem
{
	public interface IUiService
	{
		UniTask<T> GetPresenter<T>(bool setAsLast = false) where T : IBasePresenter;
		void Show(IBasePresenter presenter, bool withAnimation = true, OpenContext openContext = null);
		void HideContext(OpenContext context, bool withAnimation = false);
	}
}