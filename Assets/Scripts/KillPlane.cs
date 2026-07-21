using UnityEngine;

//  Just a backup from older builds when objects didn't have built-in timers
//  Sits under the play area and is a giant volume
public class KillPlane : MonoBehaviour
{

    public string requiredTag = "";
    public bool logDestruction = true;

    //  delets anything that collides with it to prevent 
    private void OnTriggerEnter(Collider other)
    {
        if (!string.IsNullOrEmpty(requiredTag) && !other.CompareTag(requiredTag))
            return;

        if (logDestruction)
            Debug.Log($"[KillPlane] Destroyed out-of-bounds object: {other.name}");

        Destroy(other.gameObject);
    }
}