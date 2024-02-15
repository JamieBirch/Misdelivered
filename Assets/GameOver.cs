using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public SceneFader sceneFader;
    public PlayerStats stats;
    public Text upText;
    public Text centerText;
    public Text bottomText;
    public Text conclusionText;

    private void Start()
    {
        upText.text = "Total deliveries: " + stats.showTotalDeliveries;
        centerText.text = "Correct deliveries: " + stats.showDesiredDeliveries;
        bottomText.text = "Secret deliveries: " + stats.showSecretDeliveries;
        
        //TODO
        // conclusionText.text
    }

    public void Retry ()
    {
        // Time.timeScale = 1f;
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }
}
