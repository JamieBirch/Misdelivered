using UnityEngine;

public class Character : MonoBehaviour
{
    // public Sprite sprite;
    public ReferenceCharacters character;

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

        if (package.character == ReferenceCharacters.Alien)
        {
            return GameManager.instance.facehuggerDefaultReaction;
        }

        return defaultReaction;
    }
}
