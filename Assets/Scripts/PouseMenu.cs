using UnityEngine;

public class PouseMenu : MonoBehaviour
{
    [SerializeField] GameObject _pouseMenu;
    [SerializeField] GameObject _settingsMenu;

    private bool _pause;
    private bool _settings;

    private void Start()
    {
        _pause = true;
        Pouse();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!_pause && !_settings)
            {
                Pouse();
            }
            else
            {
                if (_settings)
                {
                    ExitSettingsMenu();
                }
                else
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

    public void SettingsMenu()
    {
        _settingsMenu.SetActive(true);
        _settings = true;
        _pouseMenu.SetActive(false);
    }

    public void ExitSettingsMenu()
    {
        _settingsMenu.SetActive(false);
        _settings = false;
        _pouseMenu.SetActive(true);
    }

    public void Exit()
    {
        Debug.Log("Game is over...");
        Application.Quit();
    }
}
