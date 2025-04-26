using UnityEngine;

public class LinearAcceleration : MonoBehaviour
{
    #region Parameters

    [SerializeField] protected GameObject _goSineObject;
    [SerializeField] protected float A = 1f; //Multiplicative
    [SerializeField] protected float B = 0f; //Additive

    #endregion

    #region RuntimeVariable

    [SerializeField] protected float cronometer;
    protected Vector3 nodePosition;
    [SerializeField] protected float speed;
    [SerializeField] protected Direction direction;

    #endregion

    #region UnityMethods

    private void FixedUpdate()
    {
        LinearAccelerationInTime();
    }

    #endregion

    #region LocalMethods

    protected virtual void LinearAccelerationInTime()
    {
        nodePosition = _goSineObject.transform.localPosition;
        cronometer += Time.fixedDeltaTime;
        //Speed is accelerating linearly
        speed = A * cronometer + B; //y = f(x) = A * t + B
        nodePosition.x = (float)direction * speed * Time.fixedDeltaTime;
        _goSineObject.transform.localPosition = nodePosition;
    }

    #endregion
}
