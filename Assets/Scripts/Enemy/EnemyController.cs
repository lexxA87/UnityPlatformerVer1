using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class EnemyController : MonoBehaviour
{
    private Animator _animator;

    [Header("Params")]
    [Range(0.01f, 1f)]
    [SerializeField]
    private float _speed = 0.1f;
    [Range(50, 500)]
    [SerializeField]
    private int _health = 100;
    [Range(50, 500)]
    [SerializeField]
    private int _damage = 50;
    [SerializeField] Vector3[] _pointsPosition;
    [SerializeField] private PlayerHealth _player;
    [SerializeField] private GameObject _coinPrefab;
    [Header("Animation")]
    [SerializeField] private string _isDeadName = "IsDead";

    private int _currentTarget;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        CheckDead();
        Move();
    }

    private void CheckDead()
    {
        if (_health <= 0)
        {
            _animator.SetBool(_isDeadName, true);
            return;
        }
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, _pointsPosition[_currentTarget], _speed);

        if (_currentTarget == 1)
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }

        if (transform.position == _pointsPosition[_currentTarget])
        {
            if (_currentTarget < _pointsPosition.Length - 1)
            {
                _currentTarget++;
            }
            else
            {
                _currentTarget = 0;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _player.DamageHealth(_damage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerDamage"))
        {
            if (collision.gameObject.name == "Sword")
            {
                _health -= 50;
                Debug.Log("Sword attack" + _health);
            }
            if (collision.gameObject.name == "Fireball(Clone)")
            {
                _health -= 25;
                Debug.Log("Fireball attack" + _health);
            }
        }
    }

    private void CreateCoin()
    {
        Instantiate(_coinPrefab, transform.position, Quaternion.identity);
    }

    public void OnDestroyEnemy()
    {
        Destroy(gameObject);
        CreateCoin();
    }
}
