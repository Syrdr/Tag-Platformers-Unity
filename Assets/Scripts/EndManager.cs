using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI winnerText;
    [SerializeField] private Button playAgain;
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
        playAgain.onClick.AddListener(() => {
            GameData.ResetGameData();
            SceneManager.LoadScene("StartScene");
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
