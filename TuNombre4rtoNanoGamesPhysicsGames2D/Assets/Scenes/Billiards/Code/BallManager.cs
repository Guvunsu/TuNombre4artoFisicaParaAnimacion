using Unity.VisualScripting;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    [SerializeField] GameObject ballPrefab;
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] float speedPercentage;
    [SerializeField] float time = 5f;
    [SerializeField] float tiempoTranscurrido = 0f;
    float spawnPos;
    float cronometerPercentage;
    float cronometerTotalTime;
    bool tacoIsTouchedMe;
    float dir;


    void Start()
    {
        ballPrefab = GetComponent<GameObject>();
        SpawnBall();
    }
    void Update()
    {
        MoveBallBillardForward();
    }
    void SpawnBall()
    {
        Instantiate(ballPrefab, spawnPoints[1]);// no se si deba de agregar el pedo del ,prefab.transform.rotation
        spawnPos = Random.Range(0, spawnPoints.Length);

    }
    public void MoveBallBillardForward()
    {
        cronometerTotalTime += Time.deltaTime;
        if (cronometerPercentage % (5f * 2f) < (5f))
        {
            dir = 1f;
        }
        else //if (cronometerPercentage % (5f * 2f) >= (5f))
        {
            dir = -1f;
        }
        cronometerPercentage += Time.deltaTime * dir;
        speedPercentage += (cronometerPercentage / 5f);
        //Vector3 startPos = transform.position;
        //Vector3 endPos = transform.position + transform.right * speed;
        transform.position = Vector3.Lerp(, speedPercentage);

    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("me toco el taco");
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.CompareTag("Ball") && tacoIsTouchedMe)
        {
            MoveBallBillardForward();
        }
    }
}
