using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private float timeLeft;
    [SerializeField] private TextMeshProUGUI timerTXT;

    public UnityEvent onTimerEnd;


    private void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            UpdateTimerUI(timeLeft);
        }
        else
        {
            onTimerEnd.Invoke();
        }
    }

    private void UpdateTimerUI(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        timerTXT.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
}
