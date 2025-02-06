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
