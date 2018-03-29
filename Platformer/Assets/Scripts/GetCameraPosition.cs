using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCameraPosition : MonoBehaviour
{

    // game object
    public GameObject cameraPos;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        // update camera's position
        gameObject.transform.position = cameraPos.transform.position;
	}
}
