using UnityEngine;
using System.Collections;
using Gavryk.Physics.BlackHole;

public class BlackHoleSpawner : MonoBehaviour {
    [Header("Spawns")]
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] GameObject blackHolePrefab;

    [Header("Timing")]
    [SerializeField] float spawnInterval = 1f;
    [SerializeField] float spawnDuration = 6.66f;
    [SerializeField] float blackHoleLifeTime = 3.33f;

    void Start() {
        StartCoroutine(SpawnBlackHolesRoutine());
    }

    IEnumerator SpawnBlackHolesRoutine() {
        float elapsed = 0f;

        while (elapsed < spawnDuration) {
            SpawnBlackHole();
            yield return new WaitForSeconds(spawnInterval);
            elapsed += spawnInterval;
        }
    }

    void SpawnBlackHole() {
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomIndex];

        GameObject bh = Instantiate(blackHolePrefab, spawnPoint.position, Quaternion.identity);
        BlackHoleMovement controller = bh.GetComponent<BlackHoleMovement>();

        if (controller != null) {
            controller.RandomMovementLifeTime(blackHoleLifeTime);
        }
    }
}

