using UnityEngine;

// Put this on a child of the Gate that has a BoxCollider2D with "Is Trigger" ticked.
// Players only die while the gate is coming down (closing) onto them.
public class KillZone : MonoBehaviour
{
    public Gate gate;   // found automatically from the parent if left empty

    void Awake()
    {
        if (gate == null) gate = GetComponentInParent<Gate>();
    }

    void OnTriggerEnter2D(Collider2D other) => TryKill(other);
    void OnTriggerStay2D(Collider2D other) => TryKill(other);

    void TryKill(Collider2D other)
    {
        if (gate == null || !gate.IsClosing) return;
        if (other.GetComponent<PlayerController>() != null)
            GameManager.Instance.PlayerDied();
    }
}