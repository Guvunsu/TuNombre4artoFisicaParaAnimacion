using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
namespace Gavryk.Physics.Ballom
{

    public class InputPlayerBlowUpBallom : MonoBehaviour
    {
        #region variables
        [SerializeField] protected TimerBlowUpBallom timerBlowUpBallom;
        [SerializeField] protected StatesBallom statesBallomPlayer;
        [SerializeField] States _currentBallomState;
        [SerializeField] PlayerInput playerInput;

        [SerializeField] GameObject ballom;
        [SerializeField] GameObject panelWin;
        [SerializeField] GameObject panelLose;

        [SerializeField] Transform maxVolume;
        Vector3 initializeScale;
        Vector3 maxScale;
        Vector3 direction;

        [SerializeField] float speed;
        [SerializeField] int pressCount;
        [SerializeField] int maxPress = 12;
        bool isGameActive;

        #endregion variables

        #region UnityMethods
        void Awake()
        {
            timerBlowUpBallom = FindAnyObjectByType<TimerBlowUpBallom>();
        }
        void Start()
        {
            _currentBallomState = States.NORMAL_STATE;
            isGameActive = true;
        }
        void FixedUpdate()
        {
            switch (_currentBallomState)
            {
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
        void Update()
        {

        }
        #endregion UnityMethods

        #region Strucs
        public struct ScaleParameters
        {

        }
        #endregion Strucs

        #region Victory&Lose
        public void VictoryPanel()
        {
            panelWin.SetActive(true);
            isGameActive = false;
        }

        public void LosePanel()
        {
            panelLose.SetActive(true);
            isGameActive = false;
        }
        #endregion Victory&Lose

        #region PlayerInput
        public void OnClickMouseLeft(InputAction.CallbackContext value)
        {
            IncrementSizeBallom();
        }
        public void IncrementSizeBallom()
        {
            Debug.Log("Voy apretar el boton");
            if (pressCount == 3)
            {
                Debug.Log("aprete el boton");
                pressCount++;
                ballom.transform.localScale += (speed * direction * Time.fixedDeltaTime);
            }
            if (pressCount == 6)
            {
                pressCount++;
                ballom.transform.localScale += (speed * direction * Time.fixedDeltaTime);
            }
            if (pressCount == 9)
            {
                pressCount++;
                ballom.transform.localScale += (speed * direction * Time.fixedDeltaTime);
            }
            if (maxPress == 12)
            {
                pressCount++;
                ballom.transform.localScale += (speed * direction * Time.fixedDeltaTime);
                maxVolume.position = Vector3.zero;
                Destroy(ballom);
                VictoryPanel();
            }
            else if (maxPress >= 11)
            {
                timerBlowUpBallom = FindAnyObjectByType<TimerBlowUpBallom>();
                isGameActive = false;
                LosePanel();
            }
            else
            {
                timerBlowUpBallom = FindAnyObjectByType<TimerBlowUpBallom>();
                isGameActive = false;
                LosePanel();
            }
        }
        #endregion PlayerInput
    }
}