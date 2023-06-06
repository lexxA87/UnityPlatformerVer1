using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    public Player Player;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        Player = new();
        WriteParamsPlayer();
    }

    public void WriteParamsPlayer()
    {
        int _health = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().Health;
        int _coin = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScore>().ScoreCoin;
        float _time = GameObject.Find("Timer").GetComponent<TimerText>().TimerCount;

        Player.Health = _health;
        Player.Coin = _coin;
        Player.Time = _time;
    }
}
