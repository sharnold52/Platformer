using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    // player prefab
    public GameObject playerPrefab;

    // current player and camera
    GameObject playerCur;
    GameObject playerCurCam;

    // player spawn point
    float respawnX;
    float respawnY;


	// Use this for initialization
	void Start ()
    {
        playerCur = FindObjectOfType<player>().gameObject;
        playerCurCam = playerCur.GetComponent<Transform>().Find("CameraPosition").gameObject;
	}
	
	// Update is called once per frame
	void Update () 
{
		
	}

    void Respawn()
    {
        // delete current player and make new one at checkpoint
        Destroy(playerCur);
        playerCur = Instantiate(playerCur, new Vector2(respawnX, respawnY), Quaternion.identity);
    }
}
