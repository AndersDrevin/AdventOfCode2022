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


public class DayInputService
{
    public string Day1Data()
    {
        return "5104\r\n6131\r\n3553\r\n4496\r\n5847\r\n3253\r\n1828\r\n1045\r\n6369\r\n5544\r\n2756\r\n7382\r\n\r\n6879\r\n9715\r\n10122\r\n\r\n14438\r\n1135\r\n13406\r\n4888\r\n\r\n67712\r\n\r\n1965\r\n4515\r\n4150\r\n3583\r\n3007\r\n4852\r\n6397\r\n6237\r\n5505\r\n3106\r\n6357\r\n2504\r\n4074\r\n\r\n1689\r\n5612\r\n5531\r\n3138\r\n1203\r\n4821\r\n3468\r\n2424\r\n3425\r\n5566\r\n4374\r\n5233\r\n5452\r\n3049\r\n2260\r\n\r\n3933\r\n6073\r\n2260\r\n2376\r\n7298\r\n3397\r\n3479\r\n5316\r\n6750\r\n4800\r\n\r\n14581\r\n6385\r\n18287\r\n14353\r\n\r\n44790\r\n\r\n66296\r\n\r\n3368\r\n1373\r\n9644\r\n1682\r\n11146\r\n6876\r\n\r\n2544\r\n5471\r\n3632\r\n1837\r\n2272\r\n2956\r\n4989\r\n1754\r\n4574\r\n5076\r\n3201\r\n2199\r\n5688\r\n1161\r\n3326\r\n\r\n4897\r\n3510\r\n4961\r\n2747\r\n3931\r\n2722\r\n4021\r\n2231\r\n5466\r\n6371\r\n4691\r\n2851\r\n1846\r\n4720\r\n\r\n13096\r\n5576\r\n5765\r\n3715\r\n8851\r\n\r\n3339\r\n6453\r\n4991\r\n3935\r\n5900\r\n2326\r\n4731\r\n3323\r\n6270\r\n7505\r\n\r\n2208\r\n3058\r\n6908\r\n6343\r\n6513\r\n5383\r\n6087\r\n5836\r\n4824\r\n5588\r\n5039\r\n6138\r\n1077\r\n\r\n9045\r\n6898\r\n8327\r\n4103\r\n4006\r\n6797\r\n8591\r\n6515\r\n5228\r\n\r\n39541\r\n\r\n33536\r\n7614\r\n\r\n4637\r\n7862\r\n3038\r\n6682\r\n8880\r\n2784\r\n7924\r\n5589\r\n2107\r\n\r\n33278\r\n18120\r\n\r\n1005\r\n3479\r\n4129\r\n2799\r\n5324\r\n7799\r\n5380\r\n6998\r\n1877\r\n\r\n31663\r\n9259\r\n\r\n1009\r\n3987\r\n1254\r\n6449\r\n5134\r\n6090\r\n6341\r\n3229\r\n5902\r\n4492\r\n4025\r\n\r\n10079\r\n16006\r\n19943\r\n1970\r\n\r\n3428\r\n5671\r\n1030\r\n2461\r\n6073\r\n2875\r\n3493\r\n3103\r\n1476\r\n4305\r\n2664\r\n5017\r\n4210\r\n3629\r\n\r\n45170\r\n\r\n11282\r\n8209\r\n10075\r\n1366\r\n13841\r\n3412\r\n\r\n9981\r\n7808\r\n1565\r\n3171\r\n6684\r\n1905\r\n1087\r\n\r\n14447\r\n11220\r\n3519\r\n10317\r\n14174\r\n\r\n7518\r\n3810\r\n12824\r\n3137\r\n2272\r\n2616\r\n\r\n7827\r\n29197\r\n\r\n8799\r\n12459\r\n16530\r\n16661\r\n\r\n1965\r\n10267\r\n11399\r\n11562\r\n13367\r\n10223\r\n\r\n6410\r\n2536\r\n7255\r\n2151\r\n3190\r\n5786\r\n4114\r\n6875\r\n4013\r\n2381\r\n6020\r\n\r\n1751\r\n6262\r\n5190\r\n5091\r\n6453\r\n3652\r\n2929\r\n3411\r\n4954\r\n1275\r\n1468\r\n2689\r\n1200\r\n4362\r\n\r\n4928\r\n5282\r\n6845\r\n5328\r\n2101\r\n6522\r\n3929\r\n3276\r\n4740\r\n4493\r\n4003\r\n6797\r\n\r\n6529\r\n11489\r\n1696\r\n12922\r\n8959\r\n10927\r\n\r\n13381\r\n5921\r\n5375\r\n10424\r\n9279\r\n10031\r\n\r\n3800\r\n2166\r\n1964\r\n4182\r\n3395\r\n5956\r\n6024\r\n2473\r\n2780\r\n3686\r\n5791\r\n1449\r\n2698\r\n4430\r\n1008\r\n\r\n37175\r\n26191\r\n\r\n2863\r\n1329\r\n6266\r\n5911\r\n6331\r\n5211\r\n2420\r\n6006\r\n2259\r\n4125\r\n1885\r\n2284\r\n6213\r\n\r\n3560\r\n4007\r\n5585\r\n4484\r\n1828\r\n7935\r\n5895\r\n3139\r\n6413\r\n\r\n1201\r\n3350\r\n3763\r\n1920\r\n4653\r\n1662\r\n2559\r\n1918\r\n6378\r\n6688\r\n2569\r\n3180\r\n\r\n4347\r\n1526\r\n7318\r\n3860\r\n4250\r\n6191\r\n10198\r\n\r\n8179\r\n5824\r\n4470\r\n7569\r\n3163\r\n1859\r\n2541\r\n3433\r\n1851\r\n7360\r\n\r\n1028\r\n3103\r\n3166\r\n2025\r\n5630\r\n5067\r\n1819\r\n1590\r\n2290\r\n3636\r\n2411\r\n2856\r\n1918\r\n3050\r\n\r\n3582\r\n4564\r\n3311\r\n4695\r\n4780\r\n5863\r\n3626\r\n5304\r\n4009\r\n1499\r\n1528\r\n2261\r\n2743\r\n3992\r\n\r\n6432\r\n4354\r\n1747\r\n3799\r\n7415\r\n6951\r\n4928\r\n7862\r\n6082\r\n1010\r\n3586\r\n\r\n11375\r\n14308\r\n7595\r\n18952\r\n\r\n23213\r\n18972\r\n\r\n15795\r\n17712\r\n5850\r\n\r\n4756\r\n3643\r\n3397\r\n6067\r\n5206\r\n1658\r\n2436\r\n4293\r\n2352\r\n5096\r\n2038\r\n3780\r\n2620\r\n3124\r\n2525\r\n\r\n8776\r\n3372\r\n2945\r\n7681\r\n7636\r\n1851\r\n2755\r\n2058\r\n7856\r\n\r\n9353\r\n5890\r\n3167\r\n2211\r\n6056\r\n4894\r\n10709\r\n2793\r\n\r\n3477\r\n7013\r\n4750\r\n7568\r\n7135\r\n4165\r\n7490\r\n5932\r\n1434\r\n6230\r\n7262\r\n\r\n2988\r\n3138\r\n6692\r\n5152\r\n6590\r\n7063\r\n5578\r\n3263\r\n2918\r\n4021\r\n6292\r\n3340\r\n\r\n8043\r\n7625\r\n3391\r\n6085\r\n6245\r\n4824\r\n5767\r\n6627\r\n2367\r\n2729\r\n\r\n1520\r\n6438\r\n2132\r\n1601\r\n4488\r\n5254\r\n2935\r\n2368\r\n1569\r\n3680\r\n5772\r\n6031\r\n6288\r\n5836\r\n\r\n5254\r\n4257\r\n6075\r\n5658\r\n6032\r\n6121\r\n5074\r\n4844\r\n3709\r\n4521\r\n5464\r\n1705\r\n6448\r\n6340\r\n\r\n14988\r\n3400\r\n15846\r\n12040\r\n6216\r\n\r\n45130\r\n\r\n4014\r\n2376\r\n2115\r\n3132\r\n5487\r\n6934\r\n5925\r\n2810\r\n4421\r\n6486\r\n2346\r\n5692\r\n\r\n12105\r\n2383\r\n9893\r\n5469\r\n7597\r\n5977\r\n7022\r\n\r\n2795\r\n6488\r\n20406\r\n\r\n12538\r\n3692\r\n3757\r\n12964\r\n15776\r\n\r\n1139\r\n2432\r\n1346\r\n1249\r\n1454\r\n5679\r\n2131\r\n1057\r\n6395\r\n2604\r\n6336\r\n1444\r\n6703\r\n\r\n1499\r\n6392\r\n4154\r\n1207\r\n3827\r\n4646\r\n4283\r\n2106\r\n2958\r\n1327\r\n3760\r\n2575\r\n3961\r\n\r\n11023\r\n7199\r\n6134\r\n12194\r\n10621\r\n\r\n6715\r\n5245\r\n4073\r\n5978\r\n3950\r\n5613\r\n3517\r\n3845\r\n5611\r\n2993\r\n4704\r\n4468\r\n4833\r\n\r\n25207\r\n18060\r\n8124\r\n\r\n3263\r\n2985\r\n6577\r\n3964\r\n1392\r\n2316\r\n5224\r\n3923\r\n1392\r\n5569\r\n4833\r\n5873\r\n1627\r\n\r\n1347\r\n5148\r\n4046\r\n3618\r\n3677\r\n6767\r\n3809\r\n1979\r\n8779\r\n2274\r\n\r\n15152\r\n5482\r\n5835\r\n4176\r\n\r\n1191\r\n5296\r\n1107\r\n7245\r\n4121\r\n2598\r\n4148\r\n2388\r\n5949\r\n1851\r\n3747\r\n3829\r\n\r\n1942\r\n6426\r\n4916\r\n4572\r\n2965\r\n3453\r\n7020\r\n8067\r\n1144\r\n5931\r\n2985\r\n\r\n3315\r\n1989\r\n1609\r\n2161\r\n5836\r\n3243\r\n3901\r\n2795\r\n4187\r\n2790\r\n2083\r\n3164\r\n4843\r\n5887\r\n\r\n16209\r\n2858\r\n2586\r\n15877\r\n13993\r\n\r\n8898\r\n4191\r\n1864\r\n9272\r\n9284\r\n5581\r\n4912\r\n1084\r\n6000\r\n\r\n3410\r\n5934\r\n1609\r\n4699\r\n5008\r\n3104\r\n6016\r\n3130\r\n7286\r\n1906\r\n\r\n7381\r\n4765\r\n2427\r\n2044\r\n6493\r\n2845\r\n1431\r\n5109\r\n7242\r\n3930\r\n1350\r\n4999\r\n\r\n10119\r\n12971\r\n5407\r\n6448\r\n14453\r\n\r\n5647\r\n5083\r\n3317\r\n6707\r\n1586\r\n2179\r\n1468\r\n1214\r\n5992\r\n5732\r\n5813\r\n\r\n1260\r\n3272\r\n11607\r\n3794\r\n4599\r\n2890\r\n\r\n16930\r\n16192\r\n\r\n3140\r\n6499\r\n4094\r\n5617\r\n4379\r\n6309\r\n4400\r\n4764\r\n1143\r\n2004\r\n4451\r\n3034\r\n4209\r\n\r\n2653\r\n5514\r\n3335\r\n8021\r\n7656\r\n7956\r\n9425\r\n5902\r\n7280\r\n\r\n23692\r\n\r\n1097\r\n8621\r\n2372\r\n4381\r\n10625\r\n6138\r\n7127\r\n\r\n1719\r\n5845\r\n3170\r\n4103\r\n5550\r\n3430\r\n2824\r\n\r\n2489\r\n13880\r\n7484\r\n4617\r\n\r\n11152\r\n2300\r\n2349\r\n9548\r\n7018\r\n\r\n5745\r\n2571\r\n5758\r\n1580\r\n1411\r\n3944\r\n4867\r\n2541\r\n1998\r\n3769\r\n5598\r\n5138\r\n4369\r\n5419\r\n\r\n10223\r\n10283\r\n2268\r\n\r\n5604\r\n4701\r\n2054\r\n3920\r\n4925\r\n5118\r\n3707\r\n1838\r\n2159\r\n4192\r\n5164\r\n4419\r\n5630\r\n3127\r\n5255\r\n\r\n3686\r\n1791\r\n3543\r\n6111\r\n4260\r\n3612\r\n4021\r\n3316\r\n3100\r\n5383\r\n1962\r\n3323\r\n5630\r\n\r\n2391\r\n6494\r\n3948\r\n3811\r\n2101\r\n4075\r\n3030\r\n4553\r\n1170\r\n1604\r\n6209\r\n1122\r\n3951\r\n3447\r\n\r\n4687\r\n1165\r\n2511\r\n2316\r\n4002\r\n3832\r\n4968\r\n1082\r\n5374\r\n1960\r\n2355\r\n1975\r\n4569\r\n2324\r\n\r\n4285\r\n3885\r\n2613\r\n1102\r\n4284\r\n3439\r\n5074\r\n1279\r\n6287\r\n\r\n3501\r\n10232\r\n9878\r\n10401\r\n5166\r\n9221\r\n2356\r\n7718\r\n\r\n17368\r\n8643\r\n12092\r\n3319\r\n\r\n7510\r\n4438\r\n8329\r\n4973\r\n6213\r\n6441\r\n1801\r\n7936\r\n7802\r\n\r\n6491\r\n4569\r\n4130\r\n3144\r\n2275\r\n1831\r\n2833\r\n6234\r\n5260\r\n5476\r\n5066\r\n4356\r\n1835\r\n\r\n3842\r\n19003\r\n9012\r\n7301\r\n\r\n1596\r\n8020\r\n\r\n5794\r\n6031\r\n4868\r\n5488\r\n3633\r\n5019\r\n1113\r\n4435\r\n1258\r\n2866\r\n6007\r\n4006\r\n4164\r\n4748\r\n1899\r\n\r\n5388\r\n7788\r\n1063\r\n3566\r\n3185\r\n3839\r\n6396\r\n6052\r\n4822\r\n1345\r\n4405\r\n\r\n5048\r\n8412\r\n11211\r\n10874\r\n11580\r\n9113\r\n\r\n2921\r\n9381\r\n5501\r\n5959\r\n3511\r\n6107\r\n\r\n3827\r\n2400\r\n1465\r\n4592\r\n1070\r\n5953\r\n1252\r\n5820\r\n4496\r\n6461\r\n4105\r\n5273\r\n\r\n7867\r\n\r\n7212\r\n2869\r\n4893\r\n5449\r\n1790\r\n4027\r\n4863\r\n6804\r\n4589\r\n4431\r\n2978\r\n5646\r\n\r\n6820\r\n1830\r\n5561\r\n11648\r\n2836\r\n\r\n3711\r\n7705\r\n8802\r\n1093\r\n10308\r\n1310\r\n4998\r\n9163\r\n\r\n6599\r\n6302\r\n1817\r\n5460\r\n1686\r\n1639\r\n6831\r\n2245\r\n2026\r\n1250\r\n1521\r\n6664\r\n4326\r\n\r\n8495\r\n15655\r\n1144\r\n6597\r\n13967\r\n\r\n5998\r\n4313\r\n5903\r\n3692\r\n8234\r\n2692\r\n10457\r\n3100\r\n\r\n1861\r\n\r\n14744\r\n4779\r\n12847\r\n5418\r\n\r\n1755\r\n11466\r\n4824\r\n3036\r\n2407\r\n3799\r\n\r\n3703\r\n5199\r\n4767\r\n3220\r\n4703\r\n1618\r\n2408\r\n6934\r\n6244\r\n\r\n10247\r\n6489\r\n6234\r\n2992\r\n8650\r\n2238\r\n\r\n4281\r\n13678\r\n15074\r\n6788\r\n6291\r\n\r\n8999\r\n10740\r\n12070\r\n5488\r\n1840\r\n6026\r\n8971\r\n\r\n1663\r\n9127\r\n3582\r\n5593\r\n1469\r\n7933\r\n3315\r\n8614\r\n8179\r\n\r\n31510\r\n20867\r\n\r\n3135\r\n4978\r\n2113\r\n3443\r\n2488\r\n2805\r\n5385\r\n2765\r\n2443\r\n5948\r\n4798\r\n2685\r\n3580\r\n5153\r\n3035\r\n\r\n7786\r\n5488\r\n6868\r\n6016\r\n8457\r\n3381\r\n1057\r\n3370\r\n\r\n5493\r\n5175\r\n4669\r\n3522\r\n6305\r\n2560\r\n5075\r\n4445\r\n3881\r\n6694\r\n4367\r\n6287\r\n3496\r\n\r\n10356\r\n13146\r\n10433\r\n7922\r\n5423\r\n6508\r\n\r\n17729\r\n30047\r\n\r\n1564\r\n8859\r\n7596\r\n10579\r\n7432\r\n7320\r\n4046\r\n\r\n4402\r\n4813\r\n7497\r\n8596\r\n8944\r\n\r\n9946\r\n3497\r\n4322\r\n1990\r\n6240\r\n9993\r\n8309\r\n\r\n24706\r\n3277\r\n22881\r\n\r\n6766\r\n1997\r\n3845\r\n6791\r\n5254\r\n2251\r\n5959\r\n5030\r\n7267\r\n6440\r\n3457\r\n4772\r\n\r\n8589\r\n5044\r\n9480\r\n10563\r\n8685\r\n7787\r\n9630\r\n9199\r\n\r\n9423\r\n2006\r\n5708\r\n4116\r\n14214\r\n\r\n18376\r\n3635\r\n11243\r\n\r\n6763\r\n4005\r\n1430\r\n3727\r\n1574\r\n4517\r\n1253\r\n5880\r\n4091\r\n1499\r\n6714\r\n6510\r\n6452\r\n\r\n5479\r\n4798\r\n6197\r\n3737\r\n6874\r\n2965\r\n5032\r\n4024\r\n5051\r\n8053\r\n\r\n34588\r\n18765\r\n\r\n6659\r\n7468\r\n3910\r\n7243\r\n5199\r\n6073\r\n4412\r\n7824\r\n3338\r\n3599\r\n7017\r\n\r\n3490\r\n2272\r\n2824\r\n5088\r\n6335\r\n1716\r\n2386\r\n3356\r\n2055\r\n2013\r\n2983\r\n5382\r\n1507\r\n4581\r\n\r\n19218\r\n16697\r\n9274\r\n\r\n22122\r\n13616\r\n2703\r\n\r\n1794\r\n4376\r\n2241\r\n2705\r\n2104\r\n1658\r\n3543\r\n2607\r\n3770\r\n4622\r\n3370\r\n2443\r\n4444\r\n5476\r\n3714\r\n\r\n53258\r\n\r\n11510\r\n15672\r\n12872\r\n8622\r\n\r\n5586\r\n15502\r\n3056\r\n3890\r\n14551\r\n\r\n1608\r\n4148\r\n3884\r\n4981\r\n8494\r\n6494\r\n6595\r\n6983\r\n2298\r\n1599\r\n\r\n22767\r\n5219\r\n2814\r\n\r\n1034\r\n1031\r\n4757\r\n7276\r\n7064\r\n5309\r\n7766\r\n8161\r\n\r\n9332\r\n\r\n21322\r\n13644\r\n17204\r\n\r\n52338\r\n\r\n4741\r\n11019\r\n9823\r\n11376\r\n12363\r\n7345\r\n\r\n13565\r\n12029\r\n6517\r\n4188\r\n11311\r\n3153\r\n\r\n3172\r\n2579\r\n2147\r\n6643\r\n6720\r\n1518\r\n3920\r\n3744\r\n4907\r\n3887\r\n5811\r\n2260\r\n3640\r\n\r\n2065\r\n4288\r\n2840\r\n3551\r\n2354\r\n3243\r\n5448\r\n2120\r\n2476\r\n3959\r\n2807\r\n5822\r\n1824\r\n6027\r\n\r\n13354\r\n13217\r\n9371\r\n10773\r\n12933\r\n\r\n4706\r\n3934\r\n8085\r\n3861\r\n3474\r\n4037\r\n4333\r\n1915\r\n7439\r\n4724\r\n1584\r\n\r\n5587\r\n3880\r\n5972\r\n1568\r\n2925\r\n6189\r\n5112\r\n1034\r\n5146\r\n4456\r\n3302\r\n5930\r\n6509\r\n2232\r\n\r\n10574\r\n13136\r\n19197\r\n12598\r\n\r\n3319\r\n5343\r\n6042\r\n15163\r\n7853\r\n\r\n2549\r\n9485\r\n3150\r\n3717\r\n4202\r\n8582\r\n9314\r\n1959\r\n\r\n1651\r\n3584\r\n2353\r\n4100\r\n4642\r\n4105\r\n2675\r\n3241\r\n1441\r\n1093\r\n1012\r\n4275\r\n3727\r\n4598\r\n2407\r\n\r\n9617\r\n2108\r\n4265\r\n5498\r\n2914\r\n7770\r\n5898\r\n9319\r\n2295\r\n\r\n8587\r\n\r\n2644\r\n3717\r\n4921\r\n2297\r\n4454\r\n5287\r\n2479\r\n4581\r\n1824\r\n3108\r\n4297\r\n1055\r\n4726\r\n1859\r\n\r\n2170\r\n16319\r\n2974\r\n\r\n3092\r\n4627\r\n5913\r\n4495\r\n3009\r\n4501\r\n4341\r\n4668\r\n4858\r\n3240\r\n1347\r\n3448\r\n3748\r\n5811\r\n\r\n4644\r\n4571\r\n2358\r\n5466\r\n2255\r\n5075\r\n5466\r\n4229\r\n2211\r\n4262\r\n2900\r\n1966\r\n2627\r\n4728\r\n\r\n7682\r\n9016\r\n4473\r\n8449\r\n9257\r\n2068\r\n10641\r\n\r\n1499\r\n6224\r\n7304\r\n5201\r\n3451\r\n4984\r\n7737\r\n1728\r\n5302\r\n1062\r\n2178\r\n\r\n30845\r\n12424\r\n\r\n3483\r\n1244\r\n1700\r\n8543\r\n12788\r\n\r\n2430\r\n9187\r\n9178\r\n3168\r\n10377\r\n8889\r\n\r\n15000\r\n12965\r\n10098\r\n5507\r\n13346\r\n\r\n5401\r\n2056\r\n7490\r\n9384\r\n3312\r\n7124\r\n7199\r\n5571\r\n6029\r\n\r\n1218\r\n10456\r\n6922\r\n1815\r\n12092\r\n11303\r\n2461\r\n\r\n1766\r\n5195\r\n4704\r\n1476\r\n4579\r\n1104\r\n3018\r\n6172\r\n3826\r\n6543\r\n1323\r\n6306\r\n6367\r\n\r\n17963\r\n2012\r\n1664\r\n15479\r\n\r\n32869\r\n\r\n9473\r\n3761\r\n6631\r\n2522\r\n2878\r\n2435\r\n9087\r\n3261\r\n6032\r\n\r\n4461\r\n1087\r\n4793\r\n1273\r\n1848\r\n2882\r\n1662\r\n4454\r\n1347\r\n5600\r\n2619\r\n\r\n2300\r\n8563\r\n3317\r\n2462\r\n1118\r\n6131\r\n5801\r\n4281\r\n7292\r\n\r\n3848\r\n13902\r\n7899\r\n10164\r\n12727\r\n8578\r\n\r\n1076\r\n6192\r\n3308\r\n2648\r\n1919\r\n6489\r\n6050\r\n1639\r\n6490\r\n2768\r\n1169\r\n4718\r\n5799\r\n2658\r\n\r\n12611\r\n18417\r\n13289\r\n16060\r\n\r\n1806\r\n6019\r\n1058\r\n13468\r\n8292\r\n9682\r\n\r\n13648\r\n24310\r\n6025\r\n\r\n10030\r\n7199\r\n5366\r\n6080\r\n8944\r\n3416\r\n3746\r\n3597\r\n\r\n8809\r\n10699\r\n5362\r\n8302\r\n3712\r\n2911\r\n2598\r\n5279\r\n\r\n23190\r\n15323\r\n2535\r\n\r\n2663\r\n10325\r\n9513\r\n2403\r\n6443\r\n6731\r\n8188\r\n9559\r\n\r\n15302\r\n\r\n4544\r\n6035\r\n4595\r\n5512\r\n7162\r\n1160\r\n2866\r\n5003\r\n6449\r\n3936\r\n2083\r\n1154\r\n\r\n16473\r\n\r\n4333\r\n2861\r\n2754\r\n5775\r\n5774\r\n3313\r\n1763\r\n3326\r\n3888\r\n2639\r\n3981\r\n1788\r\n6272\r\n2151\r\n\r\n3830\r\n11759\r\n9430\r\n7412\r\n5190\r\n5926\r\n9828\r\n\r\n19524\r\n15948\r\n5297\r\n16516\r\n\r\n8073\r\n9705\r\n7872\r\n5308\r\n10279\r\n3645\r\n7983\r\n3467\r\n\r\n12230\r\n22031\r\n6843\r\n\r\n15329\r\n6540\r\n\r\n4960\r\n5485\r\n1829\r\n5062\r\n4777\r\n4193\r\n4594\r\n5705\r\n1282\r\n4546\r\n6063\r\n4304\r\n2533\r\n\r\n51798\r\n\r\n11965\r\n12511\r\n2156\r\n13850\r\n10025\r\n12395\r\n\r\n3476\r\n1232\r\n7227\r\n2031\r\n4457\r\n5152\r\n3616\r\n5374\r\n4576\r\n2413\r\n\r\n1045\r\n24929\r\n\r\n13068\r\n14356\r\n9552\r\n3897\r\n4369\r\n\r\n3172\r\n3437\r\n2380\r\n5542\r\n4850\r\n4325\r\n1065\r\n1051\r\n3385\r\n5905\r\n1182\r\n6138\r\n6487\r\n2307\r\n\r\n11847\r\n9674\r\n1409\r\n16351\r\n3793\r\n\r\n1567\r\n6941\r\n2520\r\n6617\r\n4815\r\n4700\r\n2297\r\n2336\r\n3626\r\n8035\r\n1777\r\n\r\n7380\r\n6231\r\n1464\r\n5680\r\n4298\r\n8018\r\n5861\r\n5967\r\n2799\r\n6888\r\n1401\r\n\r\n1944\r\n3424\r\n4104\r\n5697\r\n2082\r\n1829\r\n4128\r\n3775\r\n3450\r\n5312\r\n3475\r\n4868\r\n4857\r\n1469\r\n4757\r\n\r\n22339\r\n15143\r\n\r\n4573\r\n5384\r\n2968\r\n6696\r\n2208\r\n3068\r\n3104\r\n2654\r\n5539\r\n7329\r\n6127\r\n3874\r\n\r\n6693\r\n6878\r\n3318\r\n5526\r\n2042\r\n4895\r\n3809\r\n3718\r\n5881\r\n2444\r\n2448\r\n\r\n6304\r\n5014\r\n1337\r\n5766\r\n9098\r\n4456\r\n6604\r\n1722\r\n4674\r\n\r\n2403\r\n5059\r\n2439\r\n2113\r\n3879\r\n2289\r\n6524\r\n3707\r\n5199\r\n2908\r\n4624\r\n3742\r\n6077\r\n\r\n2717\r\n3042\r\n1843\r\n2403\r\n5750\r\n2473\r\n3017\r\n3407\r\n4392\r\n2513\r\n6913\r\n6585\r\n\r\n1996\r\n1118\r\n5230\r\n6283\r\n2639\r\n3324\r\n1236\r\n3483\r\n5270\r\n3399\r\n3111\r\n6098\r\n\r\n6418\r\n3395\r\n3771\r\n5816\r\n2494\r\n1049\r\n5846\r\n4483\r\n3137\r\n4445\r\n2248\r\n1216\r\n2499\r\n3556\r\n\r\n20769\r\n22782\r\n15068\r\n\r\n2645\r\n3388\r\n2219\r\n6061\r\n6664\r\n4330\r\n5003\r\n4035\r\n6370\r\n2023\r\n6309\r\n5013\r\n\r\n1222\r\n6095\r\n7410\r\n7957\r\n4087\r\n5677\r\n6829\r\n7151\r\n8731\r\n8168\r\n\r\n11570\r\n27420\r\n\r\n14541\r\n3035\r\n10295\r\n14965\r\n\r\n4122\r\n5939\r\n8715\r\n1188\r\n4674\r\n4654\r\n7742\r\n4976\r\n\r\n5269\r\n3341\r\n7796\r\n8918\r\n\r\n9598\r\n2036\r\n10718\r\n9696\r\n8890\r\n2709\r\n4114\r\n1940\r\n\r\n5054\r\n3311\r\n2306\r\n2633\r\n1810\r\n4733\r\n2133\r\n2185\r\n2322\r\n2396\r\n2992\r\n2202\r\n2671\r\n1932\r\n3398\r\n\r\n4357\r\n1706\r\n3417\r\n2442\r\n5608\r\n5032\r\n6296\r\n5015\r\n1874\r\n5295\r\n3812\r\n4698\r\n\r\n18153\r\n16982\r\n6152\r\n\r\n16245\r\n\r\n3048\r\n3827\r\n3394\r\n2137\r\n2364\r\n5478\r\n5992\r\n2396\r\n6139\r\n2323\r\n4458\r\n2292\r\n4003\r\n\r\n1561\r\n1154\r\n1545\r\n1144\r\n2206\r\n4454\r\n5640\r\n4697\r\n1908\r\n3175\r\n5130\r\n2386\r\n6358\r\n4169\r\n\r\n5785\r\n6697\r\n5246\r\n3215\r\n9759\r\n10601\r\n5507\r\n\r\n4892\r\n7629\r\n4323\r\n3881\r\n4247\r\n2154\r\n1352\r\n3468\r\n3053\r\n\r\n6710\r\n2117\r\n4776\r\n3991\r\n4021\r\n7543\r\n4268\r\n3355\r\n4701\r\n4663\r\n1015\r\n\r\n13221\r\n14163\r\n11213\r\n\r\n9488\r\n7173\r\n3708\r\n3524\r\n9290\r\n3390\r\n1272\r\n3325\r\n4864\r\n\r\n3098\r\n6656\r\n1387\r\n4710\r\n1962\r\n7262\r\n7442\r\n2097\r\n4544\r\n1976\r\n6250\r\n1974\r\n\r\n5122\r\n1843\r\n2929\r\n4066\r\n2403\r\n1020\r\n2723\r\n5914\r\n3821\r\n2189\r\n1047\r\n3334\r\n5773\r\n4396\r\n2777\r\n\r\n2073\r\n2094\r\n4901\r\n1093\r\n5029\r\n5458\r\n5963\r\n4011\r\n3454\r\n3174\r\n2086\r\n1451\r\n5640\r\n2311\r\n2511\r\n\r\n4243\r\n7521\r\n5018\r\n1914\r\n6762\r\n8155\r\n\r\n19726\r\n11376\r\n6504\r\n4086\r\n\r\n4150\r\n7015\r\n3007\r\n5002\r\n5151\r\n8346\r\n8024\r\n6498\r\n1696\r\n\r\n5404\r\n5249\r\n1474\r\n2284\r\n1642\r\n3323\r\n4110\r\n1796\r\n5132\r\n1940\r\n1266\r\n2679\r\n4027\r\n2289\r\n\r\n10301\r\n7013\r\n1666\r\n7987\r\n10426\r\n6423\r\n10008\r\n10015\r\n\r\n6115\r\n6361\r\n1902\r\n8036\r\n5703\r\n2771\r\n7330\r\n8606\r\n6447\r\n\r\n8917\r\n1183\r\n8966\r\n1255\r\n5988\r\n5233\r\n7358\r\n3806\r\n9333";
    }

