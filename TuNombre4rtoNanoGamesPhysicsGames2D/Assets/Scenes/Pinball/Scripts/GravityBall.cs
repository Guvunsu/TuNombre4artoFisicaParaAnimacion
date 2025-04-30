using UnityEngine;
namespace Gravity
{
    public class GravityBall : MonoBehaviour
    {
        [Header("Gravity")]
        [SerializeField] Transform ball_Pinball;
        [SerializeField] protected float gravityForce;
        [SerializeField] float accelation;
        float verticalGravity;
        bool isGround;

        Vector2 direction;

        void FixedUpdate()
        {
            if (isGround == true)
            {

                verticalGravity -= gravityForce * Time.fixedDeltaTime;
                transform.Translate(Vector3.up * verticalGravity * Time.fixedDeltaTime);

            }
        }

        #region Getters

        public float GetGravityForce
        {
            get { return gravityForce; }
        }
        #endregion Getters

        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Fondo") && isGround)
            {
                Physics.gravity = new Vector3(0, 0, 0);
            }
        }
    }

}