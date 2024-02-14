using UnityEngine;
using UnityEngine.UI;

public class DoorPanel : MonoBehaviour
{
    public Image door;
    public Image character;

    public void ChangeDoor(Sprite newSprite)
    {
        door.sprite = newSprite;
    }
    
    public void ChangeCharacter(Sprite newSprite)
    {
        character.sprite = newSprite;
    }
    

}
