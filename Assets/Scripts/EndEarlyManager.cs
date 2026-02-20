using UnityEngine;
using UnityEngine.UI;

public class EndEarlyManager : MonoBehaviour
{
    private Toggle toggle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        toggle = GetComponent<Toggle>();
    }

    // Update is called once per frame
    void Update()
    {
        if (toggle.isOn)
        {
            GameData.endIfPLayer2Slow = true;
        }
        else
        {
            GameData.endIfPLayer2Slow = false;
        }
    }
}
