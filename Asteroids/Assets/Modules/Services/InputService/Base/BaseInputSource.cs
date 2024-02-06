using System;
using Modules.Common;
using UnityEngine;

namespace Modules.Services.InputService
{
    public abstract class BaseInputSource : IInputSource
    {
        public event Action<InputEventData> OnTap = delegate { };
        public event Action<InputEventData> OnDragStarted = delegate { };
        public event Action<InputEventData> OnDragging = delegate { };
        public event Action<InputEventData> OnDrop = delegate { };

        public abstract Vector2 InputPosition { get; }
        public abstract bool PointerDown { get; }
        public abstract bool PointerUp { get; }
        public abstract bool PointerStay { get; }

        public void Tap(InputEventData inputEventData) {
            OnTap?.Invoke(inputEventData);
        }

        public void DragStarted(InputEventData inputEventData) {
            OnDragStarted?.Invoke(inputEventData);
        }

        public void Dragging(InputEventData inputEventData) {
            OnDragging?.Invoke(inputEventData);
        }

        public void Drop(InputEventData inputEventData) {
            OnDrop?.Invoke(inputEventData);
        }
    }
}