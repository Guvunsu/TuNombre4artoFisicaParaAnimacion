using UnityEngine;

namespace Gravity
{
    #region structs

    [System.Serializable]
    public struct SpaceshipParameters
    {
        [Header("General")]
        [SerializeField] public float coroutineDeltaTime;
        [Header("Piloting")]
        [SerializeField] public float steerCap;
        [SerializeField] public float throttleCap;
        [Header("Gravity")]
        [SerializeField] public float gravityCap;
        [SerializeField] public float gravityStopThreshold;
        [Header("Collision")]
        [SerializeField] public float raycastDistance;
        [Range(0f, 1000000000f)]
        [SerializeField] public float frictionBounce;
    }

    #endregion

    [CreateAssetMenu(fileName = "Spaceship_SO", menuName = "Scriptable Objects/Spaceship_SO")]
    public class Spaceship_SO : ScriptableObject
    {
        [SerializeField] public SpaceshipParameters parameters;
    }
}