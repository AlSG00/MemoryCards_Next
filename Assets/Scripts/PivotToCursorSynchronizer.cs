using UnityEngine;

public class PivotToCursorSynchronizer : MonoBehaviour
{
    [SerializeField] private Vector3 _positionOffset;
    [SerializeField] private Vector3 _rotationOffset; //TODO: replace with vector3 type
    [SerializeField] private LayerMask _activeLayer;

    private void Update()
    {
        Ray mouseWorldPosition = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(mouseWorldPosition, out RaycastHit raycastHit, 100, _activeLayer))
        {
            transform.position = raycastHit.point + _positionOffset;
            transform.rotation = Quaternion.Euler(_rotationOffset);
        }
    }
}
