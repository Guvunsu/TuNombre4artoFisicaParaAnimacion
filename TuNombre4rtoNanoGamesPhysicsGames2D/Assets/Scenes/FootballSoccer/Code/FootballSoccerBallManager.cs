using UnityEngine;

namespace Gavryk.Physics.Football
{
    public class FootballSoccerBallManager : MonoBehaviour
    {
        #region ENUM
        public enum FootBall_FSM
        {
            START_POSE,
            TRANSITION_BALL,
            STOP_IT
        }
        #endregion ENUM

        #region Variables
        [SerializeField] public SineParameters soSP;

        [SerializeField] GameObject panelWin;
        [SerializeField] GameObject panelLose;

        [SerializeField] Transform PointA;
        [SerializeField] Transform PointB;

        protected Vector3 nodePosition;

        [SerializeField] protected float cronometer; //Lerp()
        [Range(0f, 5f), SerializeField] protected float shootTime;
        bool isGameActive = true;

        [SerializeField] protected FootBall_FSM fsmBall;

        #endregion Variables

        #region PublicUnityMethods
        void Start()
        {
            soSP.sineParameters.A = Random.Range(-6.6f, 6.6f);
            soSP.sineParameters.B = Random.Range(0.25f, 1.5f);
            soSP.sineParameters.C = Random.Range(-6.6f, 6.6f);
            soSP.sineParameters.D = Random.Range(-6.6f, 6.6f);
            soSP.sineParameters.horizontalScale = Random.Range(-6.6f, 6.6f);
            Invoke("ShootTheBall", 1.1f);
        }
        void Update()
        {
            switch (fsmBall)
            {
                case FootBall_FSM.START_POSE:
                    break;
                case FootBall_FSM.TRANSITION_BALL:
                    TransitionForwardBall();
                    break;
            }
        }

        #endregion PublicUnityMethods

        #region PublicMethods

        public void TransitionForwardBall()
        {
            SineTransitionBall();
        }
        public void ShootTheBall()
        {
            if (isGameActive)
            {
                fsmBall = FootBall_FSM.TRANSITION_BALL;
                cronometer = 0f;
            }
        }
        public void SineTransitionBall()
        {
            cronometer += Time.fixedDeltaTime;
            nodePosition.x = Mathf.Lerp(PointA.position.x, PointB.position.x, cronometer / shootTime);
            nodePosition.z = soSP.sineParameters.A * Mathf.Sin(soSP.sineParameters.B * nodePosition.x /
                soSP.sineParameters.horizontalScale + soSP.sineParameters.C) + soSP.sineParameters.D; //A * sen(B * percentage + C) + D
            transform.position = nodePosition;
            if (cronometer >= shootTime)
            {
                fsmBall = FootBall_FSM.STOP_IT;
            }
        }

        #region Victory&LosePanel
        public void VictoryPanel()
        {
            panelWin.SetActive(true);
            fsmBall = FootBall_FSM.STOP_IT;
            // isGameActive = false;
        }
        public void LosePanel()
        {
            panelLose.SetActive(true);
            fsmBall = FootBall_FSM.STOP_IT;
            // isGameActive = false;
        }

        #endregion Victory&LosePanel

        #endregion PublicMethods

        public void OnTriggerEnter(Collider collision)
        {
            Debug.LogWarning("entro?");
            if (collision.gameObject.CompareTag("Humans") || collision.gameObject.CompareTag("PostePorteria"))
            {
                Debug.LogWarning("toco y gane ?");
                VictoryPanel();
            }
            else if (collision.gameObject.CompareTag("RedPorteria"))
            {
                Debug.LogWarning("toco Red y perdi?");
                LosePanel();
            }
        }
    }
}