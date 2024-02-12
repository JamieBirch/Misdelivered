using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Scenario scenario;
    
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
    
    public Package facegrab;
    public Package kitten;
    public Package motorcycle;
    public Package teslaCoil;

    
    //TODO remove after GenerateScenario() implemented

    public Door door1;
    public Door door2;
    public Character char1;
    public Character char2;
    
    
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
        currentRound = scenario.GetRound(currentRoundIndex);
        
        //TODO show next round
        //show door
        characterName.text = currentRound.GetCharacter().Name;
        
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
        // Scenario scenario = new Scenario(rounds);
        Scenario scenario = DefaultScenario();
        //TODO
        //doors
        /*for (int i = 0; i < rounds; i++)
        {
            //TODO create rounds
        }*/

        return scenario;
    }

    private Scenario DefaultScenario()
    {
        Scenario scenario = new Scenario(2);
        scenario.CreateRound(0, door1, char1);
        scenario.CreateRound(1, door2, char2);
        return scenario;
    }

    void EndGame()
    {
        GameIsOver = true;
        log.text = log.text + "\n > " + "Game Over";
    }
}
