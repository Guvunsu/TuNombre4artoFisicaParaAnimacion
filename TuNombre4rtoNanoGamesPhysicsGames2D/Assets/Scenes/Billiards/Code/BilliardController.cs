namespace Gavryk.Physics.Billiard {
    using NUnit.Framework.Internal.Commands;
    using TMPro;
    using UnityEngine;
    using UnityEngine.InputSystem;
    using UnityEngine.InputSystem.LowLevel;
    using UnityEngine.ProBuilder.MeshOperations;

    // se me olvidara si no escribo esto 
    // ponerle el iskinematic en una linea 
    public class BilliardController : MonoBehaviour {

        #region LocalVariables & ENUM
        // es recomendable hacer un script de FSM y heredar su info aqui ya que por estrucutra de scripts ya hace 2 cosas este script, solo 1 tarea por script
        public enum TacoFSM {
            WAITING_FOR_HIT,
            GOING_TO_HIT,
            FINISHED, //LOSE
            VICTORY
        }

        BallManager ballManager;
        [SerializeField] GameObject vicotoryPanel;
        [SerializeField] GameObject losePanel;

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

        [SerializeField] protected TacoFSM tacoState;

        protected float attemptingHitCronometer = 0f;

        #endregion LocalVariables & ENUM

        #region InputActions
        public void OnClickMouse(InputAction.CallbackContext value) {
            Debug.Log("Clic derecho detectado");
            if (value.performed && tacoState == TacoFSM.WAITING_FOR_HIT) {
                Debug.Log("Barra espaciadora presionada");
                ConfirmHit();
            }
        }

        #endregion InputActions

        #region UnityMethods
        void Start() {
            tacoState = TacoFSM.WAITING_FOR_HIT;
            player = GetComponent<GameObject>();
        }
        void Update() {
            timeGame += Time.deltaTime;
            switch (tacoState) {
                case TacoFSM.WAITING_FOR_HIT:
                    WaitingForHit();
                    break;
                case TacoFSM.GOING_TO_HIT:
                    AttemptingToHitTheBall();
                    break;
                case TacoFSM.FINISHED:
                    //None
                    break;
            }
            //Manejo general del tiempo para validar la condición de fracaso
            if (timeGame >= 6.66f && tacoState != TacoFSM.VICTORY) //TODO: Validate victory
            {
                print("Loose the game");
                tacoState = TacoFSM.FINISHED;
                losePanel.SetActive(true);
            }
        }
        private void OnTriggerEnter(Collider other) {
            if (other.gameObject.tag == "Ball") {
                tacoState = TacoFSM.VICTORY;
                //TextMeshPros 
                vicotoryPanel.SetActive(true);
            }
        }

        #endregion UnityMethods

        #region ActionGame
        /// <summary>
        /// Movimiento del taco de billar 
        /// </summary>
        public void WaitingForHit() {
            float porcentaje = Mathf.PingPong(timeGame, timeOscilationTaco) / timeOscilationTaco;
            transform.localPosition = Vector3.Lerp(pointA.position, pointB.position, porcentaje);

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
        public void AttemptingToHitTheBall() {
            attemptingHitCronometer += Time.deltaTime;
            transform.position = Vector3.Lerp(PointC.position, PointD.position, attemptingHitCronometer / 1f);

            if (attemptingHitCronometer > 1f) {
                tacoState = TacoFSM.FINISHED;
            }
        }
        /// <summary>
        /// intento de mover con 2 puntos de referewncia del taco 
        /// </summary>
        public void ConfirmHit() {
            Debug.Log("Me activó al Confirm Hit");
            tacoState = TacoFSM.GOING_TO_HIT;
            PointC.position = transform.position;
            PointD.position = PointC.position + (Vector3.right * 8f);
        }
        #endregion ActionGame
    }
}
