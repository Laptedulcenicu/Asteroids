using UnityEngine;

namespace Modules.Services.InputService
{
    public class EditorInputSource : BaseInputSource
    {
        public override Vector2 InputPosition => Input.mousePosition;
        public override bool PointerDown => Input.GetMouseButtonDown(0);
        public override bool PointerUp => Input.GetMouseButtonUp(0);
        public override bool PointerStay => Input.GetMouseButton(0);
    }
}