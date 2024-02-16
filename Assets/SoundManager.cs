using System.Collections.Generic;
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
        portal,
        sigh,
        soundTrack
    }

    private static Dictionary<Sound, float> soundTimerDictionary;

    public static void Initialize()
    {
        soundTimerDictionary = new Dictionary<Sound, float>();
        soundTimerDictionary[Sound.sigh] = 0f;
    }

    public static void PlaySound(Sound sound)
    {
        if (CanPlaySound(sound))
        {
            GameObject soundGameObject = new GameObject("Sound");
            AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
            audioSource.PlayOneShot(GetAudioClip(sound));
        }
    }

    private static AudioClip GetAudioClip(Sound sound)
    {
        if (sound == Sound.sigh)
        {
            return GetRandomElement(GameManager.instance.sighs);
        }
        
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

    private static bool CanPlaySound(Sound sound)
    {
        switch (sound)
        {
            case Sound.sigh:
                if (soundTimerDictionary.ContainsKey(sound))
                {
                    float lastTimePlayed = soundTimerDictionary[sound];
                    float sighTimerMax = 0.5f;
                    if (lastTimePlayed + sighTimerMax < Time.time)
                    {
                        soundTimerDictionary[sound] = Time.time;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                break;
            default:
                return true;
        }
        return true;
    }
    
    public static void PlaySoundTrack()
    {
        // if (CanPlaySound(sound))
        // {
            GameObject soundGameObject = new GameObject("SoundTrack");
            AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
            audioSource.loop = true;
            audioSource.volume = 0.2f;
            audioSource.PlayOneShot(GetAudioClip(Sound.soundTrack));
        // }
    }
    
    static T GetRandomElement<T>(T[] array)
    {
        System.Random rand = new System.Random();
        int randomIndex = rand.Next(0, array.Length);
        return array[randomIndex];
    }
}
