using UnityEngine;

// Button on the floor. Holds a Gate open while anything dynamic (player or box) stands on it.
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
        if (count == 1) gate.Press();      // first thing stepped on this button
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!IsActivator(other)) return;
        count = Mathf.Max(0, count - 1);
        if (count == 0) gate.Release();    // last thing left this button
    }
}