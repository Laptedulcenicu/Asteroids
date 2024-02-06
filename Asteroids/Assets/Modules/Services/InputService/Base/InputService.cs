using Modules.Common;
using UnityEngine;

namespace Modules.Services.InputService
{
    public class InputService : IInputService
    {
        private readonly IInputSource _inputSource;
        
        public IInputSource InputSource => _inputSource;

        public InputService()
        {
            switch (Application.platform)
            {
                case RuntimePlatform.Android:
                    _inputSource = new AndroidInputSource();
                    break;
                case RuntimePlatform.IPhonePlayer:
                    _inputSource = new IOSInputSource();
                    break;
                case RuntimePlatform.WindowsEditor:
                case RuntimePlatform.OSXEditor:
                    _inputSource = new EditorInputSource();
                    break;
            }
        }
    }
}