using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsPlayer : MonoBehaviour {

    [SerializeField] private float speed = 10f;

    private PlayerController playerControllerScript;

    private void Start() {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update() {
        if (!playerControllerScript.gameOver) {
            transform.Translate(Vector3.back * speed * Time.deltaTime, relativeTo: Space.World);
        }
        DestroyOutOfBound();
    }
    private void DestroyOutOfBound() {
        if (transform.position.z < -10) {
            Destroy(gameObject);
        }
    }
}
