using TMPro;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    [Header("Params")]
    [SerializeField]
    private int _scoreCoin = 0;
    [SerializeField] private TextMeshProUGUI _textScore;

    public int ScoreCoin { get { return _scoreCoin; } }

    private void Start()
    {
        _scoreCoin = PlayerManager.Instance.Player.Coin;
        _textScore.text = _scoreCoin.ToString();
    }


    public void AddCoin(int count)
    {
        _scoreCoin += count;

        _textScore.text = _scoreCoin.ToString();
    }
}
