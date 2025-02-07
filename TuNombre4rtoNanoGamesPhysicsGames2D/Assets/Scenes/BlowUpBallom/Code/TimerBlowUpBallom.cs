using TMPro;
using UnityEngine;

public class TimerBlowUpBallom : MonoBehaviour
{
    [SerializeField, HideInInspector] bool timerActiveBlowUpBallom;
    [SerializeField] float timerBlowUpBallom;
    [SerializeField] TMP_Text timeTxtBlowUpBallom;

    void Start()
    {
        timerActiveBlowUpBallom = true;
        timerBlowUpBallom = 0f;
    }
    void Update()
    {
        IncrementCronometerBlowUpBallom();
    }
    public void IncrementCronometerBlowUpBallom()
    {
        if (timerActiveBlowUpBallom == true)
        {
            timerBlowUpBallom += Time.deltaTime;
            timeTxtBlowUpBallom.text = timerBlowUpBallom.ToString("F2");
        }
        else if (timerActiveBlowUpBallom == false)
        {
            timerBlowUpBallom = 6.66f;
        }
    }
}

