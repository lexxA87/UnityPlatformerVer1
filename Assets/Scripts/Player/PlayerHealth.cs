using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    private Animator _animator;

    [Header("Params")]
    [SerializeField]
    [Range(50, 500)]
    private int _currentHealth = 200;
    [SerializeField]
    [Range(50, 500)]
    private int _maxHealth = 200;
    [SerializeField] private TextMeshProUGUI _textHealth;

    [Header("Animation")]
    [SerializeField] private string _isDeadName = "IsDead";
    [SerializeField] private string _hurtName = "HurtTrigger";

    [Header("iFrames")]
    [SerializeField] private float _iFramesDuration;
    [SerializeField] private int _numberOfFlashes;
    private SpriteRenderer _spriteRenderer;
    private bool invulnerable;

    [Header("Sounds")]
    [SerializeField] private AudioClip _hurtSound;
    [SerializeField] private AudioClip _deathSound;

    [Header("Reload")]
    [SerializeField] private int _reloadToScene;

    public int Health { get { return _currentHealth; } }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _textHealth.text = _currentHealth.ToString();
    }

    private void FixedUpdate()
    {
        if (_currentHealth >= 0)
            _textHealth.text = _currentHealth.ToString();
        else _textHealth.text = "0";
    }

    private void CheckDead()
    {
        if (_currentHealth <= 0)
        {
            _animator.SetBool(_isDeadName, true);
            SoundManager.instance.PlaySound(_deathSound);
            GetComponent<PlayerMove>().enabled = false;
            return;
        }
        else
        {
            _animator.SetTrigger(_hurtName);
            SoundManager.instance.PlaySound(_hurtSound);
            StartCoroutine(Invunerability());
        }
    }

    public void AddHealth(int count)
    {
        if ((count + _currentHealth) > _maxHealth)
        {
            _currentHealth = _maxHealth;
        }
        else
        {
            _currentHealth += count;
        }
    }

    public void DamageHealth(int _damage)
    {
        if (invulnerable) return;
        _currentHealth -= _damage;
        CheckDead();
    }

    private IEnumerator Invunerability()
    {
        invulnerable = true;
        Physics2D.IgnoreLayerCollision(7, 8, true);
        for (int i = 0; i < _numberOfFlashes; i++)
        {
            _spriteRenderer.material.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(_iFramesDuration / (_numberOfFlashes * 2));
            _spriteRenderer.material.color = Color.white;
            yield return new WaitForSeconds(_iFramesDuration / (_numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(7, 8, false);
        invulnerable = false;
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(_reloadToScene);
    }
}
