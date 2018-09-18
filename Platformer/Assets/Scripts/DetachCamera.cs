using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetachCamera : MonoBehaviour
{
    // reference to camera and player
    GameObject playerCam;
    GameObject playerObj;

    // scene manager for respawning player
    

	// Use this for initialization
	void Start ()
    {
        // get the player
        playerObj = FindObjectOfType<player>().gameObject;

        // get the player cam
        playerCam = playerObj.transform.Find("CameraPosition").gameObject;
	}
	
	// Update is called once per frame
	void Update ()
    {
        // if player was deleted, find new one
        if(playerObj == null)
        {
            // get the player
            playerObj = FindObjectOfType<player>().gameObject;

            // get the player cam
            playerCam = playerObj.transform.Find("CameraPosition").gameObject;
        }
	}

    // runs when object enters trigger
    void OnTriggerEnter2D(Collider2D other)
    {
        // remove camera from player
        playerCam.transform.parent = null;

        // delete player after set amount of time (enough time for player to fall off screen)
        Invoke("DeletePlayer", 3);
    }

    // deletes player after 2 seconds
    void DeletePlayer()
    {
        // reset player position
        
    }

}
