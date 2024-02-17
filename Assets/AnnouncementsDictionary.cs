using System.Collections.Generic;
using UnityEngine;

public class AnnouncementsDictionary : MonoBehaviour
{
    public Sprite[] announcementsSprites;

    public Sprite GetRandomSprite()
    {
        return GetRandomElement(announcementsSprites);
    }
    
    static T GetRandomElement<T>(T[] array)
    {
        System.Random rand = new System.Random();
        int randomIndex = rand.Next(0, array.Length);
        return array[randomIndex];
    }
}
