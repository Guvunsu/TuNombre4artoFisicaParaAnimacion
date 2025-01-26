namespace Gavryk.Physics.Billiard {
    using System.Drawing;
    using Unity.VisualScripting;
    using UnityEngine;

    public class BallManager : MonoBehaviour {
        public enum BallFSM {
            WAITING_FOR_HIT,
            HIT_AND_MOVING,
            FINISHED
        }
        protected BallFSM ballState;
        [SerializeField] GameObject ballPrefab;
        [SerializeField] Transform goToX;
        [SerializeField] float speedBall;
        [SerializeField] float speedPercentage;
        //[SerializeField] float time = 5f;
        // [SerializeField] float tiempoTranscurrido = 0f;
        int spawnPos;
        //float cronometerPercentage;
        //float cronometerTotalTime;
        //bool tacoIsTouchedMe;
        //float dir;


        void Start() {
            ballPrefab = GetComponent<GameObject>();
            WaitingForHit();
        }
        void Update() {
            switch (ballState) {
                case BallFSM.WAITING_FOR_HIT:
                    WaitingForHit();
                    break;
                case BallFSM.HIT_AND_MOVING:
                    MoveBallBillardForward();
                    break;
                case BallFSM.FINISHED:
                    break;
            }
        }
        void WaitingForHit() {
            //spawnPos = Random.Range(1, 1 * spawnPoints.Length);
            //transform.position = spawnPoints[spawnPos].position;
            ballState = BallFSM.WAITING_FOR_HIT;
        }
        public void MoveBallBillardForward() {

            if (ballState == BallFSM.HIT_AND_MOVING) {
                // implementar tal vez un vector3 lerp para que avance hacia adelante
                transform.position = Vector3.Lerp(transform.position, goToX.position, speedPercentage).normalized;
                GetComponent<Rigidbody>().linearVelocity = Vector3.right * speedBall;
                speedBall--;
                speedBall = 0f;
                ballState = BallFSM.FINISHED;
            }


            //cronometerTotalTime += Time.deltaTime;
            //if (cronometerPercentage % (5f * 2f) < (5f)) {
            //    dir = 1f;
            //} else //if (cronometerPercentage % (5f * 2f) >= (5f))
            //  {
            //    dir = -1f;
            //}
            //cronometerPercentage += Time.deltaTime * dir;
            //speedPercentage += (cronometerPercentage / 5f);

        }
        private void OnCollisionEnter(Collision collision) {
            Debug.Log("me toco el taco");
            if (collision.gameObject.CompareTag("Player") /*&& collision.gameObject.CompareTag("Ball") /*&& tacoIsTouchedMe==true*/) {
                MoveBallBillardForward();
            }
        }
    }
}