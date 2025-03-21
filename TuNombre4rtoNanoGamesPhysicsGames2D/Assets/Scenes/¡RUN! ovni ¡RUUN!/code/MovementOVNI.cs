using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.ProBuilder.MeshOperations;
using static Gavryk.Physics.BlackHole.MovementOVNI;
using static UnityEngine.InputSystem.InputAction;
using static UnityEngine.Rendering.DebugUI;

namespace Gavryk.Physics.BlackHole {
    public class MovementOVNI : MonoBehaviour {
        #region Enum
        public enum MoveShip {
            MOVING,
            STOP_IT
        }
        public enum GameState {
            PLAYING,
            WIN,
            LOSE
        }
        #endregion Enum

        #region Variables
        [SerializeField] float constantVelocityFactor = 1f;
        [SerializeField] float speedMov = 0f;

        [SerializeField] protected MoveShip playerFSM;
        [SerializeField] protected GameState gameFSM;

        Vector3 direction;
        Vector3 youShallNotPass;

        [SerializeField] GameObject panelWin;
        [SerializeField] GameObject panelLose;

        //[SerializeField] TimerRUNovniRUUUN timerScript;

        #endregion Variables

        #region UnityMethods
        void Start() {
            transform.position = Vector3.zero;
            gameFSM = GameState.PLAYING;
            direction = Vector3.zero;
            playerFSM = MoveShip.STOP_IT;
            speedMov = 0f;
        }

        void FixedUpdate() {
            switch (playerFSM) {
                case MoveShip.MOVING:
                    speedMov += Time.fixedDeltaTime * constantVelocityFactor; //aceleration: speed change over time
                    if (speedMov >= 0.50f) {
                        speedMov -= 0.05f;
                    }
                    break;
                case MoveShip.STOP_IT:
                    if (speedMov > 0f) {
                        speedMov -= Time.fixedDeltaTime * constantVelocityFactor;
                    } else {
                        speedMov = 0f;
                    }
                    break;
            }
            transform.Translate(direction * speedMov, Space.World);
        }
        #endregion UnityMethods

        #region MoveShip
        public void MoveSpaceShip(InputAction.CallbackContext value) {
            Debug.Log("MoveSpaceShip: " + value.ReadValue<Vector2>());
            if (value.performed && gameFSM == GameState.PLAYING) {
                direction = value.ReadValue<Vector2>();
                playerFSM = MoveShip.MOVING;
                //gameFSM = GameState.PLAYING;
            } else if (value.canceled && gameFSM == GameState.PLAYING) {
                //direction = Vector2.zero;
                playerFSM = MoveShip.STOP_IT;
                //gameFSM = GameState.PLAYING;
            }
        }
        #endregion MoveShip

        #region Victory&LosePanel
        // hacer un estado finito que me dga si estoy en el estado finito de game, vic,loose y se lo ponga en estos metodos y luyegp brincan al fsm de stopit
        public void VictoryPanel() {
            if (gameFSM != GameState.PLAYING) {
                return;
            }
            gameFSM = GameState.WIN;
            panelWin.SetActive(true);
            playerFSM = MoveShip.STOP_IT;
            panelLose.SetActive(false);
        }
        public void LosePanel() {
            if (gameFSM != GameState.PLAYING) {
                return;
            }
            gameFSM = GameState.LOSE;
            panelLose.SetActive(true);
            playerFSM = MoveShip.STOP_IT;
            panelWin.SetActive(false);
        }

        #endregion Victory&LosePanel

        #region Collisions
        public void OnTriggerStay(Collider other) {
            if (other.gameObject.CompareTag("Wall")) {
                //el vector youShallNotPass podria solo existir aqui solamente este este trigger
                playerFSM = MoveShip.STOP_IT;
                youShallNotPass = Vector3.zero;
                direction = Vector3.zero;
                youShallNotPass = (transform.position - direction).normalized;
                LosePanel();
            }
            if (other.gameObject.CompareTag("BlackHole")) {
                LosePanel();
                if (panelWin == null) {
                    panelLose.SetActive(false);
                }
            }
        }
        #endregion Collisions
    }
}