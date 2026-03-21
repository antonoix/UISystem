using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Plugins.Antonoix.UISystem.Base
{
    public abstract class BaseUIView : MonoBehaviour
    {
        [SerializeField] private ScreenFadeAnimator[] _showAnimators;
        [SerializeField] private ScreenFadeAnimator[] _hideAnimators;
        
        public virtual void Initialize() { }

        public virtual void Dispose() { }

        public async UniTask Show(bool withAnimation)
        {
            OnBeforeShow();
            gameObject.SetActive(true);

            if (withAnimation)
            {
                List<UniTask> tasks = new List<UniTask>();
                foreach (var animator in _showAnimators)
                {
                    if (animator)
                        tasks.Add(animator.FadeIn());
                }

                await UniTask.WhenAll(tasks);
            }

            OnAfterShow();
        }

        public async UniTask Hide(bool withAnimation)
        {
            OnBeforeHide();

            if (withAnimation)
            {
                List<UniTask> tasks = new List<UniTask>();
                foreach (var animator in _hideAnimators)
                {
                    if (animator)
                        tasks.Add(animator.FadeOut());
                }

                await UniTask.WhenAll(tasks);
            }
            
            gameObject.SetActive(false);
            OnAfterHide();
        }

        protected virtual UniTask OnBeforeShow()
        {
            return UniTask.CompletedTask;
        }

        protected virtual UniTask OnAfterShow()
        {
            return UniTask.CompletedTask;
        }

        protected virtual UniTask OnBeforeHide()
        {
            return UniTask.CompletedTask;
        }

        protected virtual UniTask OnAfterHide()
        {
            return UniTask.CompletedTask;
        }
    }
}