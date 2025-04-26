using UnityEditor.Experimental.GraphView;
using UnityEngine;

public enum Direction
{
    POSITIVE = 1,
    NEGATIVE = -1
}

public class SineMovementAcrossTime : MonoBehaviour
{
    #region References

    [SerializeField] protected GameObject _goSineObject;

    #endregion

    #region Parameters

    [SerializeField] protected SineParameters_SO soSP;
    [SerializeField] protected Direction direction;
    [SerializeField, Range(0f, 100f)] protected float speed;

    #endregion

    #region RuntimeVariables

    protected Vector3 nodePosition;
    [SerializeField] protected float cronometer;

    #endregion

    #region UnityMethods

    private void FixedUpdate()
    {
        SineCoordinateCalculationAcrossTime();
    }

    #endregion

    #region LocalMethods

    protected virtual void SineCoordinateCalculationAcrossTime()
    {
        cronometer += Time.fixedDeltaTime;
        //Acquire the local position at this frame
        nodePosition = _goSineObject.transform.localPosition;
        //X: works as a cronometer += fixedDeltaTime
        nodePosition.x += (float)direction * speed * Time.fixedDeltaTime; 
        //DeLorean Formula: (+1f or -1f) * meter * per second
        nodePosition.y =
            soSP.sineParameters.A *
            Mathf.Sin(soSP.sineParameters.B * nodePosition.x / soSP.sineParameters.horizontalScale + soSP.sineParameters.C) +
            soSP.sineParameters.D
            ; //A * sen(B * nodePosition.x + C) + D 
        _goSineObject.transform.localPosition = nodePosition;

        //if (nodePosition.x >= 1f) //meter
        //{
        //    Debug.Break();
        //}
    }

    #endregion
}
