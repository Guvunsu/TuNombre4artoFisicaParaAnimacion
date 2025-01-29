namespace Gavryk.Physics.Billiard
{
    using NUnit.Framework.Internal.Commands;
    using UnityEngine;
    using UnityEngine.InputSystem;
    using UnityEngine.InputSystem.LowLevel;
    using UnityEngine.ProBuilder.MeshOperations;

    // se me olvidara si no escribo esto 
    // ponerle el iskinematic en una linea 
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
        [SerializeField] float timeOscilationTaco = 1f;
        [SerializeField] float timeGame = 0f;
        [SerializeField] float hitForce;
        //[SerializeField] float speedPercentage;
        //[SerializeField] float cronometerTotalTime;
        //[SerializeField] float cronometerPercentage;
        //float dir;
        //bool isTouch;

        protected TacoFSM tacoState;

        #endregion LocalVariables & ENUM

        #region InputActions
        public void OnClickMouse(InputAction.CallbackContext value)
        {
            if (value.performed && tacoState == TacoFSM.WAITING_FOR_HIT)
            {
                Debug.Log("Barra espaciadora presionada");
                //if (Keyboard.current.spaceKey.wasPressedThisFrame) {
                //    moveInput = value.ReadValue<Vector3>();
                //    Debug.Log("Click" + value.ReadValue<Vector2>().ToString());
                //    ConfirmHit(value);
                //}
                if (value.performed)
                {
                    tacoState = TacoFSM.GOING_TO_HIT;
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
            //button = GetComponent<PlayerInput>();
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
                    ConfirmHit();
                    MovingTacoTowardBall();
                    break;
                case TacoFSM.FINISHED:
                    //None
                    break;
            }
        }

        #endregion UnityMethods

        #region ActionGame
        /// <summary>
        /// Movimiento del taco de billar 
        /// </summary>
        public void WaitingForHit()
        {
            timeGame += Time.deltaTime;
            float porcentaje = Mathf.PingPong(timeGame, timeOscilationTaco) / timeOscilationTaco;
            transform.position = Vector3.Lerp(pointA.position, pointB.position, porcentaje);

            // intento Opcional recomendado en vez del PingPong
            /*//TODO: implementar eso que tengo en la foto de x (0,5) se regresa x (5,10) se iba 
            //if (timeGame >= timeOscilationTaco)
            //{
            //    timeOscilationTaco -= Time.deltaTime;
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
        /// <summary>
        /// intento de limitar el mov. para detenerlo a los 6 sec
        /// activa el esatdo finito de quedarse quieto
        /// </summary>
        public void GoingToHit()
        {
            if (timeGame <= timeOscilationTaco)
            {
                timeGame = 6f;
                print("Loose the game");
                tacoState = TacoFSM.FINISHED;
            }
        }
        /// <summary>
        /// intento de mover con 2 puntos de referewncia del taco 
        /// </summary>
        public void ConfirmHit()
        {
            Debug.Log("Me activo en Confirm Hit ?");
            switch (tacoState)
            {
                case TacoFSM.GOING_TO_HIT:
                    transform.position += Vector3.Lerp(PointC.position, PointD.position, timeGame / timeOscilationTaco);
                    print("Haz ganado !");
                    break;
            }
        }
        /// <summary>
        /// mover taco hacia adelante del taco 
        /// </summary>
        public void MovingTacoTowardBall()
        {

            transform.position += Vector3.right * hitForce * Time.deltaTime;
            if (transform.position.x > 15.0f)
            {
                tacoState = TacoFSM.FINISHED;
            }
        }

        #endregion ActionGame
    }
}
