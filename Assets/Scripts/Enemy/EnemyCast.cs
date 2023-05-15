using System;
using UnityEngine;

public class EnemyCast : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _castPoint;
    [Range(0.1f, 10f)]
    [SerializeField] private float _castCalldown = 1;

    private float _timer;

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > _castCalldown)
        {
            _timer = 0;
            Shoot();
        }
    }

    private void Shoot()
    {
        Instantiate(_bulletPrefab, _castPoint.position, Quaternion.identity);
    }
}
