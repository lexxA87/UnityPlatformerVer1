using UnityEngine;

public class Parallax : MonoBehaviour
{
    [Range(0f, 1f)][SerializeField] float _lagAmount = 0f;

    private Transform _camera;
    private Vector3 _previousCameraPosition;
    private Vector3 _targetPosition;

    private float ParallaxAmount => 1f - _lagAmount;

    private void Awake()
    {
        _camera = GameObject.Find("PlayerCamera").GetComponent<Transform>();
        _previousCameraPosition = _camera.position;
    }

    private void LateUpdate()
    {
        Vector3 movement = CameraMovement;
        if (movement == Vector3.zero) return;
        _targetPosition = new Vector3(transform.position.x + movement.x * ParallaxAmount,
            transform.position.y, transform.position.z);
        transform.position = _targetPosition;
    }

    Vector3 CameraMovement
    {
        get
        {
            Vector3 movement = _camera.position - _previousCameraPosition;
            _previousCameraPosition = _camera.position;
            return movement;
        }
    }
}
