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
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.CompareTag("Asteroid"))
            {
                collision.GetComponent<Asteroid>().Hit();
            }

            // if(collision.CompareTag(tagManager.Enemy))
            // {
            //     collisions.HandleCollisionWithEnemy(collision);
            // }

            Destroy(gameObject);
        }
    }
}