using UnityEngine;
using UnityEngine.UI;

public class PackageSelection : MonoBehaviour
{
    public Package package;
    public Text text;

    public void GivePackage()
    {
        GameManager.instance.GivePackage(package);
    }
}
