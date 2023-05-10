using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator _animator;

    [Header("Params")]
    [Range(0.1f, 10f)]
    [SerializeField] private float _attackCalldown = 1;
    [Range(10, 200)]
    [SerializeField] private int _damageSword = 50;

    [Header("Animation")]
    [SerializeField] private string _attackTriggerName = "AttackTrigger";

    [Header("Sound attack")]
    [SerializeField] private AudioClip _attackSound;

    public int DamageSword { get { return _damageSword; } }
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
            SoundManager.instance.PlaySound(_attackSound);
            _counterCallDown = 0;
        }
    }

    void FixedUpdate()
    {
        _counterCallDown += Time.deltaTime;

        CheckAttack();
    }
}
