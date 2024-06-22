using UnityEngine;

// Deactivates object's collider when setting game to pause

public class ColliderDeactivator : MonoBehaviour
{
    private void OnEnable()
    {
        GameProgression.PauseGame += SetColliderEnabled;
    }

    private void OnDisable()
    {
        GameProgression.PauseGame -= SetColliderEnabled;
    }

    private protected void SetColliderEnabled(bool setEnable)
    {
        gameObject.GetComponent<Collider>().enabled = !setEnable;
    }
}
