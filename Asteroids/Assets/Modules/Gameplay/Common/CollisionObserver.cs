using System;
using UnityEngine;

public class CollisionObserver : MonoBehaviour
{
    public event Action<Collision> CollisionEnter;
    private void OnCollisionEnter(Collision other)=> CollisionEnter?.Invoke(other);

}
