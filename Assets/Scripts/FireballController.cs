using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class FireballController : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody2D _body;
    private CircleCollider2D _collider;

    private sbyte _direction = 1;
    private bool _isMove = true;


    [Header("Params")]
    [Range(1f, 50f)]
    [SerializeField] private float _speed = 5;
    [Header("Animation")]
    [SerializeField] private string _exploreTriggerName = "ExploreTrigger";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _body = GetComponent<Rigidbody2D>();
        _collider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isMove)
            _body.velocity = new Vector2(_speed * _direction, _body.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) return;

        _body.velocity = new Vector2(0, 0);
        _isMove = false;
        _collider.enabled = false;
        _animator.SetTrigger(_exploreTriggerName);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public void Init(bool isLeft)
    {
        if (isLeft)
        {
            var localScale = transform.localScale;
            transform.localScale = new Vector3(localScale.x, localScale.y, localScale.z);
            _direction = -1;
        }
        _isMove = true;
    }
}
