namespace Gavryk.Physics.Billiard {
    using NUnit.Framework.Internal.Commands;
    using UnityEngine;
    using UnityEngine.InputSystem;
    using UnityEngine.InputSystem.LowLevel;

    public enum TacoFSM {
        WAITING_FOR_HIT,
        GOING_TO_HIT,
        FINISHED
    }

    public class BilliardController : MonoBehaviour {

        #region LocalVariables
        BallManager ballManager;
        [SerializeField] GameObject player;
        [SerializeField] Transform pointA;
        [SerializeField] Transform pointB;
        PlayerInput button;

        [SerializeField] float time = 5f;
        [SerializeField] float tiempoTranscurrido = 0f;
        [SerializeField] float hitForce;
        [SerializeField] float speedPercentage;
        [SerializeField] float cronometerTotalTime;
        [SerializeField] float cronometerPercentage;
        float dir;
        float porcentaje;
        //bool isTouch = true;

        protected TacoFSM tacoState;

        #endregion

        public void OnClickMouse(InputAction.CallbackContext value) {
            if (value.performed && tacoState == TacoFSM.WAITING_FOR_HIT) {
                ConfirmHit();
                Debug.Log("Click" + value.ReadValue<Vector2>().ToString());
            }
        }
        void Start() {
            tacoState = TacoFSM.WAITING_FOR_HIT;
            player = GetComponent<GameObject>();
            button = GetComponent<PlayerInput>();
        }
        void Update() {
            switch (tacoState) {
                case TacoFSM.WAITING_FOR_HIT:
                    WaitingForHit();
                    break;
                case TacoFSM.GOING_TO_HIT:
                    GoingToHit();
                    break;
                case TacoFSM.FINISHED:
                    //None
                    break;
            }
        }
        protected void WaitingForHit() {
            tacoState = TacoFSM.WAITING_FOR_HIT | TacoFSM.FINISHED;
            tiempoTranscurrido += Time.deltaTime;
            porcentaje = Mathf.PingPong(tiempoTranscurrido, time) / time;
            transform.position = Vector3.Lerp(pointA.position, pointB.position, porcentaje);

            //TODO: implementar eso que tengo en la foto de x (0,5) se regresa x (5,10) se iba 
            if (tiempoTranscurrido >= time) {
                time -= Time.deltaTime;
                cronometerTotalTime += Time.deltaTime;
                if (cronometerPercentage % (5f * 2f) < (5f)) {
                    dir = 1f;
                } else //if (cronometerPercentage % (5f * 2f) >= (5f))
                  {
                    dir = -1f;
                }
                cronometerPercentage += Time.deltaTime * dir;
                speedPercentage += (cronometerPercentage / 5f);
                tacoState = TacoFSM.FINISHED;
            }
        }
        protected void GoingToHit() {
            tiempoTranscurrido += Time.deltaTime;
            porcentaje = tiempoTranscurrido / time;
            transform.position = Vector3.Lerp(pointA.position, pointB.position, tiempoTranscurrido);
            if (tiempoTranscurrido >= time) {
                tacoState = TacoFSM.FINISHED;
            }
        }
        protected void ConfirmHit() {
            tacoState = TacoFSM.GOING_TO_HIT;
            tiempoTranscurrido = 0f;
            //pointA.position = transform.position;
            //pointB.position = pointA.transform.position + (Vector3.right * direction); // ponerle un 3.0f
        }
        protected void MovingTacoTowardBall() {
            transform.position = Vector3.forward * hitForce * Time.deltaTime;
            if (transform.position.z > 3.0f) {
                tacoState= TacoFSM.FINISHED;
            }
            
        }
        void OnCollisionEnter(Collision collision) {
            if (/*collision.gameObject.CompareTag("Player") && */collision.gameObject.CompareTag("Ball") /*&& isTouch*/) {
                ballManager.MoveBallBillardForward();
                print("Haz ganado !");
            }
        }
    }
}
