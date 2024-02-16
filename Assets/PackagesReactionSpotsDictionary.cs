using System.Collections.Generic;
using UnityEngine;

public class PackagesReactionSpotsDictionary : MonoBehaviour
{
    public Dictionary<ReactionOptions, Sprite> reactionToSpot;

    public Sprite spotCorrect;
    public Sprite spotSecret;
    public Sprite spotWrong;
    public Sprite spotFacehugger;

    private void Awake()
    {
        reactionToSpot = new Dictionary<ReactionOptions, Sprite>()
        {
            {ReactionOptions.DesiredMatch, spotCorrect},
            {ReactionOptions.SecretMatch, spotSecret},
            {ReactionOptions.Misdelivery, spotWrong},
            {ReactionOptions.Facehugger, spotFacehugger}
        };
    }

    public Sprite GetPackageSpot(ReactionOptions reaction)
    {
        return reactionToSpot[reaction];
    }
}
