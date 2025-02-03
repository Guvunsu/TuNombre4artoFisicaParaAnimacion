using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour {

    [SerializeField, HideInInspector] bool timerActive;
    [SerializeField] float timer;
    [SerializeField] TMP_Text timeTxt;

    void Start() {
        timerActive = true;
        timer = 0f;
    }
    void Update() {
        IncrementCronometer();
    }
    void IncrementCronometer() {
        if (timerActive == true) {
            timer += Time.deltaTime;
            timeTxt.text = timer.ToString("F2");
        } else if (timerActive == false) {
            timer = 6.66f;
        }
    }
}
