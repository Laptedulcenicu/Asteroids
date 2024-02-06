using Modules.Common;
using UnityEngine;

namespace Modules.Services.InputService
{
    public class InputController : MonoBehaviour, IInputController
    {
        private IInputSource _inputSource;

        private bool _isDragStarted;
        private const double k_Threshold = 0.01f;

        private Vector3 _inputDownPosition;
        private Vector3 _inputUpPosition;
        private Vector3 _inputCurrentPosition;

        private void Update()
        {
            if (_inputSource == null)
            {
                return;
            }

            if (_inputSource.PointerDown)
            {
                InputDown(_inputSource.InputPosition);
            }

            if (_inputSource.PointerStay)
            {
                InputHold(_inputSource.InputPosition);
            }

            if (_inputSource.PointerUp)
            {
                InputUp(_inputSource.InputPosition);
            }
        }

        public void Setup(IInputSource inputSource)
        {
            _inputSource = inputSource;
        }

        private void InputDown(Vector3 position)
        {
            _inputDownPosition = position;
            _inputSource.Tap(new InputEventData(_inputUpPosition, _isDragStarted));
        }

        private void InputHold(Vector3 position)
        {
            _inputCurrentPosition = position;
            CheckDragStaredStatus();
            CheckDraggingStatus();
        }

        private void InputUp(Vector3 position)
        {
            _inputUpPosition = position;
            _isDragStarted = false;
            _inputSource.Drop(new InputEventData(_inputCurrentPosition, _isDragStarted));
        }

        private void CheckDragStaredStatus()
        {
            if (_isDragStarted || !IsChangePositionDrag()) return;
            _isDragStarted = true;
            _inputSource.DragStarted(new InputEventData(_inputCurrentPosition, _isDragStarted));
        }

        private void CheckDraggingStatus()
        {
            if (IsChangePositionDrag())
            {
                _inputSource.Dragging(new InputEventData(_inputCurrentPosition, _isDragStarted));
            }
        }

        private bool IsChangePositionDrag()
        {
            var displacement = _inputCurrentPosition - _inputDownPosition;
            return displacement.magnitude > k_Threshold;
        }
    }
}