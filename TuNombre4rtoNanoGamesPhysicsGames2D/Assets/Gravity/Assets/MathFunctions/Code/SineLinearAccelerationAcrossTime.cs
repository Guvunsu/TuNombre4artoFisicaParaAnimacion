using UnityEngine;

public class SineLinearAccelerationAcrossTime : SineMovementAcrossTime
{
    #region Parameters

    [SerializeField] protected float A = 1f; //Multiplicative
    [SerializeField] protected float B = 0f; //Additive

    #endregion

    #region UnityMethods

    private void FixedUpdate()
    {
        SineCoordinateCalculationAcrossTime();
    }

    #endregion

    #region LocalMethods
    
    protected override void SineCoordinateCalculationAcrossTime()
    {
        //Speed is accelerating linearly
        if (speed <= 5f)
        {
            speed = A * cronometer + B;  //y = f(x) = A * t + B
        }
        else
        {
            speed = 5f;
        }
        base.SineCoordinateCalculationAcrossTime();
    }

    #endregion
}
