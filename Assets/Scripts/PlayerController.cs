using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField] Transform leftRay;
    [SerializeField] Transform middleRay;
    [SerializeField] Transform rightRay;
    private Transform currentRay;

    [SerializeField] float transitionSpeed = 5f;

    [SerializeField] ParticleSystem dirtParticle;
    [SerializeField] ParticleSystem explosionParticle;
    [SerializeField] AudioSource playerAudio;
    [SerializeField] AudioClip jumpSound;
    [SerializeField] AudioClip crashSound;

    Rigidbody playerRb;
    bool isOnGround = true;
    [SerializeField] float gravityModifier;
    [SerializeField] float jumpForce = 5.0f;

    public bool gameOver;

    private void Start() {
        playerRb = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();
        currentRay = middleRay;
        Physics.gravity *= gravityModifier;
    }

    private void Update() {
        PlayerMovement();
        PlayerJump();
    }
    private void PlayerMovement() {
        if (Input.GetKeyDown(KeyCode.RightArrow) && !gameOver) {
            if (currentRay == leftRay) {
                MoveToRay(middleRay);
            }
            else if (currentRay == middleRay) {
                MoveToRay(rightRay);
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && !gameOver) {
            if (currentRay == rightRay) {
                MoveToRay(middleRay);
            }
            else if (currentRay == middleRay) {
                MoveToRay(leftRay);
            }
        }
    }

    private void PlayerJump() {
        if (Input.GetKeyDown(KeyCode.UpArrow) && isOnGround && !gameOver) {
            isOnGround = false;
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }
    }

    private void MoveToRay(Transform targetRay) {
        StartCoroutine(Transition(targetRay));
    }

    private System.Collections.IEnumerator Transition(Transform targetRay) {
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = targetRay.position;

        float elapsedTime = 0f;
        while (elapsedTime < 1f) {
            elapsedTime += Time.deltaTime * transitionSpeed;
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime);
            yield return null;
        }

        // Geçiþ tamamlandýðýnda mevcut ray'i güncelle
        currentRay = targetRay;
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Obstacle")) {
            playerAudio.PlayOneShot(crashSound, 1.0f);
            explosionParticle.Play();
            dirtParticle.Stop();
            Destroy(gameObject, 0.4f);
            Destroy(collision.gameObject, 0.4f);
            gameOver = true;
        }
        else if (collision.gameObject.CompareTag("Ground")) {
            isOnGround = true;
            dirtParticle.Play();
        }
    }
}
