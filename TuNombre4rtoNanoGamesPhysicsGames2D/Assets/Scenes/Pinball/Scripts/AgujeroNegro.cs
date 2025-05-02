using UnityEngine;

public class AgujeroNegro : MonoBehaviour {
    [Header("Componentes")]
    [SerializeField] Transform[] blackHolePoints;
    [SerializeField] GameObject blackHolePrefab;
    // hacer un trigger qu eis toca el tag de la bola se activa el panl de victoria y en el
    // tiempo vincular esa funcion para decirle que si gano en menos de 60 seg ganas o llmaar el timer desde aqui
    PanelManager script_PanelManager;
    TimerPinball script_TimerPinball;

    void Start() {
        int indexAleatorio = Random.Range(0, blackHolePoints.Length);
        Instantiate(blackHolePrefab, blackHolePoints[indexAleatorio].position, Quaternion.identity);
    }

}
