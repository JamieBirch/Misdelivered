public class Round
{
    private Door door;
    private Character character;

    public Round(Door door, Character character)
    {
        this.door = door;
        this.character = character;
    }
    
    public void SetDoor(Door door)
    {
        this.door = door;
    }
    
    public void SetCharacter(Character character)
    {
        this.character = character;
    }
    
    public Door GetDoor()
    {
        return door;
    }
    
    public Character GetCharacter()
    {
        return character;
    }
}