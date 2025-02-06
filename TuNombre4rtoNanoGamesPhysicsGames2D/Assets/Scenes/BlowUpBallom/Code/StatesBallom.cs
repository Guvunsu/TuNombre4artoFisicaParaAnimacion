using UnityEngine;

public class StatesBallom : MonoBehaviour {
    #region ENUM
    public enum States {
        NORMAL_STATE,
        INFLATING_BALLOM,
        KABOOM
    }
    #endregion ENUM

    #region variables

    [SerializeField] States _currentBallomState;
    public InputPlayerBlowUpBallom inputPlayer;

    #endregion variables
    void Start() {

    }

    private void FixedUpdate() {
        switch (_currentBallomState) {
            case States.NORMAL_STATE:
                //
                break;
            case States.INFLATING_BALLOM:
                //
                break;
            case States.KABOOM:
                //
                break;
        }
    }

    void Update() {

    }
}
