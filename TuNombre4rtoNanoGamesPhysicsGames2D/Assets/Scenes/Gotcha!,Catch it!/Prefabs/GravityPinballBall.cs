using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

namespace Gravity
{

    public class GravityPinballBall : MonoBehaviour
    {
        #region Parameters

        [SerializeField] protected Spaceship_SO _soParameters;
        [SerializeField] protected Transform _raycastPivot;

        #endregion

        #region RuntimeVariables

        //DeLorean variables
        [SerializeField] protected float throttle; //speed / acceleration
        protected float steerDirection;  //Rotation local (X axis)

        //Other variables
        protected SpaceshipStates currentShipState;
        protected Coroutine throttleCoroutine;
        protected Coroutine steerCoroutine;
        protected float throttleInputValue;
        protected float steerInputValue;
        protected bool throttleState;
        protected bool steerState;

        //Gravity variable
        protected bool isBeingPulledByGravity;
        [SerializeField] protected float gravityAccelerationForce = 0.981f;
        protected Transform centerOfGravity;
        protected Vector3 gravityDirection;
        [SerializeField] protected float gravityForce;
        protected float distanceToCenterOfGravity;
        protected float orbitLimitFromCenterOfGravity;
        [SerializeField] protected float gravityPercentage;

        //Raycast variables
        protected Ray ray;
        protected RaycastHit raycastHit;
        protected float reflectionAngle;

        #endregion

        #region UnityMethods

        private void Start()
        {
            StartCoroutine(PilotSpaceship());
            gravityAccelerationForce = 0f;
            ray = new Ray();
        }

        #region TriggerEvents

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("OnTriggerEnter() with " + other.gameObject.name + " with layer " + other.gameObject.layer);
            switch (other.gameObject.layer)
            {
                case (int)Layers.GRAVITY:
                    isBeingPulledByGravity = true;
                    centerOfGravity = other.transform;
                    gravityForce = other.gameObject.GetComponent<GravityForce>().GetGravityForce;
                    orbitLimitFromCenterOfGravity = other.gameObject.GetComponent<SphereCollider>().radius * transform.localScale.magnitude;
                    break;
                case (int)Layers.OBSTACLE: //Forbidden to use LayerMask.GetMask("Obstacle") for game object validation
                    ray.origin = _raycastPivot.position;
                    ray.direction = _raycastPivot.forward;
                    //Draws a prototype of a future Physics.Raycast
                    Debug.DrawRay(
                            ray.origin,
                            ray.direction * _soParameters.parameters.raycastDistance,
                            Color.blue,
                            5.0f //seconds
                        );
                    if (Physics.Raycast(
                        ray,
                        out raycastHit,
                        _soParameters.parameters.raycastDistance,
                        LayerMask.GetMask("Obstacle")
                        )
                    )
                    {
                        Debug.DrawRay(
                            raycastHit.point,
                            raycastHit.normal * 2f,
                            Color.red,
                            10f
                            );
                        //Prepare for the reflected bounce
                        reflectionAngle = Vector3.SignedAngle(
                            -1f * transform.forward,
                            raycastHit.normal,
                            transform.right
                            );

                        //transform.forward = raycastHit.normal;
                        transform.forward =
                            Quaternion.AngleAxis(reflectionAngle, transform.right) * raycastHit.normal;

                        Debug.DrawRay(
                            raycastHit.point,
                            transform.forward * 2f,
                            Color.green,
                            10f
                            );

                        //friction factor to modify the throttle
                        throttle *= _soParameters.parameters.frictionBounce;

                        Debug.Break(); //Pauses the game in editor mode
                    }
                    break;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Gravity"))
            {
                isBeingPulledByGravity = false;
                //we reset acceleration for the next contact
                //with a gravity force
                gravityAccelerationForce = 0.0f;
                //we clear the data from this gravity force
                gravityForce = 0f;
            }
        }

        #endregion

        #endregion

        #region PublicMethods

        #region BroadcastMessage

        public void OnThrottle(InputValue value) //InputAction.CallbackContext value
        {
            //Debug.Log(this.name + ", " + gameObject.name + " OnThrottle(): " + value.Get<float>());
            if (value.Get<float>() != 0f) //value.ReadValue<float>()
            {
                if (!throttleState)
                {
                    throttleState = true;
                    //Debug.Log(this.name + ", " + gameObject.name + " OnThrottle(): The coroutine will START");
                    throttleCoroutine = StartCoroutine(ThrottleSpaceship());
                }
                //for every change of input, we store the value
                //and it will be read if the coroutine is working
                throttleInputValue = value.Get<float>();
            }
            else //if(value.Get<float>() == 0f)
            {
                if (throttleState)
                {
                    throttleState = false;
                    //Debug.Log(this.name + ", " + gameObject.name + " OnThrottle(): The coroutine will END");
                    StopCoroutine(throttleCoroutine);
                    throttleCoroutine = null;
                }
            }
        }

