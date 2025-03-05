using UnityEngine;

namespace Gavryk.Physics.Football
{
    [System.Serializable]
    public struct SineParametersFootball
    {
        //A * sen(B * x + C) + D
        public float A, B, C, D, horizontalScale;
        //A: Vertical stretch
        //B: Horizontal stretch
        //C: Horizontal displacement
        //D: Vertical displacement
    }

    [CreateAssetMenu(fileName = "SineParameters_SO", menuName = "Scriptable Objects/SineParametersFootball_SO")]
    public class SineParameters : ScriptableObject
    {
        [SerializeField] public SineParametersFootball sineParameters;
    }
}

