using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public SceneFader sceneFader;
    public AudioClip badEndingSound;
    public AudioClip goodEndingSound;
    
    public PlayerStats stats;
    public Text upText;
    public Text centerText;
    public Text bottomText;
    public Text conclusionText;
    public int totalCount;
    
    public string perfectDesiredEnding;
    public string perfectSecretEnding;
    public string goodEnding;
    public string badEnding;
    public string worstEnding;

    private void Start()
    {
        
        upText.text = "Total deliveries: " + stats.showTotalDeliveries;
        centerText.text = "Correct deliveries: " + stats.showDesiredDeliveries;
        bottomText.text = "Secret deliveries: " + stats.showSecretDeliveries;

        totalCount = stats.showDesiredDeliveries + stats.showSecretDeliveries * 2;

        conclusionText.text = GetFeedbackText();
    }

    public void Retry ()
    {
        // Time.timeScale = 1f;
        sceneFader.FadeTo("Intro scene");
    }

    private string GetFeedbackText()
    {
        if (stats.showDesiredDeliveries == stats.showTotalDeliveries)
        {
            return perfectDesiredEnding;
        }
        
        if (stats.showSecretDeliveries == stats.showTotalDeliveries)
        {
            return perfectSecretEnding;
        }
        
        if (totalCount == 0)
        {
            return worstEnding;
        }

        if (totalCount <= stats.showTotalDeliveries/2)
        {
            return badEnding;
        }

        return goodEnding;
    }
}
