using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerFinishLevel : MonoBehaviour
{
    [SerializeField] private int _numberNextScene;
    [SerializeField] private Behaviour[] _componentsToTurnOff;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            foreach (var component in _componentsToTurnOff)
            {
                component.enabled = false;
            }

            _animator.SetTrigger("finish");
        }
    }

    public void ReloadNextScene()
    {
        SceneManager.LoadScene(_numberNextScene);
    }
}
