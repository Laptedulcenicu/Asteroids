using System;
using UnityEngine;

public class WallsPosition : MonoBehaviour
{
    private const float k_DefaultRatio = 1.7f;

    [SerializeField] private Transform leftWall;
    [SerializeField] private Transform rightWall;

    private float _defaultXPositionRight;
    private float _defaultXPositionLeft;

    private void Awake()
    {
        var right = rightWall.transform.position;
        var left = leftWall.transform.position;
        _defaultXPositionRight = right.x;
        _defaultXPositionLeft = left.x;

        left = new Vector3(GetLimit(_defaultXPositionLeft), left.y, left.z);
        leftWall.transform.position = left;
        right = new Vector3(GetLimit(_defaultXPositionRight), right.y, right.z);
        rightWall.transform.position = right;
    }

    private float GetLimit(float currentLimit)
    {
        //TODO the formula need to be improved
        const float step = 0.49f;
        var targetRatio = (float)(Math.Floor(Camera.main.aspect * 10) / 10.0);
        var targetLimit = Mathf.Abs(currentLimit);

        var aspectDifference = (k_DefaultRatio - targetRatio) * 10;
        targetLimit -= step * aspectDifference;

        if (currentLimit < 0)
        {
            targetLimit *= -1;
        }

        return targetLimit;
    }
}