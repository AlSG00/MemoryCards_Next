using UnityEngine;
using TMPro;

public class TutorialHint : MonoBehaviour
{
    [SerializeField] private int _hintIndex;
    [SerializeField] private TextMeshProUGUI _hintTextMesh;
    private bool _isVisible;

    private void OnEnable()
    {
        NEW_GameProgression.OnShowHint += EnableHint;
    }

    private void OnDisable()
    {
        NEW_GameProgression.OnShowHint += EnableHint;
    }

    private void Start()
    {
        _isVisible = false;
        _hintTextMesh.enabled = false;
    }

    private void EnableHint(int hintIndex)
    {
        if (_hintIndex == hintIndex)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        if (_isVisible)
        {
            return;
        }

        _isVisible = true;
        _hintTextMesh.enabled = true;
    }

    private void Hide()
    {
        if (_isVisible == false)
        {
            return;
        }

        _isVisible = false;
        _hintTextMesh.enabled = false;
    }
}
