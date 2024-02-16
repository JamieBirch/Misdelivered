using UnityEngine;

public static class SoundManager
{
    public enum Sound
    {
        knockAndScreech,
        correctDelivery,
        secretDelivery,
        wrongDelivery,
        facehuggerDelivery,
        portal
    }
    
    public static void PlaySound(Sound sound)
    {
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.PlayOneShot(getAudioClip(sound));
    }

    private static AudioClip getAudioClip(Sound sound)
    {
        foreach (GameManager.SoundAudioClip soundClip in GameManager.instance.soundClips)
        {
            if (soundClip.sound == sound)
            {
                return soundClip.audioClip;
            }
        }
        Debug.LogError("matching audio not found");
        return null;
    }
}
