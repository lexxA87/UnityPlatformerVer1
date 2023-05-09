using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class FireballController : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody2D _body;
    private CircleCollider2D _collider;

    private sbyte _direction = 1;
    private bool _isMove = true;
    private float _lifeTimeCounter = 0;


    [Header("Params")]
    [Range(1f, 50f)]
    [SerializeField] private float _speed = 5;
    [Range(10, 100)]
    [SerializeField] private int _damage = 25;
    [Range(1f, 10f)]
    [SerializeField] private float _lifeTime = 3;
    [Header("Animation")]
    [SerializeField] private string _exploreTriggerName = "ExploreTrigger";

    public int Damage { get { return _damage; } }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _body = GetComponent<Rigidbody2D>();
        _collider = GetComponent<CircleCollider2D>();
    }

    private void OnEnable()
    {
        _lifeTimeCounter = 0;
    }

    private void CheckLifeTime()
    {
        if (_lifeTimeCounter >= _lifeTime)
            Deactivate();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_isMove)
        {
            _body.velocity = new Vector2(_speed * _direction, _body.velocity.y);
            _lifeTimeCounter += Time.deltaTime;
            CheckLifeTime();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) return;

        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Health>().TakeDamage(_damage);
        }

        _body.velocity = new Vector2(0, 0);
        _isMove = false;
        _collider.enabled = false;
        _animator.SetTrigger(_exploreTriggerName);
    }

    public void Deactivate()
    {
        var localScale = transform.localScale;
        transform.localScale = new Vector3(Mathf.Abs(localScale.x), localScale.y, localScale.z);
        _direction = 1;
        gameObject.SetActive(false);
    }

    public void Init(bool isLeft)
    {
        if (isLeft)
        {
            var localScale = transform.localScale;
            transform.localScale = new Vector3(-localScale.x, localScale.y, localScale.z);
            _direction = -1;
        }
        _collider.enabled = true;
        _isMove = true;
    }
}
