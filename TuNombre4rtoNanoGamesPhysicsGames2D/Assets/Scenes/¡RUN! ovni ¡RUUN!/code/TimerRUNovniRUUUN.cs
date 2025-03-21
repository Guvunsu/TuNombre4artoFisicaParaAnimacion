using TMPro;
using UnityEngine;
namespace Gavryk.Physics.BlackHole {
    public class TimerRUNovniRUUUN : MonoBehaviour {
        [SerializeField] bool timerActive;
        [SerializeField] float timer;
        [SerializeField] TMP_Text timeTxt;

        [SerializeField] public MovementOVNI iNeedThisPanel;
     //   [SerializeField] GameObject panelLose;

        void Start() {
            //iNeedThisPanel = GetComponent<MovementOVNI>();
            timerActive = true;
            timer = 0f;
        }
        void Update() {
            IncrementCronometerRUNovniRUUUN();
        }
        void IncrementCronometerRUNovniRUUUN() {
            if (timerActive) {
                timer += Time.deltaTime;
                timeTxt.text = timer.ToString("F2");
                if (timer >= 6.66f) {
                    timerActive = false;
                    ActivateVictory();
                }
            }
        }
        void ActivateVictory() {
            if (iNeedThisPanel != null) {
                iNeedThisPanel.VictoryPanel();
                //panelLose.SetActive(false);
            }
        }
    }
}

