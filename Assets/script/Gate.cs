using UnityEngine;
using UnityEngine.Serialization;

// A solid wall that slides when opened. Needs a (non-trigger) collider.
// Opens while at least one button pointing at it is pressed.
[RequireComponent(typeof(Rigidbody2D))]
public class Gate : MonoBehaviour
{
    // How far the gate slides when open.
    [FormerlySerializedAs("openHeight")]
    public float openDistance = 3f;

    // Direction to slide: (1,0) = right, (-1,0) = left.
    public Vector2 openDirection = Vector2.right;

    public float speed = 3f;

    // True while the gate is moving back to its closed position.
    public bool IsClosing { get; private set; }

    Rigidbody2D rb;
    Vector2 closedPos, openPos;
    int pressCount;     // how many buttons are currently pressed

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;

        closedPos = transform.position;
        openPos = closedPos + openDirection.normalized * openDistance;
    }

    public void Press() => pressCount++;
    public void Release() => pressCount = Mathf.Max(0, pressCount - 1);

    void FixedUpdate()
    {
        bool open = pressCount > 0;
        IsClosing = !open && (rb.position - closedPos).sqrMagnitude > 0.0001f;

        Vector2 target = open ? openPos : closedPos;
        rb.MovePosition(Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime));
    }
}