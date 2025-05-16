using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveGotchaCatchItPinball : MonoBehaviour {
    #region ENUM
    public enum PlayerState {
        PLAYING,
        STOP_IT
    }
    #endregion ENUM

    #region Variables
    [Header("PlayerState")]
    public PlayerState playerState_FSM;

    [SerializeField] protected GameManagerGotchaCatchItPinball script_GameManagerGotchaCatchItPinball;

    [SerializeField] InputAction spawnAction;
    [SerializeField] GameObject ballPrefab;
    [SerializeField] Transform pointEjectBall;
    private Vector3 directionBall;
    bool canIStartTheShoot = true;
    int ballsGame = 9;
    int activeBalls = 0;

    [SerializeField] float speed = 5f;
    Vector3 direction;
 


    #endregion Variables
    void Start() {
        script_GameManagerGotchaCatchItPinball.UpdateBallCount(ballsGame);
        spawnAction.performed += SpawnObject;
    }
    void Update() {
        if (ballPrefab != null) {
            ballPrefab.transform.position += directionBall * speed * Time.deltaTime;
        }
        switch (playerState_FSM) {
            case PlayerState.PLAYING:
                break;
            case PlayerState.STOP_IT:
                break;
        }
    }
    #region ShootTheBall

    public void ShootTheBallGame(InputAction.CallbackContext value) {
        if (value.performed && canIStartTheShoot && ballsGame > 0) //hacer que esto sea is pressed 
        {
            playerState_FSM = PlayerState.PLAYING;
            Debug.Log("instanceo la pelota a la posicion selecionada ");
            ballsGame--;
            Debug.Log("disminuye mi contador");
            canIStartTheShoot = true;
            if (!value.performed && !canIStartTheShoot && ballsGame == 0) {
                script_GameManagerGotchaCatchItPinball.FinishBallCycle();
            }
            //script_GameManagerGotchaCatchItPinball.UpdateBallCount(ballsGame);
            //Debug.Log("Se actualiza los puntos de mi texto ");

        }
    }
    public void SpawnObject(InputAction.CallbackContext context) {
        if (context.performed && ballsGame > 0 && activeBalls == 0) {
            ballPrefab = Instantiate(ballPrefab, pointEjectBall.position, Quaternion.identity);

            // Dirección aleatoria normalizada (solo en X e Y, 2D)
            directionBall = new Vector3(Random.Range(-1f, 1f), 1f, 0f).normalized;

            activeBalls = 1;
            ballsGame--;
            script_GameManagerGotchaCatchItPinball.UpdateBallCount(ballsGame);

            if (ballsGame == 0) {
                canIStartTheShoot = false;
            }
        }
    }
    void OnEnable() => spawnAction.Enable();
    void OnDisable() => spawnAction.Disable();

    public void StopTheGame() {
        if (ballsGame == 0) {
            playerState_FSM = PlayerState.STOP_IT;
            canIStartTheShoot = false;
            Time.timeScale = 0;
        } else {
            playerState_FSM = PlayerState.PLAYING;
        }
    }
    public int GetRemainingBallsGame() {
        return ballsGame;
    }

    public void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Canasta")) {
            playerState_FSM = PlayerState.STOP_IT;
            if (other.name.Contains("2X")) script_GameManagerGotchaCatchItPinball.MultipliquerBasket(2);
            else if (other.name.Contains("4X")) script_GameManagerGotchaCatchItPinball.MultipliquerBasket(4);
            else if (other.name.Contains("8X")) script_GameManagerGotchaCatchItPinball.MultipliquerBasket(8);
            //invoke end game mechanic
            script_GameManagerGotchaCatchItPinball.FinishBallCycle();
            script_GameManagerGotchaCatchItPinball.UpdateScore();
            Destroy(gameObject);
            GetRemainingBallsGame();
            script_GameManagerGotchaCatchItPinball.FinishBallCycle();
        }
        if (other.gameObject.CompareTag("Obstacules")) {
            //playerState_FSM = PlayerState.PLAYING;
            if (other.name.Contains("ObstaculosRectangulo")) script_GameManagerGotchaCatchItPinball.MultipliquerObstacules(2);
            else if (other.name.Contains("ObstaculosCubo")) script_GameManagerGotchaCatchItPinball.MultipliquerObstacules(4);
            else if (other.name.Contains("ObstaculosPiramide")) script_GameManagerGotchaCatchItPinball.MultipliquerObstacules(8);
        }
    }

    #endregion ShootTheBall
}
