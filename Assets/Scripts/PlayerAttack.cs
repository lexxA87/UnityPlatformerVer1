using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator _animator;

    [Header("Params")]
    [Range(0.1f, 10f)]
    [SerializeField] private float _attackCalldown = 1;

    [Header("Animation")]
    [SerializeField] private string _attackTriggerName = "AttackTrigger";


    private float _counterCallDown = 0;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void CheckAttack()
    {
        if (Input.GetAxis("Fire2") != 0 && _counterCallDown >= _attackCalldown)
        {
            _animator.SetTrigger(_attackTriggerName);
            _counterCallDown = 0;
        }
    }

    void Update()
    {
        _counterCallDown += Time.deltaTime;

        CheckAttack();
    }
}
