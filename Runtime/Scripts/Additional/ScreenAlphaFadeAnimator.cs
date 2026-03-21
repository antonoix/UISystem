using Cysharp.Threading.Tasks;
using DG.Tweening;
using Plugins.Antonoix.UISystem.Base;
using UnityEngine;

namespace Plugins.Antonoix.UISystem.Additional
{
	public class ScreenAlphaFadeAnimator : ScreenFadeAnimator
	{
		[SerializeField] private CanvasGroup _canvasGroup;
		[SerializeField] private Ease _ease = Ease.OutQuad;

		private Tween _tween;

		public override async UniTask FadeIn()
		{
			Kill();
			_canvasGroup.alpha = 0f;
			_tween = DOTween.To(() => _canvasGroup.alpha, x => _canvasGroup.alpha = x, 1f, _duration)
				.SetEase(_ease).SetLink(gameObject);
			await UniTask.WaitWhile(() => _tween != null && _tween.IsActive() && !_tween.IsComplete());
		}

		public override async UniTask FadeOut()
		{
			Kill();
			_canvasGroup.alpha = 1f;
			_tween = DOTween.To(() => _canvasGroup.alpha, x => _canvasGroup.alpha = x, 0f, _duration)
				.SetEase(_ease).SetLink(gameObject);
			await UniTask.WaitWhile(() => _tween != null && _tween.IsActive() && !_tween.IsComplete());
		}

		private void Kill()
		{
			_tween?.Kill();
			_tween = null;
		}

		private void OnDestroy()
		{
			Kill();
		}
	}
}