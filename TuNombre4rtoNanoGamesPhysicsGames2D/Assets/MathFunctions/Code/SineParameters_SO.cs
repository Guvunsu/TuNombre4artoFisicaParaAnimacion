using UnityEngine;

[System.Serializable]
public struct SineParameters
{
    //A * sen(B * x + C) + D
    public float A, B, C, D, horizontalScale;
    //A: Vertical stretch
    //B: Horizontal stretch
    //C: Horizontal displacement
    //D: Vertical displacement
}

[CreateAssetMenu(fileName = "SineParameters_SO", menuName = "Scriptable Objects/SineParameters_SO")]
public class SineParameters_SO : ScriptableObject
{
    [SerializeField] public SineParameters sineParameters;
}
