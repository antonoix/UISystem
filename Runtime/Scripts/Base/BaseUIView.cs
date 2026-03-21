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

        public virtual async UniTask Show()
        {
            gameObject.SetActive(true);

            List<UniTask> tasks = new List<UniTask>();
            foreach (var animator in _showAnimators)
            {
                if (animator)
                    tasks.Add(animator.FadeIn());
            }

            await UniTask.WhenAll(tasks);
        }

        public virtual async UniTask Hide()
        {
            List<UniTask> tasks = new List<UniTask>();
            foreach (var animator in _hideAnimators)
            {
                if (animator)
                    tasks.Add(animator.FadeOut());
            }

            await UniTask.WhenAll(tasks);

            gameObject.SetActive(false);
        }
    }
}