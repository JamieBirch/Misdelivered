public class Scenario
{
    private Round[] _rounds;

    public Scenario(int rounds)
    {
        _rounds = new Round[rounds];
    }

    public void CreateRound(int index, Door door, Character character)
    {
        _rounds[index] = new Round(door, character);
    }

    public Round GetRound(int index)
    {
        return _rounds[index];
    }
}

