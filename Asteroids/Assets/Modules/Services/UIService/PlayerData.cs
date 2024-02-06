using TMPro;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerNumber;
    [SerializeField] private TextMeshProUGUI playerName;
    [SerializeField] private TextMeshProUGUI playerScore;

    private int _score;

    public int Score => _score;

    public void Initialize(string name, int score)
    {
        _score = score;
        playerName.text = name;
        playerScore.text = score.ToString();
    }

    public void SetOrder(int orderNumber)
    {
        playerNumber.text = orderNumber.ToString();
    }

    public void SetColor(Color color)
    {
        playerNumber.color = color;
        playerName.color = color;
    }
}