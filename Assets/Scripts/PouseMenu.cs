using UnityEngine;

public class PouseMenu : MonoBehaviour
{
    [SerializeField] GameObject _pouseMenu;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pouse();
        }
    }

    private void Pouse()
    {
        _pouseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Play()
    {
        _pouseMenu.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void Exit()
    {
        Debug.Log("Game is over...");
        Application.Quit();
    }
}
