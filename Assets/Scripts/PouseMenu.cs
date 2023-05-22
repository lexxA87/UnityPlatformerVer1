using UnityEngine;

public class PouseMenu : MonoBehaviour
{
    [SerializeField] GameObject _pouseMenu;

    private bool _pause;

    private void Start()
    {
        _pause = true;
        Pouse();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!_pause)
            {
                Pouse();
            }
            else
            {
                Play();
            }
        }
    }

    private void Pouse()
    {
        _pouseMenu.SetActive(true);
        _pause = true;
        Time.timeScale = 0f;
    }

    public void Play()
    {
        _pouseMenu.SetActive(false);
        _pause = false;
        Time.timeScale = 1.0f;
    }

    public void Exit()
    {
        Debug.Log("Game is over...");
        Application.Quit();
    }
}
