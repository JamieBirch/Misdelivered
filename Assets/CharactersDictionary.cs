using System.Collections.Generic;
using UnityEngine;

public class CharactersDictionary : MonoBehaviour
{
    public Dictionary<ReferenceCharacters, Character> characterDictionary;

    public Character alien;
    public Character terminator;
    public Character pikachu;
    public Character vazovski;
    public Character witcher;
    public Character holmes;
    public Character potter;
    public Character gollum;
    public Character girl;

    private void Awake()
    {
        characterDictionary = new Dictionary<ReferenceCharacters, Character>()
        {
            {ReferenceCharacters.Alien, alien},
            {ReferenceCharacters.Terminator, terminator},
            {ReferenceCharacters.Pikachu, pikachu},
            {ReferenceCharacters.Vazovski, vazovski},
            {ReferenceCharacters.Witcher, witcher},
            {ReferenceCharacters.Holmes, holmes},
            {ReferenceCharacters.Potter, potter},
            {ReferenceCharacters.Gollum, gollum},
            {ReferenceCharacters.Girl, girl}
        };
    }

    public Character GetCharacter(ReferenceCharacters character)
    {
        return characterDictionary[character];
    }

}
