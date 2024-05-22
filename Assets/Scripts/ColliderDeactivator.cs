using UnityEngine;

// Deactivates object's collider when setting game to pause

public class ColliderDeactivator : MonoBehaviour
{
    private void OnEnable()
    {
        NEW_GameProgression.PauseGame += SetColliderEnabled;
    }

    private void OnDisable()
    {
        NEW_GameProgression.PauseGame -= SetColliderEnabled;
    }

    private protected void SetColliderEnabled(bool setEnable)
    {
        gameObject.GetComponent<Collider>().enabled = !setEnable;
    }
}
