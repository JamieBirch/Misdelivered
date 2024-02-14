using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Scenario scenario;
    public DoorPanel DoorPanel;
    public DoorsDictionary DoorsDictionary;
    public CharactersDictionary CharactersDictionary;
    public PackagesDictionary PackagesDictionary;
    public GameObject CharacterStanding;

    public GameObject CharacterReactionPanel;
    public Text CharacterReaction;
    public GameObject NextRoundButton;
    
    public string facehuggerDefaultReaction;
    
    // public Text log;
    public Text characterName;
    public int rounds;
    public int currentRoundIndex = 0;
    public Round currentRound;

    public static bool GameIsOver;

    //TODO define next round condition
    // public bool showNextRound = false;

    private PackageSelection[] _packageSelections;
    
    //TODO assign packages to buttons - one set per round
    public PackageSelection selection1;
    public PackageSelection selection2;
    public PackageSelection selection3;
    public PackageSelection selection4;
    public PackageSelection selection5;
    
    private void Awake()
    {
        instance = this;
        // characterName.gameObject.SetActive(false);
        _packageSelections = new[] { selection1, selection2, selection3, selection4, selection5 };
    }
    
    // Start is called before the first frame update
    private void Start()
    {
        // CharacterStanding.SetActive(false);
        scenario = GenerateScenario();
        GameIsOver = false;
        // currentRound = scenario.GetRound(0);
        ShowCurrentRound();
    }
    
    private void Update()
    {
        if (GameIsOver)
        {
            return;
        }
        
        /*if (showNextRound)
        {
            // showNextRound = false;
            if (currentRoundIndex < rounds)
            {
                NextRound();
            }
            else
            {
                EndGame();
            }
        }*/
    }

    public void GivePackage(Package package)
    {
        //TODO disable button press if on previous round 
        
        //open Door
        if (CharacterStanding)
        {
            Debug.Log("Door opens!");
            CharacterStanding.SetActive(true);
            DoorPanel.ChangeCharacter(currentRound.GetCharacter().sprite);
            // characterName.gameObject.SetActive(true);
        }
        CharacterReactionPanel.SetActive(true);
        NextRoundButton.SetActive(true);

        DoorPanel.ChangeDoor(currentRound.GetDoor().imageOpen);

        Debug.Log("Giving " + package.name + " to " + currentRound.GetCharacter().name);
        //reaction
        ReactionOptions reaction = currentRound.GetCharacter().ReactToPackage(package);
        CharacterReactionPanel.GetComponent<Image>().color = GetReactionColor(reaction);
        string reactionText = currentRound.GetCharacter().GetReaction(reaction);
        CharacterReaction.text = reactionText;
        

    }

    private Color GetReactionColor(ReactionOptions reaction)
    {
        if (reaction == ReactionOptions.DesiredMatch)
        {
            return Color.green;
        }

        if (reaction == ReactionOptions.SecretMatch)
        {
            return Color.yellow;
        }

        if (reaction == ReactionOptions.Facehugger)
        {
            return Color.gray;
        }

        return Color.red;
    }

    public void NextRound()
    {
        currentRoundIndex++;
        //TODO fade
        ShowCurrentRound();
    }

    private void ShowCurrentRound()
    {
        Debug.Log("Round: " + currentRoundIndex);

        if (currentRoundIndex >= rounds)
        {
            EndGame();
            return;
        }

        //Hide objects of prev. round
        if (CharacterStanding)
        {
            CharacterStanding.SetActive(false);
        }
        CharacterReactionPanel.SetActive(false);
        NextRoundButton.SetActive(false);

        currentRound = scenario.GetRound(currentRoundIndex);
        //show door
        DoorPanel.ChangeDoor(currentRound.GetDoor().imageClosed);
        //TODO remove this line when testing finished
        characterName.text = currentRound.GetCharacter().character.ToString();

        AssignPackagesToButtons();
    }

    private void AssignPackagesToButtons()
    {
        Random random = new Random();
        HashSet<ReferenceCharacters> charactersSet = new HashSet<ReferenceCharacters>();
        Array values = Enum.GetValues(typeof(ReferenceCharacters));
        charactersSet.Add(currentRound.GetCharacter().desiredMatch.character);
        charactersSet.Add(currentRound.GetCharacter().secretMatch.character);
        while (charactersSet.Count != 5)
        {
            int index = random.Next(values.Length);
            charactersSet.Add((ReferenceCharacters)values.GetValue(index));
        }

        IList<ReferenceCharacters> referenceCharactersList = Shuffle(charactersSet.ToList());
        int i = 0;
        foreach (ReferenceCharacters characterInList in referenceCharactersList)
        {
            AssignPackageToButton(characterInList, i);
            i++;
        }
    }

    private void AssignPackageToButton(ReferenceCharacters characterInList, int i)
    {
        Package package = PackagesDictionary.GetPackage(characterInList);
        _packageSelections[i].package = package;
        //TODO 
        // _packageSelections[i].image.sprite = package.sprite;
        _packageSelections[i].text.text = package.name;
    }

    private Scenario GenerateScenario()
    {
        scenario = new Scenario(rounds);
        Array values = Enum.GetValues(typeof(ReferenceCharacters));
        
        var rndIndexes = GenerateRandomIndexes(values);

        CreateRounds(rndIndexes, values, scenario);

        return scenario;
    }

    private void CreateRounds(HashSet<int> rndIndexes, Array values, Scenario scenario)
    {
        int i = 0;
        foreach (int num in rndIndexes)
        {
            ReferenceCharacters randomCharacter = (ReferenceCharacters)values.GetValue(num);
            scenario.CreateRound(i, DoorsDictionary.GetDoor(randomCharacter),
                CharactersDictionary.GetCharacter(randomCharacter));
            i++;
        }
    }

    private HashSet<int> GenerateRandomIndexes(Array values)
    {
        Random random = new Random();
        HashSet<int> rndIndexes = new HashSet<int>();
        int iter = 0;
        while (rndIndexes.Count != rounds)
        {
            int index = random.Next(values.Length);
            rndIndexes.Add(index);
            iter++;
        }

        return rndIndexes;
    }

    void EndGame()
    {
        GameIsOver = true;
        CharacterReaction.text = "Game Over";
    }
    
    public static IList<T> Shuffle<T>(IList<T> list)  
    {  
        Random rng = new Random();
        
        int n = list.Count;  
        while (n > 1) {  
            n--;  
            int k = rng.Next(n + 1);  
            (list[k], list[n]) = (list[n], list[k]);
        }

        return list;
    }
}
