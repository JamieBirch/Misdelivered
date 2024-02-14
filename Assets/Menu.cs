using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject ui;
    public SceneFader sceneFader;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Toggle();
        }
    }
    
    public void Toggle()
    {
        ui.SetActive(!ui.activeSelf);
    }
    
    public void Quit ()
    {
        Debug.Log("Quitting...");
        Application.Quit();
    }
    
    public void ShowMenu()
    {
        Toggle();
        // sceneFader.FadeTo(MainMenuSceneName);
    }
}
