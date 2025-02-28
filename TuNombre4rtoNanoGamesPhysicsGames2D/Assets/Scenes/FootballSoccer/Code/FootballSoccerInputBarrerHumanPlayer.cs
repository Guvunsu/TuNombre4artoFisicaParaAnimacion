using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gavryk.Physics.Football
{
    public class FootballSoccerInputBarrerHumanPlayer : MonoBehaviour
    {
        #region ENUM
        public enum Player_StateMechanics
        {
            IDLE,
            MOVE,
            STOP
        }
        public enum States
        {
            MOVING,
            STOPING
        }

        #endregion ENUM 

        #region Variables

        protected Player_StateMechanics playerFSM;
        protected States state;

        public Vector3 movPlayer;

        [SerializeField] PlayerInput inputActions;
        [SerializeField] GameObject barrerHuman;
        [SerializeField] Transform PointBarrerHumanA;
        [SerializeField] Transform PointBarrerHumanB;

        [SerializeField, HideInInspector] float speedMovement;


        #endregion Variables

        #region PublicUnityMethods
        void Start()
        {
            barrerHuman.GetComponent<GameObject>();
            playerFSM = Player_StateMechanics.IDLE;
        }

        // Update is called once per frame
        void Update()
        {
            switch (playerFSM)
            {
                case Player_StateMechanics.IDLE:
                    break;
                case Player_StateMechanics.MOVE:
                    break;
                case Player_StateMechanics.STOP:
                    break;
            }
        }
        #endregion PublicUnityMethods

        #region PublicMethods

        public void MovePlayer(InputAction.CallbackContext value)
        {
            if (value.performed)
            {
                movPlayer = value.ReadValue<Vector3>();
                state = States.MOVING;
                // transform.localPosition = new Vector3((float)PointBarrerHumanA.position, PointBarrerHumanB.position, porcentaje);
                speedMovement = 10f;
            }
            else if (value.canceled)
            {
                state = States.STOPING;
                speedMovement = 0f;
            }
        }

        #endregion PublicMethods
    }
}