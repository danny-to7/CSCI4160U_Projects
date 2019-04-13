using System.Collections;
using UnityEngine;

public class PlayerInput2 : MonoBehaviour {
    [SerializeField] private float movementSpeed = 5.0f;
    [SerializeField] private float jumpFactor = 20.0f;
    [SerializeField] private AnimationCurve jumpCurve;
    public AudioSource audioSource;
    public AudioClip[] soundClips;
    public float footstepRate = 2f;
    private float timeToNextStep = 0f;

    private CharacterController controller;

    private bool isJumping = false;

    void Awake() {
        controller = GetComponent<CharacterController>();
    }

    void Update() {
        // movement and strafing
        float horizontal = Input.GetAxis("Horizontal") * movementSpeed;
        float vertical = Input.GetAxis("Vertical") * movementSpeed;

        Vector3 forwardMovement = transform.forward * vertical;
        Vector3 rightMovement = transform.right * horizontal;

        controller.SimpleMove(forwardMovement + rightMovement);

        if (controller.isGrounded && controller.velocity.magnitude > 0f && Time.time >= timeToNextStep)
        {
            timeToNextStep= Time.time + 1f / footstepRate;
            int random = Random.Range(0, soundClips.Length);
            audioSource.PlayOneShot(soundClips[random]);

        }
        // jumping
        if (Input.GetButtonDown("Jump") && !isJumping) {
            isJumping = true;
            StartCoroutine(Jump());
        }

    }

    // co-routine
    private IEnumerator Jump() {
        float timeInAir = 0.0f;

        do {
            // update the position
            float jumpAmount = jumpCurve.Evaluate(timeInAir) * jumpFactor * Time.deltaTime;
            controller.Move(Vector3.up * jumpAmount);
            timeInAir += Time.deltaTime;

            yield return null;
        } while (!controller.isGrounded && controller.collisionFlags != CollisionFlags.Above);

        isJumping = false;
    }
}
