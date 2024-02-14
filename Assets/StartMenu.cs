using UnityEngine;

public class StartMenu : MonoBehaviour
{
    public string sceneToLoad = "GameScene";
    public SceneFader sceneFader;

    public void Play ()
    {
        sceneFader.FadeTo(sceneToLoad);
        //SceneManager.LoadScene(levelToLoad);
    }

    public void Quit ()
    {
        Debug.Log("Quitting...");
        Application.Quit();
    }
}
