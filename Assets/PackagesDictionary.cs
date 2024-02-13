using System.Collections.Generic;
using UnityEngine;

public class PackagesDictionary : MonoBehaviour
{
    public Dictionary<ReferenceCharacters, Package> packageDictionary;

    public Package alienPackage;
    public Package terminatorPackage;
    public Package pikachuPackage;
    public Package vazovskiPackage;
    public Package witcherPackage;
    public Package holmesPackage;
    public Package potterPackage;
    public Package gollumPackage;
    public Package girlPackage;

    private void Awake()
    {
        packageDictionary = new Dictionary<ReferenceCharacters, Package>()
        {
            {ReferenceCharacters.Alien, alienPackage},
            {ReferenceCharacters.Terminator, terminatorPackage},
            {ReferenceCharacters.Pikachu, pikachuPackage},
            {ReferenceCharacters.Vazovski, vazovskiPackage},
            {ReferenceCharacters.Witcher, witcherPackage},
            {ReferenceCharacters.Holmes, holmesPackage},
            {ReferenceCharacters.Potter, potterPackage},
            {ReferenceCharacters.Gollum, gollumPackage},
            {ReferenceCharacters.Girl, girlPackage}
        };
    }

    public Package GetPackage(ReferenceCharacters character)
    {
        return packageDictionary[character];
    }
}
