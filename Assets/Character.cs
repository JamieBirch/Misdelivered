using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public Image image;
    public string Name;

    public Package desiredMatch;
    public Package secretMatch;

    public string desiredMatchReaction;
    public string secretMatchReaction;
    public string defaultReaction;

    public string ReactToPackage(Package package)
    {
        if (package == desiredMatch)
        {
            return desiredMatchReaction;
        }

        if (package == secretMatch)
        {
            return secretMatchReaction;
        }

        if (package.Name == "Facehugger")
        {
            return GameManager.instance.facehuggerDefaultReaction;
        }

        return defaultReaction;
    }
}
