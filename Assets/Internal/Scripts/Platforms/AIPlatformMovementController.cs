using Internal.Scripts.Ball;
using UnityEngine;
using Zenject;

namespace Internal.Scripts.Platforms
{
    public class AIPlatformMovementController : PlatformMovementController
    {
        private IBallMovementController _ball;

        [Inject]
        private void Construct(IBallMovementController ball)
        {
            _ball = ball;
        }

        protected override Vector3 GetMoveVelocity()
        {
            var platformPosition = transform.position;
            var ballPosition = _ball.Transform.position;
            var targetPosition = new Vector3(Mathf.Clamp(ballPosition.x, -_maxOffset, _maxOffset), 0, 0);
            var speed = _speed * (platformPosition.z / Mathf.Pow(Vector3.Distance(ballPosition, platformPosition), 2));

            return (targetPosition - platformPosition) * Mathf.Min(speed, _speed);
        }
    }
}