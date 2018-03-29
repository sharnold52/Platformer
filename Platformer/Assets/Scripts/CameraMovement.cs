using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // player
    public GameObject player;

    // value that camera is offset by (player a little to the left or right of camera)
    public float offSet = 0;

    // value that camera moves every frame when player switches direction
    public float movementIncrement = 0.1f;

    // bool to tell camera when offset was flipped
    public bool flip = false;
    public bool facingRight = true;

    // player and camera position 
    private Vector3 position;
    private Vector3 camPosition;

	// Use this for initialization
	void Start ()
    {
        // get initial position of player and camera
        position = player.GetComponent<Transform>().position;
        camPosition = gameObject.GetComponent<Transform>().localPosition;
	}
	
	// Update is called once per frame
	void Update ()
    {
        // update position of player and camera
        position = player.GetComponent<Transform>().position;
        camPosition = gameObject.GetComponent<Transform>().localPosition;

        // update camera's position (Check if player switched directions)
        if (flip)
        {
            // player switched directions, transition between offsets smoothly
            if(facingRight && camPosition.x != offSet)
            {
                // increments cam position
                camPosition.x += movementIncrement;

                // checks if it is done incrementing
                if(camPosition.x >= offSet)
                {
                    flip = false;
                    camPosition.x = offSet;
                }
            }
            else if(!facingRight && camPosition.x != offSet)
            {
                // increments cam position
                camPosition.x += movementIncrement;

                // checks if it is done incrementing
                if (camPosition.x >= offSet)
                {
                    flip = false;
                    camPosition.x = offSet;
                }
            }

            // update camera position
            gameObject.GetComponent<Transform>().localPosition = camPosition;
        }
    }
}
