using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.ProBuilder.MeshOperations;


public class InputPlayer : MonoBehaviour
{
    #region ENUM
    public enum GameState
    {
        LIFE,
        DIYING
    }
    public enum playerFSM
    {
        SHOOT_THE_BALL,
        PLAYING,
    }

    #endregion ENUM

    #region Variables
    PanelManager script_panelManager;
    [SerializeField] GameState gameState_FSM;
    [SerializeField] playerFSM player_FSM;

    [SerializeField] float springForce;
    [SerializeField] float leverPinball;

    [SerializeField] Transform leverLeft;
    [SerializeField] Transform leverRight;
    [SerializeField] Transform SpringPinball;
    [SerializeField] GameObject pinball_Ball;

    [SerializeField] PlayerInput controller;

    #endregion Variables

    #region UnityMethods
    void Start()
    {
        player_FSM = playerFSM.SHOOT_THE_BALL;
    }

    void Update()
    {
        switch (gameState_FSM)
        {
            case GameState.LIFE:
                player_FSM = playerFSM.PLAYING;
                break;
            case GameState.DIYING:

                break;
        }
    }
    #endregion UnityMethods

    #region PublicMethods
    public void MoveLeverPinballPlayer(InputAction.CallbackContext value)
    {
        if (value.performed && player_FSM == playerFSM.PLAYING)
        {
            if (leverLeft.rotation.z > -42 * transform.rotation.z)
            {
                leverLeft.Rotate(UnityEngine.Vector3.down * springForce * Time.fixedDeltaTime);
            }
            else if (leverLeft.rotation.z < 0 * transform.rotation.z)
            {
                leverLeft.Rotate(UnityEngine.Vector3.up * springForce * Time.fixedDeltaTime);
            }
        }
        if (value.performed)
        {
            if (leverRight.rotation.z > -42 * transform.rotation.z)
            {
                leverRight.Rotate(UnityEngine.Vector3.down * springForce * Time.fixedDeltaTime);
            }
            else if (leverRight.rotation.z < 0 * transform.rotation.z)
            {
                leverRight.Rotate(UnityEngine.Vector3.up * springForce * Time.fixedDeltaTime);
            }
        }
        //else if (value.canceled)
        //{

        //}
    }

    #endregion PublicMethods

    #region Triggers

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

        }
        if (other.gameObject.CompareTag("Fondo"))
        {

        }
    }

    #endregion Triggers
}
