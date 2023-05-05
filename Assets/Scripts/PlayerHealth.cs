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



    public int Health { get { return _currentHealth; } set { _currentHealth = value; } }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _textHealth.text = _currentHealth.ToString();
    }

    private void FixedUpdate()
    {
        CheckDead();
        _textHealth.text = _currentHealth.ToString();
    }

    private void CheckDead()
    {
        if (_currentHealth <= 0)
        {
            _animator.SetBool(_isDeadName, true);
            return;
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

    public void ReloadScene()
    {
        SceneManager.LoadScene(1);
    }
}
