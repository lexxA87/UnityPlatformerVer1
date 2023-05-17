using TMPro;
using UnityEngine;

public class TimerText : MonoBehaviour
{
    [Header("Params")]
    [SerializeField] private float _timer = 0;
    [SerializeField] private TextMeshProUGUI _textTimer;
    [SerializeField] private bool _isTimerOn;


    void Start()
    {
        _isTimerOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isTimerOn)
        {
            _timer += Time.deltaTime;
            UpdateTimer(_timer);
        }
    }

    private void UpdateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        _textTimer.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
}
