using UnityEngine;

public class SineSliderToMoveObject : MonoBehaviour
{
    #region References

    [SerializeField] protected GameObject _goSineObject;

    #endregion

    #region Parameters

    [Range(0f, 1f), SerializeField] protected float percentage; //Lerp()
    [SerializeField] protected SineParameters_SO soSP;

    #endregion

    #region RuntimeVariables

    protected Vector3 nodePosition;

    #endregion

    #region UnityMethods

    private void OnDrawGizmos()  //Update in Editor Mode
    {
        Debug.Log("Hola");
        SineLerp();
    }

    #endregion

    #region LocalMethods

    protected void SineLerp()
    {
        nodePosition = _goSineObject.transform.localPosition;
        nodePosition.x = percentage; //nodePosition's original X coordinate
        nodePosition.y =
            soSP.sineParameters.A *
            Mathf.Sin(soSP.sineParameters.B * percentage + soSP.sineParameters.C) +
            soSP.sineParameters.D
            ; //A * sen(B * percentage + C) + D 
        nodePosition.x *= soSP.sineParameters.horizontalScale;
        _goSineObject.transform.localPosition = nodePosition;
    }

    #endregion
}
