using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneFader : MonoBehaviour
{
    public Image image;
    public AnimationCurve curve;
    

    // Start is called before the first frame update
    void Start()
    {
        // StartCoroutine(FadeIn());
    }

    public void FadeTo(string scene)
    {
        StartCoroutine(FadeOut(scene));
    }
    
    public void FadeToWithDelay()
    {
        
        Time.timeScale = 1f;
        Invoke(nameof(FadeToGameScreen), 3f);
    }
    
    private void FadeToGameScreen()
    {
        StartCoroutine(FadeOut("GameScene"));
    }

    IEnumerator FadeIn()
    {
        float t = 1f;
        
        while (t > 0f)
        {
            t -= Time.deltaTime;
            float a = curve.Evaluate(t);
            image.color = new Color (0f, 0f, 0f, a);
            yield return 0;
        }
    }
    
    public void FastFadeIn ()
    {
        StartCoroutine(fastFadeIn());
    }
    
    IEnumerator fastFadeIn()
    {
        float t = 1f;
        
        while (t > 0f)
        {
            t -= Time.deltaTime*3;
            float a = curve.Evaluate(t);
            image.color = new Color (0f, 0f, 0f, a);
            yield return 0;
        }
    }

    IEnumerator FadeOut(string scene)
    {
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime;
            float a = curve.Evaluate(t);
            image.color = new Color(0f, 0f, 0f, a);
            yield return 0;
        }

        SceneManager.LoadScene(scene);
    }
}
