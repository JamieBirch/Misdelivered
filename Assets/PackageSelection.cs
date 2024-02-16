using UnityEngine;
using UnityEngine.UI;

public class PackageSelection : MonoBehaviour
{
    public Package package;
    // public Text text;
    public Image img;

    public void GivePackage()
    {
        GameManager.instance.GivePackage(package);
    }

    public void SetSprite()
    {
        img.sprite = package.sprite;
    }
}
