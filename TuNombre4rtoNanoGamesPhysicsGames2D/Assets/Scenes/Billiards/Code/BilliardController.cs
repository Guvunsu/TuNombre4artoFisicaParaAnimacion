namespace Gavryk.Physics.Billiard
{
    using NUnit.Framework.Internal.Commands;
    using UnityEngine;
    using UnityEngine.InputSystem;
    using UnityEngine.InputSystem.LowLevel;
    using UnityEngine.ProBuilder.MeshOperations;


    public class BilliardController : MonoBehaviour
    {

        #region LocalVariables & ENUM
        public enum TacoFSM
        {
            WAITING_FOR_HIT,
            GOING_TO_HIT,
            FINISHED
        }

        BallManager ballManager;
        [SerializeField] PlayerInput inputActions;
        [SerializeField] GameObject player;
        [SerializeField] Transform pointA;
        [SerializeField] Transform pointB;
        [SerializeField] Transform PointC;
        [SerializeField] Transform PointD;
        PlayerInput button;
        Vector3 moveInput;
        [SerializeField] float time = 10f;
        [SerializeField] float tiempoTranscurrido = 0f;
        [SerializeField] float hitForce;
        [SerializeField] float speedPercentage;
        [SerializeField] float cronometerTotalTime;
        [SerializeField] float cronometerPercentage;
        float dir;
        float porcentaje;
        bool isTouch;

        protected TacoFSM tacoState;

        #endregion LocalVariables & ENUM

        #region InputActions
        public void OnClickMouse(InputAction.CallbackContext value)
        {
            if (value.performed && tacoState == TacoFSM.WAITING_FOR_HIT)
            {
                Debug.Log("Barra espaciadora presionada");
                if (Keyboard.current.spaceKey.wasPressedThisFrame)
                {
                    moveInput = value.ReadValue<Vector3>();
                    Debug.Log("Click" + value.ReadValue<Vector2>().ToString());
                    ConfirmHit();
                }
                Debug.Log("Clic derecho detectado");
                if (Mouse.current.rightButton.wasPressedThisFrame)
                {
                    moveInput = value.ReadValue<Vector3>();
                    Debug.Log("Click" + value.ReadValue<Vector2>().ToString());
                    ConfirmHit();
                }
            }
        }

        #endregion InputActions

        #region UnityMethods
        void Start()
        {
            tacoState = TacoFSM.WAITING_FOR_HIT;
            player = GetComponent<GameObject>();
            button = GetComponent<PlayerInput>();
        }
        void Update()
        {
            switch (tacoState)
            {
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

        #endregion UnityMethods

        #region ActionGame
        protected void WaitingForHit()
        {
            tiempoTranscurrido += Time.deltaTime;
            porcentaje = Mathf.PingPong(tiempoTranscurrido, time) / time; // / (time / 2);
            transform.position = Vector3.Lerp(pointA.position, pointB.position, porcentaje);
            /*//TODO: implementar eso que tengo en la foto de x (0,5) se regresa x (5,10) se iba 
            //if (tiempoTranscurrido >= time)
            //{
            //    time -= Time.deltaTime;
            //    cronometerTotalTime += Time.deltaTime;
            //}
            //else if (cronometerPercentage % (5f * 2f) < (5f))
            //{
            //    dir = 1f;
            //}
            //else if (cronometerPercentage % (5f * 2f) >= (10f))
            //{
            //    dir = -1f;
            //}
            //cronometerPercentage += Time.deltaTime; // * dir;
            //speedPercentage += (cronometerPercentage / 5f);
            //tacoState = TacoFSM.FINISHED;*/
        }
        protected void GoingToHit()
        {
            tiempoTranscurrido += Time.deltaTime;
            porcentaje = tiempoTranscurrido / time;
            transform.position = Vector3.Lerp(pointA.position, pointB.position, tiempoTranscurrido);
            if (tiempoTranscurrido >= time)
            {
                tacoState = TacoFSM.FINISHED;
            }
        }
        protected void ConfirmHit()
        {
            void OnCollisionEnter(Collision collision)
            {
                if (collision.gameObject.CompareTag("Ball") && isTouch == true)
                {
                    switch (tacoState)
                    {
                        case TacoFSM.GOING_TO_HIT:
                            transform.position = Vector3.Lerp(PointC.position, PointD.position, tiempoTranscurrido);
                            ballManager.MoveBallBillardForward();
                            tiempoTranscurrido = 0f;
                            print("Haz ganado !");
                            break;
                            //case TacoFSM.GOING_TO_HIT:
                            //    print("No le haz dado");
                            //    break;
                    }
                }
            }
        }
        protected void MovingTacoTowardBall()
        {
            transform.position = Vector3.right * hitForce * Time.deltaTime;
            if (transform.position.x > 15.0f)
            {
                tacoState = TacoFSM.FINISHED;
            }
        }

        #endregion ActionGame
    }
}
