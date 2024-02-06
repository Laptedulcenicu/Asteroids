using UnityEngine;

namespace Modules.Services.SceneTransitionService
{
    public class SceneTransitionFadeController : MonoBehaviour
    {
        [SerializeField] private Animator fadeAnimator;

        private static readonly int s_FadeIn = Animator.StringToHash("FadeIn");
        private static readonly int s_FadeOut = Animator.StringToHash("FadeOut");

        private void Awake() => DontDestroyOnLoad(this);

        public void FadeIn() => fadeAnimator.SetTrigger(s_FadeIn);

        public void FadeOut() => fadeAnimator.SetTrigger(s_FadeOut);
    }
}