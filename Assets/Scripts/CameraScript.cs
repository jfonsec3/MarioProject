using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    private GameObject player;
    private Vector3 offset;

    private GameObject background;
    private SpriteRenderer sr;

    private float cameraHeight, cameraWidth;
    private float backgroundHeight, backgroundWidth;
    private float minX, minY, maxX, maxY;

    void Start()
    {
        background = GameObject.FindGameObjectWithTag("Background");
        player = GameObject.FindGameObjectWithTag("Player");
        offset = transform.position - player.transform.position;

        sr = background.GetComponent<SpriteRenderer>();

        backgroundHeight = sr.size.y * sr.transform.lossyScale.y;
        backgroundWidth = sr.size.x * sr.transform.lossyScale.x;

        cameraHeight = Camera.main.orthographicSize;
        cameraWidth = cameraHeight * Camera.main.aspect;

        minX = cameraWidth - (backgroundWidth / 2f);
        maxX = (backgroundWidth / 2f ) - cameraWidth;
        minY = cameraHeight - (backgroundHeight / 2f);
        maxY = (backgroundHeight / 2f) - cameraHeight; 

    }

    void LateUpdate()
    {
        Vector3 temp = player.transform.position + offset;

        transform.position = new Vector3(CheckXBounds(temp) , CheckYBounds(temp) , -5);
    }

    private float CheckXBounds(Vector3 pos)
    {
        float X = pos.x;
        if (pos.x < minX)
        {
            X = minX;
        }
        if (pos.x > maxX)
        {
            X = maxX;
        }

        return X;
    }
    private float CheckYBounds(Vector3 pos)
    {
        float Y = pos.y;
        if (pos.y < minY)
        {
            Y = minY;
        }
        if (pos.y > maxY)
        {
            Y = maxY;
        }

        return Y;
    }
}
