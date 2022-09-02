using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] private Vector3 _offset;
    [Range(1.0f, 10.0f)]
    [SerializeField] private float _smoothness;
    private Transform _tr;
    private Camera _camera;

    [SerializeField] private BoxCollider2D _boundShape;
    private float _boundingShapeXMin;
    private float _boundingShapeXMax;
    private float _boundingShapeYMin;
    private float _boundingShapeYMax;
    private Transform _target;
    private void Awake()
    {
        _tr = GetComponent<Transform>();
        _camera = Camera.main;
        
        _boundingShapeXMin = _boundShape.transform.position.x + _boundShape.offset.x - _boundShape.size.x / 2.0f;
        _boundingShapeXMax = _boundShape.transform.position.x + _boundShape.offset.x + _boundShape.size.x / 2.0f;
        _boundingShapeYMin = _boundShape.transform.position.y + _boundShape.offset.y - _boundShape.size.y / 2.0f;
        _boundingShapeYMax = _boundShape.transform.position.y + _boundShape.offset.y + _boundShape.size.y / 2.0f;
    }

    private void Start()
    {
        _target = Player.instance.transform;
    }

    private void LateUpdate()
    {
        Follow();
    }

    private void Follow()
    {
        if (_target == null)
            return;

        Vector3 targetPos = new Vector3(_target.position.x, _target.position.y, _tr.position.z) +
                            _offset;

        Vector3 smoothPos = Vector3.Lerp(_tr.position, targetPos, _smoothness * Time.deltaTime);

        Vector3 camWorldPosLeftBottom = _camera.ViewportToWorldPoint(new Vector3(0.0f, 0.0f, _camera.nearClipPlane));
        Vector3 camWorldPosRightTop = _camera.ViewportToWorldPoint(new Vector3(1.0f, 1.0f, _camera.nearClipPlane));
        Vector3 camWorldPosSize = new Vector2(camWorldPosRightTop.x - camWorldPosLeftBottom.x,
                                              camWorldPosRightTop.y - camWorldPosLeftBottom.y);

        // X min bound
        if (smoothPos.x < _boundingShapeXMin + camWorldPosSize.x / 2.0f)
            smoothPos.x = _boundingShapeXMin + camWorldPosSize.x / 2.0f;
        // X max bound
        else if (smoothPos.x > _boundingShapeXMax - camWorldPosSize.x / 2.0f)
            smoothPos.x = _boundingShapeXMax - camWorldPosSize.x / 2.0f;

        // Y min bound
        if (smoothPos.y < _boundingShapeYMin + camWorldPosSize.y / 2.0f)
            smoothPos.y = _boundingShapeYMin + camWorldPosSize.y / 2.0f;
        // X max bound
        else if (smoothPos.y > _boundingShapeYMax - camWorldPosSize.y / 2.0f)
            smoothPos.y = _boundingShapeYMax - camWorldPosSize.y / 2.0f;

        _tr.position = smoothPos;
    }

    private void OnDrawGizmosSelected()
    {
        Camera cam = Camera.main;
        Vector3 p = cam.ViewportToWorldPoint(new Vector3(0.0f, 0.0f, cam.nearClipPlane));
        Vector3 q = cam.ViewportToWorldPoint(new Vector3(1.0f, 1.0f, cam.nearClipPlane));
        Vector3 center = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, cam.nearClipPlane));

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, new Vector2(q.x - p.x, q.y - p.y));

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(_boundShape.transform.position + (Vector3)_boundShape.offset, _boundShape.size);
    }
}
