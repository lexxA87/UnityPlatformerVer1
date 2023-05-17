using System.Timers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    [SerializeField] private Animator _player;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private float _timeToEnd = 5000;

    [Header("Reload")]
    [SerializeField] private int _reloadToScene;

    public void FinishGame()
    {
        _player.SetBool("Win", true);
        _text.gameObject.SetActive(true);

        Debug.Log("finish...");

        SceneManager.LoadScene(_reloadToScene);


        //var timer = new Timer(_timeToEnd);

        //timer.Elapsed += OnEventExecution;

        //timer.AutoReset = false;

        //timer.Start();
    }

    private void OnEventExecution(System.Object? sender, ElapsedEventArgs eventArgs)
    {
        SceneManager.LoadScene(_reloadToScene);
    }
}
