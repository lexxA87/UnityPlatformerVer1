using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class FireballController : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody2D _body;
    private CircleCollider2D _collider;


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
        _body.velocity = new Vector2(_speed, _body.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) return;

        _collider.enabled = false;
        _animator.SetTrigger(_exploreTriggerName);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
