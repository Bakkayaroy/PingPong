using UnityEngine;

namespace Internal.Scripts.Ball
{
    public interface IBallMovementController
    {
        public Transform Transform { get; }
        void ResetPosition();
        void Bounce(Vector3 platformVelocity, Vector3 normal, float speedUp, float duration);
        void StartMove();
    }
}