using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevel : MonoBehaviour
{
    [SerializeField] GameObject _levelBoss;
    [SerializeField] int _loadScene;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    private bool _isLevelBossDead()
    {
        if (_levelBoss.GetComponent<Health>().IsDead)
            return true;
        return false;
    }

    private void Update()
    {
        if (_isLevelBossDead())
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
            _animator.SetTrigger("active");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene(_loadScene);
        }
    }
}
