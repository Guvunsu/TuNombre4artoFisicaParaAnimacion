using TMPro;
using UnityEngine;

public class TimerBlowUpBallom : MonoBehaviour {
    [SerializeField] TMP_Text timeTxtBlowUpBallom;
    [SerializeField] float timerBlowUpBallom;
    float maxTime = 6f;
    bool timerActiveBlowUpBallom;
    void Start() {
        timerActiveBlowUpBallom = true;
        timerBlowUpBallom = 0f;
    }
    void Update() {
        IncrementCronometerBlowUpBallom();
    }
    public void IncrementCronometerBlowUpBallom() {
        if (timerActiveBlowUpBallom == true) {
            timerBlowUpBallom += Time.deltaTime;
            timeTxtBlowUpBallom.text = timerBlowUpBallom.ToString("F2");
        } else if (timerActiveBlowUpBallom == false) {
            timerBlowUpBallom = 6.00f;
        }
    }
    public bool TimerExpired() {
        return timerBlowUpBallom >= maxTime;
    }
}

