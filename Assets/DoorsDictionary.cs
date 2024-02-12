using System.Collections.Generic;
using UnityEngine;

public class DoorsDictionary : MonoBehaviour
{
    public Dictionary<ReferenceCharacters, Door> doorDictionary;

    public Door alienDoor;
    public Door terminatorDoor;
    public Door pikachuDoor;
    public Door vazovskiDoor;
    public Door witcherDoor;
    public Door holmesDoor;
    public Door potterDoor;
    public Door gollumDoor;
    public Door girlDoor;

    private void Awake()
    {
        doorDictionary = new Dictionary<ReferenceCharacters, Door>()
        {
            {ReferenceCharacters.Alien, alienDoor},
            {ReferenceCharacters.Terminator, terminatorDoor},
            {ReferenceCharacters.Pikachu, pikachuDoor},
            {ReferenceCharacters.Vazovski, vazovskiDoor},
            {ReferenceCharacters.Witcher, witcherDoor},
            {ReferenceCharacters.Holmes, holmesDoor},
            {ReferenceCharacters.Potter, potterDoor},
            {ReferenceCharacters.Gollum, gollumDoor},
            {ReferenceCharacters.Girl, girlDoor}
        };
    }

    public Door GetDoor(ReferenceCharacters character)
    {
        return doorDictionary[character];
    }
}
