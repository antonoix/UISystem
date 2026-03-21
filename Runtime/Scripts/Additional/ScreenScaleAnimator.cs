using Cysharp.Threading.Tasks;
using DG.Tweening;
using Plugins.Antonoix.UISystem.Base;
using UnityEngine;

namespace Plugins.Antonoix.UISystem.Additional
{
	public class ScreenScaleAnimator : ScreenFadeAnimator
	{
		[SerializeField] private Transform _target;
		[SerializeField] private Vector3 _hiddenScale = Vector3.zero;
		[SerializeField] private Vector3 _shownScale = Vector3.one;
		[SerializeField] private Ease _ease = Ease.OutBack;

		private Tween _tween;

		public override async UniTask FadeIn()
		{
			Kill();
			_target.localScale = _hiddenScale;
			_tween = _target.DOScale(_shownScale, _duration).SetEase(_ease).SetLink(gameObject);
			await UniTask.WaitWhile(() => _tween != null && _tween.IsActive() && !_tween.IsComplete());
		}

		public override async UniTask FadeOut()
		{
			Kill();
			_target.localScale = _shownScale;
			_tween = _target.DOScale(_hiddenScale, _duration).SetEase(_ease).SetLink(gameObject);
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
