using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Plugins.Antonoix.UISystem.Base
{
	public abstract class ScreenFadeAnimator : MonoBehaviour
	{
		[SerializeField] protected float _duration = 0.25f;
		
		public abstract UniTask FadeIn();
		public abstract UniTask FadeOut();
	}
}