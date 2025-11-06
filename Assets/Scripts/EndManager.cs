using TMPro;
using UnityEngine;

public class EndManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI winnerText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(GameData.winner == ItPlayer.Player1)
        {
            winnerText.text = "Player 1 Wins!";
        }
        else if(GameData.winner == ItPlayer.Player2)
        {
            winnerText.text = "Player 2 Wins!";
        }
        else
        {
            winnerText.text = "It's a Tie!";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