        public void OnSteer(InputValue value)
        {
            Debug.Log(this.name + " - " + gameObject.name + " - OnSteer(): Axis " + value.Get<float>());
            if (value.Get<float>() != 0f) //value.ReadValue<float>()
            {
                //Debug.Log("BEGIN");
                if (!steerState)
                {
                    steerState = true;
                    if (steerCoroutine != null)
                    {
                        StopCoroutine(steerCoroutine);
                    }
                    steerCoroutine = null;
                    steerCoroutine = StartCoroutine(SteerSpaceship());
                }
                //for every change of input, we store the value
                //and it will be read if the coroutine is working
                steerInputValue = value.Get<float>();
            }
            else //if(value.Get<float>() == 0f)
            {
                //Debug.Log("STOP");
                if (steerState)
                {
                    steerState = false;
                    if (steerCoroutine != null)
                    {
                        StopCoroutine(steerCoroutine);
                    }
                    steerCoroutine = null;
                    steerCoroutine = StartCoroutine(StopSteeringSpaceship());
                }
            }
        }

        #endregion BroadcastMessage

        #endregion PublicMethods

        #region LocalMethods

        #region Coroutines

        //Personalized Update() as a coroutine
        protected IEnumerator PilotSpaceship()
        {
            while (currentShipState == SpaceshipStates.ALIVE)
            {
                //Direction: Rotation of the spaceship
                //Speed (Throttle) -> Acceleration: Change of speed in time, and
                //according to the input
                transform.Translate(Vector3.forward * throttle * _soParameters.parameters.coroutineDeltaTime, Space.Self);
                transform.Rotate(Vector3.right * steerDirection, Space.Self); //Space.Self -> transform.localRotation
                //now, gravity is an acceleration, because it changes in time
                if (isBeingPulledByGravity) //according to the intersection of the trigger
                {
                    distanceToCenterOfGravity = (centerOfGravity.transform.position - transform.position).magnitude;
                    if (distanceToCenterOfGravity > _soParameters.parameters.gravityStopThreshold)
                    {
                        if (gravityAccelerationForce <= _soParameters.parameters.gravityCap)
                        {
                            gravityAccelerationForce += gravityForce * _soParameters.parameters.coroutineDeltaTime;
                        }
                        gravityPercentage = 1f - (distanceToCenterOfGravity / orbitLimitFromCenterOfGravity);
                        gravityDirection = (centerOfGravity.transform.position - transform.position).normalized;

                        //Gravity exertion force to the steering
                        //steerDirection => Vector3.right in SpaceSelf
                        //transform.forward is already normalized
                        transform.forward += gravityDirection * gravityPercentage * _soParameters.parameters.coroutineDeltaTime;
                        transform.forward.Normalize();
                    }
                    else //do not exert gravity at the center, to prevent the pull force drastically
                    {
                        //Gravity will not compute at the center of gravity
                        gravityAccelerationForce -= gravityForce * _soParameters.parameters.coroutineDeltaTime;
                        if (gravityAccelerationForce < 0f)
                            gravityAccelerationForce = 0f;
                    }
                }
                transform.Translate(gravityDirection * (gravityAccelerationForce * gravityPercentage) * _soParameters.parameters.coroutineDeltaTime, Space.World);
                yield return new WaitForSeconds(_soParameters.parameters.coroutineDeltaTime);
            }
        }

        protected IEnumerator ThrottleSpaceship()
        {
            while (true)
            {
                //Debug.Log(this.name + ", " + gameObject.name + " ThrottleSpaceship(): Throttling ;)");
                //Linear Acceleration
                if (throttle <= _soParameters.parameters.throttleCap)
                {
                    throttle += 1f * throttleInputValue * _soParameters.parameters.coroutineDeltaTime;
                }
                yield return new WaitForSeconds(_soParameters.parameters.coroutineDeltaTime);
            }
        }

        protected IEnumerator SteerSpaceship()
        {
            while (true)
            {
                Debug.Log("SteeringSpaceship()");
                //Linear Acceleration
                //Debug.Log(this.name + ", " + gameObject.name + " SteerSpaceship(): Steer Direction: " + steerDirection);
                if (Mathf.Abs(steerDirection) <= _soParameters.parameters.steerCap)
                {
                    steerDirection += 10f * steerInputValue * _soParameters.parameters.coroutineDeltaTime;
                }
                //this only rotates the spaceship while it has an input
                //transform.Rotate(Vector3.right * steerDirection, Space.Self); //Space.Self -> transform.localRotation
                yield return new WaitForSeconds(_soParameters.parameters.coroutineDeltaTime);
            }
        }

        //when you free the input, the spaceship's rotation has to eventually stop / break
        protected IEnumerator StopSteeringSpaceship()
        {
            while (true)
            {
                Debug.Log("StopSteeringSpaceship()");
                if (steerDirection > 0f)
                {
                    steerDirection -= (steerDirection * 2f) * _soParameters.parameters.coroutineDeltaTime;
                    if (steerDirection < 0f)
                    {
                        steerDirection = 0f;
                    }
                }
                else if (steerDirection < 0f)
                {
                    steerDirection += (Mathf.Abs(steerDirection) * 2f) * _soParameters.parameters.coroutineDeltaTime;
                    if (steerDirection > 0f)
                    {
                        steerDirection = 0f;
                    }
                }
                yield return new WaitForSeconds(_soParameters.parameters.coroutineDeltaTime);
            }
        }

        #endregion

        #endregion
    }
}

