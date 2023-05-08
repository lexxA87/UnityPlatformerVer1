using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol points")]
    [SerializeField] private Transform _leftEdge;
    [SerializeField] private Transform _rightEdge;

    [Header("Enemy")]
    [SerializeField] private Transform _enemy;

    [Header("Movement params")]
    [SerializeField] private float _speed;
    [SerializeField] private float _idleDuration;

    [Header("Enemy animator")]
    [SerializeField] private Animator _animator;

    private Vector3 _initScale;
    private bool _movingLeft;
    private float _idleTimer;

    private void Awake()
    {
        _initScale = _enemy.localScale;
    }

    private void Update()
    {
        if (_movingLeft)
        {
            if (_enemy.position.x >= _leftEdge.position.x)
            {
                MoveInDirection(-1);
            }
            else
            {
                DirectionChange();
            }
        }
        else
        {
            if (_enemy.position.x <= _rightEdge.position.x)
            {
                MoveInDirection(1);
            }
            else
            {
                DirectionChange();
            }
        }
    }

    private void OnDisable()
    {
        _animator.SetBool("moving", false);
    }

    private void DirectionChange()
    {
        _animator.SetBool("moving", false);

        _idleTimer += Time.deltaTime;

        if (_idleTimer > _idleDuration)
            _movingLeft = !_movingLeft;
    }

    private void MoveInDirection(int direction)
    {
        _idleTimer = 0;

        _animator.SetBool("moving", true);

        _enemy.localScale = new Vector3(Mathf.Abs(_initScale.x) * direction, _initScale.y, _initScale.z);

        _enemy.position = new Vector3(_enemy.position.x + Time.deltaTime * direction * _speed,
            _enemy.position.y, _enemy.position.z);
    }
}
