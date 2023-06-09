using System;
using UnityEngine;

public class EnemyCast : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _castPoint;
    [Range(0.1f, 10f)]
    [SerializeField] private float _castCalldown = 1;
    [Range(0, 500)]
    [SerializeField] private int _damage = 50;
    [Range(0.1f, 10f)]
    [SerializeField] private float _force;
    [Range(0.1f, 20f)]
    [SerializeField] private float _distanceAttack = 7;

    private float _timer;
    private GameObject _player;

    public int Damage { get { return _damage; } }
    public float Force { get { return _force; } }

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        float distance = Vector2.Distance(transform.position, _player.transform.position);
        // Debug.Log(distance);

        if (distance < _distanceAttack)
        {
            _timer += Time.deltaTime;

            if (_timer > _castCalldown)
            {
                _timer = 0;
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        Instantiate(_bulletPrefab, _castPoint.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int damage = 0;

        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHealth>().DamageHealth(_damage);
        }

        if (collision.CompareTag("PlayerDamage"))
        {

            if (collision.gameObject.name == "Sword")
            {
                damage = collision.GetComponentInParent<PlayerAttack>().DamageSword;
            }
            if (collision.gameObject.name == "Fireball(Clone)")
            {
                damage = collision.GetComponent<FireballController>().Damage;
            }

            transform.GetComponent<Health>().TakeDamage(damage);
        }
    }


    public void DeactivateDiamonds()
    {
        var diamonds = GameObject.FindGameObjectsWithTag("Diamond");
        foreach (var d in diamonds)
        {
            d.SetActive(false);
        }
    }
}
