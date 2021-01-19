using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private SpriteRenderer playerSprite;
    private Transform feet, feet2;
    private Rigidbody2D rb;
    public LayerMask groundLayer;

    private readonly float speed = 12f, jumpStrength = 12f, jumpTime = 0.25f, coyoteTime = 0.1f, jumpRememberTime = 0.2f;
    private float timeSinceJumpPress, speedModifier, jumpTimer, coyoteTimer;
    private bool grounded, isJumping, canJump;
    private int framesOnGround = 0;

    private void Start()
    {
        InitializePlayerController();
    }
    private void InitializePlayerController()
    {
        playerSprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        feet = transform.Find("feet");
        feet2 = transform.Find("feet2");
    }
    private void FixedUpdate()
    {
        if (grounded)
        {
            framesOnGround++;
            if (framesOnGround > 6)
            {
                canJump = true;
            }
        }
        else framesOnGround = 0;
    }
    private void Update()
    {
        #region CollisionCheck

        grounded = Physics2D.OverlapArea(feet.position, feet2.position, groundLayer);

        if (grounded)
        {
            coyoteTimer = 0f;
            speedModifier = 1f;
        }
        else
        {
            coyoteTimer += Time.deltaTime;
            speedModifier = 0.8f;
        }
        #endregion
        #region Jump

        timeSinceJumpPress -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.W))
            timeSinceJumpPress = jumpRememberTime;

        if (coyoteTimer < coyoteTime && timeSinceJumpPress > 0f && canJump)
        {
            canJump = false;
            Jump();
        }

        if (Input.GetKey(KeyCode.W) && isJumping)
        {
            if (jumpTimer > 0f)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0f) + Vector2.up * jumpStrength;
                jumpTimer -= Time.deltaTime;
            }
            else isJumping = false;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            isJumping = false;
        }
        #endregion

        #region HorizontalMovement

        float xInput = Input.GetAxisRaw("Horizontal");

        if (xInput == 1) playerSprite.flipX = false;
        else if (xInput == -1) playerSprite.flipX = true;

        rb.velocity = new Vector2(xInput * speed * speedModifier, rb.velocity.y);
        if (rb.velocity.y < 0f)
            rb.velocity += Vector2.up * Physics2D.gravity.y * 1.8f * Time.deltaTime;

        #endregion
    }
    private void Jump()
    {
        isJumping = true;
        rb.velocity = new Vector2(rb.velocity.x, 0f) + Vector2.up * jumpStrength;
        jumpTimer = jumpTime;
    }
}
