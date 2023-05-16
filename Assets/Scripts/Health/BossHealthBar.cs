using System;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    [SerializeField] private Image _healthBar;
    [SerializeField] private Health _healthBoss;

    private float _startHealth;
    private float _currentHealth;

    private void Start()
    {
        _startHealth = _healthBoss.CurrentHealth;
        _currentHealth = _startHealth;
    }

    private void Update()
    {
        _currentHealth = _healthBoss.CurrentHealth;

        _healthBar.fillAmount = MathF.Round(_currentHealth / _startHealth, 2);
    }
}
