using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{

    private GameObject _player;
    private EnemyCast _enemy;
    private Rigidbody2D _body2D;
    private Animator _animator;
    private int _damage;
    private float _force;

    // Start is called before the first frame update
    void Start()
    {
        _body2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _enemy = GameObject.Find("HeadBoss").GetComponent<EnemyCast>();
        _damage = _enemy.Damage;
        _force = _enemy.Force;

        Vector3 direction = _player.transform.position - transform.position;
        _body2D.velocity = new Vector2(direction.x, direction.y).normalized * _force;

        float rot = Mathf.Atan2(-direction.x, -direction.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, -rot - 90);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "HeadBoss") return;

        _body2D.velocity = new Vector2(0, 0);
        _animator.SetTrigger("Explosion");

        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHealth>().DamageHealth(_damage);
        }
    }

    public void DestroyEnemyFireball()
    {
        Destroy(gameObject);
    }
}
