using UnityEngine;
using UnityEngine.InputSystem;

public class MoveGotchaCatchItPinball : MonoBehaviour
{
    #region ENUM
    public enum PlayerState
    {
        PLAYING,
        STOP_IT
    }
    #endregion ENUM

    #region Variables
    [Header("PlayerState")]
    public PlayerState playerState_FSM;

    [SerializeField] protected GameManagerGotchaCatchItPinball script_GameManagerGotchaCatchItPinball;
    [SerializeField] GameObject ballPrefab;
    [SerializeField] Transform pointEjectBall;
    bool canIStartTheShoot = true;
    int ballsGame = 9;

    #endregion Variables
    void Start()
    {
        script_GameManagerGotchaCatchItPinball.UpdateBallCount(ballsGame);
    }
    void Update()
    {
        switch (playerState_FSM)
        {
            case PlayerState.PLAYING:
                break;
            case PlayerState.STOP_IT:
                break;
        }
    }
    #region ShootTheBall

    public void ShootTheBallGame(InputAction.CallbackContext value)
    {
        if (value.performed && canIStartTheShoot && ballsGame > 0) //hacer que esto sea is pressed 
        {
            playerState_FSM = PlayerState.PLAYING;
            Debug.Log("Dispara por medio del input y estoy en fsm jugando ");
            GameObject ballInstantiate = Instantiate(ballPrefab, pointEjectBall.position, Quaternion.identity);
            Debug.Log("instanceo la pelota a la posicion selecionada ");
            ballsGame--;
            Debug.Log("disminuye mi contador");
            canIStartTheShoot = true;
            if (!value.performed && !canIStartTheShoot && ballsGame == 0)
            {
                script_GameManagerGotchaCatchItPinball.FinishBallCycle();
            }
            //script_GameManagerGotchaCatchItPinball.UpdateBallCount(ballsGame);
            //Debug.Log("Se actualiza los puntos de mi texto ");

        }
    }
    public void StopTheGame()
    {
        playerState_FSM = PlayerState.STOP_IT;
        canIStartTheShoot = false;
        Time.timeScale = 0;
    }
    public int GetRemainingBallsGame()
    {
        return ballsGame;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Canasta"))
        {
            playerState_FSM = PlayerState.STOP_IT;
            if (other.name.Contains("2X")) script_GameManagerGotchaCatchItPinball.MultipliquerBasket(2);
            else if (other.name.Contains("4X")) script_GameManagerGotchaCatchItPinball.MultipliquerBasket(4);
            else if (other.name.Contains("8X")) script_GameManagerGotchaCatchItPinball.MultipliquerBasket(8);
            //invoke end game mechanic
            script_GameManagerGotchaCatchItPinball.FinishBallCycle();
            script_GameManagerGotchaCatchItPinball.UpdateScore();
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Obstacules"))
        {
            //playerState_FSM = PlayerState.PLAYING;
            if (other.name.Contains("ObstaculosRectangulo")) script_GameManagerGotchaCatchItPinball.MultipliquerObstacules(2);
            else if (other.name.Contains("ObstaculosCubo")) script_GameManagerGotchaCatchItPinball.MultipliquerObstacules(4);
            else if (other.name.Contains("ObstaculosPiramide")) script_GameManagerGotchaCatchItPinball.MultipliquerObstacules(8);
        }
    }

    #endregion ShootTheBall
}
