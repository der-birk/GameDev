using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Game : MonoBehaviour
{
    Levels newLevel, oldLevel;
    [SerializeField] TextMeshProUGUI gameText;
    [SerializeField] GameObject map;
    [SerializeField] GameObject hints;

    bool isFinal, hasJM, hasSake, hasWhiskey, hasBeer, hasEatenMeat, secretFound;
    int storedJM, assemblyCounter;
    // Start is called before the first frame update
    void Awake()
    {
        storedJM = 10;
        assemblyCounter = 0;
        isFinal = hasJM = hasSake = hasWhiskey = hasBeer = hasEatenMeat = false;
        newLevel = Levels.Intro;
        gameText.text = $"You awake sweet slumber in the middle of a business lecture.\n(1) go back to sleep\n(2) concentrate on the lecture";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                map.SetActive(!map.activeSelf);
                hints.SetActive(false);

            }
            if (Input.GetKeyDown(KeyCode.Tab)) 
            { 
                map.SetActive(false);
                hints.SetActive(!hints.activeSelf);
            
            }
            if (!map.activeSelf)
            {
                oldLevel = newLevel;
                switch (newLevel)
                {
                    case Levels.Intro:
                        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2)) newLevel = Levels.Intro2;
                        break;
                    case Levels.Intro2:
                        if (Input.GetKeyDown(KeyCode.Alpha1)) newLevel = Levels.HZO;
                        if (Input.GetKeyDown(KeyCode.Alpha2)) newLevel = Levels.Forum;
                        break;
                    case Levels.HZO:
                        if (Input.GetKeyDown(KeyCode.Alpha1)) newLevel = Levels.HZO;
                        if (Input.GetKeyDown(KeyCode.Alpha2)) newLevel = Levels.Forum;
                        break;
                    case Levels.Forum:
                        if (Input.GetKeyDown(KeyCode.Alpha1)) newLevel = Levels.Tram;
                        if (Input.GetKeyDown(KeyCode.Alpha2)) newLevel = Levels.MSector;
                        if (Input.GetKeyDown(KeyCode.Alpha3)) newLevel = Levels.ISector;
                        if (Input.GetKeyDown(KeyCode.Alpha4)) newLevel = Levels.GSector;
                        if (Input.GetKeyDown(KeyCode.Alpha5)) newLevel = Levels.NSector;
                        if (Input.GetKeyDown(KeyCode.Alpha6)) newLevel = Levels.Mensa;
                        if (Input.GetKeyDown(KeyCode.Alpha7)) newLevel = Levels.Audimax;
                        break;
                    case Levels.MSector:
                        if (Input.GetKeyDown(KeyCode.Alpha1)) newLevel = Levels.Info;
                        if (Input.GetKeyDown(KeyCode.Alpha2)) newLevel = Levels.OAW;
                        if (Input.GetKeyDown(KeyCode.Alpha3)) newLevel = Levels.Forum;
                        if (Input.GetKeyDown(KeyCode.Alpha4)) newLevel = Levels.GSector;
                        if (Input.GetKeyDown(KeyCode.Alpha5)) newLevel = Levels.Tram;
                        break;
                    case Levels.Info:
                        if (Input.GetKeyDown(KeyCode.Alpha1))
                        {
                            if (storedJM > 0) storedJM--;
                            newLevel = Levels.MSector;
                        }
                        break;
                    case Levels.OAW:
                        if (Input.GetKeyDown(KeyCode.Alpha1)) newLevel = Levels.MSector;
                        if (hasJM && Input.GetKeyDown(KeyCode.Alpha2)) newLevel = Levels.OAW2;
                        break;
                    case Levels.OAW2:
                        if (Input.GetKeyDown(KeyCode.Alpha1)) newLevel = Levels.MSector;
                        break;
                    case Levels.GSector:
                        if (Input.GetKeyDown(KeyCode.Alpha1)) newLevel = Levels.GD;
                        if (Input.GetKeyDown(KeyCode.Alpha2)) newLevel = Levels.GA;
                        if (Input.GetKeyDown(KeyCode.Alpha3)) newLevel = Levels.MSector;
                        if (Input.GetKeyDown(KeyCode.Alpha4)) newLevel = Levels.Forum;
                        if (Input.GetKeyDown(KeyCode.Alpha5)) newLevel = Levels.Mensa;
                        break;
                    case Levels.Mensa:
                        if (Input.GetKeyDown(KeyCode.Alpha1)) newLevel = Levels.Mensa2;
                        if (Input.GetKeyDown(KeyCode.Alpha2)) newLevel = Levels.GD;
                        if (Input.GetKeyDown(KeyCode.Alpha3)) newLevel = Levels.GA;
                        if (Input.GetKeyDown(KeyCode.Alpha4)) newLevel = Levels.MSector;
                        if (Input.GetKeyDown(KeyCode.Alpha5)) newLevel = Levels.Forum;
                        break;
                    case Levels.Mensa2://grrrr... i fucked up here, this is ugly but it works....
                        if (hasEatenMeat && Input.anyKeyDown)
                        {
                            Reset();
                            break;
                        }
                        if (!hasEatenMeat && Input.GetKeyDown(KeyCode.Alpha1))
                        {
                            hasEatenMeat = true;
                            newLevel = Levels.Mensa;
                        }
                        break;
                    case Levels.NSector:
                        if (Input.GetKeyDown(KeyCode.Alpha1)) newLevel = Levels.NA;
                        if (Input.GetKeyDown(KeyCode.Alpha2)) newLevel = Levels.NB;
                        if (Input.GetKeyDown(KeyCode.Alpha3)) newLevel = Levels.NC;
                        if (Input.GetKeyDown(KeyCode.Alpha4)) newLevel = Levels.Mensa;
                        if (Input.GetKeyDown(KeyCode.Alpha5)) newLevel = Levels.Forum;
                        if (Input.GetKeyDown(KeyCode.Alpha6)) newLevel = Levels.ISector;
                        if (hasBeer && Input.GetKeyDown(KeyCode.Alpha7)) newLevel = Levels.Garden;
                        break;
                    case Levels.NC:
                        if (Input.GetKeyDown(KeyCode.Alpha1)) newLevel = Levels.NSector;
                        if (hasJM && Input.GetKeyDown(KeyCode.Alpha2)) newLevel = Levels.NC2;
                        if (hasBeer && Input.GetKeyDown(KeyCode.Alpha3)) newLevel = Levels.Garden;
                        break;
                    case Levels.NC2:
                        if (Input.GetKeyDown(KeyCode.Alpha1)) newLevel = Levels.NSector;
                        if (Input.GetKeyDown(KeyCode.Alpha2)) newLevel = Levels.Garden;
                        break;
                    case Levels.Garden:
                        if (Input.GetKeyDown(KeyCode.Alpha1)) newLevel = Levels.NSector;
                        if (hasJM && Input.GetKeyDown(KeyCode.Alpha2)) newLevel = Levels.CursedEnd;
                        if (!hasJM && Input.GetKeyDown(KeyCode.Alpha2)) newLevel = Levels.Garden2;
                        break;
                    case Levels.ISector:
                        if (Input.GetKeyDown(KeyCode.Alpha1)) newLevel = Levels.IA;
                        if (Input.GetKeyDown(KeyCode.Alpha2)) newLevel = Levels.IB;
                        if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Alpha4)) newLevel = Levels.ICD;
                        if (Input.GetKeyDown(KeyCode.Alpha5)) newLevel = Levels.Forum;
                        if (Input.GetKeyDown(KeyCode.Alpha6)) newLevel = Levels.NSector;
                        if (Input.GetKeyDown(KeyCode.Alpha7)) newLevel = Levels.Tram;
                        break;
                    case Levels.IA:
                        if (Input.GetKeyDown(KeyCode.Alpha1)) newLevel = Levels.ISector;
                        if (hasJM && Input.GetKeyDown(KeyCode.Alpha2)) newLevel = Levels.IA2;
                        break;
                    case Levels.IA2:
                        if (Input.GetKeyDown(KeyCode.Alpha1)) newLevel = Levels.ISector;
                        break;
                    case Levels.Audimax:
                        if (hasBeer && hasWhiskey && hasSake && hasJM && !secretFound && Input.GetKeyDown(KeyCode.Alpha2)) newLevel = Levels.Secret;
                        if (Input.GetKeyDown(KeyCode.Alpha1)) newLevel = Levels.Forum;
                        break;
                    case Levels.Secret:
                        if (Input.GetKeyDown(KeyCode.Alpha1)) newLevel = Levels.Secret2;
                        break;
                    case Levels.Secret2:
                        if (Input.GetKeyDown(KeyCode.Alpha1)) newLevel = Levels.Forum;
                        break;
                    case Levels.Final:
                        if (Input.GetKeyDown(KeyCode.Alpha1)) newLevel = Levels.Final2;
                        break;
                    case Levels.Final2:
                        if (assemblyCounter < 50 && Input.GetKeyDown(KeyCode.Alpha1)) newLevel = Levels.Final2;
                        if (assemblyCounter > 50 && Input.GetKeyDown(KeyCode.Alpha1)) newLevel = Levels.Victory;
                        if (Input.GetKeyDown(KeyCode.Alpha2)) newLevel = Levels.FinalFail;
                        break;
                    case Levels.Victory:
                        if (Input.anyKeyDown) Application.Quit();
                        break;
                    case Levels.Tram: //the missing breaks are intentional, as all the following cases do the same
                    case Levels.GD:
                    case Levels.IB:
                    case Levels.ICD:
                    case Levels.FinalFail:
                    case Levels.CursedEnd:
                    case Levels.NA:
                    case Levels.Garden2:
                    case Levels.GA:
                        if (Input.anyKeyDown) Reset();
                        break;
                }
            }
            if (!hasJM && storedJM <= 0 && !isFinal) newLevel = Levels.Final;
            SetLevel(newLevel);

        }

    }
    public enum Levels
    {
        Intro, Intro2, HZO, Forum, Audimax, Tram, MSector, ISector, GSector, NSector, Mensa, Info, OAW, OAW2, GD, GA, Mensa2, NA, NB, NC, Garden, NC2, CursedEnd, Garden2, IA, IA2, IB, ICD, Secret, Secret2, Final, Final2, Victory, FinalFail
    }
    void SetLevel(Levels level)
    {
        switch (level)
        {
            case Levels.Intro2:
                if (Input.GetKeyDown(KeyCode.Alpha2)) gameText.text = "You try really hard to concentrate but this is a really boring lecture, unlike GameDev, and to no avail you fall asleep.\n";
                else gameText.text = "";
                gameText.text = $"{gameText.text }Your well deserved slumber is harshly interrupted by the sound of screams from outside.\n As you look around you find the lecture hall empty.\n(1)Wait some time\n(2)Go outside";
                break;
            case Levels.HZO:
                if (oldLevel == Levels.Intro2 || oldLevel == Levels.HZO) gameText.text = $"You wait for a while, nothing happens here.\n(1)Wait some time\n(2)Go outside";
                if (oldLevel == Levels.Forum) gameText.text = $"You back into the lecture hall but it is still empty\n(1)Wait some time\n(2)Go outside";
                break;
            case Levels.Forum:
                gameText.text = $"You are at the forum square, the campus is in total disarray. Bodies are everyhwere, something bad happened here and from the sounds it seems obvious it is still happening.\n" +
                    $"(1) Flee the campus via Tram\n(2) Go to NW\n(3) Go to NE\n(4)Go to SW\n(5)Go To SE\n(6)Go to the Mensa\n(7)Enter the Audimax\n";
                break;
            case Levels.Tram:
                gameText.text = $"<color=red>Bad ending:</color>\nAs you move along the administration buildings a horde of bloodthristy administrative staff spot you. They want to burry you under a pile of forms with the rest of their unfortunate victims. Luckily you can jump into the tram train before they catch you. Unfortunately the city is in even worse shape than the university and soon you find a grisly Demise and you close your eyes for the last time...\nPress any key";
                break;
            case Levels.MSector:
                gameText.text = $"You find yourself in the NW part of the campus.\n(1) Enter the MC building where the faculty of Computer Sciences resides\n(2) Enter the AkaFö-Haus where the faculty of East-Asia Studies resides\n(3) Go to the middle of the campus\n(4) Go to SW\n(5) Flee the campus via Tram";
                break;
            case Levels.Info:
                gameText.text = $"You enter the once new and fancy faculty building. The Open Space is a mess, the glass dividers are broken and there are bullet holes in the walls!\n You make your way to the rooms of the student bodies, luckily the doors are open, that is something new for once.\n";
                if (storedJM != 0)
                {
                    gameText.text += $"In the kitchen you find {storedJM} bottles of <color=green>Jägermeister</color>.";
                    if (!hasJM)
                    {
                        gameText.text += $"You grab a bottle. ";
                        storedJM -= 1;
                        hasJM = true;
                    }
                    else gameText.text += $"You already have a bottle no need get more.";
                }
                gameText.text += $"You ponder over the battle plans that were scribbled onto the white boards.\n(1)Leave";
                break;
            case Levels.OAW:
                gameText.text = $"The corridors are littered with the bodies of weebs and Harry Potter fans. As you round a corner you stumble upon a group of students clad in various asian medieval armors.";
                if (!hasSake) gameText.text += $"They shout at you in multiple languages, none of which you understand.\n(1) Run away!\n";
                else gameText.text += $"They greet you in their strange languages.\n(1) Leave the building\n";
                if (hasJM) gameText.text += $"(2) Give them a bottle of <color=green>Jägermeister</color>";
                break;
            case Levels.OAW2:
                gameText.text = $"They accept your gracious gift and bow.\n";
                hasJM = false;
                if (!hasSake)
                {
                    gameText.text += $"In exchange they gift you a bottle of <color=blue>Sake</color>\n";
                    hasSake = true;
                }
                gameText.text += $"(1) Leave the Building";
                break;
            case Levels.GSector:
                gameText.text = $"You are in front of the G-buildings. GB and GC are eerily quite, while you can hear trumpets from GD and battlesounds from GA.\n(1) Enter GD, where Law, Economics and Social Studies reside\n(2) Enter GA, where Theology, History and Philosophy & Education reside\n(3) Go to NW\n(4) Go to the middle of Campus\n(5) Go to the Mensa";
                break;
            case Levels.GD:
                gameText.text = $"<color=red>Bad ending:</color>\nAs you enter the building you are ambushed and seized by Students wearing armor of roman legionaires. In the chaos students from Law and Economics have founded a society based on economic and judiciary tomfoolery. In a short trial your skills are found inadequate and seeing how they already have more than enough social studies students to make coffee you are sentenced to death in the Arena, which is the converted courtyard in the middle of the building. The last thing you see is the gaping maw of a lion before your vision fades to black.\nPress any key";
                break;
            case Levels.GA:
                gameText.text = $"<color=red>Bad ending:</color>\nAs you enter the building you get caught in the middle of a medieval battle of faiths. Catholic theologists and evangelic theologists fight over which one is better, both sides are reinforced by history students.\n";
                gameText.text += $"A group of catholics capture you and crucify you along other students. Many of them philosopy students who start singing \"Always Look on the Bright Side of Life\" as you view fades to black\nPress any key...";
                break;
            case Levels.Mensa:
                gameText.text = $"You enter the Mensa and the air is heavy with the smell of smoke and grilled meat. Writing scrawled upon the walls tells you <color=red>TODAYS MENU: LONGPORK</color>. In  A bunch of students sit around a big table and eat some steaks with their bare hands.\n";
                if (!hasEatenMeat) gameText.text += $"(1)Eat some meat\n";
                else gameText.text += $"(1)Eat some more meat\n";
                gameText.text += $"(2) Go to SW\n(3) Go to the middle of the campus\n(4) Go to SE";
                break;
            case Levels.Mensa2:
                if (!hasEatenMeat) gameText.text = $"Nobody pays any attention to you as you sit down. The meat has a weird texture but it tastes good, like chicken.\n(1) Back to the entrance";
                else gameText.text = $"<color=red>Bad ending:</color>\nThere is no more meat and you feel their hungry eyes search for more. Finally their eyes settle on you, they grab you and drag you to the kitchen before your view fades to black you realize what \"Longpork\" is...\nPress any key";
                break;
            case Levels.NSector:
                gameText.text = $"You are in front of the N buildings. ND, where the biology faculty resides is overgrown with twisted greenery and the other buildings are mysteriously quite.\n(1) Enter NA\n(2) Enter NB where the physics faculty resides\n (3) Enter NC where the chemistry faculty resides\n(4) Go to the Mensa\n(5) Go to the middle of the campus\n(6) Go to NE";
                if (hasBeer) gameText.text += $"(7) Go to the botanical Garden";
                break;
            case Levels.NA:
                gameText.text = $"<color=red>Bad ending:</color>\nYou obviously have forgotten that the NA is empty because it is filled with super deady PCB, atleast you die painlessly...\nPress any key";
                break;
            case Levels.NB:
                gameText.text = $"<color=red>Bad ending:</color>\nToo late you realize the phsysics students have rigged the building with deadly laser traps, you end up as a heap of cubed longpork...\nPress any key;";
                break;
            case Levels.NC:
                gameText.text = $"As you enter the building the smell of chemicals numbs your sense of smell and you start to feel dizzy. A student in a hazmat suit armed with a broom blocks your way.\n(1) Leave the building\n";
                if (hasJM) gameText.text += $"(2) Give him a bottle of <color=green>Jägermeister</color>\n";
                if (hasBeer) gameText.text += $"(3)Go through the building to the botanical garden";
                break;
            case Levels.NC2:
                if (!hasBeer)
                {
                    hasBeer = true;
                    gameText.text = $"The Student takes the bottle and goes to a closet. He comes back to give you a gas mask and a botte of <color=yellow>beer</color> and gives you a thumbs up.\nYou can now cross the building to the botanical Garden.\n";
                }
                else gameText.text = $"The student happily takes another bottle.\n";
                gameText.text += $"(1) Leave the building to the front\n(2)Cross the building towards the botanical Garden";
                break;
            case Levels.Garden:
                gameText.text = $"You are in fron of the botanical garden. The trees seem bigger and darker than you remember them. Something is lurking in the shadows, something with antlers.\n(1) Go back to the front of the N-buildings\n(2) Flee through the woods";
                break;
            case Levels.Garden2:
                gameText.text = $"<color=red>Bad ending:<color>\nGiant shadowy figures in black rags begin to hunt you down, there is no escape. You are ripped apart and the shadows consume you...\nPress any key;";
                break;
            case Levels.CursedEnd:
                gameText.text = $"<color=red>Cursed ending:<color>\nYou walk for miles and miles while the shadows stalk you. They urge you to drink the Jägermeister until you give in. You continue walking and you continue to drink. You feel your limbs getting heavier and heavier and your antlers growing bigger and bigger. Finally your transformation is complete, you have become a <color=red>Jägermeister</color> and with your new compatriots you continue to hunt down the poor souls of your realm.\nPress any key;";
                break;
            case Levels.ISector:
                gameText.text = $"You are in front of the I-buildings. IC and ID have been turned into some kind of factories and sounds of construction comes from them.\n(1) Enter IA where the geoscience faculty resides\n(2) Enter IB where the faculties of math and psychology reside\n(3) Enter ID where the faculties of architecture & environment and mechanical engineering reside\n(4) Enter ID where EtIt resides\n(5) Got to the middle of campus\n(6) Go to SE\n(7) Flee the campus via tram";
                break;
            case Levels.IA:
                gameText.text = $"The students have somehow converted the building into a mine and within moments you are lost in the tunnels. You come across a nice fellow named Mathis, he has a fine assortment of fancy homemade liquors and offers you to show you the way out.\n(1) Leave with Mathis\n";
                if (hasJM) gameText.text += $"(2) Give him a bottle of <color=green>Jägermeister</color> as thanks for leading you out.";
                break;
            case Levels.IA2:
                if (hasWhiskey) gameText.text = $"He thanks you hesitantly and hands the bottle to some other miner while he leads you out.\n(1) Leave";
                else
                {
                    gameText.text = $"He thanks you and gives you a bottle of nice <color=#a52a2aff>Whiskey<color>";
                    hasWhiskey = true;
                }
                break;
            case Levels.IB:
                gameText.text = $"<color=red>Bad ending:<color>\nYou accidentally run into a psychology student, which causes them to burst scream hysterically, this distracts one of the math students and they accidentally divide by zero. The entire campus is swallowed by the resulting black hole...\nPress any key";
                break;
            case Levels.ICD:
                gameText.text = $"<color=red>Bad ending:<color>\nAs you enter the building you realize why the buildings IC and ID have been converted into factories. The faculties have begun to wage an eternal war against each other using elaborate roboters. Unfortunately the machines only distinguish between their masters and enemies, you do not even have the time to turn around and flee...\nPress any key";
                break;
            case Levels.Audimax:
                gameText.text = $"In the middle of the floor is a huge scorch mark surrounded by 4 smaller scorch marks each in a respectively <color=green>green</color>, <color=#a52a2aff>brown</color>, <color=blue>blue</color> and <color=yellow>yellow</color> square. A Disfigured corpse sits in front of a smashed laptop displaying a discord chat, where the server admins freak out that no one is answering them anymore.\n(1) Leave\n";
                if (hasBeer && hasWhiskey && hasSake && hasJM && !secretFound) gameText.text += $"(2) Place the alcoholica on the squares";
                break;
            case Levels.Secret:
                secretFound = true;
                gameText.text = $"\"You see, I do not want to complain but I physically have to or my head will explode!\" says a dude in a hawaiian shirt as he and 4 other people step out of the portal that suddenly appeared. The group, the hawaiian shirt dude, a dude in a dress shirt and camo pants, a dude in a firefighter outfit, a cute dudette who looks extremely stressed and a sexy dudette in some sexy vans -holyshit that is another dude- look at tyou, then at the mangled corpse behind the laptop.\n(1) to continue";
                break;
            case Levels.Secret2:
                gameText.text = $"\"Ireally wanted to complain but I am not gonna talk shit about dead people, even after he has talked shit about people in the protocol\" says the hawaiian shirt dude.\nAnyway, this is just a cameo appearance and by no means meant to dismiss the hard work of not-referenced people of the student council and I already spent way too much time on this \"game\", congratulations, you found the secret room but can defeat the Jägermeister?\n(1) To leave";
                break;
            case Levels.Final:
                gameText.text = $"You did it! Or rather you body did, the last ethanol molecule critically blocking your synapses has been lodged free. You awake to the general assembly of the student body and they are still discussing unnecessary stuff...\n(1) to listen to the ongoing discussion";
                isFinal = true;
                break;
            case Levels.Final2:
                assemblyCounter++;
                gameText.text = $"The discussions keep going on and on and on and on and on....\n(1) to continue\n";
                if (assemblyCounter >= 10 && assemblyCounter < 20) gameText.text += $"(2) to drink a shot of <color=green>Jägermeister</color> that is offered to you";
                if (assemblyCounter >= 20 && assemblyCounter < 30) gameText.text += $"(2) some <color=green>Jägermeister</color> could really make this less boring";
                if (assemblyCounter >= 30 && assemblyCounter < 40) gameText.text += $"(2) the discussions.... they never end, atleast <color=green>Jägermeister</color> makes it bearable, take a sip?";
                if (assemblyCounter >= 40 && assemblyCounter < 50) gameText.text += $"(2) there really is no end to this, only one way out, drink some <color=green>Jägermeister</color>!";
                break;
            case Levels.Victory:
                gameText.text = $"<color=green>Good ending:<color>\n\"And this concludes this semester's general assembly, you do not have to go home but you cannot stay here!\"\n It really is done, the assembly is over, you can finally go home but it is already dark out...\nand you have to go through the forest...\nwhere things lurk among the shadows...\nFIN";
                break;
            case Levels.FinalFail:
                gameText.text = $"<color=red>Bad ending:<color>\nAnnoyed by the discussion you take a strong swig of Jägermeister and a blissfull numbness filss your body...";
                break;
        }
    }
    private void Reset()
    {
        isFinal = hasJM = hasSake = hasWhiskey = hasBeer = hasEatenMeat = false;
        newLevel = Levels.Intro2;
        SetLevel(newLevel);

    }
}
