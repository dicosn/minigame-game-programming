using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    ///HITBOXES AND LOCATIONS
    Vector2 player_pos = new Vector2(5, 2);
    
    //world boundaries
    Vector2 SE_corner = new Vector2(11, 0);
    Vector2 NW_corner = new Vector2(0, 8);

    //bridge hitbox
    Vector2 BridgeSE = new Vector2(11, 3);
    Vector2 BridgeNW = new Vector2(0, 6);

    //Wide Goblin hitbox
    Vector2 WideGobSE = new Vector2(9, 4);
    //Secret hole in wide goblin's body the player can slip through to skip riddle
    //hole is at 10,5
    Vector2 WideGobNW = new Vector2(0, 5);

    //Theif
    Vector2 ThiefGob_pos = new Vector2(3, 1);

    //Hobgoblin
    Vector2 Hobgob_pos = new Vector2(8, 7);

    //list of vectors
    //feature point: lists - function returns a list of the current positions of objects
    List<Vector2> getPosList(List<Vector2> positions)
    {
        positions.Add(SE_corner);
        positions.Add(NW_corner);
        positions.Add(BridgeSE);
        positions.Add(BridgeNW);
        positions.Add(WideGobSE);
        positions.Add(WideGobNW);
        positions.Add(ThiefGob_pos);
        positions.Add(Hobgob_pos);
        return positions;
    }

    ///progress variables
    int eyes = 0;
    bool riddle_sol = false;
    bool theif_dead = false;
    bool Hobgob_dead = false;
    bool answer = false;

    //sensory overload
    bool sensory_o = false;
    int sense_uses = 10;
    int recharge = 20;

    //feature point: UI text : outputs a description of the player's current position and surroundings to the game screen.
    void TextFeedback()
    {
        GameObject localReference = GameObject.Find("Feedback");
        localReference.GetComponent<TextMeshProUGUI>().text = "You take a step.";
        GameObject localRef = GameObject.Find("road steps");
        if(player_pos.y >2 && player_pos.y < 7)
        {
            localReference.GetComponent<TextMeshProUGUI>().text += " The ground creaks and buckles under your weight. It seems like a wooden bridge, if the thousands of splinters on your legs are of any indication.";
            //play footsteps walking on wood sfx
            localRef = GameObject.Find("better wood");
            localRef.GetComponent<AudioSource>().Play();
        }
        else
        {
            localReference.GetComponent<TextMeshProUGUI>().text += " You sense the ground below you is asphault, as you feel the pavement between your toes.";
            //play footstep for road walking
            localRef.GetComponent<AudioSource>().Play();
        }
        //get list of pos
        List<Vector2> positions = new List<Vector2>();
        positions = getPosList(positions);
        //NEW FEATURE POINT : foreach : used to account for potentially unaccountable number of positions
        foreach(Vector2 pos in positions)
        {
            if(Vector2.Distance(pos, player_pos) < 2)
            {
                if (pos == Hobgob_pos)
                {
                    localReference.GetComponent<TextMeshProUGUI>().text += " You are overpowered by the stench of a gold crown and goblin-musk nearby.";
                }
                else if(pos == ThiefGob_pos && !theif_dead)
                {
                    localReference.GetComponent<TextMeshProUGUI>().text += " You hear running footsteps and sinister giggles nearby.";
                }
                else if((pos == WideGobNW || pos == WideGobSE) && !riddle_sol)
                {
                    localReference.GetComponent<TextMeshProUGUI>().text += " The air is hot with the taste of bad breath, width, and indifference from a nearby source.";
                }
                else
                {
                    localReference.GetComponent<TextMeshProUGUI>().text += " Your hands brush against a solid object of some sort.";
                }
            }
        }
        //tracks how many vivid descriptions were used, and when the player can use more.
        if (sense_uses == 0)
        {
            recharge -= 1;
            //countdown is done, the player can use sensory overload again;.
            if (recharge == 0)
            {
                sense_uses = 10;
                recharge = 20;
            }
        }
        //adds more detailed description of locations of in game objects.
        else if (sensory_o)
        {
            localReference.GetComponent<TextMeshProUGUI>().text += " Concentrating your energy, you overload your remaining senses.";
            foreach (Vector2 pos in positions)
            {
                
                if (Vector2.Distance(pos, player_pos) < 5)
                {
                    //makes sure the object hasn't already been disposed of by the player
                    bool object_there = true;
                    Vector2 subVec = new Vector2(player_pos.x - pos.x, player_pos.y - pos.y);
                    //various descriptions
                    if (pos == Hobgob_pos)
                    {
                        localReference.GetComponent<TextMeshProUGUI>().text += " You smell the indistinguishable scent of gold crown and goblin musk about ";
                    }
                    else if (pos == ThiefGob_pos)
                    {
                        if (!theif_dead)
                        {
                            localReference.GetComponent<TextMeshProUGUI>().text += " You hear sinister giggles about ";
                        }
                        else
                        {
                            object_there = false;
                        }
                    }
                    else if ((pos == WideGobNW || pos == WideGobSE))
                    {
                        if (!riddle_sol)
                        {
                            localReference.GetComponent<TextMeshProUGUI>().text += " The air around tastes of sweat particles, indifference...and the edge of something wide? about ";
                        }
                        else
                        {
                            object_there = false;
                        }
                    }
                    else
                    {
                        localReference.GetComponent<TextMeshProUGUI>().text += " The hairs on yo' hands sense the air current change, indicating a corner of a building about ";
                    }
                    //what cardinal directions are the clues coming from, as well as their distances?
                    int dist = (int)Vector2.Distance(pos, player_pos);
                    dist = Mathf.Abs(dist);
                    if (object_there)
                    {
                        localReference.GetComponent<TextMeshProUGUI>().text += dist + " paces to the";
                        if (subVec.y < 0)
                        {
                            localReference.GetComponent<TextMeshProUGUI>().text += " north";
                        }
                        if (subVec.y > 0)
                        {
                            localReference.GetComponent<TextMeshProUGUI>().text += " south";
                        }
                        if (subVec.x > 0)
                        {
                            localReference.GetComponent<TextMeshProUGUI>().text += " west";
                        }
                        if (subVec.x < 0)
                        {
                            localReference.GetComponent<TextMeshProUGUI>().text += " east";
                        }
                        localReference.GetComponent<TextMeshProUGUI>().text += ".";
                    }
                }
            }
            sense_uses -= 1;
        }
       
    }
    //feature point: scoring system : GotAnEye() updates the number of eyes displayed on the player's screen
    void GotAnEye()
    {
        eyes += 1;
        GameObject localReference = GameObject.Find("Score");
        localReference.GetComponent<TextMeshProUGUI>().text = "Eyes: "+ eyes;
        localReference = GameObject.Find("Feedback");
        GameObject localRef2 = GameObject.Find("kids");
        if (eyes == 2)
        {
            localReference.GetComponent<TextMeshProUGUI>().text += "YOU GOT YOUR EYES BACK! YOU WIN! (You can still walk around if you want, there might be something more... it's up to you, I ain't your lawyer).";
           //special victory sound
            localRef2 = GameObject.Find("Kids_Acending");
        }
        //play victory sound w/ music
        localRef2.GetComponent<AudioSource>().Play();
        
    }
    //updates Thief's behavior
    void Update_Theif()
    {
        GameObject localReference = GameObject.Find("Feedback");
        GameObject localRef2 = GameObject.Find("thief");
        if (player_pos == ThiefGob_pos)
        {
            theif_dead = true;
            localReference.GetComponent<TextMeshProUGUI>().text = "You stepped on a small coniving goblin. As you scrape him off your big work-boot, " +
                "you feel something round and gooey in his hairy palm. It's your 2nd favorite eye!";
            localRef2.GetComponent<AudioSource>().Stop();
            GotAnEye();
        }
        else if(ThiefGob_pos.x > 1)
        {
            move_Gob();
        }
        else
        {
            //play laughing sound, with volume related to how close the player is
            float dist = Vector2.Distance(ThiefGob_pos, player_pos);
            localRef2.GetComponent<AudioSource>().volume = 0.5f - Mathf.Clamp(dist / 5f, 0f, .9f);
        }
    }
    //updates Wide Goblin's behavior
    void WideGoblinBehavior()
    {
        if (player_pos.x >= WideGobNW.x && player_pos.x <= WideGobSE.x && player_pos.y >= WideGobSE.y && player_pos.y <= WideGobNW.y)
        {
            GameObject localReference = GameObject.Find("Feedback");
            //feature point: triggers : the player triggers different events depending on how many eyes they have when they talk to the wide goblin
            if (eyes < 2)
            {
                
                if (eyes == 0)
                {
                    //message about not talking to those with no eyes
                    localReference.GetComponent<TextMeshProUGUI>().text = "You walk into a wall. It's fleshy and pungent. It's the Very-Wide Goblin who blocks the Similarly-Wide Bridge." +
                        " \"I do not speak to those with less than one eye.\" He says directly into your eyeless sockets without thinking critically.";
                }
                if (eyes == 1)
                {
                    //riddle time
                    localReference.GetComponent<TextMeshProUGUI>().text = "You walk up to the Very-Wide Goblin. You give him the hairy eyeball. \"If you wish to pass this bridge...!\" he booms. " +
                        "\"Answer me these riddles-one: What were you eating when you had your peepers stolen? GIVE THE ANSWER IN INTERPATIVE DANCE!\" " +
                        "(LEFT: PIZZA, RIGHT: DELICIOUS CHEESE PIZZA, UP: *KISS HIM TENDERLY ON THE CHEST*, DOWN: *WRAP YOUR ARMS AROUND HIM IN A VERY WIDE EMBRACE*)";
                    answer = true;
                }
            }
            else
            {
                //SECRET ENDING: Getting past the wide goblin without touching him
                localReference.GetComponent<TextMeshProUGUI>().text = " You encounter the Very-Wide Goblin \"Ah, so you slipped through the gaping hole in my body without detection, despite my gargantuan x-axis, well done. You didn't need your eyes back at all, " +
                    "for you see, your real eyes are in your heart.\" He gently opens your ribcage with a warm smile, and places your eyes inside the newly-formed chest cavity. One of them falls to the ground and rolls away. This is the True Ending.";
            }
        }
    }
    //updates HobGoblin's behavior
    void HobGob()
    {
        GameObject localRef2 = GameObject.Find("hobhob laugh");
        if (player_pos == Hobgob_pos)
        {
            GameObject localReference = GameObject.Find("Feedback");
            localReference.GetComponent<TextMeshProUGUI>().text = "You approach the Hobgoblin: King of the Goblins. \"HA HA HEE HA!!\" He cackles, staggering over you, bearing his jagged, yellow fangs." +
                " \"I have no motivation for taking your eye, and I don't have the wherewithall to defend myself! Here you go.\"";
            GotAnEye();
            Hobgob_dead = true;
        }
        else
        {
            //play laughter sound, with volume related to how close the player is
            float dist = Vector2.Distance(Hobgob_pos, player_pos);
            localRef2.GetComponent<AudioSource>().volume = 0.75f - Mathf.Clamp(dist / 5f, 0f, .9f);
            localRef2.GetComponent<AudioSource>().Play();
        }
    }
    //updates the scene in accordance to the player's actions-- a sort of catch-all for various events
    void Update_Scene()
    {
        GameObject localReference = GameObject.Find("band");
        localReference.GetComponent<AudioSource>().Stop();
        TextFeedback();
        if (!theif_dead)
        {
            Update_Theif();
        }
        if (!riddle_sol)
        {
            WideGoblinBehavior();
        }
        if (!Hobgob_dead)
        {
            HobGob();
        }
        //feature point: background music : ambiant background sounds play throughout the time of play-- like the ambiant sounds of cities, and nearby marching band. 
        localReference = GameObject.Find("traffic");
        float dist = (float) (player_pos.x - NW_corner.x);
        //feature point: dynamic volume: adjust ambient noise based on player's proximity to the edges of the world.
        //this feature is used for nearly every sound in the overworld, except for victory sounds, the player themselves walking,
        //and bumping into the edges of the world.
        localReference.GetComponent<AudioSource>().volume = 1f - Mathf.Clamp(dist / 5f, 0f, .9f);
        dist = (float)(SE_corner.x - player_pos.x);
        localReference = GameObject.Find("Parade");
        localReference.GetComponent<AudioSource>().volume = 1f - Mathf.Clamp(dist / 5f, 0f, .9f);
    }

    //Sensor toggle, changes bool sensory_o, which determines if more vivid descriptions are written
    public void ToggleSensor(bool sense)
    {
        
        if (sense_uses > 0)
        {
            sensory_o = sense;
        }
        else
        {
            sensory_o = false;
        }
        //feature point: debug lines : this line was used to make sure the toggle for sensory overload was actually changing anything, and was a major sources of bugs
        //early on in development.
        print("Sensory Overload is " + sensory_o);
        print("\nsense is " + sense);
    }


    //feature point: UI-button : moves the player's postion vector a distance corresponding to the button they pressed. 
    public void move_y(int val)
    {
        //feature point: enforcing boundaries : clamping prevents player from moving out of the game world. This is used for both the X and Y axis
        player_pos.y = Mathf.Clamp(player_pos.y+val, SE_corner.y, NW_corner.y);
        GameObject localReference = GameObject.Find("Feedback");
        if (answer)
        {
            localReference.GetComponent<TextMeshProUGUI>().text = "Tears roll down the Very-Wide Goblin's beet-red face. " +
                "He is touched, not only by you--physically touching him, but in his heart as well. The touching also knocks him off balance. " +
                "He falls on his back, melts into goo, and seeps through the cracks of the bridge.";
            answer = false;
            riddle_sol = true;
        }
        else if(player_pos.y == SE_corner.y)
        {
            if (!theif_dead)
            {
                Update_Theif();
            }
            //special message
            localReference.GetComponent<TextMeshProUGUI>().text = "You bump into the brick wall of the Cafe.";
        }
        else if(player_pos.y == NW_corner.y)
        {
            if (!theif_dead)
            {
                Update_Theif();
            }
            //special message
            localReference.GetComponent<TextMeshProUGUI>().text = "You walk face first into a wall of stone.";
        }
        else
        {
            Update_Scene();
        }
        GameObject localReference2 = GameObject.Find("Sense Uses");
        localReference2.GetComponent<TextMeshProUGUI>().text = "Uses left: "+sense_uses;
    }
    //UI Button
    public void move_x(int val)
    {
        player_pos.x = Mathf.Clamp(player_pos.x+val, NW_corner.x, SE_corner.x);
        GameObject localReference = GameObject.Find("Feedback");
        if (answer)
        {
            localReference.GetComponent<TextMeshProUGUI>().text = "Tears roll down the Very-Wide Goblin's beet-red face. " +
                "He is sad that you outwitted him, for he didn't even know the answer, himself. " +
                "He falls on his back, melts into goo, and seeps through the cracks of the bridge.";
            answer = false;
            riddle_sol = true;
        }
        else if (player_pos.x == SE_corner.x)
        {
            if (!theif_dead)
            {
                Update_Theif();
            }
            //play sound of a parade (angry trumpet)
            GameObject localRef2 = GameObject.Find("band");
            localRef2.GetComponent<AudioSource>().Play();
            //special message
            localReference.GetComponent<TextMeshProUGUI>().text = "You get to the edge of the block. There is a parade clogging the road for miles in celebration of congested roadways. You are nearly trampled by a vicious marching band as you take a step back.";
        }
        else if (player_pos.x == NW_corner.x)
        {
            if (!theif_dead)
            {
                Update_Theif();
            }
            //play sound of cars honking horns
            GameObject localRef2 = GameObject.Find("horn");
            //feature point: triggered sounds : feedback in the form of sounds for victory, walking a direction, and bumbing into to objects is used throughout the game
            localRef2.GetComponent<AudioSource>().Play();
            //special message
            localReference.GetComponent<TextMeshProUGUI>().text = "You get to the edge of the sidewalk, but are blocked by cars bumper to bumper for miles. There is gridlock because of the parade accross the street. You take a step back.";
        }
        else
        {
            Update_Scene();
        }
        GameObject localReference2 = GameObject.Find("Sense Uses");
        localReference2.GetComponent<TextMeshProUGUI>().text = "Uses left: " + sense_uses;
    }
    //The thief should be running, move its x position
    public void move_Gob()
    {
        ThiefGob_pos.x -= 1;
        //play running sound.
        float dist = Vector2.Distance(ThiefGob_pos, player_pos);
        GameObject localRef2 = GameObject.Find("running");
        localRef2.GetComponent<AudioSource>().volume = 1f - Mathf.Clamp(dist / 5f, 0f, .7f);
        localRef2.GetComponent<AudioSource>().Play();
    }
}
