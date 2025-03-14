using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.ProBuilder.MeshOperations;
using static UnityEngine.Rendering.DebugUI;

namespace Gavryk.Physics.BlackHole
{
    public class MovementOVNI : MonoBehaviour
    {
        public enum MoveShip
        {
            MOVING,
            STOP_IT
        }

        [SerializeField] float velocity;
        [SerializeField] float speedMov = 0f;
        [SerializeField] float PosX;
        [SerializeField] float PosY;
        protected float movAxis;

        Vector3 direction;
        protected MoveShip playerFSM;

        [SerializeField] InputAction inputActions;

        void Start()
        {
            playerFSM = MoveShip.STOP_IT;
            speedMov = 0f;
            velocity = 0f;
        }

        void Update()
        {
            switch (playerFSM)
            {
                case MoveShip.MOVING:
                    MoveSpaceShip();
                    break;
                case MoveShip.STOP_IT:
                    StopTheShip();
                    break;
            }
        }
        void StopTheShip()
        {
            speedMov = 0f;
            velocity = 0f;
            speedMov = velocity;
        }
        void MoveSpaceShip(InputAction.CallbackContext value)
        {
            if (value.performed)
            {
                speedMov += Time.fixedDeltaTime + velocity + movAxis;
                direction
            }
            else if (value.canceled)
            {
                speedMov -= Time.fixedDeltaTime + velocity + movAxis;
            }
        }
    }
}