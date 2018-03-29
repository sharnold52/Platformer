using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    //parallax
    public float parallaxSpeed;
    public bool parallax, scrolling;
    private float lastCameraX;
    private float lastCameraY;

    float backgroundSize = 0;

    private Transform cameraTransform;
    private Transform[] layers;
    private float viewZone = 0;
    private int leftIndex;
    private int rightIndex;

    // Use this for initialization
    void Start ()
    {
        // position of camera
        cameraTransform = Camera.main.transform;
        lastCameraX = cameraTransform.position.x;
        lastCameraY = cameraTransform.position.y;

        // layers of background
        layers = new Transform[transform.childCount];

        // set the background layers
        for (int i = 0; i < transform.childCount; i++)
        {
            layers[i] = transform.GetChild(i);
        }

        // get the size of the background image
        backgroundSize = layers[0].GetComponent<SpriteRenderer>().bounds.size.x;

        // set the left and right indexes of the array
        rightIndex = 0;
        leftIndex = layers.Length - 1;
    }

    private void Update()
    {
        // camera position and parallax
        if(parallax)
        {
            float deltaX = cameraTransform.position.x - lastCameraX;
            transform.position += Vector3.right * (deltaX * parallaxSpeed);

            float deltaY = cameraTransform.position.y - lastCameraY;
            transform.position += Vector3.up * (deltaY * parallaxSpeed);
        }
        lastCameraX = cameraTransform.position.x;
        lastCameraY = cameraTransform.position.y;

        // check if player is scrolling
        if(scrolling)
        {
            // move background images accordingly
            if (cameraTransform.position.x < (layers[leftIndex].transform.position.x + viewZone))
            {
                ScrollLeft();
            }
            if (cameraTransform.position.x > (layers[rightIndex].transform.position.x - viewZone))
            {
                ScrollRight();
            }
        }
    }

    // scroll to the left
    void ScrollLeft()
    {
        int lastRight = rightIndex;
        layers[rightIndex].position = Vector3.right * (layers[leftIndex].position.x - backgroundSize);
        layers[rightIndex].localPosition = new Vector3(layers[rightIndex].localPosition.x, 0, 0);
        leftIndex = rightIndex;
        rightIndex--;

        if(rightIndex < 0)
        {
            rightIndex = layers.Length - 1;
        }
    }

    // scroll to the right
    void ScrollRight()
    {
        int lastLeft = leftIndex;
        layers[leftIndex].position = Vector3.right * (layers[rightIndex].position.x + backgroundSize);
        layers[leftIndex].localPosition = new Vector3(layers[leftIndex].localPosition.x, 0, 0);
        rightIndex = leftIndex;
        leftIndex++;

        if (leftIndex == layers.Length)
        {
            leftIndex = 0;
        }
    }
}
