using UnityEngine;

// Each character has their own door. Level is won when BOTH are standing in theirs.
public class ExitDoor : MonoBehaviour
{
    public Element element;

    void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<PlayerController>();
        if (player != null && player.element == element)
            GameManager.Instance.SetAtDoor(element, true);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        var player = other.GetComponent<PlayerController>();
        if (player != null && player.element == element)
            GameManager.Instance.SetAtDoor(element, false);
    }
}