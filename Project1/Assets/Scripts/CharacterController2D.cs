using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class CharacterController2D : MonoBehaviour {

    [Header("General")]
    [SerializeField] private float movementSpeed = 10f;
    [Range(0, .3f)] [SerializeField] private float movementSmoothing = .05f;
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] private float knockbackX = 100f;
    [SerializeField] private float knockbackY = 0f;
    public float speed = 0.0f;

    [Header("Jumping")]
    [SerializeField] private bool canAirControl = true;
    [SerializeField] private float jumpForce = 500f;
    [SerializeField] private Transform groundPosition;
    public bool isGrounded = true;

    [Header("Crouching")]
    [SerializeField] private Collider2D colliderToDisableOnCrouch;
    [SerializeField] private Transform ceilingPosition;
    [Range(0, 1)] [SerializeField] private float crouchSpeedMultiplier = .4f;

    private bool isFacingRight = true;

    const float groundedRadius = .2f;
    const float ceilingRadius = .2f;

    private Rigidbody2D rigidBody;
    private Vector3 velocity = Vector3.zero;
    private PlayerHealth healthManager;

    [Header("Events")]
    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    public BoolEvent OnCrouchEvent;
    private bool wasCrouching = false;

    private void Awake() {
        rigidBody = GetComponent<Rigidbody2D>();
        healthManager = GetComponent<PlayerHealth>();

        if (OnLandEvent == null) {
            OnLandEvent = new UnityEvent();
        }

        if (OnCrouchEvent == null) {
            OnCrouchEvent = new BoolEvent();
        }
    }

    private void FixedUpdate() {
        bool wasGrounded = isGrounded;
        isGrounded = false;

        // find any ground layer colliders closer than the ground position
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundPosition.position, groundedRadius, groundLayers);
        for (int i = 0; i < colliders.Length; i++) {
            // if any of the colliders are not the object iself, it must be the ground
            if (colliders[i].gameObject != gameObject) {
                isGrounded = true;

                // if we were not grounded before, but now are, generate the landed event
                if (!wasGrounded) {
                    OnLandEvent.Invoke();
                }
            }
        }

    }


    public void Move(float move, bool jump) {

        // only control the player if grounded or canAirControl is turned on
        if (isGrounded || canAirControl) {

            // what speed do we want to travel?
            Vector3 targetVelocity = new Vector2(move * movementSpeed, GetComponent<Rigidbody2D>().velocity.y);

            // apply smoothing to the speed
            rigidBody.velocity = Vector3.SmoothDamp(rigidBody.velocity, targetVelocity, ref velocity, movementSmoothing);

            // export the speed
            speed = rigidBody.velocity.magnitude;

            if (move > 0 && !isFacingRight) {
                // flip the sprite horizontally when travelling left
                Flip();
            } else if (move < 0 && isFacingRight) {
                // flip the sprite horizontally when travelling left
                Flip();
            }
        }
        if (isGrounded && jump) {
            // add a vertical force to the player
            isGrounded = false;
            rigidBody.AddForce(new Vector2(0f, jumpForce));
        }
    }

    private void Flip() {
        // remember which way the sprite is facing
        isFacingRight = !isFacingRight;

        // multiply the player's x local scale by -1
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    //Run into enemy
    void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy enemy = collision.collider.GetComponent<Enemy>();
        float playerX = gameObject.transform.position.x;
        if (enemy != null)
        {
            //Bump player to left if enemy is to the right
            if (enemy.transform.position.x > playerX)
            {
                rigidBody.velocity = new Vector2(-knockbackX, knockbackY);
            }
            //Bump player to the right if enemy is to the left
            else if (enemy.transform.position.x < playerX)
            {
                rigidBody.velocity = new Vector2(knockbackX, knockbackY);
            }
            healthManager.GetHit();
        }

        //End level
        if (collision.collider.CompareTag("End"))
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
