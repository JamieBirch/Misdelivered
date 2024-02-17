using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public SceneFader sceneFader;
    
    public Scenario scenario;
    public DoorPanel DoorPanel;
    public DoorsDictionary DoorsDictionary;
    public CharactersDictionary CharactersDictionary;
    public PackagesDictionary PackagesDictionary;
    public PackagesReactionSpotsDictionary PackagesReactionSpotsDictionary;
    
    public GameObject CharacterStanding;
    public GameObject gameOverUI;
    
    public GameObject packageReactionSpot;
    public Image packageReactionPanel;

    public GameObject CharacterReactionPanel;
    public Text CharacterReaction;
    public GameObject NextRoundButton;
    
    public string facehuggerDefaultReaction;
    
    public Text characterName;
    public int rounds;
    public int currentRoundIndex = 0;
    public Round currentRound;

    public static bool GameIsOver;

    public PackageSelection[] _packageSelections;
    
    public SoundAudioClip[] soundClips;
    public AudioClip[] sighs;

    public GameObject packageButtons;
    public GameObject FaceHuggerAnimObject;
    public Animator FaceHuggerAnimator;

    public GameObject[] placesForAnnouncements;
    public AnnouncementsDictionary AnnouncementsDictionary;
    
    private void Awake()
    {
        instance = this;
        // characterName.gameObject.SetActive(false);
        SoundManager.Initialize();
        // _packageSelections = new[] { selection1, selection2, selection3, selection4, selection5 };
        SoundManager.PlaySoundTrack();
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
        
        // SoundManager.PlaySound(SoundManager.Sound.soundTrack);
    }

    public void GivePackage(Package package)
    {
        //TODO disable button press if on previous round
        packageButtons.SetActive(false);
        
        //open Door
        //TODO: play sound openDoor
        SoundManager.PlaySound(SoundManager.Sound.knockAndScreech);
        
        if (CharacterStanding)
        {
            Debug.Log("Door opens!");
            CharacterStanding.SetActive(true);
            DoorPanel.ChangeCharacter(currentRound.GetCharacter().sprite);
            // characterName.gameObject.SetActive(true);
        }

        packageReactionSpot.SetActive(true);
        CharacterReactionPanel.SetActive(true);
        NextRoundButton.SetActive(true);

        DoorPanel.ChangeDoor(currentRound.GetDoor().imageOpen);

        Debug.Log("Giving " + package.name + " to " + currentRound.GetCharacter().name);
        //reaction
        ReactionOptions reaction = currentRound.GetCharacter().ReactToPackage(package);

        if (reaction == ReactionOptions.Facehugger)
        {
            //specific facehugger actions
            FaceHuggerAnimObject.SetActive(true);
            FaceHuggerAnimator.Play("faceHuggerAnim");
        }
        UpdatePlayerStats(reaction);
        CharacterReactionPanel.GetComponent<Image>().color = GetReactionColor(reaction);
        string reactionText = currentRound.GetCharacter().GetReaction(reaction);
        CharacterReaction.text = reactionText;
        
        //TODO show reaction spot
        packageReactionPanel.GetComponent<Image>().sprite = GetReactionSpot(reaction);
        
        //TODO play sound: reaction sound
        SoundManager.Sound reactionSound = GetReactionSound(reaction);
        SoundManager.PlaySound(reactionSound);

    }

    private Sprite GetReactionSpot(ReactionOptions reaction)
    {
        return PackagesReactionSpotsDictionary.GetPackageSpot(reaction);
    }

    private void UpdatePlayerStats(ReactionOptions reaction)
    {
        PlayerStats.TotalDeliveries++;
        if (reaction == ReactionOptions.DesiredMatch)
        {
            PlayerStats.DesiredDeliveries++;
            return;
        }

        if (reaction == ReactionOptions.SecretMatch)
        {
            PlayerStats.SecretDeliveries++;
            return;
        }
        
        return;
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
    
    private SoundManager.Sound GetReactionSound(ReactionOptions reaction)
    {
        if (reaction == ReactionOptions.DesiredMatch)
        {
            return SoundManager.Sound.correctDelivery;
        }

        if (reaction == ReactionOptions.SecretMatch)
        {
            return SoundManager.Sound.secretDelivery;
        }

        if (reaction == ReactionOptions.Facehugger)
        {
            return SoundManager.Sound.facehuggerDelivery;
        }

        return SoundManager.Sound.wrongDelivery;
    }

    public void NextRound()
    {
        sceneFader.FastFadeIn();
        currentRoundIndex++;
        //TODO fade
        ShowCurrentRound();
    }

    private void ShowCurrentRound()
    {
        //TODO play sound: portal sound
        SoundManager.PlaySound(SoundManager.Sound.portal);
        
        Debug.Log("Round: " + currentRoundIndex);

        if (currentRoundIndex >= rounds)
        {
            EndGame();
            return;
        }

        //Hide objects of prev. round
        HideEverything();
        
        //randomize places to place announcements
        HashSet<GameObject> placesSet = new HashSet<GameObject>();
        Random random = new Random();
        int numberOfPlaces = random.Next(placesForAnnouncements.Length);
        while (placesSet.Count != numberOfPlaces)
        {
            int index = random.Next(placesForAnnouncements.Length);
            placesSet.Add(placesForAnnouncements[index]);
        }

        foreach (GameObject place in placesSet)
        {
            place.SetActive(true);
            place.GetComponent<Image>().sprite = AnnouncementsDictionary.GetRandomSprite();
        }
        

        currentRound = scenario.GetRound(currentRoundIndex);
        //show door
        DoorPanel.ChangeDoor(currentRound.GetDoor().imageClosed);
        //TODO remove this line when testing finished
        // characterName.text = currentRound.GetCharacter().character.ToString();

        packageButtons.SetActive(true);
        AssignPackagesToButtons();
    }

    private void HideEverything()
    {
        if (CharacterStanding)
        {
            CharacterStanding.SetActive(false);
        }

        CharacterReactionPanel.SetActive(false);
        NextRoundButton.SetActive(false);
        packageReactionSpot.SetActive(false);
        FaceHuggerAnimObject.SetActive(false);

        foreach (GameObject announcement in placesForAnnouncements)
        {
            announcement.SetActive(false);
        }
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
        _packageSelections[i].SetSprite();
        //TODO 
        // _packageSelections[i].image.sprite = package.sprite;
        // _packageSelections[i].text.text = package.name;
        // SetSprite();
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
        //TODO show end screen
        gameOverUI.SetActive(true);
        // CharacterReaction.text = "Game Over";
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

    [System.Serializable]
    public class SoundAudioClip
    {
        public SoundManager.Sound sound;
        public AudioClip audioClip;
    }

    public void PlayButtonHoverSound()
    {
        SoundManager.PlaySound(SoundManager.Sound.sigh);
    }
    
    static T GetRandomElement<T>(T[] array)
    {
        System.Random rand = new System.Random();
        int randomIndex = rand.Next(0, array.Length);
        return array[randomIndex];
    }
}
