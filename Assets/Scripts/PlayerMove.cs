using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class PlayerMove : MonoBehaviour
{
    [Header("Params")]
    [Range(1f, 50f)]
    [SerializeField]
    private float _speed = 5;

    [Range(0.2f, 50f)]
    [SerializeField]
    private float _jumpPower = 5;

    [Header("Animation")]
    [SerializeField] private string _jumpTriggerName = "JumpTrigger";
    //[SerializeField] private string _attackTriggerName = "AttackTrigger";
    [SerializeField] private string _isWalkName = "IsWalk";
    //[SerializeField] private bool _isMoveBool = false;

    private bool _onGround = false;
    private bool _isLeftMove = false;
    private bool _onStair = false;
    public bool OnGround
    {
        get => _onGround;
    }

    public bool IsLeftMove
    {
        get => _isLeftMove;
    }

    private Rigidbody2D _body;
    private Animator _animator;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void CheckJump()
    {
        if (Input.GetAxis("Jump") != 0 && _onGround)
        {
            _body.velocity = new Vector2(_body.velocity.x, _jumpPower);
            _onGround = false;
            _animator.SetTrigger(_jumpTriggerName);
        }
    }


    private void CheckFlip(float horizontalInput)
    {
        var isLeftFlip = horizontalInput < -0.01f && !_isLeftMove;
        var isRightFlip = horizontalInput > 0.01f && _isLeftMove;

        if (isLeftFlip || isRightFlip)
        {
            _isLeftMove = !_isLeftMove;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }

    private void CheckMove()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");

        if (horizontalInput == 0 && verticalInput == 0)
        {
            _animator.SetBool(_isWalkName, false);
            return;
        }

        if (_onStair)
        {
            _body.velocity = new Vector2(_body.velocity.x, verticalInput * _speed);
            _animator.SetBool(_isWalkName, true);
        }
        else
        {
            _body.velocity = new Vector2(horizontalInput * _speed, _body.velocity.y);
            _animator.SetBool(_isWalkName, true);
        }

        CheckFlip(horizontalInput);

    }
    // Update is called once per frame
    void Update()
    {
        CheckMove();

        CheckJump();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) { _onGround = true; }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Stair")) { _onStair = true; }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Stair")) { _onStair = false; }
    }


}
