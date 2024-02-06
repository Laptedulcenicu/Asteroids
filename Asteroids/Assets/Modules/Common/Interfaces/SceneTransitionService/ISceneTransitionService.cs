using System;

namespace Modules.Common
{
    public interface ISceneTransitionService
    {
        void ChangeScene(string sceneName, Action onLoaded);

        public void FadeOut();
    }
}