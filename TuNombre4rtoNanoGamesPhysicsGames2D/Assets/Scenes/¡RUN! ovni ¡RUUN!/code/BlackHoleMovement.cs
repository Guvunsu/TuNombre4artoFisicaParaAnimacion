using UnityEngine;

namespace Gavryk.Physics.BlackHole {
    public class BlackHoleMovement : MonoBehaviour {

        #region Struct & SO
        public struct SineParametersBlackHole {
            //A * sen(B * x + C) + D
            public float A, B, C, D, horizontalScale;
            // A Amplitud (qu� tan alto/bajo oscila)
            // B Frecuencia (qu� tan r�pido oscila)
            // C Desfase horizontal
            // D Desplazamiento vertical
        }

        [System.Serializable]
        [CreateAssetMenu(fileName = "SineParameters_SO", menuName = "Scriptable Objects/SineParametersBlackHole_SO")]
        public class SineParameters : ScriptableObject {
            [SerializeField] public SineParametersBlackHole sineParameters;
        }

        #endregion Struct & SO

        #region Variables
        [Header("Seno Movimiento X")]
        [SerializeField] public SineParameters soSP_X;

        [Header("Seno Movimiento Y")]
        [SerializeField] public SineParameters soSP_Y;

        [Header("Duraci�n de Vida")]
        [SerializeField] private float lifeTime = 6.66f;

        private float posBlackHoleX;
        private float posBlackHoleY;
        private float timer = 0f;
        private bool isActive = false;

        protected Vector3 nodePosition;

        #endregion Variables

        #region Unity Methods
        void Update() {
            if (!isActive) return;

            if (timer >= lifeTime) {
                gameObject.SetActive(false);
            }
        }

        void FixedUpdate() {
            if (!isActive) return;

            timer += Time.fixedDeltaTime;
            MoveWithSine();
        }

        #endregion Unity Methods

        #region Move Methods
        void MoveWithSine() {
            posBlackHoleX = soSP_X.sineParameters.A * Mathf.Sin(soSP_X.sineParameters.B * timer + soSP_X.sineParameters.C) + soSP_X.sineParameters.D;
            posBlackHoleY = soSP_Y.sineParameters.A * Mathf.Sin(soSP_Y.sineParameters.B * timer + soSP_Y.sineParameters.C) + soSP_Y.sineParameters.D;

            nodePosition = new Vector3(posBlackHoleX, posBlackHoleY, transform.position.z);
            transform.position = nodePosition;
        }

        public void RandomMovementLifeTime(float duration) {
            lifeTime = duration;
            timer = 0f;
            isActive = true;
            nodePosition = transform.position;

            //par�metros para X
            soSP_X.sineParameters.A = Random.Range(1f, 5f);
            soSP_X.sineParameters.B = Random.Range(0.5f, 2f);
            soSP_X.sineParameters.C = Random.Range(0f, Mathf.PI * 2);// A VER QUE TAL CON EL PI
            soSP_X.sineParameters.D = Random.Range(-9f, 10f);

            // par�metros para Y
            soSP_Y.sineParameters.A = Random.Range(1f, 5f);
            soSP_Y.sineParameters.B = Random.Range(0.5f, 2f);
            soSP_Y.sineParameters.C = Random.Range(0f, Mathf.PI * 2); // experimento con el PI
            soSP_Y.sineParameters.D = Random.Range(-5.4f, 5.56f);

        }
        #endregion Move Methods

        #region Trigger Collision
        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Spaceship")) {
                Destroy(other.gameObject);

                MovementOVNI ovni = FindObjectOfType<MovementOVNI>();
                if (ovni != null) {
                    ovni.LosePanel();
                }

                gameObject.SetActive(false);
            }
        }

        #endregion Trigger Collision
    }
}