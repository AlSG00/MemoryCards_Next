using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static event System.Action EscapeButtonPressed;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnEscapeButtonPressed();
        }
    }

    private void OnEscapeButtonPressed()
    {
        EscapeButtonPressed?.Invoke();
    }
}
