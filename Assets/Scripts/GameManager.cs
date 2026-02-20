using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timer;
    [SerializeField] private TextMeshProUGUI FirstPlayertime;
    public float timeElapsed = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer.text = "00:00";
        GameData.OnItPlayerChanged += SwitchItPlayer;
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        int minutes = Mathf.FloorToInt(timeElapsed / 60f);
        int seconds = Mathf.FloorToInt(timeElapsed % 60f);
        timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void SwitchItPlayer()
    {
        //itPlayer = ItPlayer.Player2;
        //GameData.firstPlayerTime = timeElapsed;
        FirstPlayertime.text = timer.text;
        //timeElapsed = 0f;
    }
}
