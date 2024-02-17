using UnityEngine;

public class IntroSceneManager : MonoBehaviour
{
    public AudioSource AudioSource;
    
    void Start()
    {
        Time.timeScale = 0f;
    }

    public void PlaySound()
    {
        // AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        AudioSource.PlayOneShot(AudioSource.clip);
    }

}
