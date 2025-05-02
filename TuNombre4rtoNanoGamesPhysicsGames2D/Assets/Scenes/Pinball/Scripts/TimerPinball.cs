using TMPro;
using UnityEngine;

public class TimerPinball : MonoBehaviour {

    [SerializeField, HideInInspector] bool timerActive;
    [SerializeField] float timer;
    [SerializeField] TMP_Text timeTxt;
    PanelManager script_PanelManager;
    AgujeroNegro script_AgujeroNegro;
    bool areTouched;

    void Start() {
        timerActive = true;
        timer = 0f;
        script_PanelManager = Object.FindFirstObjectByType<PanelManager>();
    }
    void Update() {
        IncrementCronometer();
    }
    public void IncrementCronometer() {
        if (timerActive) {
            timer += Time.deltaTime;
            timeTxt.text = timer.ToString("F2");
            //if (script_AgujeroNegro == null) {
            //    script_PanelManager.VictoryPanel();
            //}
            // o mejor hacer una funcion en AgujeroNegro que active el panel de vicotria si trigger es correcto con el tag de la bola y aqui lo menciono
        }
        if (timer >= 59.999f) {
            timerActive = false;
            if (script_PanelManager != null) {
                script_PanelManager.LosePanel();
            }
            Time.timeScale = 0f;
        }
    }
}
