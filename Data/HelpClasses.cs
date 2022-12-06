namespace AdventOfCode2022.Data;


public class ElfFoodItem
{
    public int Calories { get; set; }
}

public class ElfBackpack
{
    public IEnumerable<ElfFoodItem>? FoodItems { get; set; }
    public int? SumCalories
    {
        get => FoodItems?.Sum(x => x.Calories);
    }
}

public class FoodInventory
{
    public IEnumerable<ElfBackpack>? Backpacks { get; set; }
    public int? LargestBackPack => Backpacks?.Max(x => x.SumCalories);
    public int? SumOfThreeLargest => Backpacks?.OrderByDescending(x => x.SumCalories)?.Take(3)?.Sum(x => x.SumCalories);
}

/*
 * Opponent ABC = A Rock, B Paper, C Scissors
 * Player  XYZ = X Rock, Y Paper, Z Scissors
 */

public enum RPSMove
{
    Rock,
    Paper,
    Scissors,
    Unknown
}

public class RPSPlayer
{
    public RPSPlayer(string moveString)
    {
        MoveString = moveString;
    }

    public string? MoveString { get; set; }
    public RPSMove Move
    {
        get => MoveString switch
        {
            "A" => RPSMove.Rock,
            "X" => RPSMove.Rock,
            "B" => RPSMove.Paper,
            "Y" => RPSMove.Paper,
            "C" => RPSMove.Scissors,
            "Z" => RPSMove.Scissors,
            _ => RPSMove.Unknown,
        };

    }

    public int Play(RPSPlayer? visitingPlayer)
    {
        if (visitingPlayer == null) return -1;
        switch (Move)
        {
            case RPSMove.Rock:
                return visitingPlayer.Move switch
                {
                    RPSMove.Rock => 1 + 3,
                    RPSMove.Paper => 1 + 0,
                    RPSMove.Scissors => 1 + 6,
                    _ => -1
                };
            case RPSMove.Paper:
                return visitingPlayer.Move switch
                {
                    RPSMove.Rock => 2 + 6,
                    RPSMove.Paper => 2 + 3,
                    RPSMove.Scissors => 2 + 0,
                    _ => -1
                };
            case RPSMove.Scissors:
                return visitingPlayer.Move switch
                {
                    RPSMove.Rock => 3 + 0,
                    RPSMove.Paper => 3 + 6,
                    RPSMove.Scissors => 3 + 3,
                    _ => -1
                };
            default:
                return -1;
        }
    }
}


public class RPSRace{
    public RPSPlayer? HomePlayer { get; set; }
    public RPSPlayer? VisitingPlayer { get; set; }
    public void AddWinningHomePlayer()
    {
        if (VisitingPlayer == null) return;
        switch (VisitingPlayer.Move)
        {
            case RPSMove.Rock:
                HomePlayer = new RPSPlayer("B");
                break;
            case RPSMove.Scissors:
                HomePlayer = new RPSPlayer("A");
                    break;
            case RPSMove.Paper:
                HomePlayer = new RPSPlayer("C");
                break;
            default:
                HomePlayer = new RPSPlayer("ZZ");
                break;
        }
    }
    public void AddDrawHomePlayer()
    {
        //A=Rock, B=Paper, C=Scissors
        if (VisitingPlayer == null) return;
        switch (VisitingPlayer.Move)
        {
            case RPSMove.Rock:
                HomePlayer = new RPSPlayer("A");
                break;
            case RPSMove.Scissors:
                HomePlayer = new RPSPlayer("C");
                break;
            case RPSMove.Paper:
                HomePlayer = new RPSPlayer("B");
                break;
            default:
                HomePlayer = new RPSPlayer("ZZ");
                break;
        }

    }

    public void AddLoosingHomePlayer()
    {
        //A=Rock, B=Paper, C=Scissors
        if (VisitingPlayer == null) return;
        switch (VisitingPlayer.Move)
        {
            case RPSMove.Rock:
                HomePlayer = new RPSPlayer("C");
                break;
            case RPSMove.Scissors:
                HomePlayer = new RPSPlayer("B");
                break;
            case RPSMove.Paper:
                HomePlayer = new RPSPlayer("A");
                break;
            default:
                HomePlayer = new RPSPlayer("ZZ");
                break;
        }
    }
    public int? HomeResult
    {
        get => HomePlayer?.Play(VisitingPlayer);
    }
    public int? VistingResult
    {
        get => VisitingPlayer?.Play(HomePlayer);
    }
}

public class RPSRaces
{
    public RPSRaces(IEnumerable<RPSRace> races)
    {
        Races = races;
    }
    public IEnumerable<RPSRace>? Races { get; set; }
    public int? HomeScore
    {
        get => Races?.Sum(x => x.HomeResult);
    }
    public int? AwayScore
    {
        get => Races?.Sum(x => x.VistingResult);
    }
}
public class Rucksack{

    public Rucksack(string c1, string c2){
        Compartment1 = c1;
        Compartment2 = c2;
    }

    public string TotalContent{
        get => Compartment1 + Compartment2;
    }

    public string? GetFaultyItem(){
        if(string.IsNullOrWhiteSpace(Compartment1) || string.IsNullOrWhiteSpace(Compartment2)) return null;
        var faultyItems = Compartment1?.Where(x => Compartment2.Contains(x));
        if(faultyItems == null) return null;
        return string.Concat(faultyItems.First());
    }

    public int GetFaultyItemAsPriority(){
        return GetCharAsPriority(GetFaultyItem()?.First());
    }

    public int GetCharAsPriority(char? ch){
        if(ch == null) return 0;
        if(ch > 96 && ch < 123)
            return ch.Value - 96; //a-z, 1 - 26
        if(ch > 64 && ch < 91)
            return ch.Value - 38; //A-Z, 27 - 52

        return 0;
    }

    public string? Compartment1{get;set;}
    public string? Compartment2{get;set;}

}

public class Cargo{

    public Cargo(){
        for(var i = 0; i<9; i++) Stacks[i] = new CrateStack();
    }

    public CrateStack[] Stacks = new CrateStack[9];
}

public class CrateStack{
    public Stack<char> Crates{get;set;} = new Stack<char>();
}

