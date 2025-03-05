using UnityEngine;

public class SineCoordinateCalculation : MonoBehaviour
{
    #region References

    [SerializeField] protected GameObject _goSineObject;

    #endregion

    #region Parameters

    [SerializeField] protected SineParameters_SO soSP;

    #endregion

    #region RuntimeVariables

    protected Vector3 nodePosition;

    #endregion

    #region UnityMethods

    private void OnDrawGizmos()  //Update in Editor Mode
    {
        Debug.Log("Hola");
        SineCoordinateCalculationUpdate();
    }

    #endregion

    #region LocalMethods

    protected void SineCoordinateCalculationUpdate()
    {
        nodePosition = _goSineObject.transform.localPosition;
        nodePosition.y =
            soSP.sineParameters.A *
            Mathf.Sin(soSP.sineParameters.B * nodePosition.x / soSP.sineParameters.horizontalScale + soSP.sineParameters.C) +
            soSP.sineParameters.D
            ; //A * sen(B * nodePosition.x + C) + D 
        _goSineObject.transform.localPosition = nodePosition;
    }

    #endregion
}
