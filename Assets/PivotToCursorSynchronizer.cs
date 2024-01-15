using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotToCursorSynchronizer : MonoBehaviour
{
    [SerializeField] private Vector3 _positionOffset;
    [SerializeField] private Quaternion _rotationOffset;
    [SerializeField] private LayerMask _activeLayer;

    private void Update()
    {
        Ray mouseWorldPosition = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(mouseWorldPosition, out RaycastHit raycastHit, 100, _activeLayer))
        {
            transform.position = new Vector3(
                raycastHit.point.x + _positionOffset.x,
                raycastHit.point.y + _positionOffset.y,
                raycastHit.point.z + _positionOffset.z
                );

            transform.rotation = new Quaternion(
                _rotationOffset.x,
                _rotationOffset.y,
                _rotationOffset.z,
                _rotationOffset.w
                );
        }
    }
}
