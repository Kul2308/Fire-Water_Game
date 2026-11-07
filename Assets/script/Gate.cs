using UnityEngine;

// A solid wall that slides when opened. Needs a (non-trigger) collider.
[RequireComponent(typeof(Rigidbody2D))]
public class Gate : MonoBehaviour
{
    public float openHeight = 3f;
    public float speed = 3f;

    // True while the gate is moving back to its closed position.
    public bool IsClosing { get; private set; }

    Rigidbody2D rb;
    Vector2 closedPos, openPos;
    bool open;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        closedPos = transform.position;
        openPos = closedPos + Vector2.up * openHeight;
    }

    public void SetOpen(bool value) => open = value;

    void FixedUpdate()
    {
        IsClosing = !open && (rb.position - closedPos).sqrMagnitude > 0.0001f;

        Vector2 target = open ? openPos : closedPos;
        rb.MovePosition(Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime));
    }
}