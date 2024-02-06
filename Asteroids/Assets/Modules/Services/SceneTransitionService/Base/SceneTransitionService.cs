using System;
using Modules.Common;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Modules.Services.SceneTransitionService
{
    public class SceneTransitionService : ISceneTransitionService
    {
        private readonly SceneTransitionFadeController _fadeController;
        private AsyncOperation _asyncOperation;
        private Action _onLoaded;

        public SceneTransitionService(IServiceFactory serviceFactory)
        {
            _fadeController = serviceFactory.CreateFadeController().GetComponent<SceneTransitionFadeController>();
        }

        public void ChangeScene(string sceneName, Action onLoaded = null)
        {
            _onLoaded = onLoaded;
            _fadeController.FadeIn();
            LoadScene(sceneName);
        }

        public void FadeOut() => _fadeController.FadeOut();

        private void LoadScene(string sceneName)
        {
            _asyncOperation = SceneManager.LoadSceneAsync(sceneName);
            _asyncOperation.allowSceneActivation = true;
            _asyncOperation.completed += OnSceneLoaded;
        }

        private void OnSceneLoaded(AsyncOperation operation)
        {
            _asyncOperation.completed -= OnSceneLoaded;
            _asyncOperation = null;
            _onLoaded?.Invoke();
        }
    }
}