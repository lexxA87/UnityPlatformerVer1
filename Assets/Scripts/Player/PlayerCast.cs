using System.Collections.Generic;
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

    [Header("Sound cast")]
    [SerializeField] private AudioClip _castSound;

    private float _counterCallDown = 0;
    private Transform _fireballHolder;
    private Transform _castPointTransform;

    private List<FireballController> _fireballPool;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerMove = GetComponent<PlayerMove>();
        var holder = new GameObject("FireballHolder");
        _fireballHolder = holder.transform;
        _fireballPool = new List<FireballController>();
        _castPointTransform = _castPoint.transform;
    }

    private void CreateFireball()
    {
        FireballController fireball = null;

        _fireballPool.ForEach(item =>
        {
            if (!item.gameObject.activeInHierarchy)
            {
                fireball = item;
                return;
            }
        });

        if (fireball == null)
        {
            fireball = Instantiate(_fireballPrefab, _castPointTransform.position, _castPointTransform.rotation, _fireballHolder);
            _fireballPool.Add(fireball);
        }
        else
        {
            fireball.gameObject.SetActive(true);
            fireball.transform.position = _castPointTransform.position;
        }

        fireball.Init(_playerMove.IsLeftMove);
    }

    private void CheckCastFireball()
    {
        if (Input.GetAxis("Fire1") != 0 && _counterCallDown >= _castCalldown)
        {
            _animator.SetTrigger(_castTriggerName);
            SoundManager.instance.PlaySound(_castSound);
            _counterCallDown = 0;

            CreateFireball();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _counterCallDown += Time.deltaTime;

        CheckCastFireball();
    }
}
