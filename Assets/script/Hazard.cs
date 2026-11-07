using UnityEngine;

public enum HazardType { Fire, Water, Acid }

// Put on a pool object that has a BoxCollider2D with "Is Trigger" ticked.
public class Hazard : MonoBehaviour
{
    public HazardType type;

    void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<PlayerController>();
        if (player == null) return;

        // Fire pool is safe for Fireboy, water pool is safe for Watergirl, acid kills both.
        bool safe =
            (type == HazardType.Fire && player.element == Element.Fire) ||
            (type == HazardType.Water && player.element == Element.Water);

        if (!safe) GameManager.Instance.PlayerDied();
    }
}
