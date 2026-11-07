using UnityEngine;

// Gem: only the matching character can pick it up. Collider must be a trigger.
public class Collectible : MonoBehaviour
{
    public Element element;

    void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<PlayerController>();
        if (player == null || player.element != element) return;

        GameManager.Instance.AddGem();
        Destroy(gameObject);
    }
}

