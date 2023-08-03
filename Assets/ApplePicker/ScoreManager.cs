using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int score;
    public GameObject PlayerPrefab;
    //FEATURE POINT: Arrays : player prefabs are stored in arrays for ease of deletion
    public GameObject[] players;
    int cur_i=2;
    void Start()
    {
        //NEW FEATURE POINT : Arrays : player's baskets are stored in an array of game objects
        players = new GameObject[3];
        score = 0;
        //NEW FEATURE POINT : For loop : used to instantiate the player prefabs at different heights
        for (int i = 0; i < 3; i++)
        {
            GameObject kid;
            //FEATURE POINT: instantiating prefabs : dropped objects are instanted prefabs, as well as the player's "baskets"
            kid = Instantiate(PlayerPrefab, transform);
            kid.transform.position += new Vector3(0, i+1, 0);
            players[i] = kid;
        }
    }
    //FEATURE POINT: Scoring system : The player's score (number of toasts eaten) increases with every collision between the playerprefab and dropped items
    public void UpdateScore()
    {
        score++;
        //FEATURE POINT: Triggered Sounds : the enemy plays a "nom" sound effect when the player catches a toast
        GameObject sound = GameObject.Find("nom");
        sound.GetComponent<AudioSource>().Play();
        //FEATURE POINT: UI Text : UI Text is used to display the score, and is updated with every increase.
        GameObject localReference = GameObject.Find("ScoreTXT");
        localReference.GetComponent<TextMeshProUGUI>().text = "Score: " + score;
    }
    public void LoseLife()
    {
        //cur_i is the index for the array of player baskets
        if (cur_i > -1)
        {
            Destroy(players[cur_i]);
            //FEATURE POINT: Triggered Sounds : the enemy plays a death sound effect when the player loses a life
            GameObject sound = GameObject.Find("die");
            sound.GetComponent<AudioSource>().Play();
        }
        cur_i--;
        if(cur_i == -1) //end of array
        {
            //game over
            //FEATURE POINT: loadscene : loads the inputed scene;
            SceneManager.LoadScene("AppleGameOver");
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
