using UnityEngine;
namespace Gravity {
    public class GravityBall : MonoBehaviour {
        [Header("Ball")]
        [SerializeField] GameObject ball_Pinball;
        bool areTouched;

        PanelManager script_PanelManager;

        public void OnTriggerEnter(Collider other) {
            if (other.gameObject.CompareTag("PinballBall") && other.gameObject.CompareTag("BlackHole") && areTouched == true) {
                script_PanelManager.VictoryPanel();
            }

        }

    }

}