using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputPlayer : MonoBehaviour {
    #region ENUM
    public enum GameState {
        LIFE,
        DIYING
    }
    public enum PlayerFSM {
        IDLE,
        SHOOT_THE_BALL,
        PLAYING,
    }
    #endregion ENUM

    #region Variables
    [SerializeField] GameState gameState_FSM;
    [SerializeField] PlayerFSM player_FSM;

    [Header("Flipper Settings")]
    [SerializeField] Transform leverLeft;
    [SerializeField] Transform leverRight;
    [SerializeField] float flipperForce = 500f;
    [SerializeField] float flipperRestPosition = 0f;
    [SerializeField] float flipperPressedPosition = 45f;

    [Header("Spring Settings")]
    [SerializeField] Transform springPinball;
    [SerializeField] GameObject pinball_Ball;
    [SerializeField] float maxSpringForce = 20f;
    [SerializeField] float chargeSpeed = 10f;
    private float currentSpringForce = 0f;
    private bool isCharging = false;

    [Header("Inputs")]
    [SerializeField] PlayerInput controller;

    private Rigidbody ballRb;

    private bool isLeftFlipperPressed = false;
    private bool isRightFlipperPressed = false;
    #endregion Variables

    #region UnityMethods
    void Start() {
        player_FSM = PlayerFSM.SHOOT_THE_BALL;
        ballRb = pinball_Ball.GetComponent<Rigidbody>();
    }

    void Update() {
        switch (gameState_FSM) {
            case GameState.LIFE:
                if (!isCharging) {
                    if (isLeftFlipperPressed || isRightFlipperPressed)
                        player_FSM = PlayerFSM.PLAYING;
                    else
                        player_FSM = PlayerFSM.IDLE;
                }
                break;
            case GameState.DIYING:
                // prototype for now :3
                break;
        }

        if (player_FSM == PlayerFSM.SHOOT_THE_BALL && isCharging) {
            currentSpringForce += chargeSpeed * Time.deltaTime;
            currentSpringForce = Mathf.Clamp(currentSpringForce, 0f, maxSpringForce);

            if (springPinball != null) {
                springPinball.localPosition = new Vector3(0, -currentSpringForce * 0.05f, 0);
            }
        }
    }
    #endregion UnityMethods

    #region PublicMethods
    public void MoveLeftFlipper(InputAction.CallbackContext context) {
        if (player_FSM == PlayerFSM.SHOOT_THE_BALL) return;

        if (context.performed) {
            RotateFlipper(leverLeft, flipperPressedPosition);
            isLeftFlipperPressed = true;
        } else if (context.canceled) {
            RotateFlipper(leverLeft, flipperRestPosition);
            isLeftFlipperPressed = false;
        }
    }

    public void MoveRightFlipper(InputAction.CallbackContext context) {
        if (player_FSM == PlayerFSM.SHOOT_THE_BALL) return;

        if (context.performed) {
            RotateFlipper(leverRight, -flipperPressedPosition);
            isRightFlipperPressed = true;
        } else if (context.canceled) {
            RotateFlipper(leverRight, flipperRestPosition);
            isRightFlipperPressed = false;
        }
    }

    public void ChargeSpring(InputAction.CallbackContext context) {
        if (player_FSM != PlayerFSM.SHOOT_THE_BALL && !isCharging) return;

        if (context.started) {
            isCharging = true;
            currentSpringForce = 0f;
        } else if (context.canceled) {
            isCharging = false;
            LaunchBall();
        }
    }

    public void LaunchBall() {
        if (ballRb != null) {
            ballRb.isKinematic = false;
            ballRb.AddForce(Vector3.forward * currentSpringForce, ForceMode.Impulse);

            player_FSM = PlayerFSM.IDLE; // Al lanzar la bola, entras en IDLE y luego Update detectará si mueves flippers
            springPinball.localPosition = Vector3.zero;
        }
    }

    public void RotateFlipper(Transform flipper, float targetAngle) {
        if (flipper == null) return;

        Vector3 rotation = flipper.localEulerAngles;
        rotation.z = targetAngle;
        flipper.localEulerAngles = rotation;
    }
    #endregion PublicMethods
}