    public string Day2Data
    {
        get => "A Z\r\nC Z\r\nC Z\r\nA Z\r\nC Y\r\nC Z\r\nA Z\r\nB Z\r\nA Z\r\nA Z\r\nC Z\r\nA X\r\nC Y\r\nC Z\r\nC Y\r\nC Z\r\nA Z\r\nC Z\r\nA Z\r\nA Z\r\nC Z\r\nC Z\r\nC Z\r\nA Y\r\nC Z\r\nC Z\r\nC Y\r\nA Z\r\nC X\r\nA Z\r\nA Z\r\nC Z\r\nB Y\r\nC Z\r\nC Y\r\nC X\r\nC Z\r\nC Z\r\nB Y\r\nA Z\r\nC Z\r\nC Y\r\nB Y\r\nC Z\r\nA Y\r\nB Y\r\nC Z\r\nC Z\r\nA Z\r\nA Z\r\nA X\r\nA Z\r\nB Z\r\nA Z\r\nA X\r\nA Y\r\nC Z\r\nC Z\r\nA Z\r\nC Y\r\nA Z\r\nC Z\r\nB Y\r\nC Z\r\nC Y\r\nB Z\r\nA Z\r\nC Z\r\nA Z\r\nB Z\r\nA X\r\nC Z\r\nA Z\r\nC Y\r\nC Y\r\nC Z\r\nA Z\r\nC Z\r\nC Y\r\nC Z\r\nC Z\r\nC Z\r\nC Z\r\nC Y\r\nA Z\r\nC Z\r\nC Y\r\nA Z\r\nA Z\r\nA Z\r\nC Z\r\nA X\r\nA Z\r\nA Z\r\nA Z\r\nC Z\r\nC Y\r\nC Y\r\nA Z\r\nA Z\r\nC Y\r\nA Z\r\nC Z\r\nA Z\r\nC Y\r\nB Y\r\nC Z\r\nA Z\r\nA Z\r\nC Z\r\nA Z\r\nC Y\r\nA Z\r\nA Z\r\nC Y\r\nA Z\r\nC Z\r\nA Z\r\nA Z\r\nA Z\r\nC Y\r\nA Z\r\nB Z\r\nC Y\r\nC Z\r\nB Y\r\nA Z\r\nA Z\r\nC Z\r\nC Z\r\nB X\r\nB Y\r\nA Z\r\nA Z\r\nA Z\r\nC Z\r\nC Y\r\nB Z\r\nC Z\r\nC Z\r\nC Z\r\nA Y\r\nA Z\r\nA Z\r\nC Y\r\nC Z\r\nA Z\r\nC Z\r\nA Z\r\nA Z\r\nC Z\r\nC Z\r\nC Z\r\nC Y\r\nC Y\r\nC Y\r\nC Z\r\nA Z\r\nC Z\r\nA Z\r\nA Z\r\nC Z\r\nA Z\r\nC Z\r\nA Z\r\nA Z\r\nB Z\r\nA Z\r\nA Z\r\nC Z\r\nC Z\r\nC Z\r\nC Z\r\nC Z\r\nC Y\r\nB X\r\nA X\r\nA Y\r\nA X\r\nC Z\r\nA X\r\nB X\r\nA X\r\nB Z\r\nA Z\r\nC Z\r\nC Z\r\nC Z\r\nC Z\r\nB Z\r\nC Z\r\nA Z\r\nA Z\r\nA Z\r\nA Z\r\nC Z\r\nA Z\r\nA Z\r\nC Z\r\nA Z\r\nC Y\r\nC Z\r\nC Z\r\nB Y\r\nC Z\r\nA X\r\nA Z\r\nA Z\r\nC Y\r\nC Z\r\nC Z\r\nA Z\r\nC Z\r\nA Z\r\nC Z\r\nA Z\r\nC Y\r\nA Z\r\nB Y\r\nC Z\r\nC Z\r\nA Z\r\nC Z\r\nC Y\r\nC Z\r\nA Z\r\nA Z\r\nC Z\r\nC Z\r\nA Z\r\nA Z\r\nC Z\r\nC Y\r\nC Z\r\nC Z\r\nC Z\r\nC Y\r\nC Z\r\nA Z\r\nC Z\r\nA Z\r\nA Z\r\nA Z\r\nB Z\r\nA Z\r\nC Y\r\nA Y\r\nA Z\r\nC Z\r\nA Z\r\nA Z\r\nC Z\r\nA Z\r\nC Z\r\nC Z\r\nC Z\r\nA Z\r\nB Z\r\nC Y\r\nC Z\r\nC Z\r\nB X\r\nC Z\r\nC Y\r\nA Z\r\nA Y\r\nC Z\r\nC Y\r\nB Y\r\nA Z\r\nC Z\r\nA Z\r\nB Y\r\nC Z\r\nC Z\r\nC Z\r\nB Z\r\nB Z\r\nC Z\r\nC Z\r\nC Z\r\nC Z\r\nA Z\r\nA X\r\nB Y\r\nC Z\r\nC Z\r\nA Z\r\nC Y\r\nA Z\r\nC Z\r\nA Z\r\nC Y\r\nC Z\r\nA Z\r\nC Y\r\nC Y\r\nC Z\r\nA Z\r\nC Z\r\nC Z\r\nC Z\r\nA X\r\nA Z\r\nC Z\r\nC Z\r\nC Z\r\nA Z\r\nC Z\r\nC Z\r\nC Y\r\nC Y\r\nC Y\r\nC Z\r\nC Y\r\nC Z\r\nA Z\r\nA Z\r\nC Z\r\nC Z\r\nC Y\r\nA Y\r\nC Z\r\nC Z\r\nB Z\r\nA Z\r\nA Z\r\nA Y\r\nA Z\r\nB Y\r\nA Z\r\nC Y\r\nA Z\r\nC Y\r\nA Z\r\nA Z\r\nA X\r\nB Z\r\nC Y\r\nA Z\r\nC Z\r\nA Y\r\nC Z\r\nC Y\r\nB Y\r\nA Z\r\nB Z\r\nB Z\r\nC Z\r\nA X\r\nB Y\r\nC Z\r\nA Y\r\nC Z\r\nA Z\r\nC Z\r\nB Z\r\nC Y\r\nC Z\r\nA Z\r\nC Z\r\nC Y\r\nA Z\r\nA X\r\nA Z\r\nA Y\r\nC Z\r\nA Z\r\nC Y\r\nA Z\r\nC Z\r\nC Y\r\nC Z\r\nB X\r\nC Z\r\nC Z\r\nC Z\r\nC Z\r\nC Z\r\nC Z\r\nC Y\r\nC Y\r\nC Z\r\nB Z\r\nB Z\r\nC Z\r\nB Z\r\nA Z\r\nC Z\r\nC Y\r\nC Z\r\nC Z\r\nA Z\r\nC Z\r\nB Y\r\nC Z\r\nA Z\r\nC Z\r\nA Z\r\nC Z\r\nA Z\r\nC Y\r\nA Z\r\nC Z\r\nC Z\r\nC Z\r\nC Z\r\nC Z\r\nC Y\r\nC Y\r\nA Z\r\nA Z\r\nC Z\r\nA Z\r\nA Z\r\nB Y\r\nA Z\r\nA Z\r\nC Z\r\nC Y\r\nA Z\r\nC Z\r\nA Z\r\nA Z\r\nC Z\r\nC Z\r\nC Z\r\nA Z\r\nC Y\r\nC Z\r\nC Z\r\nC Y\r\nC Y\r\nC Y\r\nA Z\r\nC Z\r\nA Z\r\nC Z\r\nB Y\r\nA X\r\nC Z\r\nC Z\r\nC Z\r\nC Z\r\nB Z\r\nA Z\r\nC Z\r\nA Z\r\nB Z\r\nB X\r\nC Z\r\nB X\r\nC Z\r\nC Z\r\nA Z\r\nB X\r\nC Z\r\nA Z\r\nA Z\r\nB Y\r\nC Z\r\nC Z\r\nB X\r\nC Y\r\nA Z\r\nA Z\r\nB Z\r\nA Z\r\nC Z\r\nA Z\r\nB Y\r\nA Z\r\nC Z\r\nB Y\r\nC Y\r\nC Z\r\nC X\r\nC Z\r\nC Z\r\nA X\r\nC Z\r\nA Y\r\nC Z\r\nA X\r\nA Z\r\nC Z\r\nC Z\r\nC Z\r\nB Y\r\nC Y\r\nA Z\r\nC Z\r\nA Z\r\nC Z\r\nC Z\r\nC Z\r\nC Y\r\nC Z\r\nC Z\r\nB Y\r\nA Z\r\nC Z\r\nC Z\r\nC Z\r\nA Z\r\nC Z\r\nA Z\r\nB Z\r\nC Z\r\nA Z\r\nC Z\r\nA Z\r\nC Y\r\nC Z\r\nC Z\r\nC Z\r\nC Z\r\nA Z\r\nA Z\r\nB Z\r\nA Z\r\nB Z\r\nC Z\r\nC Z\r\nC Y\r\nC Z\r\nA Z\r\nA Z\r\nC Z\r\nA Z\r\nB Y\r\nA Z\r\nC Z\r\nC Z\r\nA Z\r\nA Z\r\nA Y\r\nA Z\r\nC Z\r\nA Z\r\nC Y\r\nC Z\r\nC Z\r\nC Z\r\nA Z\r\nC Z\r\nC Z\r\nB X\r\nA Z\r\nA Z\r\nC Y\r\nB Z\r\nC Z\r\nA Z\r\nA Z\r\nA Y\r\nC Z\r\nC Z\r\nC Z\r\nA Z\r\nC Z\r\nC Z\r\nC Z\r\nB Y\r\nA Z\r\nB Z\r\nC Y\r\nB Y\r\nA Z\r\nA Z\r\nA Z\r\nC Z\r\nC Y\r\nA Z\r\nA Z\r\nC Z\r\nC Z\r\nA Z\r\nB Z\r\nC Z\r\nA Z\r\nA Z\r\nC Z\r\nA Z\r\nA Z\r\nA X\r\nB Z\r\nC Z\r\nC Z\r\nC Z\r\nC Z\r\nC Y\r\nA Z\r\nC Z\r\nC Y\r\nC Z\r\nB Y\r\nA Z\r\nA Z\r\nC Z\r\nC Z\r\nA Z\r\nC Y\r\nC Z\r\nA Z\r\nA Z\r\nC Z\r\nC Z\r\nC Y\r\nB Y\r\nC Z\r\nA Z\r\nA Z\r\nC Z\r\nC Z\r\nC Y\r\nC Z\r\nA Z\r\nC Z\r\nA Z\r\nC Z\r\nA Z\r\nC Y\r\nC Z\r\nC Z\r\nC Y\r\nA Z\r\nB Y\r\nC Z\r\nA Z\r\nC Z\r\nC Z\r\nB Z\r\nB Z\r\nA Z\r\nC Z\r\nC Y\r\nC Z\r\nC Z\r\nA Z\r\nB Y\r\nB Z\r\nC Z\r\nA Z\r\nC Z\r\nC Y\r\nC Z\r\nC Z\r\nC Y\r\nC Z\r\nA Y\r\nA Z\r\nA Z\r\nA Z\r\nB Y\r\nC Z\r\nC Z\r\nC Z\r\nC Y\r\nC Z\r\nA Z\r\nC Y\r\nB Y\r\nC Z\r\nA Z\r\nA Z\r\nC Z\r\nC Z\r\nA Z\r\nA X\r\nB Z\r\nA Z\r\nC Z\r\nA Z\r\nC Z\r\nC Y\r\nA Z\r\nA Z\r\nC Z\r\nC Z\r\nB Z\r\nB Y\r\nA X\r\nA Z\r\nA Z\r\nA Z\r\nA Z\r\nC Z\r\nC Z\r\nC Z\r\nC Z\r\nC Y\r\nA Z\r\nA Z\r\nC Y\r\nC X\r\nA Z\r\nA Z\r\nA Z\r\nA Z\r\nC Z\r\nC Z\r\nC Z\r\nC Z\r\nC Z\r\nA X\r\nC Z\r\nA Z\r\nB Y\r\nB Y\r\nA Z\r\nA Z\r\nC Z\r\nC Y\r\nC Z\r\nA Z\r\nA Z\r\nA X\r\nA Z\r\nC Z\r\nA Z\r\nA Y\r\nA Z\r\nC Z\r\nB Y\r\nB Y\r\nC Z\r\nB Y\r\nA Z\r\nB Y\r\nC Z\r\nB Z\r\nB Y\r\nC Y\r\nC Z\r\nC Y\r\nC Z\r\nA Z\r\nC Y\r\nC Z\r\nC Z\r\nC Z\r\nA Z\r\nC Z\r\nC Y\r\nC Z\r\nB Z\r\nC Z\r\nA Z\r\nB Y\r\nB Z\r\nC Z\r\nB Y\r\nC Z\r\nA Z\r\nC Z\r\nC Z\r\nA Z\r\nC Y\r\nC Z\r\nB Y\r\nA Z\r\nB X\r\nC Y\r\nC X\r\nC Y\r\nA Z\r\nB Z\r\nA Z\r\nA Y\r\nB Y\r\nC Z\r\nA Z\r\nC Z\r\nC Y\r\nC Z\r\nA Z\r\nB Y\r\nA Z\r\nA Z\r\nB Y\r\nC Y\r\nA Y\r\nC Y\r\nC Z\r\nC Z\r\nA Z\r\nC Z\r\nC Z\r\nA Y\r\nC Z\r\nA Z\r\nC Z\r\nC Z\r\nB Y\r\nB Y\r\nC Z\r\nB Z\r\nA Z\r\nA Z\r\nA Z\r\nC Z\r\nC Y\r\nC Z\r\nB Y\r\nC Y\r\nA Z\r\nA Z\r\nC Z\r\nB Y\r\nC Y\r\nB Y\r\nC Z\r\nA Z\r\nB Y\r\nA Z\r\nA Z\r\nA X\r\nC Z\r\nC Z\r\nC Y\r\nA Z\r\nC Y\r\nC Y\r\nC Z\r\nA Z\r\nA X\r\nC Z\r\nA Z\r\nA Z\r\nA Z\r\nC Z\r\nC Y\r\nA Z\r\nA X\r\nC Z\r\nC Z\r\nC Y\r\nC Y\r\nA Z\r\nC Z\r\nB Z\r\nB X\r\nA Z\r\nC Z\r\nB Y\r\nC Y\r\nA Z\r\nA Z\r\nC Y\r\nC Z\r\nC Y\r\nC Y\r\nB Y\r\nC Z\r\nC Z\r\nA Z\r\nB Y\r\nC Z\r\nC Z\r\nA Z\r\nA Z\r\nB Y\r\nC Z\r\nC Y\r\nA X\r\nA Z\r\nC Y\r\nA Z\r\nC Z\r\nC Z\r\nA Z\r\nC Y\r\nA Z\r\nC Z\r\nC Z\r\nC Z\r\nC Z\r\nA Z\r\nC Z\r\nC Y\r\nB Z\r\nC Z\r\nA Z\r\nC Z\r\nC Z\r\nB Y\r\nC Z\r\nA Z\r\nA Z\r\nA Z\r\nC Y\r\nC Z\r\nA Z\r\nA Z\r\nC Z\r\nA Z\r\nA Z\r\nC Z\r\nA Z\r\nC X\r\nA Z\r\nA Z\r\nC Z\r\nA Y\r\nB Z\r\nA Z\r\nC Y\r\nA Z\r\nC Y\r\nB Z\r\nC Z\r\nB X\r\nC Z\r\nA Z\r\nA Z\r\nC Z\r\nB Y\r\nC Z\r\nC Z\r\nB Z\r\nA Z\r\nB Y\r\nC Z\r\nC Z\r\nC Z\r\nA Z\r\nC Z\r\nB Z\r\nA Z\r\nC Z\r\nA Z\r\nB Z\r\nA X\r\nC Y\r\nA X\r\nC Z\r\nC Y\r\nC Z\r\nC Z\r\nB Y\r\nB Y\r\nC Z\r\nB X\r\nC Z\r\nA Z\r\nA Z\r\nB X\r\nA Z\r\nC Z\r\nC Z\r\nC Z\r\nB Y\r\nC Z\r\nC Y\r\nA Z\r\nB Z\r\nC Y\r\nC Z\r\nC Z\r\nA Z\r\nC Z\r\nA Y\r\nC Z\r\nC Z\r\nC Z\r\nA Z\r\nC Z\r\nC Z\r\nC Y\r\nC Z\r\nB Y\r\nB X\r\nA Z\r\nA Z\r\nC Z\r\nC Y\r\nC Y\r\nC Y\r\nB Y\r\nA X\r\nC Z\r\nB Y\r\nA Z\r\nA Z\r\nC Z\r\nB Y\r\nB Y\r\nC Z\r\nA Z\r\nC Z\r\nC Z\r\nC Z\r\nC Z\r\nB Y\r\nC Z\r\nB Y\r\nB Y\r\nA Z\r\nC Z\r\nB Y\r\nA Z\r\nA X\r\nC Z\r\nC Z\r\nC Z\r\nC Z\r\nC Z\r\nC Z\r\nC Z\r\nB Y\r\nA Z\r\nB Z\r\nA Z\r\nC Z\r\nA Z\r\nC Z\r\nA Z\r\nC Z\r\nA Z\r\nA Z\r\nC Y\r\nA Z\r\nA Z\r\nC Z\r\nC Z\r\nC Z\r\nC Z\r\nB Z\r\nC Z\r\nA X\r\nA Y\r\nB Y\r\nB Z\r\nA Z\r\nC Y\r\nA Z\r\nB X\r\nA Z\r\nC Z\r\nB Z\r\nB Y\r\nC Z\r\nC Z\r\nC Y\r\nC Z\r\nA Z\r\nA Z\r\nA Z\r\nA Z\r\nA Z\r\nA Z\r\nA Z\r\nA X\r\nA Z\r\nA Z\r\nC Z\r\nC Z\r\nC Z\r\nA Z\r\nB Z\r\nC Z\r\nC Z\r\nA X\r\nC Y\r\nA Z\r\nC Z\r\nC Y\r\nA Z\r\nA Z\r\nC Z\r\nC Z\r\nA Z\r\nC Z\r\nC X\r\nC Y\r\nA Z\r\nA Z\r\nC Z\r\nB Y\r\nC Z\r\nA Z\r\nA Z\r\nA Z\r\nB Z\r\nA Y\r\nB Y\r\nC Z\r\nC Z\r\nC Y\r\nC Y\r\nB Y\r\nA Z\r\nA Z\r\nB Y\r\nC Z\r\nC Z\r\nA Z\r\nA Y\r\nA X\r\nA X\r\nC Z\r\nA Z\r\nA X\r\nA Y\r\nA Z\r\nC Y\r\nA Z\r\nC Y\r\nA Z\r\nC Y\r\nB Z\r\nB Y\r\nA Z\r\nC Z\r\nA Z\r\nC Z\r\nC Y\r\nA Z\r\nC Z\r\nA Z\r\nB Y\r\nA Z\r\nC Z\r\nC Z\r\nC Z\r\nA Y\r\nC Z\r\nC Z\r\nC Z\r\nA Z\r\nC Y\r\nA Y\r\nC X\r\nC Z\r\nA Z\r\nA Z\r\nA Z\r\nA Z\r\nA Z\r\nB Y\r\nB Y\r\nC Z\r\nC Z\r\nC Y\r\nA Z\r\nC Z\r\nC Y\r\nC Z\r\nC Y\r\nA Z\r\nA Z\r\nC Z\r\nA Z\r\nC Z\r\nC Y\r\nA Z\r\nC Y\r\nA Z\r\nC Z\r\nC Y\r\nB Y\r\nC Y\r\nC Y\r\nA Z\r\nA Z\r\nC Y\r\nC Z\r\nC Y\r\nB Z\r\nA Z\r\nC Z\r\nC Z\r\nB Y\r\nC Y\r\nA Z\r\nA Z\r\nC Z\r\nA Z\r\nB Z\r\nC Z\r\nA Z\r\nC Z\r\nC Z\r\nA Z\r\nA Y\r\nB Y\r\nC Y\r\nC Z\r\nA Z\r\nC Z\r\nA Z\r\nC Z\r\nA Z\r\nC Y\r\nC Y\r\nC Z\r\nA Z\r\nA Z\r\nB Y\r\nC Z\r\nA Z\r\nC Z\r\nA Z\r\nA Z\r\nC Z\r\nC Z\r\nC Z\r\nC Y\r\nB Y\r\nC Z\r\nA Z\r\nB Z\r\nA Z\r\nA X\r\nC Z\r\nA Z\r\nA Z\r\nC Z\r\nB Y\r\nA Z\r\nA X\r\nC Y\r\nC Z\r\nC Z\r\nC Z\r\nC Z\r\nB Y\r\nC Z\r\nB Z\r\nA Z\r\nA Z\r\nA Z\r\nA Z\r\nB Z\r\nB X\r\nC Z\r\nA Z\r\nC Y\r\nA X\r\nC Z\r\nC Y\r\nC Z\r\nC Z\r\nB X\r\nC Z\r\nC Y\r\nA Z\r\nC Z\r\nA Z\r\nC Z\r\nB Z\r\nA Z\r\nC Z\r\nB Y\r\nC Z\r\nC Z\r\nC Z\r\nC Z\r\nC Z\r\nC Z\r\nA Z\r\nC Z\r\nA Z\r\nA Z\r\nB Y\r\nA Z\r\nC Y\r\nB Y\r\nB X\r\nC Z\r\nC Y\r\nC Z\r\nC Z\r\nC Y\r\nC Y\r\nB Y\r\nC Y\r\nC Z\r\nA Z\r\nA Z\r\nA X\r\nC Z\r\nC Z\r\nB Y\r\nC Z\r\nC Z\r\nA Z\r\nC Z\r\nB X\r\nC Z\r\nC Z\r\nC Y\r\nC Z\r\nC Z\r\nC Z\r\nA X\r\nC Z\r\nA Z\r\nA Z\r\nC Y\r\nB Z\r\nA Y\r\nC Z\r\nC Y\r\nB Y\r\nC Z\r\nC Z\r\nC Z\r\nC Z\r\nA Z\r\nC Z\r\nC Z\r\nA Z\r\nC Z\r\nC Y\r\nC Z\r\nC Z\r\nA Y\r\nC Z\r\nC Y\r\nA Z\r\nC Z\r\nC Y\r\nC Z\r\nC Y\r\nC Y\r\nB X\r\nA Z\r\nC Z\r\nC Y\r\nC Y\r\nA Z\r\nA X\r\nC Z\r\nA Z\r\nC X\r\nB Y\r\nA X\r\nB Z\r\nC Z\r\nB Y\r\nA Z\r\nC Z\r\nA Z\r\nC Z\r\nA X\r\nC Z\r\nC Z\r\nC Z\r\nC Y\r\nA Z\r\nC Y\r\nC Z\r\nC Z\r\nA Z\r\nA Z\r\nA Z\r\nC Z\r\nA Z\r\nC Y\r\nC Z\r\nA Z\r\nA Z\r\nC Z\r\nA Z\r\nC Z\r\nC Z\r\nC Z\r\nC Z\r\nB Y\r\nA Z\r\nC Z\r\nA Z\r\nC Y\r\nC Z\r\nA Z\r\nC Y\r\nC Z\r\nA Z\r\nC Y\r\nC Y\r\nA Z\r\nA Z\r\nA Z\r\nC Z\r\nC Z\r\nB Y\r\nA X\r\nA X\r\nA Z\r\nC Z\r\nC Y\r\nC Y\r\nC Y\r\nC Z\r\nA Z\r\nB Y\r\nA Z\r\nA Z\r\nC Z\r\nC Y\r\nC Z\r\nC Y\r\nC Z\r\nA Z\r\nC Z\r\nC Z\r\nC Z\r\nC Y\r\nA Z\r\nC Z\r\nB X\r\nC Z\r\nC Z\r\nC Z\r\nC Z\r\nA Z\r\nC Z\r\nC Z\r\nC Z\r\nC Y\r\nC Z\r\nA Z\r\nC Z\r\nB Y\r\nA Z\r\nC Y\r\nA Y\r\nC Z\r\nC Z\r\nB Z\r\nC Z\r\nC Z\r\nB Y\r\nC Z\r\nA Z\r\nC Y\r\nC Z\r\nB Y\r\nC Z\r\nA Z\r\nC Z\r\nC Z\r\nC Z\r\nA Z\r\nB Y\r\nC Y\r\nA Z\r\nC X\r\nC Z\r\nC Y\r\nC Z\r\nA Z\r\nA Z\r\nC Y\r\nC Z\r\nA Z\r\nB Y\r\nA X\r\nC Y\r\nB Y\r\nA Z\r\nB Y\r\nA X\r\nB Z\r\nA Z\r\nA Z\r\nA Z\r\nA Z\r\nC Z\r\nC Z\r\nA Z\r\nA Z\r\nA X\r\nC Z\r\nA Z\r\nA Z\r\nA X\r\nB Y\r\nC Z\r\nC Z\r\nC Z\r\nB Z\r\nC Z\r\nA Z\r\nC Z\r\nC Z\r\nA Z\r\nA Z\r\nC Y\r\nC Z\r\nC Z\r\nA Z\r\nA Z\r\nC Y\r\nC Y\r\nC Z\r\nA Z\r\nB X\r\nA Z\r\nA Z\r\nC Z\r\nC Z\r\nC Z\r\nB Y\r\nA Z\r\nC Z\r\nA X\r\nB Z\r\nB Z\r\nC Z\r\nC Z\r\nC Y\r\nA X\r\nA Z\r\nB Y\r\nC Z\r\nB Y\r\nC Z\r\nB Y\r\nC Z\r\nC Y\r\nC Z\r\nA Z\r\nC Y\r\nC Y\r\nA Z\r\nC X\r\nB Z\r\nC Z\r\nC Z\r\nA Z\r\nC Y\r\nA Z\r\nA Z\r\nB Z\r\nA Z\r\nC Z\r\nC Z\r\nC Z\r\nC Z\r\nA Y\r\nC Y\r\nA Z\r\nC Z\r\nC Z\r\nC Y\r\nA Z\r\nB Y\r\nA Z\r\nB Z\r\nA Z\r\nA Z\r\nB X\r\nB Z\r\nA Z\r\nC Z\r\nC Z\r\nC Z\r\nC Z\r\nC Z\r\nA Z\r\nC X\r\nC Z\r\nC X\r\nC Z\r\nB Z\r\nC Y\r\nA X\r\nA Z\r\nA Z\r\nC Z\r\nC Y\r\nC Z\r\nC Z\r\nA Z\r\nC Z\r\nA Z\r\nA Z\r\nA X\r\nC Z\r\nA Z\r\nA Z\r\nA Z\r\nA Z\r\nA Z\r\nA Z\r\nB Y\r\nA Z\r\nC Z\r\nB X\r\nB Z\r\nC Z\r\nA Z\r\nA Z\r\nB Z\r\nC Y\r\nA X\r\nC Z\r\nB Y\r\nB Y\r\nC Y\r\nA Z\r\nA Z\r\nA X\r\nC Z\r\nC Z\r\nA Z\r\nC Y\r\nB Y\r\nA Z\r\nB X\r\nA Z\r\nA X\r\nA X\r\nA X\r\nA Z\r\nA Z\r\nC Z\r\nA Z\r\nA Z\r\nA X\r\nB X\r\nB Z\r\nB X\r\nA Z\r\nB Y\r\nA Z\r\nC Z\r\nA Z\r\nA Z\r\nC Z\r\nA Y\r\nC Z\r\nB Z\r\nC Y\r\nC Y\r\nA Z\r\nC Z\r\nC Z\r\nA Z\r\nB Y\r\nC Z\r\nA Z\r\nA X\r\nB Z\r\nA Y\r\nB Z\r\nC Y\r\nC Y\r\nC Z\r\nA Z\r\nC Z\r\nC Z\r\nA Y\r\nC Z\r\nA Z\r\nB Y\r\nC Z\r\nB X\r\nA Z\r\nC Z\r\nC Y\r\nB Y\r\nA Z\r\nB Y\r\nB Z\r\nA Z\r\nA Z\r\nB Y\r\nC Z\r\nA Z\r\nC X\r\nC Z\r\nC Z\r\nB Z\r\nB Z\r\nC Z\r\nA Z\r\nA Z\r\nC Z\r\nA Z\r\nC Z\r\nC Z\r\nA Y\r\nC Z\r\nA X\r\nA Z\r\nA Z\r\nA Z\r\nA Z\r\nA Y\r\nA X\r\nC Z\r\nC Z\r\nC Z\r\nC Z\r\nA Z\r\nB Z\r\nC Z\r\nB Z\r\nB Y\r\nC Z\r\nC Z\r\nC Z\r\nA Z\r\nC Z\r\nB Y\r\nC Y\r\nA X\r\nA Z\r\nA X\r\nC Z\r\nB X\r\nC Z\r\nA Z\r\nB Y\r\nC X\r\nA Z\r\nC Z\r\nC Z\r\nA Z\r\nC Z\r\nA Z\r\nA Z\r\nA Z\r\nA Z\r\nA Y\r\nA Y\r\nA Z\r\nA Z\r\nA Z\r\nA Z\r\nC Z\r\nC Z\r\nC Z\r\nA Z\r\nA Z\r\nC Z\r\nA Y\r\nC Z\r\nB Z\r\nC X\r\nC Z\r\nC Y\r\nA Y\r\nB Y\r\nC Z\r\nC Z\r\nA Z\r\nC Z\r\nA X\r\nB Y\r\nC Y\r\nC Z\r\nC Z\r\nA Z\r\nA Y\r\nA Z\r\nA Z\r\nC Z\r\nA Z\r\nC Z\r\nC Z\r\nC X\r\nA Z\r\nC Z\r\nA Z\r\nA Z\r\nC Y\r\nB Y\r\nC Z\r\nA X\r\nA Z\r\nC Z\r\nA Z\r\nC X\r\nA Z\r\nC Z\r\nC Y\r\nA Z\r\nA Z\r\nC Z\r\nC Z\r\nC Z\r\nA Z\r\nA Z\r\nC Z\r\nA Z\r\nA X\r\nC X\r\nC Z\r\nC Z\r\nA Z\r\nA Z\r\nA Y\r\nC Z\r\nC Z\r\nB X\r\nC Z\r\nC Y\r\nC Z\r\nC Z\r\nA Z\r\nC Z\r\nA Z\r\nC Y\r\nC Y\r\nC Z\r\nA Z\r\nC Z\r\nB Y\r\nC Y\r\nA Z\r\nB Z\r\nA Z\r\nB Z\r\nA Z\r\nA Z\r\nC Z\r\nC Z\r\nC Z\r\nA Z\r\nC Z\r\nA Z\r\nB Z\r\nC Z\r\nC Z\r\nC Z\r\nA Z\r\nC Z\r\nC Z\r\nC Z\r\nC Z\r\nC Z\r\nC Z\r\nC Z\r\nB Y\r\nA Z\r\nB Y\r\nC Y\r\nC X\r\nC Z\r\nC Z\r\nC Z\r\nC Z\r\nC Y\r\nA Z\r\nC Y\r\nA Z\r\nC Y\r\nC Z\r\nC Y\r\nC Z\r\nC Y\r\nA Z\r\nA Z\r\nC Z\r\nC Z\r\nB Z\r\nC Y\r\nC Z\r\nC Z\r\nC Z\r\nB Z\r\nB Z\r\nB Z\r\nA Z\r\nA Z\r\nA Z\r\nC Z\r\nB Z\r\nA Z\r\nA Z\r\nC Y\r\nA Y\r\nC Y\r\nC Z\r\nC Y\r\nC Z\r\nC Z\r\nC Y\r\nA Z\r\nC Z\r\nB Y\r\nC Y\r\nB Y\r\nA Z\r\nC Z\r\nA Z\r\nA X\r\nA Z\r\nC Z\r\nA Z\r\nA Z\r\nA Z\r\nC X\r\nC Z\r\nA Z\r\nC Z\r\nC Z\r\nC Z\r\nC Z\r\nC Z\r\nC Z\r\nA Z\r\nC Z\r\nC Y\r\nA Y\r\nB Y\r\nB X\r\nB Z\r\nC Z\r\nC Z\r\nA Z\r\nB Y\r\nC Z\r\nA Z\r\nB Z\r\nC Z\r\nA Z\r\nC Y\r\nC Y\r\nB Y\r\nC Z\r\nC Y\r\nC Z\r\nC Y\r\nA Z\r\nC Z\r\nC Z\r\nA Z\r\nA Z\r\nA Z\r\nC Z\r\nA Z\r\nA X\r\nA Z\r\nA Z\r\nC Z\r\nC Y\r\nC Z\r\nC Z\r\nC Z\r\nA Y\r\nB Y\r\nA Z\r\nA Z\r\nA Z\r\nA X\r\nB Z\r\nA Z\r\nC Y\r\nB Y\r\nA Z\r\nA Y\r\nB Z\r\nA Z\r\nB Y\r\nC Z\r\nB Y\r\nA Z\r\nB Y\r\nC Z\r\nC Z\r\nA Z\r\nA Z\r\nA Z\r\nC Y\r\nB Y\r\nB X\r\nA Z\r\nC Z\r\nA X\r\nB Z\r\nA X\r\nA Y\r\nC Z\r\nB X\r\nB Y\r\nA Z\r\nA Z\r\nB X\r\nC Z\r\nC Y\r\nC Y\r\nC Y\r\nC X\r\nC Z\r\nC Z\r\nC Z\r\nA Z\r\nB Y\r\nC Z\r\nA Z\r\nA Z\r\nA Z\r\nA Z\r\nC Y\r\nC Z\r\nC Z\r\nA Z\r\nC Z\r\nB Z\r\nB Y\r\nA Z\r\nA Z\r\nC Z\r\nA X\r\nA Y\r\nB Y\r\nA Z\r\nA Z\r\nB Z\r\nA Z\r\nC Z\r\nA Y\r\nA Z\r\nC Z\r\nC Z\r\nA Z\r\nA Z\r\nC Z\r\nB Y\r\nA Z\r\nA X\r\nC Y\r\nA Z\r\nC Z\r\nC Z\r\nC Z\r\nC Z\r\nA Z\r\nC Y\r\nC Z\r\nA Z\r\nA Z\r\nC Y\r\nA X\r\nC Z\r\nB Y\r\nA Z\r\nB Z\r\nC Z\r\nB Z\r\nA Z\r\nC Y\r\nC Z\r\nB Y\r\nC Y\r\nC Z\r\nA Z\r\nC Z\r\nC Z\r\nA Y\r\nA Z\r\nB Z\r\nA X\r\nC Y\r\nA Z\r\nA Z\r\nC Z\r\nB Y\r\nC Z\r\nA Z\r\nC Z\r\nA Z\r\nB Y\r\nA Z\r\nC Z\r\nA Z\r\nB Y\r\nA Z\r\nC Z\r\nC Z\r\nC Z\r\nA Z\r\nA Z\r\nC Z\r\nA Z\r\nA Z\r\nC Y\r\nA Y\r\nB Y\r\nB Z\r\nC Z\r\nC Y\r\nA Z\r\nC Y\r\nC Z\r\nB Y\r\nC Y\r\nC Z\r\nB Z\r\nC Z\r\nC Y\r\nC Z\r\nC Y\r\nC Z\r\nC Y\r\nB X\r\nC Z\r\nC Y\r\nA Z\r\nA X\r\nA Z\r\nC Z\r\nA Z\r\nB Y\r\nC Z\r\nA Z\r\nA Z\r\nB Y\r\nC Y\r\nA Z\r\nA Z\r\nC Z\r\nC Z\r\nC Y\r\nA Z\r\nC Y\r\nB Y\r\nA Y\r\nC Z\r\nA Z\r\nA Z\r\nC Z\r\nC Z\r\nC Z\r\nC Z\r\nA Z\r\nA Z\r\nC Y\r\nA Z\r\nC Z\r\nC Z\r\nC Z\r\nC Z\r\nA Z\r\nA Y\r\nC Y\r\nA Z\r\nC Y\r\nA Z\r\nA Z\r\nA Z\r\nC Z\r\nA Z\r\nC Z\r\nA Z\r\nC Z\r\nC Z\r\nA Z\r\nC Z\r\nC Z\r\nC Y\r\nB Z\r\nC Z\r\nC Y\r\nA Z\r\nB X\r\nC Y\r\nC Y\r\nC Z\r\nA Z\r\nB Y\r\nA Z\r\nA X\r\nB Y\r\nA Z\r\nA Z\r\nB Y\r\nB Z\r\nA Z\r\nA Z\r\nC Z\r\nA X\r\nC Z\r\nC Z\r\nB Z\r\nC Z\r\nC Y\r\nB Y\r\nA Z\r\nB Z\r\nC Z\r\nC Z\r\nC Z\r\nA Y\r\nA X\r\nC Y\r\nA Z\r\nB Z\r\nA Z\r\nB Z\r\nC Z\r\nA Y\r\nC Z\r\nC Y\r\nA Z\r\nC Y\r\nC Z\r\nC Y\r\nB Y\r\nB Y\r\nC Z\r\nA X\r\nC Z\r\nA Z\r\nA Z\r\nC Y\r\nC Y\r\nC Z\r\nA Z\r\nB Z\r\nC Z\r\nC Z\r\nC Z\r\nA Y\r\nA Z\r\nC Z\r\nC Z\r\nC Z\r\nC Z\r\nC Z\r\nB Y\r\nC Z\r\nC Z\r\nA Z\r\nB Y\r\nA Z\r\nC Z\r\nC Z\r\nC Z\r\nB Z\r\nA Z\r\nC Y\r\nA Z\r\nC Z\r\nB Z\r\nC Z\r\nB Y\r\nC Y\r\nA X\r\nC Z\r\nA Z\r\nC Y\r\nC Z\r\nC Z\r\nC Y\r\nA Z\r\nC Z\r\nB Z\r\nA Z\r\nC Z\r\nA Z\r\nA X\r\nC Z\r\nA Z\r\nC Z\r\nB X\r\nC Z\r\nC Z\r\nC Z\r\nC Z\r\nA Z\r\nA X\r\nC Z\r\nC Z\r\nA Z\r\nC Z\r\nA Z\r\nC Z\r\nB Y\r\nA Z\r\nC Z\r\nA Z\r\nC Y\r\nC Z\r\nC Z\r\nA Z\r\nC Z\r\nA Z\r\nA Z\r\nA Z\r\nB Z\r\nC Y\r\nB X\r\nC Y\r\nC Z\r\nC Z\r\nA X\r\nA Z\r\nC Z\r\nB Y\r\nC Z\r\nC Y\r\nC Z\r\nA Z\r\nC Z\r\nB Y\r\nC Z\r\nC Y\r\nB Z\r\nA Z\r\nC Z\r\nB Z\r\nA Z\r\nB Z\r\nA Z\r\nC Z\r\nC Z\r\nB Y\r\nC Y\r\nC Y\r\nA Z\r\nC Z\r\nA Z\r\nA Y\r\nB Y\r\nC X\r\nA X\r\nC Z\r\nC Y\r\nB Y\r\nC Z\r\nA Z\r\nA Z\r\nC Y\r\nC Z\r\nC Z\r\nA Z\r\nB Y\r\nB Z\r\nA Z\r\nA Z\r\nA Z\r\nB Z\r\nC Z\r\nC Z\r\nC Y\r\nA X\r\nC Z\r\nA Z\r\nC Z\r\nB Z\r\nB Y\r\nC X\r\nC Z\r\nA X\r\nA X\r\nC Y\r\nB Y\r\nC Z\r\nC Z\r\nA Z\r\nA Z\r\nA Z\r\nA Z\r\nC Y\r\nC Z\r\nA Z\r\nA Z\r\nC Z\r\nC Z\r\nC Z\r\nC Z\r\nC Z\r\nA Y\r\nA Z\r\nC Y\r\nC Z\r\nB Y\r\nA Z\r\nC Y\r\nA Z\r\nA Z\r\nB Z\r\nC X\r\nB Y\r\nC Z\r\nA Z\r\nC Y\r\nC Z\r\nB Y\r\nA Z\r\nC Y\r\nC Z\r\nB Y\r\nC Y\r\nC Z\r\nA Z\r\nA X\r\nC Z\r\nC Y\r\nB Z\r\nA Z\r\nA Z\r\nB Y\r\nC Z\r\nA Z\r\nC Z\r\nC Z\r\nB Y\r\nA Z\r\nC Z\r\nC Z\r\nB Y\r\nB Y\r\nA Z\r\nC Z\r\nC Z\r\nC Z\r\nC Z\r\nA Z\r\nC Z\r\nB Z\r\nB Y\r\nC Z\r\nB Y\r\nA Z\r\nA Z\r\nC Y\r\nA Z\r\nA X\r\nB Z\r\nC Y\r\nC Z\r\nC Z\r\nC Z\r\nC Y\r\nC Z\r\nC Z\r\nC Y\r\nA X\r\nB X\r\nB X\r\nC Z\r\nC Y\r\nA Z\r\nB Y\r\nC Y\r\nC Z\r\nC Z\r\nA Z\r\nC Y\r\nA Z\r\nB Z\r\nC Z\r\nC Y\r\nC Y\r\nC Z\r\nA Z\r\nC Z\r\nA Z\r\nB Z\r\nB Z\r\nC Y\r\nA Z\r\nC Z\r\nA Z\r\nA Z\r\nC Z\r\nC Z\r\nC Z\r\nC Z\r\nC Z\r\nC Z\r\nA Z\r\nA Z\r\nA Z\r\nB Y\r\nB Z\r\nB Y\r\nC Y\r\nA Z\r\nC Z\r\nC Y\r\nA Z\r\nC Z\r\nA Z\r\nA X\r\nB Z\r\nA Z\r\nC Z\r\nC Z\r\nC Z\r\nA Z\r\nC Y\r\nB Y\r\nC Y\r\nA Z\r\nC Y\r\nB Z\r\nB Y\r\nB Y\r\nC Z\r\nC Z\r\nA Z";
    }

