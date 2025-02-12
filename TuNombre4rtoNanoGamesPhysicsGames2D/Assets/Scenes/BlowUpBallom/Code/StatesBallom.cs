using Gavryk.Physics.Ballom;
using UnityEngine;

#region ENUM
public enum States {
    //NORMAL_STATE,
    INFLATING_BALLOM,
    KABOOM, //WIN
    LOSE
}
#endregion ENUM
public class StatesBallom : MonoBehaviour {
    #region variables
    [SerializeField] States _currentBaloonState;
    [SerializeField] InputPlayerBlowUpBallom player;
    [SerializeField] TimerBlowUpBallom timer;
    #endregion variables
    void Awake() {
        timer = FindFirstObjectByType<TimerBlowUpBallom>();
    }
    void Start() {
        _currentBaloonState = States.INFLATING_BALLOM;
    }
    private void FixedUpdate() {
        switch (_currentBaloonState) {
            //case States.NORMAL_STATE:
            //    break;
            case States.INFLATING_BALLOM:
                if (timer.TimerExpired()) {
                    player.LosePanel();
                    _currentBaloonState = States.LOSE;
                }
                break;
            case States.KABOOM:
                player.VictoryPanel();
                break;
        }
    }

    public void KaboomBalloon()
    {
        _currentBaloonState = States.KABOOM;
    }
}
