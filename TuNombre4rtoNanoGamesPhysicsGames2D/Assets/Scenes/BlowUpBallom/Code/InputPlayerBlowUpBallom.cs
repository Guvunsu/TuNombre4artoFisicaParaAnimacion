using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputPlayerBlowUpBallom : MonoBehaviour {
    #region variables
    public TimerBlowUpBallom timerBlowUpBallom;
    public StatesBallom statesBallom;

    [SerializeField] protected StatesBallom statesBallomPlayer;
    [SerializeField] PlayerInput playerInput;

    [SerializeField] GameObject ballom;
    [SerializeField] GameObject panelWin;
    [SerializeField] GameObject panelLose;

    [SerializeField] Transform pointA;
    [SerializeField] Transform pointB;
    [SerializeField] Transform PointC;
    [SerializeField] Transform PointD;

    [SerializeField] int preshButton;

    #endregion variables

    #region UnityMethods
    void Start() {

    }
    void FixedUpdate() {

    }
    void Update() {

    }
    #endregion UnityMethods

    #region PlayerInput

    public void OnClickMouse(InputAction.CallbackContext value) {
        IncrementSizeBallom();
        BlowUpBallom();
    }
    public void IncrementSizeBallom() {

    }
    public void BlowUpBallom() {

    }
    #endregion PlayerInput
}
//using UnityEngine;
//using UnityEngine.InputSystem;
//using TMPro;

//public class InputPlayerBlowUpBalloon : MonoBehaviour {
//    #region Variables
//    public TimerBlowUpBalloon timer;
//    public StatesBalloon stateMachine;

//    [SerializeField] private PlayerInput playerInput;
//    [SerializeField] private GameObject balloon;
//    [SerializeField] private Transform maxSizePoint;
//    [SerializeField] private TMP_Text timerText;

//    private Vector3 initialScale;
//    private Vector3 maxScale;
//    private int pressCount;
//    private const int maxPresses = 3;
//    private bool gameActive;
//    #endregion

//    #region UnityMethods
//    void Start() {
//        initialScale = balloon.transform.localScale;
//        maxScale = maxSizePoint.localScale;
//        gameActive = true;
//        timer.StartTimer();
//        stateMachine.SetState(StatesBalloon.States.NORMAL_STATE);
//    }

//    void Update() {
//        if (timer.IsTimeUp() && gameActive) {
//            gameActive = false;
//            stateMachine.SetState(StatesBalloon.States.KABOOM);
//        }
//    }
//    #endregion

//    #region PlayerInput
//    public void OnClickMouse(InputAction.CallbackContext context) {
//        if (context.performed && gameActive) {
//            IncrementSizeBalloon();
//        }
//    }

//    private void IncrementSizeBalloon() {
//        pressCount++;
//        balloon.transform.localScale = Vector3.Lerp(initialScale, maxScale, (float)pressCount / maxPresses);

//        if (pressCount >= maxPresses) {
//            gameActive = false;
//            stateMachine.SetState(StatesBalloon.States.INFLATING_BALLOON);
//        }
//    }
//    #endregion
//}

//public class StatesBalloon : MonoBehaviour {
//    public enum States { NORMAL_STATE, INFLATING_BALLOON, KABOOM }
//    private States currentState;

//    public void SetState(States newState) {
//        currentState = newState;
//        HandleStateChange();
//    }

//    private void HandleStateChange() {
//        switch (currentState) {
//            case States.NORMAL_STATE:
//                Debug.Log("Balloon is normal");
//                break;
//            case States.INFLATING_BALLOON:
//                Debug.Log("Balloon fully inflated");
//                break;
//            case States.KABOOM:
//                Debug.Log("Balloon exploded!");
//                gameObject.SetActive(false);
//                break;
//        }
//    }
//}

//public class TimerBlowUpBalloon : MonoBehaviour {
//    [SerializeField] private float timeLimit = 6f;
//    private float currentTime;
//    private bool isRunning;

//    public void StartTimer() {
//        currentTime = 0f;
//        isRunning = true;
//    }

//    void Update() {
//        if (isRunning) {
//            currentTime += Time.deltaTime;
//            if (currentTime >= timeLimit) {
//                isRunning = false;
//            }
//        }
//    }

//    public bool IsTimeUp() {
//        return !isRunning;
//    }
//}
