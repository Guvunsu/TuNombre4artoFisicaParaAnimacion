using Gavryk.Physics.Ballom;
using UnityEngine;

#region ENUM
public enum States {
    NORMAL_STATE,
    INFLATING_BALLOM,
    KABOOM
}
#endregion ENUM
public class StatesBallom : MonoBehaviour {
    #region variables
    [SerializeField] States _currentBallomState;
    [SerializeField] InputPlayerBlowUpBallom player;
    [SerializeField] TimerBlowUpBallom timer;
    #endregion variables
    void Awake() {
        timer = FindObjectOfType<TimerBlowUpBallom>();
    }
    void Start() {
        _currentBallomState = States.NORMAL_STATE;
    }
    private void FixedUpdate() {
        switch (_currentBallomState) {
            case States.NORMAL_STATE:
                break;
            case States.INFLATING_BALLOM:
                if (timer.TimerExpired()) {
                    player.LosePanel();
                }
                break;
            case States.KABOOM:
                player.VictoryPanel();
                break;
        }
    }
}
