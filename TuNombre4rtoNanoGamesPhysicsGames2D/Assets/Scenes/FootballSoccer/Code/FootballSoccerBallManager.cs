using UnityEngine;

namespace Gavryk.Physics.Football
{
    public class FootballSoccerBallManager : MonoBehaviour
    {

        #region ENUM
        public enum StateMechanics
        {
            DEFAULT,
            MOVE,
            STOP
        }
        public enum FootBall_FSM
        {
            START_POSE,
            TRANSITION_BALL,
            STOP_IT
        }
        #endregion ENUM

        #region Variables
        [SerializeField] public SineParameters soSP;

        [SerializeField] GameObject footballBall;
        [SerializeField] GameObject panelWin;
        [SerializeField] GameObject panelLose;

        [SerializeField] Transform PointA;
        [SerializeField] Transform PointB;

        protected Vector3 nodePosition;
        protected Vector3 movForward;

        [Range(0f, 1f), SerializeField] protected float percentage; //Lerp()
        [SerializeField] float speedBall;
        [SerializeField] float time;
        bool isGameActive = true;

        protected FootBall_FSM fsmBall;

        #endregion Variables

        #region PublicUnityMethods
        void Start()
        {
            footballBall = GetComponent<GameObject>();
        }

        // Update is called once per frame
        void Update()
        {
            switch (fsmBall)
            {
                case FootBall_FSM.START_POSE:
                    speedBall = 0;
                    break;
                case FootBall_FSM.TRANSITION_BALL:
                    TransitionForwardBall();
                    break;
                case FootBall_FSM.STOP_IT:
                    if (footballBall == null)
                    {
                        VictoryPanel();
                    }
                    else
                    {
                        LosePanel();
                    }
                    break;
            }
        }

        #endregion PublicUnityMethods

        #region PublicMethods

        public void TransitionForwardBall()
        {
            movForward = transform.localPosition = Vector3.Lerp(transform.position, PointB.position, percentage).normalized;
            GetComponent<Rigidbody>().linearVelocity = Vector3.right * speedBall;
            SineTransitionBall();
        }

        public void SineTransitionBall()
        {
            nodePosition = footballBall.transform.localPosition;
            nodePosition.x = percentage; //nodePosition's original X coordinate
            nodePosition.y =
                soSP.sineParameters.A *
                Mathf.Sin(soSP.sineParameters.B * percentage + soSP.sineParameters.C) +
                soSP.sineParameters.D
                ; //A * sen(B * percentage + C) + D 
            nodePosition.x *= soSP.sineParameters.horizontalScale;
            footballBall.transform.localPosition = nodePosition;
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
            if (other.gameObject.CompareTag("RedPorteria"))
            {
                panelWin.SetActive(true);
                VictoryPanel();
            }
            else if (other.gameObject.CompareTag("PostePorteria"))
            {
                panelLose.SetActive(true);
                LosePanel();
            }
            else if (other.gameObject.CompareTag("Humans"))
            {
                panelLose.SetActive(true);
                LosePanel();
            }
            else
            {
                panelLose.SetActive(true);
            }
        }
    }
}
