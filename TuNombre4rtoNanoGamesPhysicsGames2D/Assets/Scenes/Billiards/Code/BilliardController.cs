
namespace Gavryk.Physics.Billiard
{
    using UnityEngine;
    using UnityEngine.InputSystem;

    public class BilliardController : MonoBehaviour
    {

        public void OnClickMouse(InputAction.CallbackContext value)
        {
            if (value.performed)
            {
                Debug.Log("Click" + value.ReadValue<Vector2>().ToString());
            }
        }
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
