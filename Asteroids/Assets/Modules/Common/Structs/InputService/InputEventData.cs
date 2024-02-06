using UnityEngine;

namespace Modules.Common
{
    public struct InputEventData
    {
        public bool IsDraggingStarted { get; }
        public Vector2 InputPosition { get; }

        public InputEventData(Vector2 inputPosition, bool isDraggingStarted) {
            InputPosition = inputPosition;
            IsDraggingStarted = isDraggingStarted;
        }
    }
}