    public RPSRaces Day2()
    {
        List<RPSRace> races = new();
        foreach(var raceString in Day2Data.Split("\r\n")){
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
        foreach (var raceString in Day2Data.Split("\r\n"))
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

        foreach (string backpack in Day1Data().Split("\r\n\r\n"))
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
    private readonly string Day5Stacks = "    [C]         [Q]         [V]    \r\n    [D]         [D] [S]     [M] [Z]\r\n    [G]     [P] [W] [M]     [C] [G]\r\n    [F]     [Z] [C] [D] [P] [S] [W]\r\n[P] [L]     [C] [V] [W] [W] [H] [L]\r\n[G] [B] [V] [R] [L] [N] [G] [P] [F]\r\n[R] [T] [S] [S] [S] [T] [D] [L] [P]\r\n[N] [J] [M] [L] [P] [C] [H] [Z] [R]\r\n 1   2   3   4   5   6   7   8   9";

    private readonly string Day5Moves = "move 2 from 4 to 6\r\nmove 4 from 5 to 3\r\nmove 6 from 6 to 1\r\nmove 4 from 1 to 4\r\nmove 4 from 9 to 4\r\nmove 7 from 2 to 4\r\nmove 1 from 9 to 3\r\nmove 1 from 2 to 6\r\nmove 2 from 9 to 5\r\nmove 2 from 6 to 8\r\nmove 5 from 8 to 1\r\nmove 2 from 6 to 9\r\nmove 5 from 8 to 3\r\nmove 1 from 5 to 4\r\nmove 3 from 7 to 2\r\nmove 10 from 4 to 7\r\nmove 7 from 4 to 3\r\nmove 1 from 4 to 7\r\nmove 1 from 7 to 9\r\nmove 1 from 2 to 3\r\nmove 11 from 1 to 7\r\nmove 12 from 3 to 7\r\nmove 8 from 3 to 8\r\nmove 29 from 7 to 2\r\nmove 3 from 7 to 3\r\nmove 3 from 9 to 2\r\nmove 4 from 5 to 3\r\nmove 7 from 3 to 5\r\nmove 28 from 2 to 3\r\nmove 1 from 7 to 5\r\nmove 2 from 8 to 5\r\nmove 2 from 4 to 1\r\nmove 2 from 1 to 4\r\nmove 1 from 7 to 6\r\nmove 1 from 7 to 1\r\nmove 3 from 2 to 8\r\nmove 1 from 1 to 7\r\nmove 9 from 5 to 3\r\nmove 12 from 3 to 1\r\nmove 1 from 4 to 3\r\nmove 1 from 6 to 4\r\nmove 3 from 2 to 9\r\nmove 16 from 3 to 7\r\nmove 2 from 9 to 6\r\nmove 5 from 7 to 2\r\nmove 1 from 9 to 7\r\nmove 1 from 4 to 2\r\nmove 13 from 7 to 2\r\nmove 13 from 2 to 7\r\nmove 12 from 7 to 8\r\nmove 2 from 6 to 4\r\nmove 16 from 8 to 1\r\nmove 4 from 3 to 1\r\nmove 3 from 3 to 2\r\nmove 1 from 5 to 7\r\nmove 1 from 5 to 3\r\nmove 3 from 4 to 6\r\nmove 19 from 1 to 3\r\nmove 5 from 8 to 4\r\nmove 6 from 3 to 2\r\nmove 5 from 4 to 2\r\nmove 1 from 7 to 4\r\nmove 1 from 4 to 9\r\nmove 3 from 6 to 7\r\nmove 1 from 9 to 2\r\nmove 16 from 2 to 4\r\nmove 9 from 1 to 8\r\nmove 10 from 4 to 2\r\nmove 2 from 7 to 5\r\nmove 5 from 8 to 4\r\nmove 12 from 2 to 9\r\nmove 2 from 7 to 4\r\nmove 12 from 9 to 5\r\nmove 11 from 5 to 6\r\nmove 3 from 1 to 9\r\nmove 1 from 5 to 7\r\nmove 2 from 9 to 2\r\nmove 10 from 3 to 2\r\nmove 1 from 9 to 2\r\nmove 2 from 8 to 9\r\nmove 1 from 7 to 8\r\nmove 1 from 8 to 4\r\nmove 7 from 2 to 6\r\nmove 1 from 1 to 5\r\nmove 5 from 3 to 1\r\nmove 1 from 5 to 1\r\nmove 2 from 3 to 9\r\nmove 2 from 1 to 6\r\nmove 3 from 9 to 8\r\nmove 14 from 6 to 1\r\nmove 1 from 3 to 5\r\nmove 5 from 4 to 6\r\nmove 1 from 9 to 6\r\nmove 7 from 6 to 9\r\nmove 1 from 6 to 2\r\nmove 8 from 1 to 4\r\nmove 7 from 1 to 7\r\nmove 10 from 2 to 1\r\nmove 4 from 7 to 6\r\nmove 10 from 4 to 6\r\nmove 5 from 8 to 2\r\nmove 1 from 5 to 9\r\nmove 2 from 2 to 6\r\nmove 2 from 4 to 7\r\nmove 1 from 2 to 7\r\nmove 5 from 9 to 2\r\nmove 1 from 2 to 9\r\nmove 14 from 6 to 8\r\nmove 2 from 8 to 4\r\nmove 1 from 2 to 6\r\nmove 4 from 9 to 3\r\nmove 2 from 6 to 8\r\nmove 5 from 4 to 5\r\nmove 5 from 8 to 3\r\nmove 1 from 2 to 4\r\nmove 3 from 7 to 1\r\nmove 2 from 2 to 7\r\nmove 1 from 4 to 7\r\nmove 1 from 4 to 5\r\nmove 1 from 2 to 8\r\nmove 1 from 4 to 9\r\nmove 8 from 8 to 2\r\nmove 3 from 1 to 5\r\nmove 7 from 2 to 9\r\nmove 8 from 1 to 6\r\nmove 6 from 7 to 2\r\nmove 2 from 2 to 8\r\nmove 5 from 1 to 8\r\nmove 3 from 6 to 8\r\nmove 4 from 3 to 6\r\nmove 3 from 6 to 2\r\nmove 8 from 9 to 2\r\nmove 11 from 5 to 7\r\nmove 12 from 2 to 6\r\nmove 2 from 3 to 7\r\nmove 12 from 7 to 2\r\nmove 10 from 6 to 9\r\nmove 1 from 7 to 1\r\nmove 12 from 8 to 7\r\nmove 2 from 3 to 2\r\nmove 8 from 9 to 7\r\nmove 6 from 2 to 5\r\nmove 1 from 1 to 6\r\nmove 3 from 2 to 6\r\nmove 1 from 3 to 7\r\nmove 5 from 5 to 3\r\nmove 10 from 7 to 2\r\nmove 2 from 3 to 7\r\nmove 8 from 7 to 6\r\nmove 20 from 2 to 8\r\nmove 5 from 8 to 1\r\nmove 5 from 8 to 6\r\nmove 1 from 5 to 7\r\nmove 1 from 1 to 4\r\nmove 4 from 1 to 2\r\nmove 1 from 9 to 6\r\nmove 3 from 3 to 1\r\nmove 4 from 7 to 5\r\nmove 1 from 9 to 8\r\nmove 11 from 8 to 7\r\nmove 1 from 4 to 9\r\nmove 2 from 7 to 5\r\nmove 31 from 6 to 9\r\nmove 4 from 2 to 3\r\nmove 6 from 5 to 1\r\nmove 4 from 1 to 2\r\nmove 7 from 7 to 8\r\nmove 1 from 7 to 6\r\nmove 1 from 1 to 7\r\nmove 24 from 9 to 4\r\nmove 2 from 7 to 8\r\nmove 2 from 9 to 2\r\nmove 2 from 7 to 5\r\nmove 2 from 5 to 9\r\nmove 3 from 4 to 1\r\nmove 20 from 4 to 2\r\nmove 1 from 6 to 1\r\nmove 16 from 2 to 1\r\nmove 4 from 3 to 1\r\nmove 1 from 4 to 8\r\nmove 5 from 8 to 5\r\nmove 5 from 8 to 1\r\nmove 1 from 5 to 2\r\nmove 3 from 5 to 6\r\nmove 33 from 1 to 6\r\nmove 6 from 9 to 4\r\nmove 15 from 6 to 7\r\nmove 6 from 4 to 3\r\nmove 1 from 5 to 3\r\nmove 7 from 3 to 9\r\nmove 11 from 7 to 5\r\nmove 10 from 5 to 8\r\nmove 2 from 7 to 3\r\nmove 5 from 8 to 9\r\nmove 1 from 7 to 5\r\nmove 1 from 5 to 8\r\nmove 1 from 5 to 7\r\nmove 2 from 3 to 8\r\nmove 2 from 7 to 5\r\nmove 2 from 8 to 7\r\nmove 1 from 5 to 9\r\nmove 1 from 7 to 6\r\nmove 3 from 8 to 6\r\nmove 22 from 6 to 9\r\nmove 1 from 7 to 6\r\nmove 27 from 9 to 4\r\nmove 18 from 4 to 8\r\nmove 5 from 4 to 1\r\nmove 1 from 5 to 1\r\nmove 3 from 6 to 3\r\nmove 2 from 3 to 5\r\nmove 2 from 5 to 2\r\nmove 1 from 2 to 6\r\nmove 1 from 6 to 3\r\nmove 9 from 8 to 6\r\nmove 3 from 9 to 8\r\nmove 9 from 6 to 5\r\nmove 1 from 6 to 9\r\nmove 15 from 8 to 5\r\nmove 1 from 3 to 4\r\nmove 6 from 1 to 8\r\nmove 1 from 3 to 7\r\nmove 8 from 5 to 8\r\nmove 2 from 5 to 6\r\nmove 3 from 4 to 6\r\nmove 1 from 7 to 6\r\nmove 2 from 5 to 3\r\nmove 5 from 5 to 1\r\nmove 2 from 3 to 7\r\nmove 1 from 8 to 1\r\nmove 10 from 2 to 9\r\nmove 5 from 6 to 3\r\nmove 7 from 8 to 5\r\nmove 4 from 3 to 5\r\nmove 1 from 2 to 1\r\nmove 2 from 7 to 6\r\nmove 5 from 1 to 5\r\nmove 1 from 3 to 7\r\nmove 1 from 7 to 6\r\nmove 3 from 8 to 5\r\nmove 4 from 6 to 4\r\nmove 1 from 2 to 9\r\nmove 5 from 4 to 6\r\nmove 21 from 5 to 3\r\nmove 2 from 8 to 4\r\nmove 3 from 4 to 1\r\nmove 1 from 8 to 4\r\nmove 18 from 3 to 5\r\nmove 2 from 3 to 6\r\nmove 2 from 6 to 9\r\nmove 2 from 6 to 2\r\nmove 1 from 2 to 9\r\nmove 19 from 9 to 4\r\nmove 3 from 6 to 3\r\nmove 2 from 9 to 4\r\nmove 1 from 1 to 2\r\nmove 1 from 3 to 7\r\nmove 16 from 5 to 2\r\nmove 4 from 1 to 9\r\nmove 3 from 3 to 4\r\nmove 4 from 9 to 8\r\nmove 3 from 5 to 1\r\nmove 22 from 4 to 5\r\nmove 1 from 7 to 2\r\nmove 22 from 5 to 9\r\nmove 2 from 5 to 2\r\nmove 2 from 4 to 6\r\nmove 10 from 9 to 5\r\nmove 1 from 8 to 3\r\nmove 13 from 9 to 2\r\nmove 1 from 6 to 3\r\nmove 19 from 2 to 7\r\nmove 2 from 7 to 4\r\nmove 1 from 8 to 4\r\nmove 1 from 8 to 2\r\nmove 11 from 5 to 7\r\nmove 3 from 1 to 7\r\nmove 8 from 7 to 8\r\nmove 1 from 3 to 5\r\nmove 1 from 8 to 3\r\nmove 1 from 5 to 3\r\nmove 6 from 2 to 3\r\nmove 1 from 8 to 7\r\nmove 1 from 6 to 1\r\nmove 1 from 1 to 8\r\nmove 4 from 8 to 1\r\nmove 1 from 4 to 6\r\nmove 8 from 3 to 9\r\nmove 2 from 2 to 3\r\nmove 3 from 8 to 5\r\nmove 1 from 8 to 2\r\nmove 4 from 2 to 7\r\nmove 5 from 9 to 7\r\nmove 1 from 6 to 3\r\nmove 4 from 2 to 4\r\nmove 23 from 7 to 5\r\nmove 4 from 1 to 2\r\nmove 3 from 9 to 6\r\nmove 2 from 4 to 8\r\nmove 2 from 8 to 3\r\nmove 2 from 6 to 1\r\nmove 1 from 6 to 8\r\nmove 8 from 5 to 3\r\nmove 5 from 2 to 6\r\nmove 5 from 6 to 3\r\nmove 1 from 8 to 3\r\nmove 4 from 4 to 7\r\nmove 15 from 5 to 2\r\nmove 1 from 1 to 9\r\nmove 2 from 5 to 1\r\nmove 4 from 3 to 7\r\nmove 1 from 4 to 9\r\nmove 4 from 7 to 1\r\nmove 2 from 5 to 6\r\nmove 7 from 1 to 2\r\nmove 6 from 2 to 3\r\nmove 16 from 2 to 5\r\nmove 1 from 6 to 3\r\nmove 1 from 6 to 3\r\nmove 9 from 7 to 4\r\nmove 6 from 4 to 6\r\nmove 1 from 9 to 8\r\nmove 23 from 3 to 9\r\nmove 1 from 3 to 4\r\nmove 3 from 4 to 5\r\nmove 9 from 5 to 2\r\nmove 6 from 9 to 7\r\nmove 7 from 7 to 5\r\nmove 5 from 5 to 3\r\nmove 1 from 4 to 6\r\nmove 3 from 3 to 8\r\nmove 6 from 2 to 1\r\nmove 3 from 5 to 6\r\nmove 4 from 7 to 1\r\nmove 2 from 3 to 9\r\nmove 5 from 6 to 8\r\nmove 19 from 9 to 6\r\nmove 1 from 9 to 2\r\nmove 9 from 5 to 9\r\nmove 4 from 8 to 3\r\nmove 5 from 6 to 1\r\nmove 4 from 6 to 1\r\nmove 2 from 3 to 8\r\nmove 17 from 1 to 7\r\nmove 2 from 1 to 2\r\nmove 6 from 6 to 9\r\nmove 4 from 8 to 5\r\nmove 3 from 8 to 2\r\nmove 3 from 5 to 6\r\nmove 4 from 6 to 8\r\nmove 2 from 6 to 9\r\nmove 4 from 8 to 7\r\nmove 9 from 9 to 5\r\nmove 5 from 9 to 4\r\nmove 7 from 2 to 8\r\nmove 1 from 2 to 1\r\nmove 3 from 6 to 5\r\nmove 6 from 8 to 5\r\nmove 1 from 3 to 4\r\nmove 1 from 3 to 1\r\nmove 12 from 7 to 2\r\nmove 5 from 2 to 7\r\nmove 8 from 7 to 5\r\nmove 1 from 9 to 3\r\nmove 5 from 2 to 8\r\nmove 3 from 6 to 3\r\nmove 2 from 2 to 3\r\nmove 1 from 2 to 4\r\nmove 2 from 3 to 4\r\nmove 1 from 1 to 6\r\nmove 14 from 5 to 6\r\nmove 1 from 8 to 6\r\nmove 3 from 3 to 7\r\nmove 4 from 7 to 1\r\nmove 9 from 4 to 3\r\nmove 3 from 1 to 4\r\nmove 1 from 1 to 2\r\nmove 1 from 8 to 4\r\nmove 8 from 3 to 1\r\nmove 1 from 3 to 2\r\nmove 5 from 7 to 6\r\nmove 3 from 1 to 6\r\nmove 2 from 2 to 8\r\nmove 13 from 5 to 3\r\nmove 5 from 1 to 3\r\nmove 3 from 4 to 5\r\nmove 1 from 9 to 2\r\nmove 4 from 3 to 9\r\nmove 1 from 1 to 7\r\nmove 2 from 5 to 8\r\nmove 1 from 7 to 5\r\nmove 2 from 5 to 4\r\nmove 1 from 2 to 6\r\nmove 1 from 4 to 5\r\nmove 7 from 3 to 6\r\nmove 31 from 6 to 1\r\nmove 25 from 1 to 7\r\nmove 2 from 3 to 2\r\nmove 13 from 7 to 9\r\nmove 1 from 1 to 6\r\nmove 1 from 4 to 1\r\nmove 2 from 2 to 9\r\nmove 1 from 4 to 6\r\nmove 3 from 7 to 1\r\nmove 7 from 8 to 3\r\nmove 1 from 8 to 2\r\nmove 1 from 2 to 8\r\nmove 4 from 3 to 4\r\nmove 1 from 8 to 7\r\nmove 3 from 6 to 9\r\nmove 5 from 7 to 6\r\nmove 1 from 4 to 7\r\nmove 5 from 7 to 9\r\nmove 5 from 3 to 6\r\nmove 3 from 4 to 7\r\nmove 1 from 5 to 4\r\nmove 4 from 7 to 9\r\nmove 32 from 9 to 1\r\nmove 1 from 6 to 5\r\nmove 1 from 5 to 9\r\nmove 4 from 3 to 8\r\nmove 5 from 1 to 4\r\nmove 4 from 4 to 9\r\nmove 6 from 1 to 7\r\nmove 4 from 9 to 8\r\nmove 4 from 7 to 8\r\nmove 1 from 7 to 1\r\nmove 1 from 7 to 6\r\nmove 7 from 6 to 3\r\nmove 1 from 9 to 5\r\nmove 2 from 4 to 7\r\nmove 25 from 1 to 6\r\nmove 1 from 7 to 1\r\nmove 1 from 3 to 4\r\nmove 18 from 6 to 8\r\nmove 1 from 5 to 1\r\nmove 3 from 1 to 6\r\nmove 21 from 8 to 3\r\nmove 1 from 8 to 4\r\nmove 2 from 4 to 2\r\nmove 1 from 8 to 1\r\nmove 1 from 7 to 6\r\nmove 5 from 6 to 3\r\nmove 30 from 3 to 1\r\nmove 4 from 8 to 6\r\nmove 1 from 2 to 9\r\nmove 1 from 8 to 5\r\nmove 9 from 6 to 5\r\nmove 2 from 8 to 7\r\nmove 3 from 5 to 9\r\nmove 2 from 3 to 4\r\nmove 1 from 2 to 1\r\nmove 1 from 5 to 8\r\nmove 1 from 8 to 3\r\nmove 2 from 4 to 6\r\nmove 1 from 3 to 1\r\nmove 1 from 5 to 6\r\nmove 5 from 5 to 7\r\nmove 4 from 6 to 8\r\nmove 3 from 8 to 2\r\nmove 1 from 1 to 5\r\nmove 1 from 8 to 7\r\nmove 4 from 9 to 6\r\nmove 1 from 5 to 1\r\nmove 4 from 6 to 8\r\nmove 6 from 7 to 3\r\nmove 4 from 3 to 9\r\nmove 2 from 2 to 7\r\nmove 1 from 3 to 5\r\nmove 3 from 7 to 6\r\nmove 2 from 9 to 8\r\nmove 1 from 2 to 4\r\nmove 1 from 3 to 4\r\nmove 5 from 8 to 4\r\nmove 1 from 9 to 2\r\nmove 1 from 7 to 5\r\nmove 3 from 4 to 5\r\nmove 1 from 9 to 1\r\nmove 1 from 2 to 7\r\nmove 1 from 7 to 1\r\nmove 5 from 5 to 4\r\nmove 4 from 1 to 4\r\nmove 19 from 1 to 9\r\nmove 6 from 6 to 2\r\nmove 12 from 9 to 1\r\nmove 1 from 8 to 6\r\nmove 1 from 9 to 4\r\nmove 4 from 4 to 8\r\nmove 1 from 6 to 5\r\nmove 1 from 5 to 3\r\nmove 2 from 8 to 9\r\nmove 5 from 4 to 6\r\nmove 5 from 9 to 4\r\nmove 1 from 4 to 3\r\nmove 2 from 2 to 9\r\nmove 1 from 6 to 5\r\nmove 1 from 6 to 9\r\nmove 7 from 1 to 5\r\nmove 1 from 3 to 1\r\nmove 2 from 8 to 3\r\nmove 1 from 5 to 7\r\nmove 2 from 9 to 8";

    private readonly string day4 = "7-7,8-42\r\n2-95,2-94\r\n10-54,33-90\r\n23-24,24-40\r\n1-48,12-47\r\n9-27,9-26\r\n24-33,25-45\r\n16-26,15-26\r\n28-38,38-40\r\n26-27,27-48\r\n39-97,20-96\r\n21-73,21-73\r\n15-28,27-63\r\n6-91,91-91\r\n52-80,81-96\r\n18-99,17-83\r\n90-97,91-96\r\n9-91,10-82\r\n8-97,9-99\r\n67-73,66-74\r\n48-63,11-63\r\n35-90,36-92\r\n9-94,1-9\r\n7-10,9-95\r\n19-46,18-47\r\n41-82,48-83\r\n35-81,36-84\r\n5-92,2-91\r\n54-94,53-94\r\n12-27,26-81\r\n17-26,8-14\r\n18-84,9-85\r\n49-64,65-82\r\n39-84,38-83\r\n20-65,64-66\r\n16-95,16-80\r\n31-36,37-78\r\n19-71,19-19\r\n12-90,11-89\r\n12-47,11-48\r\n10-11,10-74\r\n30-90,6-89\r\n17-26,16-25\r\n15-35,14-36\r\n29-29,28-28\r\n34-34,26-33\r\n11-26,12-76\r\n94-95,29-94\r\n62-65,62-64\r\n58-61,9-59\r\n12-75,11-13\r\n8-20,11-24\r\n66-95,66-86\r\n36-37,37-91\r\n6-17,16-18\r\n13-94,94-94\r\n83-96,75-92\r\n12-81,12-80\r\n4-68,3-46\r\n85-87,66-86\r\n8-31,30-37\r\n4-94,2-5\r\n14-30,33-84\r\n22-92,21-22\r\n4-95,20-95\r\n35-78,35-78\r\n8-80,8-8\r\n3-69,23-76\r\n11-98,10-96\r\n11-85,20-83\r\n24-58,11-31\r\n7-96,8-97\r\n66-77,78-97\r\n40-86,40-87\r\n53-79,53-79\r\n37-46,36-38\r\n1-91,1-92\r\n14-21,9-14\r\n15-37,38-38\r\n3-58,1-58\r\n59-85,51-86\r\n1-69,3-70\r\n3-74,36-75\r\n34-74,35-86\r\n8-98,7-75\r\n79-86,78-98\r\n23-97,23-93\r\n1-2,2-92\r\n68-77,68-78\r\n4-57,1-57\r\n99-99,53-96\r\n15-62,61-88\r\n13-65,9-14\r\n25-82,82-82\r\n48-54,47-77\r\n16-26,27-97\r\n16-18,17-91\r\n10-95,2-9\r\n98-99,7-98\r\n19-36,14-20\r\n6-30,7-84\r\n1-1,1-99\r\n24-52,11-51\r\n92-92,45-88\r\n88-89,88-90\r\n4-84,3-53\r\n37-52,36-38\r\n20-93,6-93\r\n63-63,17-62\r\n8-22,5-9\r\n52-77,52-52\r\n44-46,45-87\r\n14-82,13-74\r\n56-68,41-68\r\n80-80,16-80\r\n4-95,3-94\r\n57-95,63-96\r\n77-94,75-93\r\n55-91,91-91\r\n15-15,1-15\r\n44-98,45-45\r\n14-21,6-13\r\n4-42,2-4\r\n5-90,8-60\r\n32-62,33-50\r\n18-51,18-54\r\n4-30,3-4\r\n28-51,28-50\r\n93-96,31-94\r\n3-15,14-81\r\n15-74,84-89\r\n5-93,4-92\r\n1-5,6-90\r\n53-55,54-85\r\n20-90,20-57\r\n97-98,48-97\r\n27-48,26-36\r\n16-47,16-48\r\n94-96,88-94\r\n43-58,59-85\r\n88-90,10-84\r\n18-62,17-93\r\n14-97,14-98\r\n19-81,8-80\r\n13-94,12-89\r\n65-98,64-66\r\n36-48,35-49\r\n26-79,36-87\r\n54-62,53-69\r\n14-26,14-26\r\n18-71,70-71\r\n31-45,7-67\r\n3-94,1-4\r\n60-81,61-82\r\n16-66,66-66\r\n46-95,94-94\r\n10-89,10-90\r\n29-86,11-28\r\n55-96,54-56\r\n7-52,9-76\r\n65-93,83-92\r\n61-74,61-74\r\n72-93,93-93\r\n45-74,74-74\r\n19-98,20-98\r\n5-95,6-96\r\n21-96,96-97\r\n33-73,23-33\r\n68-85,15-42\r\n10-99,10-10\r\n4-96,93-97\r\n86-97,3-86\r\n46-86,47-87\r\n21-94,18-93\r\n64-64,28-64\r\n42-43,42-94\r\n52-97,53-98\r\n2-41,2-41\r\n12-80,11-13\r\n55-95,55-96\r\n12-35,21-36\r\n19-87,20-86\r\n56-67,37-57\r\n34-51,33-52\r\n9-92,8-91\r\n31-99,3-99\r\n23-24,24-84\r\n11-11,11-52\r\n12-72,11-46\r\n85-93,18-92\r\n33-90,99-99\r\n91-94,92-95\r\n14-68,68-69\r\n45-88,45-89\r\n7-89,4-6\r\n23-33,24-93\r\n35-84,46-83\r\n8-81,8-81\r\n50-72,40-71\r\n6-80,41-79\r\n8-64,5-7\r\n25-49,24-24\r\n43-96,7-42\r\n3-92,3-92\r\n6-46,1-45\r\n13-57,12-56\r\n55-62,32-63\r\n28-32,21-39\r\n5-6,5-46\r\n2-87,2-86\r\n10-83,8-82\r\n14-69,13-69\r\n67-69,68-89\r\n55-91,28-39\r\n23-30,29-58\r\n35-35,35-62\r\n48-63,49-65\r\n4-80,1-79\r\n2-60,2-61\r\n30-75,97-98\r\n15-95,14-40\r\n40-41,40-40\r\n69-96,27-96\r\n6-96,7-98\r\n14-98,13-15\r\n5-5,5-83\r\n2-92,3-91\r\n73-91,84-90\r\n3-97,90-96\r\n3-97,35-98\r\n73-93,73-74\r\n32-79,32-51\r\n44-80,44-79\r\n6-53,5-84\r\n41-48,42-49\r\n32-86,31-85\r\n4-86,4-86\r\n3-59,27-60\r\n24-93,10-23\r\n35-59,34-84\r\n8-93,9-9\r\n34-69,53-68\r\n64-91,63-64\r\n4-45,3-5\r\n81-88,43-87\r\n7-30,35-75\r\n47-78,5-46\r\n51-51,51-74\r\n20-67,49-76\r\n53-80,52-53\r\n2-37,1-98\r\n16-99,15-97\r\n97-97,6-97\r\n9-99,9-98\r\n38-93,24-55\r\n20-77,78-78\r\n10-76,75-76\r\n41-56,1-55\r\n39-93,39-91\r\n87-93,15-30\r\n9-98,9-85\r\n14-53,53-53\r\n70-94,11-95\r\n97-98,39-97\r\n50-81,71-72\r\n55-62,55-82\r\n9-21,9-9\r\n47-84,46-57\r\n1-32,3-31\r\n17-77,3-18\r\n20-69,20-68\r\n10-61,10-10\r\n18-97,97-98\r\n27-97,99-99\r\n10-60,1-60\r\n26-78,1-66\r\n4-10,11-11\r\n7-77,6-78\r\n29-75,74-75\r\n62-62,61-84\r\n3-82,82-82\r\n38-43,38-42\r\n2-84,3-85\r\n72-83,83-84\r\n40-92,98-98\r\n84-92,17-91\r\n36-85,14-35\r\n30-32,31-70\r\n11-61,12-12\r\n6-81,92-98\r\n16-93,16-93\r\n7-63,6-62\r\n4-98,4-97\r\n93-94,10-93\r\n19-30,18-30\r\n17-18,19-55\r\n53-98,50-98\r\n3-97,97-97\r\n2-97,2-98\r\n33-77,2-32\r\n52-53,10-52\r\n14-16,15-91\r\n29-99,17-98\r\n3-4,5-96\r\n4-92,3-4\r\n37-56,2-36\r\n28-52,26-99\r\n12-97,12-97\r\n14-43,44-72\r\n90-94,91-93\r\n5-80,10-81\r\n21-77,77-78\r\n39-91,42-90\r\n40-51,28-50\r\n4-91,5-78\r\n34-92,33-92\r\n6-8,5-99\r\n7-7,7-39\r\n54-88,53-53\r\n56-84,55-83\r\n7-80,6-8\r\n9-17,17-22\r\n19-50,51-53\r\n42-91,2-90\r\n26-27,27-69\r\n23-58,15-22\r\n22-92,25-93\r\n4-5,4-81\r\n75-89,76-76\r\n16-74,15-73\r\n3-70,4-69\r\n30-48,36-69\r\n23-40,23-94\r\n35-73,35-75\r\n78-86,31-87\r\n61-87,60-61\r\n72-91,2-92\r\n88-98,4-86\r\n14-25,13-17\r\n35-45,35-98\r\n7-92,7-99\r\n9-78,19-77\r\n16-92,12-42\r\n4-19,5-20\r\n1-95,3-96\r\n92-92,12-92\r\n80-80,80-84\r\n12-73,12-73\r\n4-92,5-93\r\n19-83,18-82\r\n12-13,12-14\r\n2-97,2-97\r\n6-93,7-58\r\n34-57,56-73\r\n6-39,13-98\r\n7-72,3-71\r\n11-86,13-86\r\n50-56,11-55\r\n12-31,19-19\r\n4-94,1-95\r\n44-93,1-94\r\n80-80,58-79\r\n7-95,7-96\r\n5-40,6-39\r\n7-57,19-57\r\n70-74,70-81\r\n7-91,1-91\r\n1-98,1-99\r\n1-98,56-61\r\n36-87,36-87\r\n1-43,8-44\r\n1-63,63-63\r\n2-91,1-82\r\n8-88,88-89\r\n53-54,2-53\r\n32-91,32-85\r\n3-88,88-89\r\n3-88,1-2\r\n33-71,29-34\r\n40-74,50-75\r\n8-12,11-53\r\n13-80,5-80\r\n2-12,4-11\r\n2-91,1-35\r\n4-77,3-76\r\n36-86,19-85\r\n27-53,28-52\r\n44-57,58-68\r\n61-89,58-88\r\n41-42,42-85\r\n91-92,27-91\r\n1-35,3-71\r\n40-40,10-40\r\n47-53,50-64\r\n77-78,31-77\r\n21-22,23-31\r\n19-69,18-70\r\n21-98,21-21\r\n24-70,24-70\r\n55-57,52-53\r\n53-84,53-53\r\n71-77,71-78\r\n13-48,7-47\r\n4-97,4-4\r\n4-28,37-73\r\n36-40,35-39\r\n9-18,28-90\r\n42-95,95-96\r\n11-21,7-20\r\n19-84,18-83\r\n5-69,1-4\r\n37-65,3-38\r\n2-98,1-99\r\n93-99,9-94\r\n44-93,44-93\r\n7-11,6-70\r\n8-94,93-95\r\n13-84,17-84\r\n11-73,30-74\r\n3-5,5-96\r\n55-80,79-91\r\n4-6,5-71\r\n74-77,74-78\r\n47-54,47-54\r\n52-72,73-80\r\n11-19,18-73\r\n42-44,43-83\r\n53-67,52-52\r\n2-42,18-86\r\n18-18,19-67\r\n12-91,12-95\r\n19-73,20-74\r\n47-97,96-98\r\n14-62,15-63\r\n14-69,3-70\r\n33-52,33-52\r\n32-63,62-64\r\n3-7,8-84\r\n47-48,12-47\r\n8-8,9-9\r\n43-92,20-20\r\n44-91,90-92\r\n62-68,63-71\r\n6-49,5-48\r\n14-24,3-23\r\n28-94,23-95\r\n82-91,27-82\r\n13-84,3-85\r\n1-92,1-87\r\n58-96,58-83\r\n99-99,7-98\r\n6-44,15-43\r\n72-72,17-71\r\n22-95,96-97\r\n10-92,10-92\r\n56-56,56-97\r\n79-81,80-83\r\n4-89,95-99\r\n10-76,14-72\r\n20-66,20-65\r\n4-76,4-77\r\n5-64,2-6\r\n17-78,4-77\r\n33-41,40-66\r\n46-54,46-72\r\n18-97,6-14\r\n1-99,21-96\r\n1-39,38-90\r\n15-25,7-17\r\n23-83,23-82\r\n23-82,24-60\r\n8-54,54-83\r\n42-60,12-31\r\n25-96,13-87\r\n15-20,14-98\r\n80-80,21-79\r\n2-88,2-2\r\n91-94,32-90\r\n12-98,10-12\r\n9-83,10-84\r\n17-39,19-40\r\n39-48,40-74\r\n20-84,1-19\r\n42-77,42-76\r\n29-33,31-34\r\n11-86,12-88\r\n2-99,2-99\r\n38-95,19-54\r\n4-4,5-90\r\n3-93,21-93\r\n58-88,88-89\r\n15-98,9-12\r\n9-76,9-75\r\n9-9,10-32\r\n1-30,1-31\r\n2-81,2-82\r\n28-72,89-97\r\n4-40,39-90\r\n23-54,53-55\r\n26-95,27-95\r\n4-84,8-83\r\n24-26,28-31\r\n32-57,32-72\r\n46-61,36-60\r\n22-80,21-21\r\n9-73,8-74\r\n15-30,21-64\r\n42-97,42-42\r\n20-31,20-20\r\n5-9,9-97\r\n39-83,11-84\r\n45-83,44-84\r\n72-95,71-73\r\n12-90,8-13\r\n5-7,7-89\r\n79-81,80-81\r\n19-20,20-58\r\n4-30,3-5\r\n30-67,66-68\r\n2-2,5-96\r\n23-97,23-93\r\n45-98,43-97\r\n53-93,53-92\r\n10-74,9-10\r\n66-70,24-66\r\n15-53,48-65\r\n31-96,31-96\r\n11-79,10-80\r\n43-76,43-53\r\n74-93,7-94\r\n7-45,8-79\r\n76-85,85-85\r\n1-56,56-56\r\n85-90,86-90\r\n24-43,12-43\r\n8-9,8-53\r\n56-94,55-57\r\n28-76,29-77\r\n12-90,10-13\r\n38-90,40-42\r\n48-99,59-98\r\n81-81,18-81\r\n73-73,72-87\r\n29-94,94-95\r\n7-46,46-46\r\n94-94,3-94\r\n4-89,4-89\r\n66-71,67-70\r\n19-62,63-87\r\n16-16,16-34\r\n22-98,8-97\r\n43-87,42-44\r\n5-35,1-34\r\n32-56,95-97\r\n7-46,6-98\r\n76-88,67-75\r\n7-62,6-8\r\n29-75,30-76\r\n55-99,34-55\r\n18-57,3-30\r\n91-92,8-91\r\n4-95,98-98\r\n55-55,45-57\r\n35-88,34-87\r\n21-98,21-99\r\n42-44,43-76\r\n14-15,14-76\r\n36-90,36-87\r\n8-14,7-14\r\n26-99,25-98\r\n28-50,14-39\r\n39-64,39-95\r\n3-50,2-22\r\n58-84,57-59\r\n10-50,9-11\r\n14-42,13-15\r\n51-65,50-54\r\n47-82,83-93\r\n76-76,12-76\r\n14-38,3-37\r\n15-44,14-45\r\n32-97,4-98\r\n30-61,31-62\r\n42-84,5-37\r\n20-99,19-21\r\n13-37,14-45\r\n19-72,11-20\r\n22-98,22-98\r\n61-96,35-62\r\n54-70,46-69\r\n70-82,68-74\r\n25-73,25-73\r\n42-44,41-46\r\n27-97,26-27\r\n1-80,1-81\r\n6-98,3-5\r\n60-61,37-60\r\n20-80,5-21\r\n29-78,30-44\r\n8-76,7-75\r\n45-62,45-96\r\n8-99,7-7\r\n46-72,46-46\r\n68-86,67-83\r\n9-25,8-24\r\n11-55,10-12\r\n19-82,20-83\r\n20-98,19-97\r\n57-59,55-59\r\n80-82,81-87\r\n5-85,84-86\r\n80-94,2-63\r\n29-81,80-82\r\n35-35,36-71\r\n41-45,40-77\r\n38-97,11-98\r\n2-99,98-99\r\n24-26,25-48\r\n60-61,35-60\r\n45-70,44-84\r\n17-49,49-50\r\n69-99,68-98\r\n25-70,70-99\r\n47-50,39-51\r\n67-85,67-85\r\n47-98,47-99\r\n98-98,56-98\r\n21-98,3-99\r\n5-94,1-2\r\n51-98,19-97\r\n52-72,73-77\r\n36-74,24-44\r\n8-18,70-95\r\n7-83,96-96\r\n1-94,2-95\r\n10-24,25-44\r\n14-53,13-43\r\n41-73,63-66\r\n56-59,57-61\r\n65-66,64-77\r\n42-42,41-88\r\n3-6,2-4\r\n52-93,51-92\r\n22-84,21-85\r\n20-99,8-20\r\n6-84,84-85\r\n53-66,30-66\r\n27-31,13-29\r\n12-55,11-12\r\n7-85,76-86\r\n94-99,53-93\r\n2-3,4-85\r\n35-37,36-41\r\n13-78,89-93\r\n22-84,21-83\r\n93-95,11-68\r\n15-72,53-94\r\n5-58,5-5\r\n98-98,55-92\r\n20-59,21-60\r\n6-96,8-96\r\n7-33,7-32\r\n69-69,36-68\r\n24-97,10-96\r\n56-96,63-95\r\n3-61,3-94\r\n35-59,34-39\r\n21-52,21-53\r\n26-92,26-27\r\n53-81,52-82\r\n17-63,16-18\r\n42-42,42-56\r\n7-67,7-8\r\n43-92,43-92\r\n2-51,25-57\r\n5-7,6-90\r\n14-71,71-85\r\n16-87,2-8\r\n24-51,18-44\r\n49-51,50-60\r\n3-98,1-4\r\n3-96,1-2\r\n8-76,8-95\r\n32-95,32-94\r\n46-87,19-86\r\n77-96,28-66\r\n10-71,71-94\r\n3-97,3-96\r\n10-61,1-11\r\n13-81,14-82\r\n26-99,27-69\r\n40-69,40-69\r\n9-61,1-8\r\n19-23,22-99\r\n3-6,6-85\r\n24-95,94-96\r\n76-77,34-77\r\n6-79,6-7\r\n22-68,21-60\r\n12-84,84-85\r\n7-72,7-81\r\n10-74,49-73\r\n47-89,46-48\r\n2-2,2-96\r\n20-82,36-81\r\n25-62,24-61\r\n14-25,24-88\r\n10-57,28-57\r\n58-89,59-93\r\n5-9,9-10\r\n18-18,18-90\r\n62-84,24-85\r\n8-62,8-63\r\n55-90,8-55\r\n2-93,1-92\r\n35-60,59-75\r\n24-93,32-92\r\n35-69,7-70\r\n4-93,5-92\r\n27-45,18-27\r\n30-98,24-24\r\n7-90,6-91\r\n72-93,72-83\r\n8-98,6-95\r\n35-75,35-74\r\n3-58,2-33\r\n17-98,98-98\r\n5-90,3-70\r\n13-25,12-14\r\n17-61,18-62\r\n25-54,12-53\r\n5-79,78-80\r\n8-85,87-94\r\n1-2,1-96\r\n55-64,55-65\r\n51-60,59-89\r\n2-54,2-2\r\n12-96,12-92\r\n18-99,17-62\r\n50-93,45-92\r\n41-91,41-91\r\n8-86,9-75\r\n34-46,47-47\r\n89-90,30-90\r\n65-88,65-88\r\n75-75,8-75\r\n1-93,6-93\r\n7-7,6-86\r\n94-98,37-92\r\n8-72,72-72\r\n14-33,14-14\r\n30-52,53-82\r\n25-81,17-25\r\n10-70,44-85\r\n9-34,34-45\r\n31-94,30-92\r\n55-84,55-83\r\n3-86,4-89\r\n27-32,28-32\r\n21-50,51-60\r\n9-80,27-81\r\n17-72,17-70\r\n49-49,51-87\r\n32-42,35-41\r\n3-6,4-40\r\n11-13,12-90\r\n42-63,18-64\r\n50-91,12-92\r\n7-90,90-98\r\n43-99,10-98\r\n53-63,55-57\r\n62-83,84-84\r\n1-58,4-82\r\n36-62,36-71\r\n80-91,8-77\r\n37-46,5-47\r\n5-7,6-78\r\n25-60,24-24\r\n16-62,16-95\r\n10-11,10-23\r\n2-13,12-53\r\n96-96,41-95\r\n33-48,32-48\r\n29-79,29-67\r\n17-30,31-90\r\n3-96,1-97\r\n2-99,6-98\r\n58-98,45-59\r\n72-74,72-72\r\n50-94,49-93\r\n2-6,13-87\r\n47-92,98-99\r\n49-90,33-91\r\n65-67,54-66\r\n8-40,2-7\r\n41-47,41-41\r\n12-87,11-43\r\n3-81,2-82\r\n13-25,13-25\r\n21-66,55-65\r\n3-50,34-51\r\n42-93,28-43\r\n57-89,57-57\r\n25-95,99-99\r\n7-98,97-98\r\n61-62,44-61\r\n70-71,3-70\r\n6-71,21-70\r\n16-54,8-17\r\n33-78,53-70\r\n19-21,20-70\r\n25-88,26-99\r\n2-35,34-71\r\n84-96,23-77\r\n6-97,4-31\r\n21-87,25-47\r\n31-38,38-82\r\n4-91,4-92\r\n81-81,1-81\r\n6-94,94-95\r\n2-11,1-3\r\n6-98,4-98\r\n41-74,74-74\r\n30-96,97-97\r\n15-91,15-92\r\n32-97,31-94\r\n9-48,6-47\r\n4-75,4-74\r\n33-49,49-49\r\n58-95,39-94\r\n6-99,5-53\r\n69-72,70-73\r\n2-91,89-90\r\n8-8,8-21\r\n2-11,9-10\r\n5-67,5-5\r\n21-58,20-59\r\n3-97,1-3\r\n56-62,57-76\r\n26-93,92-94\r\n98-98,95-96\r\n59-96,60-80\r\n76-94,51-77\r\n76-99,76-98\r\n10-83,28-84\r\n1-91,91-91\r\n9-58,3-57\r\n54-70,55-69\r\n70-71,8-71\r\n6-9,9-96\r\n53-65,53-66\r\n9-84,11-85\r\n29-77,29-78\r\n17-65,16-80\r\n16-74,2-73\r\n46-91,77-92\r\n11-66,12-25\r\n4-81,80-82\r\n5-48,52-56\r\n8-94,6-11\r\n13-88,89-99\r\n97-99,3-98\r\n83-85,72-86\r\n3-4,4-10\r\n21-43,21-89\r\n14-28,15-39\r\n79-79,7-79\r\n20-20,21-95\r\n4-80,8-95\r\n61-92,62-95\r\n37-37,11-36\r\n19-81,20-88\r\n20-40,20-40\r\n55-97,54-98\r\n72-93,10-94\r\n13-94,14-98\r\n15-16,16-30\r\n43-62,58-63\r\n1-2,3-90\r\n3-77,3-77\r\n5-39,2-38\r\n49-74,82-87\r\n17-24,23-24\r\n82-84,12-83\r\n11-90,31-90\r\n41-41,42-95\r\n53-73,79-92\r\n10-92,11-91\r\n64-69,64-64\r\n90-97,28-76\r\n8-98,8-95\r\n10-84,10-11\r\n27-78,20-79\r\n59-59,58-99\r\n15-54,24-90\r\n18-36,9-28\r\n51-52,24-53\r\n90-93,47-78\r\n8-17,16-77\r\n3-76,83-98\r\n25-62,24-25\r\n10-13,14-17\r\n5-35,10-34\r\n11-98,10-98\r\n22-70,22-71\r\n5-96,1-98\r\n13-63,30-62\r\n13-42,47-92\r\n2-98,2-89\r\n3-7,6-97\r\n42-47,42-42\r\n5-63,62-64\r\n13-96,15-97\r\n88-88,33-88\r\n49-70,48-69\r\n9-96,95-97\r\n3-86,45-74\r\n12-67,12-12\r\n7-87,8-92\r\n48-97,17-67\r\n40-60,38-59\r\n4-10,13-15\r\n8-81,9-80\r\n1-73,1-26\r\n1-3,3-74\r\n54-54,55-55\r\n24-34,35-93\r\n44-65,43-66\r\n15-15,15-98\r\n11-85,53-87\r\n13-73,13-28\r\n1-5,7-49\r\n8-54,8-89\r\n4-38,3-37\r\n43-94,93-95\r\n9-96,8-10\r\n40-77,27-41\r\n38-40,39-69\r\n4-62,52-67\r\n35-47,17-46\r\n33-92,30-32\r\n32-90,90-90\r\n2-87,2-87\r\n8-8,8-90\r\n65-69,69-70\r\n86-86,1-86\r\n71-93,4-51\r\n41-71,57-71\r\n68-85,60-84\r\n3-97,7-97\r\n98-98,12-98\r\n23-71,22-65\r\n42-54,44-54\r\n77-98,24-97\r\n81-84,83-84\r\n5-93,5-92\r\n1-99,97-98\r\n25-98,17-26\r\n19-99,19-97\r\n11-93,11-93\r\n12-95,10-13\r\n34-39,34-57\r\n25-39,14-38\r\n11-48,11-48\r\n84-91,1-83\r\n22-95,21-21\r\n1-1,3-91\r\n57-95,95-95\r\n12-99,6-98\r\n12-71,12-23\r\n89-96,13-70\r\n64-74,1-75\r\n19-91,19-90\r\n93-94,4-93\r\n20-89,89-89\r\n90-92,1-91\r\n7-98,8-28\r\n1-47,2-48\r\n99-99,1-99\r\n32-74,32-74\r\n5-8,4-89\r\n5-94,2-95\r\n58-92,91-92\r\n35-49,48-49\r\n24-73,74-92\r\n90-91,4-91\r\n24-90,18-91\r\n62-70,63-69\r\n16-88,87-88\r\n6-8,7-93\r\n7-98,8-67\r\n12-86,11-86\r\n64-86,63-63\r\n48-73,29-76\r\n3-35,31-34\r\n59-68,59-69\r\n11-98,10-12\r\n17-17,17-87\r\n6-88,6-87\r\n6-63,80-92";

    private readonly string day3 = "FzQrhQpJtJMFzlpplrTWjTnTTrjVsVvvTnTs\r\nmScqSqqgcfPCqGPZcfGNSvTNsVVNSjNvWSNsNz\r\nfPcPGqgCcHgFzQpJJtHtJH\r\nDZDqqlrjplDHrNCmnBcHBMCRcJzb\r\nRQFLStFvdcBbzdJbJM\r\nPThQtwftTPFvtTPhvtFtfFtpZZllwjRNlsqNqqZjwpGlrZ\r\npPwtqgwJZPJLgQqSFlqhFFlqMd\r\nDBmCWBBDWTRGvcVRTCCnnfQlFSdlzfhfdMWQfjhhQz\r\ndrmBVVCRgprPtrZp\r\nHznjQjvmzDMVrQnMLJMMlfWgPSlJGWWJPl\r\nBdcqqhcdBRpFhhZBthhctdJSJJWfgGFlJCSFgbWPCDJS\r\nNdRTZdNqBwqtthpRBTTRqdtZrsLQVzrrzjzDwDsnmrQrnsrr\r\nHZFZCFzZWszqsRTBZTNMhmthVTmhDppmMQVPpm\r\nwjvSbJddvrvlrvnJSJJvlJmhPlhVPVtGVpQDBVMpphQP\r\nfrbrfrcvvnvjfwbcJgrrCBRsCFsNzRgRCHCqssRH\r\ndDFNqNqZqPLNqvqTTvCLSPdZssGHClJQJcRHJGHHcHBcsMsQ\r\nlrjmWgWWrhjgrppQHHMQrsQRJGcBJc\r\nlVlmnwjmdTTSvVFN\r\nFWNFHvQPmLGwwwSHtswwln\r\nRfMJcDdfdcfdddfZjdchrtZmSmCZVtqVnZmrnrtC\r\nJMmJcfjjphcghpgjhRGzGzBBGPFGNBvPTpFL\r\ncVPVwStmmcQPBQPpSCppwhHZNNqHszNBhsNRNjqHzj\r\nMfWdDgvdbnvgMTWgvgZfzmsZJHzNhqjqjRhJ\r\nMDWMWGndMgFDnFLDwQrPPCSrCSVrlmGS\r\nQLZmPdRdWmMsMDWZmsLWWrhMHcHGzHvGzFcvrvzNrc\r\ntplSbLVBlvHHcFNnSr\r\nVqfgwLlCJWmWQTfW\r\nnRWvlvRbtLvdMCPFGL\r\nwrfsJNNGhNzGrTgDMDLgPMLPfq\r\nwcVhJQhwhrrBpmVblBRGSG\r\nHHHcggrZLcQQcQll\r\nGzfzTRTzmmFMwSNSwdSJQtNLNB\r\nTGbmLMFTzVVVTMzmFMfFPMHPZhnjZCpHnhgnZnPWCPZZ\r\nMRwwpVMHRspqVqwmccDlDrcHBBZgBl\r\njQfQQQjWWFBgmcgDfcZg\r\nhvvSQzSnQQSWWQWSjTZVTRMshwVCssppwV\r\npvrTvCvtFppCHMMZcdDFdcZM\r\nwLjTQnqljjSnlwjqjRgLcHHHMBDMZhBMHgHcbBDh\r\nmqjqlSNqRqwSRrWCvzGmtfTfzs\r\nTWScDCqCQQVBWDqWHsHswwBgRJzRhhHp\r\ndPttGrvFfGjMjnjvshsJgsJLgghRgH\r\nrFMlGdtjPffNnnrffSNcVCDqQqCQRqQRRN\r\nGmBRbVpPbmJcwggBBgWW\r\nLjsTCNNtddjHqLLgWwccqgfq\r\nnsjNjntNtjHCsDwZmwZZVmmGSvSD\r\nbwDDgNFtMMDbFsMbFwWWVcRcSpcgjgQWhWSp\r\nlfTJJlvdfCffccWppRjRlcSc\r\nRnzGdJJmsMNnMFtM\r\nbsBTFsqqTTmFZTsQBWWznWCRshlJNJlCVh\r\nGjGnDvDjvjPppHwwpwgrPPClJhNVRCzhhzJWlWlhNlvJ\r\nffdgLrgdLrDjdfHPbbZbttcBbcbLmntn\r\nTNTwwvTTHNtTHNLLVqtqTSZBJnrnhhbrFJjZjnVZgghF\r\ncplWfRlzcWfRCZZhFrGjBfjZjn\r\npddzDsRpDcclzCQMWBvNSmTTSqdvPPvqwqtT\r\nDQTttwwLtQtVSDMJDRmmSS\r\nffsWfvrBWrPvwJhPhPSMPMVn\r\nWsvsggFvwNLgHtNQ\r\nllBbVDMTlFVdFDTbVggSVsqZqZZZqqvNJZJRNRWgtv\r\nHhpjcHHvjPsqCsWcNcsq\r\nGfpvnPvwFDTTFFDw\r\nGMmFGMGFFgVwQHQwwM\r\ncJtZNtZTbThcZtcZJJtTZWJPllgNgpPvVgpjHvQpRpHQNg\r\nhWcJZcnhcJznbcBZLqSLDfCmHqnqCLsD\r\nzQpjLpnhnsHTnlQLrMCCHPFrvvCMPcHm\r\nZfgdSBtNqBwlgSDfZDwtqSFvJCvrPrVvFmwCJFvrmmFV\r\ndfbRNZBqDtgRNBNNNljLLjhGRGGWGLGTRhjz\r\nhhrnfBzhtzZgDgDnBfrfDZsRpMNCNNWjwCCfGQGGNGCGQC\r\nlcdPmHLSPDSdFDpQMLjCQQQCRGpN\r\nlJSSbmPdVdVvdHbvSDFHHPlZqgBnttzgTsssTrqgbZbsTT\r\nFsdsShrgggLDdbSDsgrGrlWHTpfRpTjjfFTzRTRjBWWp\r\nmPvqCmJCqJNnPvPNPCvvLTTVjHjzNWHHTWRBRVTWVz\r\nwJLvqPZmJtccncvZmJqqrghDGQwbdSGdsgGgQgQr\r\nzFwtNJGtNFlpnwHccZjZbcpprsmc\r\nPWQfBWhBgQgTWQRLThBqMSVDSbbDRsVDmsmZsSZDjr\r\nfvQfWBfLqfTqhLhCvNFttJlCwGrrCC\r\nfNrGLNrfNrGjllRRRPmWVL\r\ntbJdcFbSSssZSmmpFcsSbwDWVWBlllVPDnnjBFjDRnBF\r\nZZJcvZctgNmmvMGhQm\r\nHhhjFRhgrcRTFLvWVJVQWJVHDHQJPP\r\nGwCmwBfGzfSCzCfwtmtzzJVWSVJJZrbWQQQqJJDZVJ\r\nmtfzpGdststtBmfmCwrGRFcTcvjngjFnRcLnpLLn\r\nrrwjdwLgVmVwHrfPCJPQBCBGmPtt\r\nccNZqbNnMMblNpTlNpnhhBPSJsQhJtJtChPJqS\r\nvTWvNcWNWTFvnnvcgjzDLVQLgHVwWDrW\r\njNPgbNHbfLJgLzfz\r\nShvhhFVVDShFVqMSSSvZfffvPLtBBBBJJlpfLJJv\r\nDqhnShhMnZZwCSDCMhChrRnNrNdNQbHNNPmjmdHN\r\nVQVZGQFnzFTSsBfgzgfs\r\nrjlpjtDrtMLZPMtPtpPZPwCsgSHgMHCCmCTWsgBWSBmg\r\npjvDqLwrlDtwqtqNLvtjpPPwRNbQRncQVQddZhRhJQbJncbG\r\nPsBSqnSdQsFhmmmnppFc\r\nTRhNvrTCvNTHVcfHbJVTpc\r\nrhtWvGWLrjRqdSqqLLqdld\r\nvPhfqPJvrMrnffDDhvpMjdzGMLdLLQpllLGQ\r\nmbmcFSScGbSCcQlzwQQlclsg\r\nBSGVCmCTZWCGGvnvfZHqqrDhHN\r\nGSRfrzGRhzsGChjTBBlqBgjgCTCn\r\nwHQwtDVDHwHHDJcDWJZwzHZBqTnnBFlvjFgBqnljjvBdBZ\r\nJNmVJpVmNtDHJWHrbfPLhbGhrzRbpr\r\nWcWcbzNPbDwBNvWBwRMPQmJZQRQZftRZGP\r\nLhVHFgggTHCFHhfMQQSMMGQRMLLM\r\nqnrqppFVHphqfDsNbzjrzbrN\r\ncwgDrdLSrBrvvhDzCljjTW\r\nVHtVZpspQtMQsVRQppFVQVHtCdPTPTzdjvhTzTTPRvjjvWhn\r\nQQZpMdJsQFJHtMHdScwLwLJGrSScSwqw\r\nZsjNflGfRfRPrZNRFcffLwJdwcLdDBnwzzzDznVn\r\nCTGvhhTqbtbgTqLJWdDntzWWdnLw\r\nphCMgmQGvvHCvMhbTQQFsNsNFPZSfZjffmNsll\r\nCNpCJHLNhhSSHZPgrFlFFWgpFpmzjj\r\nqQttDVDwQGdQGvqDQfwbcVrrlljjzzmzrVJgrr\r\nnvMDsqqqQvfvsqDnRSZHJPPZHhLHLS\r\nRNNrrPfDNRQwQhjscghMqs\r\nWVZlHvnZqtlLVLvwjwhsggTstMhwTw\r\nvGHWLJlVWlmLVqRCGCFFNfqqGf\r\nMNzqCnvqvqvCVLBvvCVCpVcRssncrPSTWGrPSPdGTcrP\r\nhmHwFmQjFlhtZmHwtZjjddSSGcsdPrrGcQQQRGPW\r\nfHbbFjlhZwmtwhfjmmwmmLbpLqzqvBzLzCvLNRMbNB\r\ntQfLrtQPrrfDSSCVlDfLSrmbBjGvWjjLmWWWpWNNppmv\r\nwdHhRTTndnRThdvnBFGpNBMnpvvp\r\nJdqTHTHHRdqzsJRRzTRHscJdDSGCfDlqQZqlfZrZZCffqSSQ\r\nhQMWLsgGJMMhsCHggQWhgspDWFPzZvPvptDvzvmtdtdF\r\nBrBlrTBrNRbfnjNQlZDztPvpmpppmzvfdd\r\njQlQlqQVbVcsMgMgChhJVs\r\nMtFMCTWRFRRtCRTTRTMGJddjLdstHvBzBHzHVVpL\r\nlZSDnbDlnZPrbHpzJJsdSVJpBL\r\nnNghhPrlZlgDTFhCfMFJRMQF\r\nRGpPFZPRQZPFRGvpPQPpjvpmhnnCMjhmhgBgVgMVWBVgVM\r\nwLtfNdNHmrNthCBgCbhnngWd\r\nsrSfwHfszsNmtswlrqQDGQFDRPJGDvzRppRJ\r\nGVFFGvVWZLFsmssFRNfVvmGGJPpJTTqDBvTpqlpDvqbBtTPl\r\ngQhzzChzrMQhjpzlzWzJpPpBJb\r\nghgWjcCjMgCHWdQMhdjChCmfwmRRGZZGVHLZHRfmNwVs\r\nDnDVhdnrfSfpcGGjQQGdJddJ\r\nbPWPRbRsRMsHNzDqTZcGBcqZqmmN\r\nHvwPvvzMPwDCChDVwS\r\nvTCCvTfWFDTtRPMvfWFlDFHBqGLpLzbwBgWwqzGqbBbB\r\ncQcSNchSJSZShVJNnZrhSqBpgwGHHtGwqtbwLbqpbr\r\nJNnJVsJscNstNhQsjnVVNlFfMmTMFfCTfjFvfPRPPF\r\nVLFBsgffNFNqRvbz\r\nChltjTdjDhHpHZvdpjjZhwCpbNrbSzzbrNGMTMMNSMbWWNSN\r\nvQjpttQhHnLsBQVLsQ\r\nmbzQgTzRVVbsVdQgzzVRddmztFGWNGNNWnGtFSGBsrCNWCrC\r\njfJjvPPwLDcHDPvDDPDppLCWCFBGWntCBnrtFcrFWTGn\r\nwpJPLjvpTTDpwhfgzmVMbqhdhVRgzl\r\nPlcqbWClLmnqZVLq\r\nTHwdrrhddhhfJJhwLJhpQnDVnznnmZQQnSpfpD\r\nvrFdvGsGHhhhwHjFGrFGJHdMCCcNgbWMPccRRccMFLNPPP\r\ntbppJqcNtJnZzRJbPFsFPHfZrrshFDjj\r\nGdwgwlLgGCndsDFrhDHHFF\r\nSSlLnmmvqWNqmcqb\r\nZPFPPTZpZSWzCMMSzPBsFvhtlQvJQQtJhsVs\r\ndmNbmgbrwDNmbcDgwNdcwdLsnhlJlnvtsBJnhVQqqnstLB\r\nbNGfDGgHHVwbwNwVfgmRMzCzzCSHjSRZSZCTRS\r\ndDTffQdqQQLBLnVLLQvL\r\nrrBHZZcgJcrLvNLtLgRLbN\r\ncjjJhrFlhZwFFzwJzmTBBdmTsDPzDsBP\r\nClGrJJMNCrGQqlcPvWgnDP\r\nZBvbjHpSwBVVVcWjjjqQ\r\nBLSbbwsHSTBHwmLHHLbBsSTFdrfvCrtmdzfGJzrdzGJddGfh\r\ngljWRwmSjtJWjJtJjgjSZfVSTVVHGZSVHcVchZ\r\npBzLFQpPsFBGcGBTThfB\r\npFpQzFLPLpvQFQnLbsqqGddgjbmwRldwtWmlGWwj\r\nPDQDMFQBMfWPvjdLLndLjrmsMj\r\nqZqVzTRRqHtvZGGtVqTTzVjLLsrmJCddnLjrjHsrhdCr\r\nGzwcZtqNzqvNqwzZVGRwSzbpWfFbWPlWFpNDBfQfFNNf\r\ndfRszdzVdsjwdhLwCCqwGllHvPGPwG\r\nSpJtBLFgcGqHQClqZF\r\nJrttrtcTmSSLrmtBTrNgnBJjbNhhbhzRdsVdMhNjhMMhVd\r\nMPFSCfSMqVSBGrtzlvccfQctzbzl\r\nhZNjTHWWTZwshbLvmlWpBzmbmm\r\ndRTTJNDNhjsJqBBMMgrJPVVr\r\nWnVzDMjlDVWwwHgwhmgNhNNsJh\r\nqfvrLNCcbLdvpcvbrPPqCsGhSJGTTBspTshBpTBBms\r\nZLvvZfrPfPCLbCFFzjVQzRnNNMVzDQ\r\nnllbFTTpTFTBcnCjQPqQdZRQZhCb\r\ntvWszrrztvSmzQQvrDmZRjjjPPDVqPRdZRdCPd\r\ngfzvSsftgQHQHgQl\r\nGVbHRRGRLpdmGWTm\r\ngSPPltPlrlvccFccPlcJNCTpnnmpMCLMMmWfdRmMSS\r\nFzNJRhhvPFRvQwzqjqzBHZZj\r\nPhZSpFBPBFsNmjBVllltBj\r\nJMGLnrrnbfffrdqRqPHnnqLDVTDDjgmRgwtmjDljlDVlwl\r\nLHMqPqPnnqGLWJPMnndrGfSWppzvvFSChFFFvvzQSQZz\r\nRSWWssbvnnCqZnWsRCnssWrTggNhgbNHBgQjhhQBgjNT\r\nmcpzcppzczcDGVcPcDLLGLjmrMNTNtQNHhMHrQBQNTgN\r\nLVpPfcjjWvsFFnFf\r\nMpddpdCpJdJlbdMvBHMnnsHqSRvG\r\nPWvZfFmZrrfmwWwFznBnqRRSGcsBVmVBRG\r\nzjzzhQPQvzjLPQzwffrwrtlTCDtJDlgJLltpTTJlTl\r\nTvTWjjzpznGttFFZccrrPrSZllcB\r\ngNNSqHMqsMHQJHNZCDDCZDqLZdlZBD\r\nSMQNSRNbRRHwhwhsRmtnvWVmmnbGnjmpGn\r\nccSVQjCQddTsFJcH\r\ngLppBfgfmvCRFdsddTJJgb\r\nWMLMmWGGBZWZLCtvDhlSSDGlwhSPSzSP\r\nTpqVGVHFQGmqSqPZdccNCzzhdwCjNG\r\nfffbbvftMrBMDDcCccCZCjlvhCCd\r\nRLWMnbftDhnMRtfBftRJMtLMgFgHmmpmPmSmmQFPPLHHVTQS\r\nnRvwQSDNcpVJJcJR\r\nqZMjBhjhZMMBzLBGLGrjJbTPVTpbdPPdVbVb\r\nZZpmFFZlfGqfmmGMzlfmMmnWQDtHtSvnWWNSHSSstFtS\r\nbFDGZjGDbbRSgLtN\r\nCphJVfJWCTBgvfLHNRcwnt\r\nWVhPWBTzzChzhhhBmrpPPCJZDQtdMlrjFQdrFqsjdrQsFG\r\nZBpVQHHVMMWWdmmLWw\r\nlQhhrjcRttrqbvQLNwdDWzmNSDmStz\r\nQbGqhcbvcsqvCCHnsCZHCnTn\r\ntlWtQTTTJjTQtVnmrbnPWVShVC\r\nMDMGGzsHcwFgGZBqrmmPSnbqVmNVGC\r\nsZFPwHcMZDBRTlvQQJttTQTR\r\nFhVRfGptMGMnZhRFBNRBCCNHHNvTNTRC\r\nzmwrLLSjrbzmNlcvvrHvDPCN\r\nJLwjQdSbjdbSdqJQFGVqFVMgnGHMfGVV\r\nfffZWrJqZSHWTWHqSvrgDhggzRjttsDhpDgs\r\nPGlBLcBBbnnbLLFbGLBjRgjFTFVzshtzpgsppz\r\nTGCPnMPQlGnPmclPlnnQmbmHJvNvfHdqwddwvvZfCNHCfW\r\nClLwpspTPrTFZCdzFbZdbQ\r\nRRMWfRgWVRMRQBZZScVczVGFbjNb\r\nMfnvMqWmslvDhQPw\r\nhdndSdqsTddBhdcmmNHFDcqHttPF\r\nJjMzzMZQGwZGZJzMzZJQzGJFvPvNPtFmvmNmDvcFtvDHMv\r\ngZwzQwJfGVJQJbGLBsSTSTdTbCWDBSnd\r\nZZCHZRzMZGRMhMMVVFNThrdd\r\nSgsccSPmmgqssSlqsgcmscSqlhpFdVThjphNrdrhjdwdhFJN\r\nvmttqTcqvLqqmPccmqSBbRWnWzQZZZZBHnQCzHDH\r\nGgPnGdSPBpGsLTBL\r\nrVNJjmwZqtZZshltFTtvRFsL\r\nmqmWrZVqWjrqZMNwPMQQbsddgdsbsgPz\r\nLZLVvjZrggHLJggSZDgrnPnQnRnppVRllntRdPFz\r\nchMCzbqGmhNhhbBCMBdFnpfqFnltRRQnlPpQ\r\nTChmWcMMTmBswJzZZrWrvzgg\r\ngngRNBNRBsNFFBgfgbLLLnqdSLvLTcbLbd\r\nGWtlChlVMllcZSDWSLbdZL\r\nlljjGlhMGrGJpsFdRJfsfzfz\r\njVTdrnGQcQtTTTFQqBqsgHHFgsqf\r\nZZLbPLzDzPZCmsgqsBHt\r\nwDzDlPblRDPLPvhvwtdnnhdrnrMGWMVGMThj\r\nspjjpjvjpjmQjrpCMfSlfzrPBl\r\ndHFntHWnnbRVFtnbcqHFzBCCCPzfPMlcCSlgllzc\r\nRLbVWHnnSWtnHFbdbVRdNNtQsjsQTjDLwmGTmTssQwmLGJ\r\nJbJJSLMhRMSLhNqqwFDwFNcFqL\r\nGcpnGnznnpzpzGpffNTNTwTfwdDNNdTFdD\r\nnllnlPGWQWHcGpzzQGGzGvHGJbVVtJSChQVbmtmVJrmrmbRm\r\nGFsFrzwrflmtdtbltG\r\nggLPDngCJncNLJRDwgnllmJqjWMjhjhjWWmWjj\r\nnBNRNPgpRgDLTgNwfsSHVBQHVHwsZr\r\nWwvnvWvcFtwtSFSF\r\nzBZZZRQSzMBSgSVJGjGTPTGFzCzmmj\r\nfZDrpZZfRfMgSQDDBhgQghDHsnbrcNlWnnLWHLrHsWnllc\r\nZVncdPPwVPdhZngnqHWHNNvTHvlMvn\r\nfSLjjLSGGBjTTHqvBqrMNT\r\nRSSSDGRtSGZthTTctmtg\r\nrtzrfJbgJHRfGRZLPR\r\nhdVhlllmFlFPLwHmsRGGZP\r\nnTWhRjTBTWlvNQgnJSSbrJtz\r\nJgVTpBpfvgpTDDJFJvTgggtlFlNNMRLNNzNNZRNHMRCLlF\r\nwbPWcSGbGqWDlnNWMMMCLMWZ\r\nwrsGcbrcbcqwDwbcmGvQBQgTTsdVJgJsVdQf\r\nmztrhgJtDrhgcrZmnhbnzbhcMTMPlBCPBGVGTMVGslCCPGDs\r\nFLRQmjjFSQpQwLlPsMsCpvslvPCB\r\nfNLLwSdSwWSWjwmrtczZhhrJzdzh\r\nHHwCwJFmHZttZCfCSffSMHcVDMcPBRPcPRDhPghM\r\nnvQLsTnLslnLvpzGTssnsRPDMhPgVPVgtcVMRPgVQQ\r\nvnsTGWlTLsWTLLvNsGWlsZrwmZCJddjFmtJJNZFftj\r\nhbjSTvSJTfcSwcPSPfTbfHszVVFpGnpJpsHFnHVVls\r\nrtZrcQrRZZQrmZBQlCGppnppHzpVFCGR\r\nWmLqmgNtcLNQWTbPvfPwbbdb\r\nHzZgsdHglHlzdHsFtsNNJSlNcSpjcjlrrNVv\r\nwqqWRPPqwmbcqPjQVvSPJJrVpv\r\nqqBBqmWRhqRLqcBnhzzztgnTdDHnHsFsHn\r\nrJPFVwwsrJwmdVrLWJvvRBWBvbzWlb\r\nnDZcNGNpjTpHncvpZCDnTNZGhlWzQhWbpRRQlQhpWWSWLlQb\r\nCDNntnCCHnvmqPfwtFdVqd\r\ngqBwgBjCswwgqNBNCVDDTVdhlSDTDcZc\r\nHvRRFMzRRRRMpHrtTllfhZHHSShHTf\r\nPmlGLPrppMrrmFFmLMWRjbsjnsjwQNJWnbQjWgBN\r\npDggpFgRghZjBFPPnPPFrt\r\ncwTfLwBVwCWbLcVTVVvrdndGjMHrnGJtnttdMC\r\nNTVcWNvcBSpgNqspRQlN\r\nDLDgFlDmNZfjfnJZSF\r\ntctvttzvGGzvrHqtVVdwnJGSSnnjjZdWTdwW\r\nzvpcrbpHpqJJsPbPlLlhmhglPQ\r\npvHHvssFCFZQNCftttdQdd\r\nVgTGTTVGgLjDjlLGzgPVMTNwmcwQmMQfQtmdcmwMJwNm\r\nTPjTDjfGWTLLljgzrWpZZbsqrFqhqbps\r\nppVLcfcwSLgpSLVLgWwtfshDNDqvWvGvlQZvDNHQHjqq\r\nMPrzmdRrPPrCJFnMnMRRFRPdqqZQNQvjvZDGDlHhQvGNDG\r\nBmBMBBJTMmPBJMMFCCFJRmrsTlVpVbpwLSVwLsgcwTVlVc\r\nSSGzmFRzmRGLgSSmGMJFnvfvJnJVnJQnMl\r\ncBpjHtjwNfcpNZtppHtCMlMPMlJBVlVQlvJPvJ\r\ndNtNZwqWfqtqZWtHttsqHqrRrrdRTLbmmzSLmTGGmbrg\r\nRrrddnrgnRbbgWdGrfnwgQwjDjDpvTpBQTwBPP\r\nMHCStZJzSwvPjWQD\r\nmcJWVHCCLcGLbdcn\r\nPlMsdjPdGMjdPSrSjgddbLbmHHTszHZzpHmsTFvmpzZzmN\r\nntRJQVRfcQhcQWhnchBJWntTFTTTNTSpFtztmZFDTpDZ\r\nhQfcfCBSwCccVJhSJnrPPGLqPlbPLCrqldgb\r\nvgvWDMZvGpcqgqsP\r\ntSdtjLHLQLHjdFdDddQSQhwlsGqwQlqqqhQsPhGc\r\ntbRjtTLFRvTZDBrMrV";

    private readonly string day6 = "rhghwwsmsgmgsmmmlzljllrddvsvhvhhnvvrcccwhhvgghchwhvwvcvrrrgtgrgdggfdgffshsllvvslsppglgvgwgswwcbcwbwrrbjbwjjtgjjdzdfdhfddrmmhqhpqhhqghhzssqzqccbwwffzvzffvtftddrbrtrrcjrrmmbmrrrlrppplmplmppfzpfpvpqvpqqdndpndnmnwmwjmwmwbbrhhmzmffwfqwfqwffppvfpvffnwwwwcbwbhbccvmmplmplmmlmplpprbpbllmhlhzhffngfnntrrsrprrvlllvjvmjvvppsrrfnrfnrfrfwrffrsfrflfltfllpfprrthhhnhqnnfwwcttvzzddqsddwpddnrnttvjttjjdjvdjvjnvnrvvchhschhlffjggtqgqnnvrnrsnrsnsvspsnncppvwvvtmmcqmcmscmsmwmvmpvvlqqmbqqlwwtgtztgttpjjbfjjzhzthhzssffwqqdmdcdlclrclrccbmmqjqmqcmmnnsvnntpphhdpdhhtbtdtffhzfhhpwhppwbppfspphdhggzqgzzffdsdfsszcscpscsqcssffhjhnhhlqqdbqqdmdsmddrfddndldgglvglvlggqlggjngjnnbffthhjbjpjnjmnmgmqqbjqjzjqzzzjffwttcnnzffmllcncmnnthnncttpgpnnjdjfftmmvqmqsqggfrfjjqbjbvvshvhphfhhljlfjjvcclzclctcmmfwfvwfvwwjvwvjjplpbpqqdvqqbhbdbnnsndssgffgsglssmfmjjfrjfrfttljldjdffjjdnjjlbbnrntrtfrttmhhndnsdsffgqqrpplvvfwvfvsfszzfzhfzzlttlctllrzrggvwvhhgnnlmnlmlpppdldqqdpdlplrpppjspspzzhphccgjjpmmwvwbwssnpssrzrwrhhmzhmhsmhmttrqrvqvcclggptpjpzpccltctppflfjffrtrjrhjhffhjfjbfjfqfqzffdtthwwjwzjzggsgzgqqjdqjddvgvsvnnqwqgwgtttfvfrfwrfwfwmwmqwmmmbzmzvzfvfrfvvgvtvptpbpwbwrbwrwffzzfgzfgfcfzcffjdjjvrvnnfmmtztffsggjcjzcjcssscnscnssmmwgwbbljlnjndnrrqwqjjjbnnmbmcmbcmbbvvmtvmmfcmcjcdcnnzfzfzqfzfvvmbvmmndnbnsnnlddqvvsnngvnnrttfbtbqqnwqqzfzfzccfmmgrrsbbhvvgjjhfhssjmmsjjzzbqblqlfqllwhlwwvvstvtfvttnncjjzcjcjrrsvrssmfsfczbrzrvscvmcmrjpzwhcqfrrzbljnmqlzbzqtmhrshlrjjpvhnsvtlhqggqwppsjpszmqwfqmlwbqzwcrggrvfbvztnflwvbrqcrqbcllswvsvhwjzpldgphwptfdnlgdlnbttjfzrdcfvpdhlssfsljvdjmwddbnrpqnnqlfdfdspbnjwqjwrgtnrftsqcfjpmqwgwhttggjwzvgbwlhmtmmjlwhssrgshzpbcnstzlqdshdhjfgqlsmqqhpwbscsjhfbhprvhmftqngjgdbcvfldqgqsjqjfdmcvsflwzflsjfssnjpbwffnsfcnrdsphbjpgghmcthgnzmpgppqjdvbztvhnwqzndntcpjdtwwhvsmgdcthpssszrqcbntgsznpghmbqddpqscntjprlwzhbzhjtwzbwwcldwdgsttzmtnstjnngzrgvhncdbgqnllfzbthldztsdwsngjzprbvrnbzrsghlgssbqfnbvhnhzwmmmtncvsdngdtwcbjlnnzbnlrnmrvnvsjnvzdqnggmsvljlvjznwdszcmblhrsjvczpnlhsmqjsmwhbjbplbtqsgqjdllhncwdgbvzwnmqvndcbfhnvtjnzmvjhwzvdldhgfwbqqzcnbflfnwntlhmqgdhmrgwqcpmsvfbmwhbtsbdlhcnbcbswvdfffgjvddrbpzpcwsrsjnfvmzhlvbdnwttnqrmzbnnntbfvptrlhjhwjcsbnhvtwtwzvgfnzjplthqjbsjjwzdtqqvblnbvgcmvrmnvmwfrhcqgvrcjlfzdlpbfvncbtfgvnsflbjzqqhczcmtbwqmrppmptfgzvfbmcslwlfrfpvvnvvnwfvvmmdzmmtjsgqdfhngtphtlfjqrtljgnthgnbbqfrnpfmpwhpzdvzmtswwdvcnpsdcqwjdwlvbsbmlwdsjbcbgcrfljshlvpngfmsrzlfhtfqgwbcctnzzhnqhdmqzdwthftwtmpbcmqvdcdtgvltbzmszzwwmhzlfvbdqnhjqgdmstsnhftcwzvvbmnhwvgqzscwcdjbdgfmvpjdzctwqwltbwjlgcblnnhpnmggbmvqpqtgqjzspgqzvcvsdbvjgjfzdzhfpbzjqljjcgldzgnlmtjcmfgdbqgglvjqrppwmhccvqzvsrjjvfhjprwdsqsnszfprznljtcsrtqhcrpljfrccflmbpvqtzgmzhjrlbnrmmsmmjbtzwpglqgdvvvjvnfzmplsmvlvcnjshvjwntclwgpznnzwhjssgdcjbzrmsgnfgcgphrhfvrfhzwdcvsplhbmqwhpmjvqlmschznbqblvhtqfgtdggmncndhhplnzjphccjmlmtdqnmnlnpnfqdstljqnsqbrjrtspvrwvdmwzlgdmsfvctzgtmgqhqqrzpbgplzcfdqnzhsqrbcvhsccshnnpvvrpvqzqsgzgmpzfvvrrcvhdtntnsqnjrbzlbzmpgwdqzbhtlrrhbwdqjlsfgdhmvgmgbqhwvljmmqfllqvvrznrlftgzjdcgtstjffqmgvffpvtctzpdqjfnmlcdzscntctmqhrtmhrlhbjzttrcvcnlhsrvtpbmdchhntnnpnzlvqqnsrjcmblcvphqgwshnjmplgvnbsmmdzgqcpqztjhhgjvtlbpdpdwlwmmfrdgcvzfbgvbgpbjnwsssvhszwplcgvpgjwdrwngbcdjwvlsfhqlrqzgrzpfgjstqfdbrpqdvrlgdwqcsrgvhctznjjlzsmzctsqtfnhhlpjgnltssglmlwshfbrgmjqbvsmqwvszdfsvhmtrfjgwjctpsmgzzjbpwsztnnvzrhwvvmhdpgdmwzsjprhlgzcdvhznlfgjqvcwqrplcfvzmthsdsnrtfvnlrmvwplmbdvdggmlvpgdgzhvzmvzwmptzsnfrcrjspccmqjpjmhjqgrjbdcdjbzjmphmcdvjqtmdshhjrqgjgsnpzfbfgpjpczwzvmclgzgztlvzmdbwgncnndjwhhhjnhtjdmcnrmnqbmjdrdcmtvcsmftqcfhsvhsfjmtzjpnwffggpfqlqmzlbhnnhbtgzfgnjvdzmvthqjrhzbwvhcjzcsmsvsctrqbltpcrpjjnsbjdbfjqfcbpcgcwtqsflmlwprjcwlmcjjgsfdpwcqvhjpsgvdgsfnscnbzsrmrbbvdrlltzplbvgqsdnplcvbhddbtmwnfmvqhqdlrtrmrmzmhlccgwgmbdppjqdjtwmvdzfsbsggrfstjwjpjnljffwffmqncfnthnhglwvsgvzmgbzhtdfpfmdwmcldthvsnqnptpmhqctblgfsszhcfbvcrggjdhthqvvvlldshvqwmvdtfslrhzvgdfwztrczdjgcfcgtmwnphqthlgpfnrqwcgpzwnlgdvsnvzftlnlfflfsmjzhrhqjctsbvtccwbfsdrnbhszzjhqndvwcsmffnstnfdfwpbgfztjmjngdczzlgpscjtshpmmmzlnqndsttbdgfjqcvbqlphwhlhgcvjbhjmtrfzlgpwdnvzrllndbhvhlngvhlszzdcrdgvrmjwcvhhtbhnjmdzgctqnpdlrnqjzbchjtcsggsczlgmvtqvzmsqvtrhtvdmzlcdddfnbvbsnrzvgzfqjtbhjqhdznrhbfbqwtnwvrfqsznbqfzfzfgmhvjjsgbbdbdtzswwlnfrq";




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

    public int Day6()
    {
        //TODO: Fortsätt här.
        List<char> markerCandidate = new();
        foreach(var ch in day6)
        {
            markerCandidate.Add(ch);
            if(markerCandidate.Count >= 4)
            {
            }

        }

        return 0;
    }


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
        foreach(var rucksackString in day3.Split("\r\n")){
            var l = rucksackString.Length;
            var cl = l / 2;
            rucksacks.Add(new Rucksack(rucksackString.Substring(0, cl), rucksackString.Substring(cl, cl)));
        }
        return rucksacks;

    }

    public List<CleaningElvesPair> EatInputDay4(){

        List<CleaningElvesPair> elvesPair = new();
        foreach(var elfPairString in day4.Split("\r\n")){
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

    public class CleaningElvesPair{
        public CleaningElvesPair(CleaningElf e1, CleaningElf e2){
            Elf1 = e1;
            Elf2 = e2;
        }
        public CleaningElf? Elf1 { get; set; }
        public CleaningElf? Elf2 { get; set; }
        public bool AssignmentsOverlapsSome{
            get => AssignmentsOverlapsSome1 || AssignmentsOverlapsSome2;
        }
        public bool AssignmentsOverlapsSome1{
            get {
                if(Elf1 == null || Elf2 == null) return false;
                return
                    (Elf1.Assignment.Start.Value >= Elf2.Assignment.Start.Value &&
                    Elf1.Assignment.Start.Value <= Elf2.Assignment.End.Value) ||
                    (Elf1.Assignment.End.Value >= Elf2.Assignment.Start.Value &&
                    Elf1.Assignment.End.Value <=  Elf2.Assignment.End.Value);
            }
        }
        public bool AssignmentsOverlapsSome2{
            get {
                if(Elf1 == null || Elf2 == null) return false;
                return
                    (Elf2.Assignment.Start.Value >= Elf1.Assignment.Start.Value &&
                    Elf2.Assignment.Start.Value <= Elf1.Assignment.End.Value) ||
                    (Elf2.Assignment.End.Value >= Elf1.Assignment.Start.Value &&
                    Elf2.Assignment.End.Value <=  Elf1.Assignment.End.Value);
            }
        }
        public bool AssignmentsFullyOverlaps{
            get{
                if(Elf1 == null || Elf2 == null) return false;
                return
                    (Elf1.Assignment.Start.Value >= Elf2.Assignment.Start.Value &&
                    Elf1.Assignment.End.Value <= Elf2.Assignment.End.Value) ||
                    (Elf2.Assignment.Start.Value >= Elf1.Assignment.Start.Value &&
                    Elf2.Assignment.End.Value <= Elf1.Assignment.End.Value);
            }
        }
    }

    public class CleaningElf{
        public CleaningElf(int from, int to){
            Assignment = new Range(from, to);
        }
        public Range Assignment{get;set;}
    }

}
