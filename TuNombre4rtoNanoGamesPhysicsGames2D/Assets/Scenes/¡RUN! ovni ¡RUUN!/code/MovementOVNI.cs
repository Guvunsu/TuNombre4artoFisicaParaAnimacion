using UnityEngine;
using UnityEngine.InputSystem;

namespace Gavryk.Physics.BlackHole
{
    public class MovementOVNI : MonoBehaviour
    {
        #region Enum
        public enum MoveShip
        {
            MOVING,
            STOP_IT
        }
        #endregion Enum

        #region Variables
        [SerializeField] float constantVelocityFactor = 1f;
        [SerializeField] float speedMov = 0f;

        [SerializeField] protected MoveShip playerFSM;
        Vector3 direction;

        [SerializeField] GameObject panelWin;
        [SerializeField] GameObject panelLose;

        #endregion Variables

        #region UnityMethods
        void Start()
        {
            transform.position = Vector3.zero;
            direction = Vector3.zero;
            playerFSM = MoveShip.STOP_IT;
            speedMov = 0f;
        }

        void FixedUpdate()
        {
            switch (playerFSM)
            {
                case MoveShip.MOVING:
                    speedMov += Time.fixedDeltaTime * constantVelocityFactor; //aceleration: speed change over time
                    break;
                case MoveShip.STOP_IT:
                    if (speedMov > 0f)
                    {
                        speedMov -= Time.fixedDeltaTime * constantVelocityFactor;
                    }
                    else
                    {
                        speedMov = 0f;
                    }
                    break;
            }
            transform.Translate(direction * speedMov, Space.World);
        }
        #endregion UnityMethods

        #region MoveShip
        public void MoveSpaceShip(InputAction.CallbackContext value)
        {
            Debug.Log("MoveSpaceShip: " + value.ReadValue<Vector2>());
            if (value.performed)
            {
                direction = value.ReadValue<Vector2>();
                playerFSM = MoveShip.MOVING;
            }
            else if (value.canceled)
            {
                //direction = Vector2.zero;
                playerFSM = MoveShip.STOP_IT;
            }
        }
        #endregion MoveShip

        #region Victory&LosePanel
        // hacer un estado finito que me dga si estoy en el estado finito de game, vic,loose y se lo ponga en estos metodos y luyegp brincan al fsm de stopit
        public void VictoryPanel()
        {
            panelWin.SetActive(true);
            playerFSM = MoveShip.STOP_IT;
        }
        public void LosePanel()
        {
            panelLose.SetActive(true);
            playerFSM = MoveShip.STOP_IT;
        }

        #endregion Victory&LosePanel

        #region Collisions
        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("BlackHole"))
            {
                LosePanel();
            }
        }
        #endregion Collisions
    }
}