using System;
using System.Collections.Generic;
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
    
    public string facehuggerDefaultReaction;
    
    public Text log;
    public Text characterName;
    public int rounds;
    public int currentRoundIndex = 0;
    public Round currentRound;

    public static bool GameIsOver;

    //TODO define next round condition
    public bool showNextRound = false;

    //TODO assign packages to buttons - one set per round
    public PackageSelection selection1;
    public PackageSelection selection2;
    public PackageSelection selection3;
    public PackageSelection selection4;
    public PackageSelection selection5;
    
    public Package facegrab;
    public Package kitten;
    public Package motorcycle;
    public Package teslaCoil;
    
    private void Awake()
    {
        instance = this;
        characterName.gameObject.SetActive(false);
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
        
        if (showNextRound == true)
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
        characterName.gameObject.SetActive(true);

        Thread.Sleep(3000);
        DoorPanel.ChangeSprite(currentRound.GetDoor().imageOpen);

        // Waiter(4);
        //TODO
        string packageReaction = currentRound.GetCharacter().ReactToPackage(package);
        log.text = log.text + "\n > " + packageReaction;

        //Next round!
        currentRoundIndex++;
        showNextRound = true;
        characterName.gameObject.SetActive(false);
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
        
        //TODO update when GenerateScenario() implemented
        if (currentRoundIndex == 0)
        {
            selection1.package = facegrab;
            selection1.text.text = selection1.package.Name;
            selection2.package = kitten;
            selection2.text.text = selection2.package.Name;
            selection3.package = motorcycle;
            selection3.text.text = selection3.package.Name;
        }
        else
        {
            selection1.package = facegrab;
            selection1.text.text = selection1.package.Name;
            selection2.package = motorcycle;
            selection2.text.text = selection2.package.Name;
            selection3.package = teslaCoil;
            selection3.text.text = selection3.package.Name;
        }
    }

    private Scenario GenerateScenario()
    {
        Scenario scenario = new Scenario(rounds);
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
}
