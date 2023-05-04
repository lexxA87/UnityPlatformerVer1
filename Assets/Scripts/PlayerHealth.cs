using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    private Animator _animator;

    [Header("Params")]
    [SerializeField]
    private int _health = 200;
    [SerializeField] private TextMeshProUGUI _textHealth;
    [Header("Animation")]
    [SerializeField] private string _isDeadName = "IsDead";



    public int Health { get { return _health; } set { _health = value; } }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _textHealth.text = _health.ToString();
    }

    private void FixedUpdate()
    {
        CheckDead();
        _textHealth.text = _health.ToString();
    }

    private void CheckDead()
    {
        if (_health <= 0)
        {
            _animator.SetBool(_isDeadName, true);
            return;
        }
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(1);
    }
}
