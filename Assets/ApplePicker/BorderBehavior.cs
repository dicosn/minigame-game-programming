using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //FEATURE POINT: colliders : coliders are on the border of the camera and on nearly every game object,
    //and are used to trigger various events in the game
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //FEATURE POINT: tags : dropped items are tagged as "dropped" for use in destruction of those prefabs
        if (collision.gameObject.tag == "dropped")
        {

            //Destroy a player prefab
            GameObject.Find("ScoreManager").GetComponent<ScoreManager>().LoseLife();
        }
        //NEW FEATURE POINT : foreach : used for each to destory the potentially numerous dropped object tha thit the border
        foreach (GameObject drop in GameObject.FindGameObjectsWithTag("dropped"))
        {
            Destroy(drop);
        }
    }
}
