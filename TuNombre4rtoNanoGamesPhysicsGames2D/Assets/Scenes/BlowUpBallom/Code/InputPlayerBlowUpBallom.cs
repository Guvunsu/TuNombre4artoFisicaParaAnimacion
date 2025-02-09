using Autodesk.Fbx;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
namespace Gavryk.Physics.Ballom {

    public class InputPlayerBlowUpBallom : MonoBehaviour {
        #region variables
        [SerializeField] protected TimerBlowUpBallom timerBlowUpBallom;
        [SerializeField] protected StatesBallom statesBallomPlayer;

        [SerializeField] PlayerInput playerInput;
        [SerializeField] States _currentBallomState;

        [SerializeField] GameObject ballom;
        [SerializeField] Transform maxVolume;
        [SerializeField] GameObject panelWin;
        [SerializeField] GameObject panelLose;

        [SerializeField] float speed;
        [SerializeField] float scaleIncrement = 1f;
        [SerializeField] int pressCount;
        [SerializeField] int maxPress = 12;

        Vector3 initialScale;
        Vector3 maxScale;

        bool isGameActive;

        #endregion variables

        #region UnityMethods
        void Awake() {
            timerBlowUpBallom = FindAnyObjectByType<TimerBlowUpBallom>();
        }
        void Start() {
            _currentBallomState = States.NORMAL_STATE;
            isGameActive = true;
            initialScale = ballom.transform.localScale;
            maxScale = maxVolume.localScale;
        }
        void FixedUpdate() {
            switch (_currentBallomState) {
                case States.NORMAL_STATE:
                    // rigidbody but i cant do it in this class
                    break;
                case States.INFLATING_BALLOM:
                    IncrementSizeBallom();
                    break;
                case States.KABOOM:
                    VictoryPanel();
                    break;
            }
        }
        void Update() {
            if (timerBlowUpBallom.TimerExpired() && pressCount < maxPress) {
                LosePanel();
            }
        }
        #endregion UnityMethods

        #region Strucs
        public struct ScaleParameters {

        }
        #endregion Strucs

        #region Victory&Lose
        public void VictoryPanel() {
            panelWin.SetActive(true);
            isGameActive = false;
        }
        public void LosePanel() {
            panelLose.SetActive(true);
            isGameActive = false;
        }
        #endregion Victory&Lose

        #region PlayerInput
        public void OnClickMouseLeft(InputAction.CallbackContext value) {
            if (value.performed && isGameActive) {
                IncrementSizeBallom();
            }
        }
        public void IncrementSizeBallom() {
            pressCount++;
            ballom.transform.localScale += Vector3.one * scaleIncrement;

            if (pressCount == maxPress) {
                Destroy(ballom);
                VictoryPanel();
            } else if (pressCount <= maxPress - 1) {
                if (timerBlowUpBallom.TimerExpired()) {
                    isGameActive = false;
                    LosePanel();
                }
            }
        }

        //{
        //    Debug.Log("Voy apretar el boton");
        //    if (pressCount % 3 == 0) // quise probar el porcentaje pero no estoy seguro que funcione pero antes era sin el y igualado a 3
        //    {
        //        Debug.Log("aprete el boton");
        //        pressCount++;
        //        ballom.transform.localScale += (speed * direction * Time.fixedDeltaTime);
        //    }
        //    if (pressCount == 6)
        //    {
        //        pressCount++;
        //        ballom.transform.localScale += (speed * direction * Time.fixedDeltaTime);
        //    }
        //    if (pressCount == 9)
        //    {
        //        pressCount++;
        //        ballom.transform.localScale += (speed * direction * Time.fixedDeltaTime);
        //    }
        //    if (pressCount >= maxPress - 1)
        //    {
        //        pressCount++;
        //        ballom.transform.localScale += (speed * direction * Time.fixedDeltaTime);
        //        maxVolume.position = Vector3.zero;
        //        Destroy(ballom);
        //        VictoryPanel();
        //    }
        //    else if (maxPress >= 11)
        //    {
        //        timerBlowUpBallom = FindAnyObjectByType<TimerBlowUpBallom>();
        //        isGameActive = false;
        //        LosePanel();
        //    }
        //    else
        //    {
        //        timerBlowUpBallom = FindAnyObjectByType<TimerBlowUpBallom>();
        //        isGameActive = false;
        //        LosePanel();
        //    }
        //}
        #endregion PlayerInput
    }
}