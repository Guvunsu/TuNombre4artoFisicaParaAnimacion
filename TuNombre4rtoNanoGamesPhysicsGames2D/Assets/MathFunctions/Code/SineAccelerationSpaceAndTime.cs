using UnityEngine;

public class SineAccelerationSpaceAndTime : MonoBehaviour
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
    [SerializeField] protected float speed;
    [SerializeField] protected Direction direction;

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
        cronometer += Time.fixedDeltaTime; //F(x) = F(t) -> Speed
        //Acquire the local position at this frame
        nodePosition = _goSineObject.transform.localPosition;
        speed = //f(x) / F(t)
            Mathf.Abs(
                soSP.sineParameters.A *
                Mathf.Sin(soSP.sineParameters.B * cronometer / soSP.sineParameters.horizontalScale + soSP.sineParameters.C) +
                soSP.sineParameters.D
            ); //A * sen(B * nodePosition.x + C) + D 
        nodePosition.x += (float)direction * speed;
        //DeLorean Formula:
        nodePosition.y =
            soSP.sineParameters.A *
            Mathf.Sin(soSP.sineParameters.B * nodePosition.x / soSP.sineParameters.horizontalScale + soSP.sineParameters.C) +
            soSP.sineParameters.D
            ; //A * sen(B * nodePosition.x + C) + D 
        _goSineObject.transform.localPosition = nodePosition;
    }

    #endregion
}
