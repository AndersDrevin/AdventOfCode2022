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

public class CleaningElvesPair
{
    public CleaningElvesPair(CleaningElf e1, CleaningElf e2)
    {
        Elf1 = e1;
        Elf2 = e2;
    }
    public CleaningElf? Elf1 { get; set; }
    public CleaningElf? Elf2 { get; set; }
    public bool AssignmentsOverlapsSome
    {
        get => AssignmentsOverlapsSome1 || AssignmentsOverlapsSome2;
    }
    public bool AssignmentsOverlapsSome1
    {
        get
        {
            if (Elf1 == null || Elf2 == null) return false;
            return
                (Elf1.Assignment.Start.Value >= Elf2.Assignment.Start.Value &&
                Elf1.Assignment.Start.Value <= Elf2.Assignment.End.Value) ||
                (Elf1.Assignment.End.Value >= Elf2.Assignment.Start.Value &&
                Elf1.Assignment.End.Value <= Elf2.Assignment.End.Value);
        }
    }
    public bool AssignmentsOverlapsSome2
    {
        get
        {
            if (Elf1 == null || Elf2 == null) return false;
            return
                (Elf2.Assignment.Start.Value >= Elf1.Assignment.Start.Value &&
                Elf2.Assignment.Start.Value <= Elf1.Assignment.End.Value) ||
                (Elf2.Assignment.End.Value >= Elf1.Assignment.Start.Value &&
                Elf2.Assignment.End.Value <= Elf1.Assignment.End.Value);
        }
    }
    public bool AssignmentsFullyOverlaps
    {
        get
        {
            if (Elf1 == null || Elf2 == null) return false;
            return
                (Elf1.Assignment.Start.Value >= Elf2.Assignment.Start.Value &&
                Elf1.Assignment.End.Value <= Elf2.Assignment.End.Value) ||
                (Elf2.Assignment.Start.Value >= Elf1.Assignment.Start.Value &&
                Elf2.Assignment.End.Value <= Elf1.Assignment.End.Value);
        }
    }
}

public class CleaningElf
{
    public CleaningElf(int from, int to)
    {
        Assignment = new Range(from, to);
    }
    public Range Assignment { get; set; }
}

public class File7
{
    public File7() { }
    public File7(string name, int size)
    {
        Name = name;
        Size = size;
    }
    public string Name { get; set; } = String.Empty;
    public int Size { get; set; }
}

public class Folder7
{
    public Folder7() { }
    public Folder7(string name, Folder7? parentFolder) { Name = name; ParentFolder = parentFolder; }
    public Folder7? ParentFolder { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<Folder7> Folders { get; set; } = new List<Folder7>();
    public List<File7> Files { get; set; } = new List<File7>();
    public void FindFolderToDelete(Folder7 current, int minSize, ref int smallest)
    {
        var size = current.FolderSize;
        if(size >= minSize && size < smallest)
            smallest = size;
        foreach(var folder in current.Folders){
            FindFolderToDelete(folder, minSize, ref smallest);
        }
    }
    public void FindAll100KFolders(Folder7 current, ref int sum)
    {
        var size = current.FolderSize;
        if (size <= 100000) sum += size;
        foreach(var folder in current.Folders)
            FindAll100KFolders(folder, ref sum);
    }
    public int FolderSize
    {
        get => Files.Sum(x => x.Size) + Folders.Sum(x => x.FolderSize);
    }
}

public class Forest{
    public List<Tree> Trees{get;set;} = new();

    public Tree? this[int x, int y]{
        get => Trees.SingleOrDefault(t => t.X == x && t.Y == y);
    }

    public int MaxX{
        get => Trees.Max(x => x.X);
    }

    public int MaxY{
        get => Trees.Max(x => x.Y);
    }

    public int NumberOfVisibleTrees{
        get =>
            Trees.Where(x => x.Visible).Count();
    }

    public int MaxScore{
        get => Trees.Max(x => x.Score);
    }

}

public class Tree{

    public Tree(int x, int y, int height, Forest forest){
        X = x;
        Y = y;
        Height = height;
        Forest = forest;
        Id = Guid.NewGuid();
    }
    public Guid Id{get;set;}
    public int Height{get;set;}
    public int X{get;set;}
    public int Y{get;set;}

    public Forest? Forest{get;}

    public int Score{ 
        get{
            if(Forest == null) return 0;
            int s1 = 0, s2 = 0, s3 = 0, s4 = 0;
            for(var x = X+1; x <= Forest.MaxX; x++){
                s1++;
                if(Forest[x, Y]?.Height >= Height)
                    break;
            }
            for(var x = X-1; x >= 0; x--){
                s2++;
                if(Forest[x, Y]?.Height >= Height)
                    break;
            }
            for(var y = Y+1; y <= Forest.MaxY; y++){
                s3++;
                if(Forest[X, y]?.Height >= Height)
                    break;
            }
            for(var y = Y-1; y >= 0; y--){
                s4++;
                if(Forest[X, y]?.Height >= Height)
                    break;
            }
            return s1 * s2 * s3 * s4;
        }
    }

    public bool Visible{
        get{
            if(Forest == null) return false;

            if(OnTheEdge) return true;

            var H = Height;

            var notVisbleDirections = 0;
            //If any tree to the left (x < myX) ==> not visible from left;
            if(Forest.Trees.Any(x => x.Y == Y && x.X < X && x.Height >= H)) notVisbleDirections++;
            //If any tree to the right (x > myX) heiger => not visisble from right
            if(Forest.Trees.Any(x => x.Y == Y && x.X > X && x.Height >= H)) notVisbleDirections++;
            //If any tree above me (y < myY) is higher => not visible from above me
            if(Forest.Trees.Any(x => x.X == X && x.Y < Y && x.Height >= H)) notVisbleDirections++;
            //If any tree below me (y > myY) is higher => not visible from below
            if(Forest.Trees.Any(x => x.X == X && x.Y > Y && x.Height >= H)) notVisbleDirections++;

            //x == 4 => not visible from any direction
            return notVisbleDirections != 4;

        }
    }

    public bool OnTheEdge{
        get =>
            Forest == null ? false :
            X == 0 ||
            Y == 0 ||
            X == Forest.MaxX ||
            Y == Forest.MaxY;

    }
}