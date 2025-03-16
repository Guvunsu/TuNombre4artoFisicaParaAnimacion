using UnityEngine;
using UnityEngine.InputSystem;

namespace Gavryk.Physics.BlackHole {
    public class MovementOVNI : MonoBehaviour {
        #region Enum
        public enum MoveShip {
            MOVING,
            STOP_IT
        }
        #endregion Enum

        #region Variables
        [SerializeField] float velocity;
        [SerializeField] float speedMov = 0f;
        [SerializeField] float PosX;
        [SerializeField] float PosY;
        protected float movAxis;

        protected MoveShip playerFSM;
        Vector3 direction;

        [SerializeField] InputAction inputActions;
        [SerializeField] GameObject panelWin;
        [SerializeField] GameObject panelLose;

        #endregion Variables

        #region UnityMethods
        void Start() {
            direction = Vector3.zero;
            playerFSM = MoveShip.STOP_IT;
            speedMov = 0f;
            velocity = 0f;
        }

        void Update() {
            switch (playerFSM) {
                case MoveShip.MOVING:
                    break;
                case MoveShip.STOP_IT:
                    break;
            }
        }
        #endregion UnityMethods

        #region MoveShip
        public void MoveSpaceShip(InputAction.CallbackContext value) {
            if (value.performed) {
                speedMov += Time.fixedDeltaTime + velocity + movAxis;
                transform.Translate(direction * PosX * speedMov * Time.fixedDeltaTime);
                MoveShip playerFSM = MoveShip.MOVING;
            }
            if (value.performed) {
                speedMov += Time.fixedDeltaTime + velocity + movAxis;
                transform.Translate(direction * PosY * speedMov * Time.fixedDeltaTime);
                MoveShip playerFSM = MoveShip.MOVING;
            } else if (value.canceled) {
                movAxis = PosX + PosY;
                speedMov -= Time.fixedDeltaTime + velocity + movAxis;
                if (speedMov <= 0f) {
                    transform.Translate(direction * speedMov * Time.fixedDeltaTime);
                    MoveShip playerFSM = MoveShip.STOP_IT;
                }
            }
        }
        #endregion MoveShip

        #region Victory&LosePanel
        public void VictoryPanel() {
            panelWin.SetActive(true);
            playerFSM = MoveShip.STOP_IT;
        }
        public void LosePanel() {
            panelLose.SetActive(true);
            playerFSM = MoveShip.STOP_IT;
        }

        #endregion Victory&LosePanel

        #region Collisions
        public void OnTriggerEnter(Collider other) {
            if (other.gameObject.CompareTag("BlackHole")) {
                LosePanel();
            }
        }
        #endregion Collisions
    }
}