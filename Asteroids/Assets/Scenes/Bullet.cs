using System;
using UnityEngine;

namespace Scenes
{
    public class Bullet:MonoBehaviour
    {
        public float speed;
        private void Start()
        {
            Destroy (this.gameObject, 5f);
        }

        private void Update()
        {
            transform.position += transform.up * (Time.deltaTime*speed );
        }
    }
}