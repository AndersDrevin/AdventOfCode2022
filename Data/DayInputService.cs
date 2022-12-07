using Microsoft.AspNetCore.Routing.Tree;
using System.Linq;

namespace AdventOfCode2022.Data;
public class DayInputService
{
    private string Day1Input{
        get => File.ReadAllText("./Files/day1input.txt");
    }
    private string Day2Input{
        get => File.ReadAllText("./Files/day2input.txt");
    }
    private string Day3Input{
        get => File.ReadAllText("./Files/day3input.txt");
    }
    private string Day4Input{
        get => File.ReadAllText("./Files/day4input.txt");
    }
    private string Day5Stacks{
        get => File.ReadAllText("./Files/day5stacks.txt");
    }

    private string Day5Moves{
        get => File.ReadAllText("./Files/day5moves.txt");
    }

    private string Day6Input{
        get => File.ReadAllText("./Files/day6input.txt");
    }
    private string Day7Input
    {
        get => File.ReadAllText("./Files/day7input.txt");
    }

    public RPSRaces Day2()
    {
        List<RPSRace> races = new();
        foreach(var raceString in Day2Input.Split("\r\n")){
            var race = raceString.Split(" ");
            races.Add(new RPSRace
            {
                HomePlayer = new RPSPlayer(race[1]),
                VisitingPlayer = new RPSPlayer(race[0])
            });
        }
        return new RPSRaces(races);

    }

    public RPSRaces Day2_2()
    {
        List<RPSRace> races = new();
        foreach (var raceString in Day2Input.Split("\r\n"))
        {
            var raceArray = raceString.Split(" ");
            var race = new RPSRace
            {
                VisitingPlayer = new RPSPlayer(raceArray[0])
            };
            switch (raceArray[1]){
                case "X":
                    //Loose
                    race.AddLoosingHomePlayer();
                    break;
                case "Y":
                    //Draw
                    race.AddDrawHomePlayer();
                    break;
                case "Z":
                    //Win
                    race.AddWinningHomePlayer();
                    break;
            }
            races.Add(race);
        }
        return new RPSRaces(races);

    }

    public FoodInventory Day1()
    {
        List<ElfBackpack> backPacks = new();

        foreach (string backpack in Day1Input.Split("\r\n\r\n"))
        {
            var elfFoodItems = new List<ElfFoodItem>();
            foreach (var foodItem in backpack.Split("\r\n"))
            {
                elfFoodItems.Add(new ElfFoodItem
                {
                    Calories = int.Parse(foodItem)
                });
            }
            backPacks.Add(new ElfBackpack
            {
                FoodItems = elfFoodItems
            });
        }

        return new FoodInventory
        {
            Backpacks = backPacks
        };
    }

    private Cargo Day5_ReadCargo()
    {
        var cargo = new Cargo();

        //read initial cargo

        foreach (var stackRow in Day5Stacks.Split("\r\n").Reverse())
        {
            var state = 0; //0 = read [, 1 = crate, 2 = } 3 = space.
            var crateCount = 0;
            foreach (var s in stackRow)
            {
                switch (state)
                {
                    case 0://[
                        break;
                    case 1://Stack
                        if (s != ' ')
                            cargo.Stacks[crateCount].Crates.Push(s);
                        break;
                    case 2://]
                        break;
                    case 3://Space
                        state = -1;
                        crateCount++;
                        break;
                }
                state++;
            }
        }

        return cargo;

    }

    private string Day5_GetTops(Cargo cargo)
    {
        var result = "";
        for (var i = 0; i < 9; i++)
        {
            var ch = cargo.Stacks[i].Crates.Pop();
            Console.WriteLine($"i=stack({i})='{ch}'");
            result += ch;
        }

        return result;


    }

    public int Day6_1()
    {
        List<char> markerCandidate = new();
        var cnt = 0;
        foreach(var ch in Day6Input)
        {
            cnt++;
            markerCandidate.Add(ch);
            if(markerCandidate.Count == 4)
            {
                if(IsMarker(markerCandidate)){
                    return cnt;
                }

                markerCandidate.RemoveAt(0);

            }

        }

        return cnt;

    }

    public int Day6_2()
    {
        List<char> markerCandidate = new();
        var cnt = 0;
        foreach(var ch in Day6Input)
        {
            cnt++;
            markerCandidate.Add(ch);
            if(markerCandidate.Count == 14)
            {
                if(IsMessageMarker(markerCandidate)){
                    return cnt;
                }

                markerCandidate.RemoveAt(0);

            }

        }

        return cnt;

    }

    public Folder7 Day7_ReadDirectoryStructure()
    {
        Folder7 root = new("/");
        Stack<string> structureStack = new();
        Array.ForEach(Day7Input.Split("\r\n").Reverse().ToArray(), x => { structureStack.Push(x); });
        Day7_Iterate(structureStack, root);
        return root;

    }

