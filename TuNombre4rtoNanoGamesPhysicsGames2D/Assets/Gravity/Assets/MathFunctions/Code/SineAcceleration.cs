using UnityEngine;

public class SineAcceleration : MonoBehaviour
{
    #region References

    [SerializeField] protected GameObject _goSineObject;

    #endregion

    #region Parameters

    [SerializeField] protected SineParameters_SO soSP;

    #endregion

    #region RuntimeVariables

    protected Vector3 nodePosition;
    [SerializeField] protected float cronometer;

    #endregion

    #region UnityMethods

    private void FixedUpdate()
    {
        SineAccelerationInTime();
    }

    #endregion

    #region LocalMethods

    protected virtual void SineAccelerationInTime()
    {
        cronometer += Time.fixedDeltaTime; //F(x) = F(t)
        //Acquire the local position at this frame
        nodePosition = _goSineObject.transform.localPosition;
        //DeLorean Formula:
        nodePosition.y =
            soSP.sineParameters.A *
            Mathf.Sin(soSP.sineParameters.B * cronometer / soSP.sineParameters.horizontalScale + soSP.sineParameters.C) +
            soSP.sineParameters.D
            ; //A * sen(B * nodePosition.x + C) + D 
        _goSineObject.transform.localPosition = nodePosition;
    }

    #endregion
}
