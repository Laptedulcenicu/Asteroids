using StansAssets.Foundation.Patterns;
using UnityEngine;

namespace Modules.Gameplay
{
    public class BulletView : MonoBehaviour
    {
        [SerializeField] private float bulletSpeed;
        [SerializeField] private GameObject shootParticles;
        [SerializeField] private CollisionObserver collisionObserver;
        [SerializeField] private Rigidbody rigidbody;

        private PrefabPool _prefabPool;
        private bool _isMoving;
        private Vector3 _lastVelocity;
        private Vector3 _direction;

        public bool IsMoving => _isMoving;


        public void Initialize(PrefabPool prefabPool)
        {
            _prefabPool = prefabPool;
            rigidbody.isKinematic = true;
            _isMoving = false;
            collisionObserver.CollisionEnter += CollisionEnter;
        }

        private void FixedUpdate()
        {
            if (!_isMoving)
                return;

            rigidbody.velocity = _direction;
            _lastVelocity = rigidbody.velocity;
        }

        public void Move(Vector3 vel)
        {
            _isMoving = true;
            rigidbody.isKinematic = false;
            _direction = vel * bulletSpeed;
            rigidbody.velocity = _direction;
        }

        public void Release()
        {
            _isMoving = false;
            _prefabPool.Release(gameObject);
        }

        private void CollisionEnter(Collision other)
        {
            _direction = Vector3.Reflect(_lastVelocity.normalized, other.contacts[0].normal) *
                         Mathf.Max(bulletSpeed, 0);
            rigidbody.velocity = _direction;

            if (!other.gameObject.TryGetComponent(out IInteractable interactable)) return;
            interactable.Interact(this);
        }
    }
}