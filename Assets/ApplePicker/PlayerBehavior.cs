using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        //FEATURE POINT: Tracking the mouse : this code below track's the player's mouse position
        //and moves the character accordingly along the x-axis
        Vector3 mouse = Input.mousePosition;
        Vector3 newPos = Camera.main.ScreenToWorldPoint(mouse);
        newPos.z = transform.position.z;
        newPos.y = transform.position.y;
        newPos.x = Mathf.Clamp(newPos.x, -7.23f, 7.23f);
        transform.position = newPos;
    }
    //FEATURE POINT: colliders : coliders are on the border of the camera and on nearly every game object,
    //and are used to trigger various events in the game
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //FEATURE POINT: tags : dropped items are tagged as "dropped" for use in destruction of those prefabs
        if (collision.gameObject.tag == "dropped")
        {
            Destroy(collision.gameObject);
            //FEATURE POINT: Scores : The player's score (number of toasts eaten) increases with every collision between the playerprefab and dropped items

            
            //NEW FEATURE POINT : GetComponentInParent : used to access the scoremanager script in the parent of prefab
            GetComponentInParent<ScoreManager>().UpdateScore();

            GameObject localReference = GameObject.Find("Square");
            localReference.GetComponent<BoxBehavior>().increaseSpeed();
        }
    }
}

