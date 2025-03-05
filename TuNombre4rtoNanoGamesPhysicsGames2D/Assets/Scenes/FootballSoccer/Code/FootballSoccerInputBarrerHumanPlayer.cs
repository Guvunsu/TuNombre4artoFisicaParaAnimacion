using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gavryk.Physics.Football {
    public class FootballSoccerInputBarrerHumanPlayer : MonoBehaviour {
        #region ENUM
        public enum Player_StateMechanics {
            IDLE,
            MOVE,
            STOP
        }

        #endregion ENUM 

        #region Variables

        protected Player_StateMechanics playerFSM;

        public Vector3 movPlayer;
        protected float movAxis;

        [SerializeField] PlayerInput inputActions;
        [SerializeField] Transform PointBarrerHumanA;
        [SerializeField] Transform PointBarrerHumanB;

        [SerializeField, HideInInspector] float speedMovement = 10f;

        #endregion Variables

        #region PublicUnityMethods
        void Start() {
            playerFSM = Player_StateMechanics.IDLE;
        }
        void Update() {
            switch (playerFSM) {
                case Player_StateMechanics.IDLE:
                    break;
                case Player_StateMechanics.MOVE:
                    Vector3 newPosition = transform.position + new Vector3(0, 0, movPlayer.z) * speedMovement * Time.deltaTime;
                    newPosition.z = Mathf.Clamp(newPosition.z, PointBarrerHumanB.position.z, PointBarrerHumanA.position.z);
                    transform.position = newPosition;
                    break;
                case Player_StateMechanics.STOP:
                    break;
            }
        }
        #endregion PublicUnityMethods

        #region PublicMethods

        public void MovePlayer(InputAction.CallbackContext value) {
            if (value.performed) {
                movAxis = value.ReadValue<float>();
                movPlayer = Vector3.forward * movAxis;
                playerFSM = Player_StateMechanics.MOVE;
                speedMovement = 10f;
            } else if (value.canceled) {
                playerFSM = Player_StateMechanics.STOP;
                movPlayer = Vector3.zero;
            }
        }

        #endregion PublicMethods
    }
}