    private void Day7_Iterate(Stack<string> structureStack, Folder7? currentFolder)
    {
        if (currentFolder == null) return;
        if (!structureStack.TryPop(out string? row)) return;
        if (row == null) return;
        var rowA = row.Split(" ");
        switch (rowA[0])
        {
            case "$":
                //Command.
                switch (rowA[1])
                {
                    case "cd":
                        switch (rowA[2])
                        {
                            case "/":
                                Day7_Iterate(structureStack, currentFolder.ParentFolder ?? currentFolder);
                                break;
                            case "..":
                                Day7_Iterate(structureStack, currentFolder.ParentFolder);
                                break;
                            default:
                                Day7_Iterate(structureStack, currentFolder.Folders.FirstOrDefault(x => x.Name == rowA[2]));
                                break;
                        }
                        break;
                    case "ls":
                        Day7_Iterate(structureStack, currentFolder);
                        break;
                }
                break;
            case "dir":
                //Folder
                Console.WriteLine($"Adding folder {rowA[1]}");
                currentFolder.Folders.Add(new Folder7(rowA[1]));
                Day7_Iterate(structureStack, currentFolder);
                break;
            default:
                //File
                currentFolder.Files.Add(new File7(rowA[1], int.Parse(rowA[0])));
                Day7_Iterate(structureStack, currentFolder);
                break;
        }


    }

    public Folder7 Day7_1()
    {
        var root = Day7_ReadDirectoryStructure();
        return root;
    }

    private bool IsMessageMarker(List<char> candidate){
        for(var i=0; i<14; i++){
            if(candidate.Where(x => x == candidate[i]).Count() != 1) return false;
        }

        return true;
    }

    private bool IsMarker(List<char> candidate) =>
            candidate.Where(x => x == candidate[0]).Count() == 1 &&
            candidate.Where(x => x == candidate[1]).Count() == 1 &&
            candidate.Where(x => x == candidate[2]).Count() == 1 &&
            candidate.Where(x => x == candidate[3]).Count() == 1;


    public string Day5_1(){

        var cargo = Day5_ReadCargo();


        //Read move instructions
        foreach(var moveRow in Day5Moves.Split("\r\n")){
            var movesA = moveRow.Split(" ");
            var nMoves = int.Parse(movesA[1]);
            var fromStack = int.Parse(movesA[3]) - 1;
            var toStack = int.Parse(movesA[5]) - 1;
            for(var move = 0; move < nMoves; move++){
                cargo.Stacks[toStack].Crates.Push(cargo.Stacks[fromStack].Crates.Pop());
            }
        }


        return Day5_GetTops(cargo);
    }
    public string Day5_2()
    {

        var cargo = Day5_ReadCargo();


        //Read move instructions
        foreach (var moveRow in Day5Moves.Split("\r\n"))
        {
            var movesA = moveRow.Split(" ");
            var nMoves = int.Parse(movesA[1]);
            var fromStack = int.Parse(movesA[3]) - 1;
            var toStack = int.Parse(movesA[5]) - 1;
            Stack<char> tempStack = new();
            for (var move = 0; move < nMoves; move++)
            {
                tempStack.Push(cargo.Stacks[fromStack].Crates.Pop());
            }
            for(var move = 0; move < nMoves; move++)
            {
                cargo.Stacks[toStack].Crates.Push(tempStack.Pop());
            }
        }


        return Day5_GetTops(cargo);
    }

    public IEnumerable<Rucksack> Day3(){

        List<Rucksack> rucksacks = new();
        foreach(var rucksackString in Day3Input.Split("\r\n")){
            var l = rucksackString.Length;
            var cl = l / 2;
            rucksacks.Add(new Rucksack(rucksackString.Substring(0, cl), rucksackString.Substring(cl, cl)));
        }
        return rucksacks;

    }

    public List<CleaningElvesPair> EatInputDay4(){

        List<CleaningElvesPair> elvesPair = new();
        foreach(var elfPairString in Day4Input.Split("\r\n")){
            var elfPairArray = elfPairString.Split(",");
            var elfOneArray = elfPairArray[0].Split("-");
            var elfTwoArray = elfPairArray[1].Split("-");
            var elfOne = new CleaningElf(
                int.Parse(elfOneArray[0]),
                int.Parse(elfOneArray[1])
            );
            var elfTwo = new CleaningElf(
                int.Parse(elfTwoArray[0]),
                int.Parse(elfTwoArray[1])
            );
            elvesPair.Add(new CleaningElvesPair(elfOne, elfTwo));

        }

        return elvesPair;
    }
    public int Day4_1() => EatInputDay4().Where(x => x.AssignmentsFullyOverlaps).Count();

    public int Day4_2() => EatInputDay4().Where(x => x.AssignmentsOverlapsSome).Count();



}
