using UnityEngine;

public class RaycastReflection : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] public int reflections;
    [SerializeField] public float maxLength;

    private Ray _ray;
    private RaycastHit _hit;
    private Vector3 _direction;
    private const int k_InitPositionCount = 1;

    private void Update()
    {
        DrawLine();
    }

    private void DrawLine()
    {
        _ray = new Ray(transform.position, transform.forward);

        lineRenderer.positionCount = k_InitPositionCount;
        lineRenderer.SetPosition(0, transform.position);
        var remainingLength = maxLength;

        ReflectLine(remainingLength);
    }

    private void ReflectLine(float remainingLength)
    {
        for (int i = 0; i < reflections; i++)
        {
            if (Physics.Raycast(_ray.origin, _ray.direction, out _hit, remainingLength))
            {
                remainingLength = HitRaycast(remainingLength);
            }
            else
            {
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, _ray.origin + _ray.direction * remainingLength);
            }
        }
    }

    private float HitRaycast(float remainingLength)
    {
        lineRenderer.positionCount += 1;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, _hit.point);
        remainingLength -= Vector3.Distance(_ray.origin, _hit.point);
        _ray = new Ray(_hit.point, Vector3.Reflect(_ray.direction, _hit.normal));
        return remainingLength;
    }
}