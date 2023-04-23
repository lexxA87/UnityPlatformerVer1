using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerCast : MonoBehaviour
{
    private Animator _animator;

    [Header("Params")]
    [Range(0.1f, 10f)]
    [SerializeField] private float _castCalldown = 1;
    [SerializeField] private FireballController _fireballPrefab;
    [Header("Animation")]
    [SerializeField] private string _castTriggerName = "CastTrigger";

    private float _counterCallDown = 0;
    private GameObject _fireballHolder;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _fireballHolder = new GameObject("FireballHolder");
    }

    // Update is called once per frame
    void Update()
    {
        _counterCallDown += Time.deltaTime;

        if (Input.GetAxis("Fire1") != 0 && _counterCallDown >= _castCalldown)
        {
            _animator.SetTrigger(_castTriggerName);
            _counterCallDown = 0;

            var bullet = Instantiate(_fireballPrefab, _fireballHolder.transform);
        }
    }
}
