using UnityEngine;

public enum Element { Fire, Water }

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    Animator anim;
    [Header("Who am I?")]
    public Element element;

    [Header("Controls (change per player in the Inspector)")]
    public KeyCode leftKey = KeyCode.A;
    public KeyCode rightKey = KeyCode.D;
    public KeyCode jumpKey = KeyCode.W;

    [Header("Movement")]
    public float moveSpeed = 6f;
    public float jumpSpeed = 12f;

    [Header("Ground check")]
    public Transform groundCheck;                       // empty child at the character's feet
    public Vector2 groundCheckSize = new Vector2(0.5f, 0.1f);
    public LayerMask groundMask;                        // set to the "Ground" layer

    Rigidbody2D rb;
    SpriteRenderer sr;
    float moveInput;
    bool jumpPressed;

    // Unity 6 renamed Rigidbody2D.velocity -> linearVelocity. This works in both.
    Vector2 Velocity
    {
#if UNITY_6000_0_OR_NEWER
        get => rb.linearVelocity;
        set => rb.linearVelocity = value;
#else
        get => rb.velocity;
        set => rb.velocity = value;
#endif
    }

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();   // NEW
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    // Read input every frame...
    void Update()
    {
        moveInput = 0f;
        if (Input.GetKey(leftKey)) moveInput -= 1f;
        if (Input.GetKey(rightKey)) moveInput += 1f;

        if (Input.GetKeyDown(jumpKey)) jumpPressed = true;

        if (moveInput != 0f) sr.flipX = moveInput < 0f;
        if (anim) anim.SetFloat("Speed", Mathf.Abs(moveInput));   // NEW
    }

    // ...but apply it in the physics step.
    void FixedUpdate()
    {
        bool grounded = Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0f, groundMask);
        if (anim) anim.SetBool("Grounded", grounded);   // NEW

        Vector2 v = Velocity;
        v.x = moveInput * moveSpeed;
        if (jumpPressed && grounded) v.y = jumpSpeed;
        jumpPressed = false;
        Velocity = v;
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(groundCheck.position, groundCheckSize);
    }
}