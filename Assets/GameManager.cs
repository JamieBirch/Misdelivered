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
    
    public string facehuggerDefaultReaction;
    
    public Text log;
    public Text characterName;
    public int rounds;
    public int currentRoundIndex = 0;
    public Round currentRound;

    public static bool GameIsOver;

    //TODO define next round condition
    public bool showNextRound = false;

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
        scenario = GenerateScenario();
        GameIsOver = false;
        // currentRound = scenario.GetRound(0);
        showNextRound = true;
    }
    
    private void Update()
    {
        if (GameIsOver)
        {
            return;
        }
        
        if (showNextRound)
        {
            showNextRound = false;
            if (currentRoundIndex < rounds)
            {
                NextRound();
            }
            else
            {
                EndGame();
            }
        }
    }

    public void GivePackage(Package package)
    {
        //open Door
        // characterName.gameObject.SetActive(true);

        Thread.Sleep(3000);
        DoorPanel.ChangeSprite(currentRound.GetDoor().imageOpen);

        // Waiter(4);
        //TODO
        Debug.Log("Giving " + package.name + " to " + currentRound.GetCharacter().name);
        string packageReaction = currentRound.GetCharacter().ReactToPackage(package);
        log.text = log.text + "\n > " + packageReaction;

        //Next round!
        currentRoundIndex++;
        showNextRound = true;
        // characterName.gameObject.SetActive(false);
    }

    /*void Waiter(float waitTime)
    {
        //Wait for 4 seconds
        float counter = 0;

        while (counter < waitTime)
        {
            //Increment Timer until counter >= waitTime
            counter += Time.deltaTime;
            Debug.Log("We have waited for: " + counter + " seconds");
            
        }
    }*/

    private void NextRound()
    {
        //TODO fade
        
        currentRound = scenario.GetRound(currentRoundIndex);
        
        //TODO show next round
        //show door
        DoorPanel.ChangeSprite(currentRound.GetDoor().imageClosed);
        characterName.text = currentRound.GetCharacter().character.ToString();
        
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
            Package package = PackagesDictionary.GetPackage(characterInList);
            _packageSelections[i].package = package;
            _packageSelections[i].text.text = package.name;
            
            i++;
        }
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
        log.text = log.text + "\n > " + "Game Over";
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
