using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMove : MonoBehaviour
{
    [Range(1f, 50f)]
    [SerializeField]
    private float _speed = 5;

    [Range(0.2f, 50f)]
    [SerializeField]
    private float _jumpPower = 5;

    private bool _onGround = false;
    public bool OnGround
    {
        get => _onGround;
    }

    private Rigidbody2D _body;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        var horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput != 0)
        {
            _body.velocity = new Vector2(horizontalInput * _speed, _body.velocity.y);
        }

        if (Input.GetAxis("Jump") != 0 && _onGround)
        {
            _body.velocity = new Vector2(_body.velocity.x, _jumpPower);
            _onGround = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) { _onGround = true; }
    }
}
