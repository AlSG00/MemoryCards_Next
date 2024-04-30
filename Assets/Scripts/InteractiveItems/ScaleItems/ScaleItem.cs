using UnityEngine;

public class ScaleItem : InteractiveItem
{
    [SerializeField] private Transform _selfPivot;
    [SerializeField] private bool _isVisible;
    private Collider _collider;

    private void Awake()
    {
        _collider = gameObject.GetComponent<Collider>();

        _isPicked = false;
        _isReadyToUse = false;
        _isVisible = false;
        _collider.enabled = false;
    }

    private void OnEnable()
    {
        ScaleColliderHandler.OnEnterCollider += SetReadyToUse;
        ShopHandler.OnShowScaleItems += ChangeVisibility;
        NEW_GameProgression.PauseGame += SetColliderEnabled;
    }

    private void OnDisable()
    {
        ScaleColliderHandler.OnEnterCollider -= SetReadyToUse;
        ShopHandler.OnShowScaleItems -= ChangeVisibility;
        NEW_GameProgression.PauseGame -= SetColliderEnabled;
    }

    private protected override void OnMouseDown()
    {
        _isChangingPosition = true;
        _currentPivot = _cursorPivot;
        _collider.enabled = false;
        MoveToPivot(_currentPivot, _moveToCursorTime);
        _audioPlayer.MouseDown();
        _isPicked = true;
    }

    private protected override void OnMouseUp()
    {
        _isPicked = false;
        if (_isReadyToUse)
        {
            _isReadyToUse = false;
            gameObject.GetComponent<IUsable>().Use();
            if (_audioPlayer is ScaleItemAudioPlayer scaleItemAudioPlayer)
            {
                scaleItemAudioPlayer.OnPlaceOnScale();
            }
        }
        else
        {
            _collider.enabled = true;
        }

        _isChangingPosition = true;
        Cursor.visible = true;
        _currentPivot = _selfPivot;
        MoveToPivot(_currentPivot, _moveToStandartPositionTime);
        _audioPlayer.MouseUp();
        _isPicked = false;
    }

    private void ChangeVisibility(bool isActive)
    {
        if (isActive == _isVisible)
        {
            return;
        }

        if (isActive)
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
        if (_isVisible == false)
        {
            _isVisible = true;
            _animator.SetTrigger("Show");
        }

        _collider.enabled = true;
    }

    private void Hide()
    {
        if (_isVisible)
        {
            _isVisible = false;
            _animator.SetTrigger("Hide");
        }

        _collider.enabled = false;
    }

    private void SetColliderEnabled(bool enabled)
    {
        _collider.enabled = enabled;
    }
}
