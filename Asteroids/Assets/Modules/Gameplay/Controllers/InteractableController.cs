using Modules.Common;
using UnityEngine;

namespace Modules.Gameplay
{
    public class InteractableController : MonoBehaviour
    {
        private IInputSource _inputSource;
        private PlayerView _playerView;
        private InputEventData _inputEventData = new(Vector2.zero, false);
        public bool CanControl { get; set; }

        private void OnDestroy() => RemoveInputListeners();

        public void Initialize(IInputSource inputSource, PlayerView playerView)
        {
            _inputSource = inputSource;
            _playerView = playerView;
            CanControl = true;
            AddInputListeners();
        }

        private void InputSourceOnTap(InputEventData inputEventData)
        {
            if (!CanControl) return;
            _playerView.ReflectionLine.SetActive(true);
        }

        private void InputSourceOnDragStarted(InputEventData inputEventData)
        {
            _inputEventData = inputEventData;
        }

        private void InputSourceOnDragging(InputEventData inputEventData)
        {
            _inputEventData = inputEventData;
            if (!CanControl) return;


            Vector3 lookAtPosition = GetLookAtPosition(_inputEventData.InputPosition);
            _playerView.transform.LookAt(new Vector3(lookAtPosition.x, _playerView.transform.position.y,
                lookAtPosition.z));
        }

        private void InputSourceOnDrop(InputEventData inputEventData)
        {
            _inputEventData = inputEventData;
            if (!CanControl) return;
            _playerView.ReflectionLine.SetActive(false);
            _playerView.PlayerShoot.Shoot();
            CanControl = false;
        }

        private Vector3 GetLookAtPosition(Vector3 inputPosition)
        {
            var ray = Camera.main.ScreenPointToRay(inputPosition);
            Physics.Raycast(ray, out var hit, 300);
            return hit.point;
        }

        private void AddInputListeners()
        {
            _inputSource.OnTap += InputSourceOnTap;
            _inputSource.OnDrop += InputSourceOnDrop;
            _inputSource.OnDragging += InputSourceOnDragging;
            _inputSource.OnDragStarted += InputSourceOnDragStarted;
        }

        private void RemoveInputListeners()
        {
            _inputSource.OnTap -= InputSourceOnTap;
            _inputSource.OnDrop -= InputSourceOnDrop;
            _inputSource.OnDragging -= InputSourceOnDragging;
            _inputSource.OnDragStarted -= InputSourceOnDragStarted;
        }
    }
}