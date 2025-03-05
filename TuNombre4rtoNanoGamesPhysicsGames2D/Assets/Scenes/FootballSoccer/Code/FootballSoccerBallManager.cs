using UnityEngine;

namespace Gavryk.Physics.Football
{
    public class FootballSoccerBallManager : MonoBehaviour
    {

        #region ENUM
        //public enum StateMechanics
        //{
        //    DEFAULT,
        //    MOVE,
        //    STOP
        //}
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
            Invoke("ShootTheBall", 3f);
        }
        void Update()
        {
            switch (fsmBall)
            {
                case FootBall_FSM.START_POSE:
                    //speedBall = 0;
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
            isGameActive = false;
        }
        public void LosePanel()
        {
            panelLose.SetActive(true);
            isGameActive = false;
        }

        #endregion Victory&LosePanel

        #endregion PublicMethods

        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Humans") || other.CompareTag("PostePorteria"))
            {
                VictoryPanel();

            }
            else if (other.gameObject.CompareTag("RedPorteria"))
            {
                LosePanel();
            }
        }
    }
}
