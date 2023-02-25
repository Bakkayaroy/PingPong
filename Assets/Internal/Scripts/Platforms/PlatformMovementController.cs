using System.Linq;
using Internal.Scripts.Ball;
using UnityEngine;
using Zenject;

namespace Internal.Scripts.Platforms
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlatformMovementController : MonoBehaviour
    {
        [SerializeField] protected float _speed;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] protected float _maxOffset = 3f;

        private PlayerInput _playerInput;
        private IBallMovementController _ballMovementController;

        [Inject]
        private void Construct(IBallMovementController ballMovementController)
        {
            _ballMovementController = ballMovementController;
        }

        private void Awake()
        {
            _playerInput = new PlayerInput();
        }

        private void OnEnable()
        {
            _playerInput.Enable();
        }

        private void OnDisable()
        {
            _playerInput.Disable();
        }

        private void FixedUpdate()
        {
            Move(GetMoveVelocity());
        }

        protected virtual Vector3 GetMoveVelocity()
        {
            var touchPosition = _playerInput.Player.TouchPosition.ReadValue<Vector2>();
            var position = touchPosition.x / Screen.width * _maxOffset * 2;
            position = Mathf.Clamp(position, 0, _maxOffset * 2);
            var direction = position - transform.position.x - _maxOffset;
            return new Vector3(direction * _speed, 0, 0);
        }

        private void Move(Vector3 velocity)
        {
            _rigidbody.velocity = velocity;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Ball"))
            {
                _ballMovementController.Bounce(_rigidbody.velocity, collision.contacts.First().normal,
                    _rigidbody.velocity.magnitude, 1f);
            }
        }
    }
}