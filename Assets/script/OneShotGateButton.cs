using System.Collections;
using UnityEngine;

public class OneShotGateButton : MonoBehaviour
{
    public Transform gate;
    public Vector3 moveOffset = new Vector3(0f, 2f, 0f);
    public float speed = 3f;
    public string playerTag = "Player";

    bool used;

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Button touched by: " + other.name + " (tag: " + other.tag + ")");

        bool isPlayer = other.CompareTag(playerTag) ||
            (other.attachedRigidbody != null && other.attachedRigidbody.CompareTag(playerTag));

        if (used || !isPlayer) return;
        used = true;
        Debug.Log("Moving gate: " + gate.name);
        StartCoroutine(MoveGate());
    }

    IEnumerator MoveGate()
    {
        Vector3 target = gate.position + moveOffset;
        while (Vector3.Distance(gate.position, target) > 0.01f)
        {
            gate.position = Vector3.MoveTowards(gate.position, target, speed * Time.deltaTime);
            yield return null;
        }
        gate.position = target;
    }
}