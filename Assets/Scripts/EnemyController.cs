using UnityEngine;

public class EnemyController : MonoBehaviour
{
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


    private int _currentTarget;


    private void FixedUpdate()
    {
        Move();
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
            Debug.Log("Player attacked!");
            _player.Health -= _damage;
            Debug.Log("Health player: " + _player.Health);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerDamage"))
        {

            Debug.Log("DAmage!!!");
            Debug.Log(collision.gameObject.name);
        }
    }
}
