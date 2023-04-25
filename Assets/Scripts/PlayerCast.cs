using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerCast : MonoBehaviour
{
    private Animator _animator;
    private PlayerMove _playerMove;

    [Header("Params")]
    [Range(0.1f, 10f)]
    [SerializeField] private float _castCalldown = 1;
    [SerializeField] private FireballController _fireballPrefab;
    [SerializeField] private GameObject _castPoint;
    [Header("Animation")]
    [SerializeField] private string _castTriggerName = "CastTrigger";

    private float _counterCallDown = 0;
    private Transform _fireballHolder;
    private Transform _castPointTransform;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerMove = GetComponent<PlayerMove>();
        var holder = new GameObject("FireballHolder");
        _fireballHolder = holder.transform;

        _castPointTransform = _castPoint.transform;
    }

    // Update is called once per frame
    void Update()
    {
        _counterCallDown += Time.deltaTime;

        if (Input.GetAxis("Fire1") != 0 && _counterCallDown >= _castCalldown)
        {
            _animator.SetTrigger(_castTriggerName);
            _counterCallDown = 0;

            var fireball = Instantiate(_fireballPrefab, _castPointTransform.position, _castPointTransform.rotation, _fireballHolder);
            fireball.Init(_playerMove.IsLeftMove);
        }
    }
}
