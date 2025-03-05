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
        //public enum States
        //{
        //    MOVING,
        //    STOPING
        //}

        #endregion ENUM 

        #region Variables
        // public FootballSoccerBallManager footballSoccerBallManager;

        protected Player_StateMechanics playerFSM;
        //protected States state;

        public Vector3 movPlayer;
        protected float movAxis;

        [SerializeField] PlayerInput inputActions;
        [SerializeField] Transform PointBarrerHumanA;
        [SerializeField] Transform PointBarrerHumanB;
        //[SerializeField] GameObject barrerHuman;

        [SerializeField, HideInInspector] float speedMovement = 10f;


        #endregion Variables

        #region PublicUnityMethods
        void Start() {
            //barrerHuman.GetComponent<GameObject>();
            playerFSM = Player_StateMechanics.IDLE;
        }
        void Update() {
            switch (playerFSM) {
                case Player_StateMechanics.IDLE:
                    break;
                case Player_StateMechanics.MOVE:
                    Vector3 newPosition = transform.position + new Vector3(0, 0, movPlayer.z) * speedMovement * Time.deltaTime;
                    newPosition.z = Mathf.Clamp(newPosition.z, PointBarrerHumanA.position.z, PointBarrerHumanB.position.z);
                    transform.position = newPosition;
                    break;
                case Player_StateMechanics.STOP:
                    break;
            }
        }
        #endregion PublicUnityMethods

        #region PublicMethods

        public void MovePlayer(InputAction.CallbackContext value) {
            Debug.Log("Hola");
            if (value.performed) {
                movAxis = value.ReadValue<float>();
                movPlayer = Vector3.forward * movAxis;
                playerFSM = Player_StateMechanics.MOVE;
                //footballSoccerBallManager.TransitionForwardBall(); a ver si lo hago que lo haga el jugador
                // transform.localPosition = new Vector3((float)PointBarrerHumanA.position, PointBarrerHumanB.position, porcentaje);
                speedMovement = 10f;
            } else if (value.canceled) {
                playerFSM = Player_StateMechanics.STOP;
                //speedMovement = 0f;
                movPlayer = Vector3.zero;
            }
        }

        #endregion PublicMethods
    }
}