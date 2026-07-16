using UnityEngine;

public class KillPlane : MonoBehaviour
{

    public string requiredTag = "";
    public bool logDestruction = true;

    private void OnTriggerEnter(Collider other)
    {
        if (!string.IsNullOrEmpty(requiredTag) && !other.CompareTag(requiredTag))
            return;

        if (logDestruction)
            Debug.Log($"[KillPlane] Destroyed out-of-bounds object: {other.name}");

        Destroy(other.gameObject);
    }
}