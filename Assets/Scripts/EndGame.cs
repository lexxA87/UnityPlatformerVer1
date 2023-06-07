using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    private Behaviour[] _playerComponents;
    private Animator _playerAnimator;
    [SerializeField] private Health _boss;
    [SerializeField] private TextMeshProUGUI[] _texts;
    private bool _isBossDead;
    private float _timer;

    [Header("Reload")]
    [SerializeField] private float _timeToReload = 3;
    [SerializeField] private int _reloadToScene;

    private void Start()
    {
        _playerAnimator = _player.GetComponent<Animator>();
        _playerComponents = new Behaviour[3];
        _playerComponents[0] = _player.GetComponent<PlayerMove>();
        _playerComponents[1] = _player.GetComponent<PlayerAttack>();
        _playerComponents[2] = _player.GetComponent<PlayerCast>();
    }

    private void Update()
    {
        _isBossDead = _boss.IsDead;
        if (_isBossDead)
        {
            FinishGame();
            _isBossDead = false;
        }

        if (_texts[0].isActiveAndEnabled)
        {
            _timer += Time.deltaTime;
            if (_timer > _timeToReload)
            {
                if (Input.anyKey)
                {
                    PlayerManager.Instance.DestroyPlayerManager();
                    SceneManager.LoadScene(_reloadToScene);
                }
            }
        }
    }

    public void FinishGame()
    {
        _playerAnimator.SetBool("Win", true);

        foreach (Behaviour component in _playerComponents)
            component.enabled = false;

        foreach (TextMeshProUGUI text in _texts)
            text.gameObject.SetActive(true);
    }
}
