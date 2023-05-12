using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    [Header("Params")]
    [Range(50, 500)]
    [SerializeField]
    private int _damage = 100;
    [Range(0f, 10f)]
    [SerializeField]
    private float _speed = 0.1f;
    [Range(0f, 20f)]
    [SerializeField]
    private float _movementDistance = 5f;

    private bool _movingLeft;
    private float _leftEdge;
    private float _rightEdge;

    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _leftEdge = transform.position.x - _movementDistance;
        _rightEdge = transform.position.x + _movementDistance;
    }

    private void Update()
    {
        if (_movingLeft)
        {
            if (transform.position.x > _leftEdge)
            {
                transform.position = new Vector3(transform.position.x - _speed * Time.deltaTime, transform.position.y, transform.position.z);
                _spriteRenderer.flipX = false;
            }
            else _movingLeft = false;
        }
        else
        {
            if (transform.position.x < _rightEdge)
            {
                transform.position = new Vector3(transform.position.x + _speed * Time.deltaTime, transform.position.y, transform.position.z);
                _spriteRenderer.flipX = true;
            }
            else _movingLeft = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int damage = 0;

        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHealth>().DamageHealth(_damage);
        }

        if (collision.CompareTag("PlayerDamage"))
        {

            if (collision.gameObject.name == "Sword")
            {
                damage = collision.GetComponentInParent<PlayerAttack>().DamageSword;
            }
            if (collision.gameObject.name == "Fireball(Clone)")
            {
                damage = collision.GetComponent<FireballController>().Damage;
            }

            transform.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
