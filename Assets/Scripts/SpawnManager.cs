using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    [SerializeField] private GameObject[] obstaclePrefabs;
    [SerializeField] private Transform leftRay;
    [SerializeField] private Transform middleRay;
    [SerializeField] private Transform rightRay;

    private Vector3 offset = new Vector3(0, 0, 20);

    private PlayerController playerControllerScript;

    void Start() {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        InvokeRepeating("SpawnObstaclesMid", 2, Random.Range(0, 6));
        InvokeRepeating("SpawnObstaclesLeft", 4, Random.Range(0, 6));
        InvokeRepeating("SpawnObstaclesRight", 6, Random.Range(0, 6));
    }

    void SpawnObstaclesLeft() {
        int index = Random.Range(0, obstaclePrefabs.Length);
        if (!playerControllerScript.gameOver) {
        Instantiate(obstaclePrefabs[index], (leftRay.transform.position + offset), obstaclePrefabs[index].transform.rotation);
        }
    }
    private void SpawnObstaclesMid() {
        int index = Random.Range(0, obstaclePrefabs.Length);
        if (!playerControllerScript.gameOver) {
        Instantiate(obstaclePrefabs[index], (middleRay.transform.position + offset), obstaclePrefabs[index].transform.rotation);
        }
    }
    private void SpawnObstaclesRight() {
        int index = Random.Range(0, obstaclePrefabs.Length);
        if (!playerControllerScript.gameOver) {
        Instantiate(obstaclePrefabs[index], (rightRay.transform.position + offset), obstaclePrefabs[index].transform.rotation);
        }
    }
}
