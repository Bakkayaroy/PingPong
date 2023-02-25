using System.Linq;
using DG.Tweening;
using UnityEngine;

namespace Internal.Scripts.Ball
{
    [RequireComponent(typeof(Rigidbody))]
    public class BallMovementController : MonoBehaviour, IBallMovementController
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _startSpeed = 15f;
        [SerializeField] private float _coeficientPlatformVelocity = 0.2f;
        [SerializeField] private float _ballAnimationScale = 0.15f;
        [SerializeField] private float _ballAnimationDuration = 1f;


        private float _currentSpeed;
        private Vector3 _lastVelocity;
        private bool _active;
        private Tweener _tween;

        private void Awake()
        {
            _currentSpeed = _startSpeed;
        }

        private void FixedUpdate()
        {
            if (!_active) return;

            var velocity = _rigidbody.velocity;

            if (velocity != Vector3.zero)
                _lastVelocity = velocity;

            _rigidbody.velocity = velocity.normalized * _currentSpeed;
        }

        public void Bounce(Vector3 platformVelocity, Vector3 normal, float speedUp, float duration)
        {
            _tween = DOVirtual.Float(Mathf.Min(speedUp, _startSpeed) + _startSpeed, _startSpeed, duration,
                value => _currentSpeed = value);
            Bounce(platformVelocity, normal);
        }

        private void Bounce(Vector3 platformVelocity, Vector3 normal)
        {
            var currentVelocity = _lastVelocity + platformVelocity * _coeficientPlatformVelocity;
            var direction = Vector3.Reflect(currentVelocity.normalized, normal);
            direction.y = 0;
            _rigidbody.velocity = direction * _currentSpeed;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Wall"))
            {
                Bounce(Vector3.zero, collision.contacts.First().normal);
            }
        }

        public Transform Transform => transform;

        public void ResetPosition()
        {
            _rigidbody.isKinematic = true;
            _rigidbody.velocity = Vector3.zero;
            transform.position = Vector3.zero;
            _tween?.Kill(true);
            _currentSpeed = _startSpeed;
            _active = false;
        }

        public void StartMove()
        {
            transform.DOPunchScale(new Vector3(_ballAnimationScale, _ballAnimationScale, _ballAnimationScale),
                _ballAnimationDuration).OnComplete(() =>
            {
                var direction = new Vector3(Random.Range(-1f, 1f), 0, Random.value > .5 ? 1f : -1f);
                _rigidbody.isKinematic = false;
                _rigidbody.AddForce(direction.normalized * _startSpeed);
                _active = true;
            });
        }
    }
}