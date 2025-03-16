using UnityEngine;
namespace Gavryk.Physics.BlackHole {
    public class BlackHoleMovement : MonoBehaviour {
        public struct SineParametersFootball {
            //A * sen(B * x + C) + D
            public float A, B, C, D, horizontalScale;
            //A: Vertical stretch
            //B: Horizontal stretch
            //C: Horizontal displacement
            //D: Vertical displacement
        }

        [SerializeField] float SpeedBlackHole;
        [SerializeField] float lifetimeBlackHole;
        [SerializeField] float startPos;
        [SerializeField] float endPos;

        [SerializeField] GameObject blackHole;

        [SerializeField] Transform PointSpawnA;
        [SerializeField] Transform PointSpawnB;
        [SerializeField] Transform PointSpawnC;
        [SerializeField] Transform PointSpawnD;
        [SerializeField] Transform PointSpawnE;
        [SerializeField] Transform PointSpawnF;
        [SerializeField] Transform PointSpawnG;
        [SerializeField] Transform PointSpawnH;

        void Update() {
            MovementBlackHole();
        }

        void MovementBlackHole() {
            SpeedBlackHole += Time.fixedDeltaTime + 0.4f;
            TimerLifeBlackHole();
            SineTransitionBlackHole();
        }
        void SineTransitionBlackHole() {

        }

        void TimerLifeBlackHole() {
            if (blackHole == null) {
                //lifetimeBlackHole += Time.fixedDeltaTime;
                //lifetimeBlackHole = 6f;
                Destroy(blackHole, 6f);
            }
        }

        void SpawnPositionBlackHoles() {
            PointSpawnA.position = new Vector2(startPos, endPos);
        }

        [System.Serializable]

        [CreateAssetMenu(fileName = "SineParameters_SO", menuName = "Scriptable Objects/SineParametersFootball_SO")]
        public class SineParameters : ScriptableObject {
            [SerializeField] public SineParametersFootball sineParameters;
        }
    }
}