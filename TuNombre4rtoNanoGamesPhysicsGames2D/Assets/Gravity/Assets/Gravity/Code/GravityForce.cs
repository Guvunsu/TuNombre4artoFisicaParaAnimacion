using UnityEngine;

namespace Gravity
{
    public class GravityForce : MonoBehaviour
    {
        [SerializeField] protected float gravityForce;

        #region Getters

        public float GetGravityForce
        {
            get { return gravityForce; }
        }

        #endregion
    }
}
