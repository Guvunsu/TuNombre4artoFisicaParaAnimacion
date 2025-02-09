using UnityEngine;


public class RealTimeTransformation : MonoBehaviour
{
    #region Structs

    [System.Serializable] // conversion of bytes for the hdd
    public struct TranslationParameters
    {
        [SerializeField] public Vector3 direction;
        public float speed;
        // float = time es igual FixedDeltaTime 
    }
    public struct ScaleParameters
    {

    }
    public struct RotationParameters
    {

    }

    #endregion Structs

    #region Parametros

    [SerializeField] protected TranslationParameters translationParameters;
    [SerializeField] protected ScaleParameters scaleParameters;
    [SerializeField] protected RotationParameters rotationParameters;

    #endregion Parametros

    #region UnityMethods
    private void FixedUpdate()
    {
        //Time.fixedDeltaTime
        transform.position += translationParameters.speed * translationParameters.direction * Time.fixedDeltaTime;
        // ambos son iguales , es como decirlo en español e ingles el otro, son iguales 
        transform.Translate(translationParameters.speed * translationParameters.direction * Time.fixedDeltaTime, Space.World);

        // local tranlation -> Self
        // transform.Translate(translationParameters.speed * translationParameters.direction * Time.fixedDeltaTime, Space.Self);

    }

    #endregion UnityMethods
}
