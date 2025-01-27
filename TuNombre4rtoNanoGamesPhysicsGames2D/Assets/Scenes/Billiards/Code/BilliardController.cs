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
        PlayerInput button;

        [SerializeField] float time = 10f;
        [SerializeField] float tiempoTranscurrido = 0f;
        [SerializeField] float hitForce;
        [SerializeField] float speedPercentage;
        [SerializeField] float cronometerTotalTime;
        [SerializeField] float cronometerPercentage;
        float dir;
        float porcentaje;
        //bool isTouch = true;

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
                    Debug.Log("Click" + value.ReadValue<Vector2>().ToString());
                    ConfirmHit();
                }
                Debug.Log("Clic derecho detectado");
                if (Mouse.current.rightButton.wasPressedThisFrame)
                {
                    Debug.Log("Click" + value.ReadValue<Vector2>().ToString());
                    ConfirmHit();
                }
            }
        }
        #region OnEnable or Disable 
        private void OnEnable()
        {
            inputActions.Player.Move.performed += WaitingForHit();
            inputActions.Player.Attack.performed += ConfirmHit();

            inputActions.Enable();
        }

        private void OnDisable()
        {
            inputActions.Disable();
        }
        #endregion OnEnable or Disable 

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
            tacoState = TacoFSM.WAITING_FOR_HIT | TacoFSM.FINISHED;
            tiempoTranscurrido += Time.deltaTime;
            porcentaje = Mathf.PingPong(tiempoTranscurrido, time / 2) / (time / 2);
            transform.position = Vector3.Lerp(pointA.position, pointB.position, porcentaje);

            //TODO: implementar eso que tengo en la foto de x (0,5) se regresa x (5,10) se iba 
            if (tiempoTranscurrido >= time)
            {
                time -= Time.deltaTime;
                cronometerTotalTime += Time.deltaTime;
            }
            else if (cronometerPercentage % (5f * 2f) < (5f))
            {
                dir = 1f;
            }
            else if (cronometerPercentage % (5f * 2f) >= (10f))
            {
                dir = -1f;
            }
            cronometerPercentage += Time.deltaTime * dir;
            speedPercentage += (cronometerPercentage / 5f);
            tacoState = TacoFSM.FINISHED;
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
            tacoState = TacoFSM.GOING_TO_HIT;
            tiempoTranscurrido = 0f;
            //pointA.position = transform.position;
            //pointB.position = pointA.transform.position + (Vector3.right * direction); // ponerle un 3.0f
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

        void OnCollisionEnter(Collision collision)
        {
            if (/*collision.gameObject.CompareTag("Player") && */collision.gameObject.CompareTag("Ball") /*&& isTouch*/)
            {
                ballManager.MoveBallBillardForward();
                print("Haz ganado !");
            }
        }
    }
}
