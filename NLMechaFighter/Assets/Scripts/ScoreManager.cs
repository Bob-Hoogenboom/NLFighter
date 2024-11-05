using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private List<IFighter> _fightersInGame = new List<IFighter>();

    [SerializeField] private GameObject canvasScoreSection;
    [SerializeField] private GameObject scorePlaquePrefab;

    private Dictionary<IFighter, TextMeshProUGUI> _linkedScoreText = new Dictionary<IFighter, TextMeshProUGUI>();

    private void Awake()
    {
        _fightersInGame = FindObjectsOfType<MonoBehaviour>().OfType<IFighter>().ToList();
    }

    private void Start()
    {
        foreach (IFighter fighter in _fightersInGame)
        {
            GameObject TextObject = Instantiate(scorePlaquePrefab, canvasScoreSection.transform);
            TextMeshProUGUI scoreText = TextObject.GetComponentInChildren<TextMeshProUGUI>();
            _linkedScoreText.Add(fighter, scoreText);
        }
    }

    private void Update()
    {
        UpdateScore();
    }

    //#Fix Only update score when it changes (using events)
    private void UpdateScore()
    {
        foreach (KeyValuePair<IFighter, TextMeshProUGUI> item in _linkedScoreText)
        {
            _linkedScoreText[item.Key].text = item.Key.ToString().Split('(')[0] + "\nScore: " + item.Key.score;
        }
    }
}
