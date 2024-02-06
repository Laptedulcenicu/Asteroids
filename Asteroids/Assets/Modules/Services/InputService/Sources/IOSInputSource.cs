using UnityEngine;

namespace Modules.Services.InputService
{
    public class IOSInputSource : BaseInputSource
    {
        public override Vector2 InputPosition => Input.GetTouch(0).position;
        public override bool PointerDown => Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;
        public override bool PointerUp => Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended;
        public override bool PointerStay => Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved;
    }
}