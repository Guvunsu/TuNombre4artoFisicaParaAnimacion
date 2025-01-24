
namespace Gavryk.Physics.Billiard
{
    using UnityEngine;
    using UnityEngine.InputSystem;
    using UnityEngine.InputSystem.LowLevel;

    public enum TacoFSM
    {
        WAITING_FOR_HIT,
        GOING_TO_HIT,
        FINISHED
    }

    public class BilliardController : MonoBehaviour
    {
        BallManager ballManager;
        [SerializeField] GameObject player;
        [SerializeField] Transform pointA;
        [SerializeField] Transform pointB;
        PlayerInput button;

        [SerializeField] float time = 5f;
        [SerializeField] float tiempoTranscurrido = 0f;
        float direction;
        float porcentaje;
        bool isTouch = true;

        #region LocalVariables

        protected TacoFSM tacoState;

        #endregion


        public void OnClickMouse(InputAction.CallbackContext value)
        {
            if (value.performed)
            {
                //MoveTaco(value.ReadValue<Vector2>());
                ConfirmHit();
                Debug.Log("Click" + value.ReadValue<Vector2>().ToString());
            }
        }
        void Start()
        {
            player = GetComponent<GameObject>();
            button = GetComponent<PlayerInput>();
            tacoState = TacoFSM.WAITING_FOR_HIT;

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
        void WaitingForHit()
        {
            tiempoTranscurrido += Time.deltaTime;
            porcentaje = tiempoTranscurrido / time;
            transform.position = Vector3.Lerp(pointA.position, pointB.position, tiempoTranscurrido);

            //TODO: implementar eso que tengo en la foto de x (0,5) se regresa x (5,10) se iba 
            if (tiempoTranscurrido >= time)
            {
                
                time -= Time.deltaTime;

            }
          
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
            pointA.position = transform.position;
            pointB.position = pointA.transform.position + (Vector3.right * direction); // ponerle un 3.0f
        }

        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player") && collision.gameObject.CompareTag("Ball") && isTouch)
            {
                ballManager.MoveBallBillardForward();
                print("Haz ganado !");
            }
        }
    }
}
