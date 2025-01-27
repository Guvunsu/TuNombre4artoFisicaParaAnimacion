namespace Gavryk.Physics.Billiard {
    using System.Drawing;
    using Unity.VisualScripting;
    using UnityEngine;

    public class BallManager : MonoBehaviour {

        #region Enum & Variables
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

        #endregion Enum & Variables

        #region UnityMethods
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

        #endregion UnityMethods

        #region BallInteraction
        void WaitingForHit() {
            //spawnPos = Random.Range(1, 1 * spawnPoints.Length);
            //transform.position = spawnPoints[spawnPos].position;
            ballState = BallFSM.WAITING_FOR_HIT;
        }
        public void MoveBallBillardForward() {

            if (ballState == BallFSM.HIT_AND_MOVING) {
                transform.position = Vector3.Lerp(transform.position, goToX.position, speedPercentage).normalized;
                GetComponent<Rigidbody>().linearVelocity = Vector3.right * speedBall;
                speedBall--;
                speedBall = 0f;
                ballState = BallFSM.FINISHED;
            }
          /*  //cronometerTotalTime += Time.deltaTime;
            //if (cronometerPercentage % (5f * 2f) < (5f)) {
            //    dir = 1f;
            //} else //if (cronometerPercentage % (5f * 2f) >= (5f))
            //  {
            //    dir = -1f;
            //}
            //cronometerPercentage += Time.deltaTime * dir;
            //speedPercentage += (cronometerPercentage / 5f);*/
        }

        #endregion BallInteraction

        private void OnCollisionEnter(Collision collision) {
            Debug.Log("me toco el taco");
            if (collision.gameObject.CompareTag("Player") /*&& collision.gameObject.CompareTag("Ball") /*&& tacoIsTouchedMe==true*/) {
                MoveBallBillardForward();
            }
        }
    }
}