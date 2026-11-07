using UnityEngine;

// Button on the floor. Opens a Gate while anything dynamic (player or box) stands on it.
// Collider must be a trigger.
public class PressurePlate : MonoBehaviour
{
    public Gate gate;
    int count;

    bool IsActivator(Collider2D o) =>
        o.attachedRigidbody != null && o.attachedRigidbody.bodyType == RigidbodyType2D.Dynamic;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!IsActivator(other)) return;
        count++;
        gate.SetOpen(true);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!IsActivator(other)) return;
        count--;
        if (count <= 0) { count = 0; gate.SetOpen(false); }
    }
}