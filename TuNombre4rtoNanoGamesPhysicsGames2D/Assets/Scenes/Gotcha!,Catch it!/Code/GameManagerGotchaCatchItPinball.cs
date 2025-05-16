using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using static Gavryk.Physics.BlackHole.MovementOVNI;
using static InputPlayer;

public class GameManagerGotchaCatchItPinball : MonoBehaviour {

    #region Variables
    MoveGotchaCatchItPinball script_MoveGotchaCatchItPinball;

    [SerializeField] Transform basket8x;
    [SerializeField] Transform basket4x;
    [SerializeField] Transform basket2x;

    int scoreAmount = 0;
    int amount;
    [SerializeField] TextMeshProUGUI scoreUpdate;
    [SerializeField] TextMeshProUGUI ballCount;

    #endregion Variables

    void Start() {

    }

    void Update() {
        UpdateScore();
    }
    public void FinishBallCycle() {
        script_MoveGotchaCatchItPinball.StopTheGame();
    }
    #region CanastaThings

    public void MultipliquerBasket(int points) {
        scoreAmount += points;
        UpdateScore();
    }
    public void MultipliquerObstacules(int points) {
        scoreAmount += points;
        UpdateScore();
    }

    #endregion CanastaThings

    #region Score
    public void UpdateScore() {
        scoreUpdate.text = ("") + scoreAmount.ToString();
    }
    public void AddPointsGotchaGatchaCatchIt() {
        scoreAmount += amount;
        UpdateScore();
    }
    public void UpdateBallCount(int count) {
        ballCount.text = "Pelotas: " + count.ToString();
    }
    #endregion Score
}
