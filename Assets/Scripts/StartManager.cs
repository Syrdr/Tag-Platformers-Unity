using UnityEngine;
using UnityEngine.UI;

public class StartManager : MonoBehaviour
{
    [SerializeField] private Button pcButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pcButton.onClick.AddListener(() =>
        {
            GameData.selectedPlatform = Platforms.PC;
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
