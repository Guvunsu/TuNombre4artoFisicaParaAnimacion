using UnityEngine;
namespace Gravity {
    public class GravityBall : MonoBehaviour {
        [Header("Ball")]
        [SerializeField] Rigidbody ball_PinballRB;

        PanelManager script_PanelManager;

        public void OnTriggerEnter(Collider other) {
            if (other.gameObject.CompareTag("BlackHole")) {
                script_PanelManager.VictoryPanel();
            }
            if (other.gameObject.CompareTag("Player")) {
                ball_PinballRB = GetComponent<Rigidbody>();
                if (ball_PinballRB != null) {
                    Vector3 directionHit = transform.position - other.transform.position;
                    directionHit = directionHit.normalized;

                    float strongHit = 10f;
                    ball_PinballRB.AddForce(directionHit * strongHit, ForceMode.Impulse);
                }
            }

        }

    }

}