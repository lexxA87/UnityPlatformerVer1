using TMPro;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    [Header("Params")]
    [SerializeField]
    private int _scoreCoin = 0;
    [SerializeField] private TextMeshProUGUI _textScore;

    private void Awake()
    {
        _textScore.text = _scoreCoin.ToString();
    }
    public void AddCoin(int count)
    {
        _scoreCoin += count;

        _textScore.text = _scoreCoin.ToString();
    }
}
