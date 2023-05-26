using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(Animator))]
public class MeleeEnemy : MonoBehaviour
{
    [Header("Attack params")]
    [Range(0, 3f)]
    [SerializeField] private float _attackCooldown;
    [Range(0.5f, 5f)]
    [SerializeField] private float _range;
    [Range(10, 200)]
    [SerializeField] private int _damage;

    [Header("Collider params")]
    [SerializeField] private float _colliderDistance;
    [SerializeField] private BoxCollider2D _boxCollider;

    [Header("Player layer")]
    [SerializeField] private LayerMask _playerLayer;

    [Header("Sound attack")]
    [SerializeField] private AudioClip _attackSound;

    private float _cooldownTimer = Mathf.Infinity;
    private Animator _animator;
    private PlayerHealth _playerHealth;
    private EnemyPatrol _enemyPatrol;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _enemyPatrol = GetComponentInParent<EnemyPatrol>();
        _playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        _cooldownTimer += Time.deltaTime;

        if (PlayerInSight())
        {
            if (_cooldownTimer > _attackCooldown && _playerHealth.Health > 0)
            {
                //attack!
                _cooldownTimer = 0;
                _animator.SetTrigger("attack");
                SoundManager.instance.PlaySound(_attackSound);
            }
        }

        if (_enemyPatrol != null)
        {
            _enemyPatrol.enabled = !PlayerInSight();
        }
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit =
            Physics2D.BoxCast(_boxCollider.bounds.center + transform.right * _range * transform.localScale.x * _colliderDistance,
            new Vector3(_boxCollider.bounds.size.x * _range, _boxCollider.bounds.size.y, _boxCollider.bounds.size.z),
            0, Vector2.left, 0, _playerLayer);

        //if (hit.collider != null)
        //{
        //    _playerHealth = hit.transform.GetComponent<PlayerHealth>();
        //}

        return hit.collider != null;
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


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireCube(_boxCollider.bounds.center + transform.right * _range * transform.localScale.x * _colliderDistance,
            new Vector3(_boxCollider.bounds.size.x * _range, _boxCollider.bounds.size.y, _boxCollider.bounds.size.z));
    }

    public void DamagePlayer()
    {
        if (PlayerInSight())
        {
            _playerHealth.DamageHealth(_damage);
        }
    }
}
