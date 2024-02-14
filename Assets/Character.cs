using UnityEngine;

public class Character : MonoBehaviour
{
    public Sprite sprite;
    public ReferenceCharacters character;

    public Package desiredMatch;
    public Package secretMatch;

    public string desiredMatchReaction;
    public string secretMatchReaction;
    public string defaultReaction;

    public ReactionOptions ReactToPackage(Package package)
    {
        if (package == desiredMatch)
        {
            return ReactionOptions.DesiredMatch;
            // return desiredMatchReaction;
        }

        if (package == secretMatch)
        {
            return ReactionOptions.SecretMatch;
            // return secretMatchReaction;
        }

        if (package.character == ReferenceCharacters.Alien)
        {
            return ReactionOptions.Facehugger;
            // return GameManager.instance.facehuggerDefaultReaction;
        }

        return ReactionOptions.Misdelivery;
        // return defaultReaction;
    }
    
    public string GetReaction(ReactionOptions reactionOption)
    {
        if (reactionOption == ReactionOptions.DesiredMatch)
        {
            return desiredMatchReaction;
        }

        if (reactionOption == ReactionOptions.SecretMatch)
        {
            return secretMatchReaction;
        }

        if (reactionOption == ReactionOptions.Facehugger)
        {
            return GameManager.instance.facehuggerDefaultReaction;
        }

        return defaultReaction;
    }
}
