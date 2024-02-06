using System;
using UnityEngine;

namespace Modules.Common
{
    public interface IInputSource
    {
        event Action<InputEventData> OnTap;
        event Action<InputEventData> OnDragStarted;
        event Action<InputEventData> OnDragging;
        event Action<InputEventData> OnDrop;

        Vector2 InputPosition { get; }
        bool PointerDown { get; }
        bool PointerUp { get; }
        bool PointerStay { get; }

        void Tap(InputEventData inputEventData);
        void DragStarted(InputEventData inputEventData);
        void Dragging(InputEventData inputEventData);
        void Drop(InputEventData inputEventData);
    }
}