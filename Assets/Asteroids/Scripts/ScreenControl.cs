﻿using UnityEngine;

public class ScreenControl : MonoBehaviour
{
    Camera screenCam;

    void Start()
    {
        screenCam = Camera.main;
    }

    void FixedUpdate()
    {
        SetSpacePosition();
    }

    void SetSpacePosition()
    {
        float sceneWidth = screenCam.orthographicSize * 2 * screenCam.aspect;
        float sceneHeight = screenCam.orthographicSize * 2;

        float sceneRightEdge = sceneWidth / 2;
        float sceneLeftEdge = sceneRightEdge * -1;
        float sceneTopEdge = sceneHeight / 2 ;
        float sceneBottomEdge = sceneTopEdge * -1 ;

        if (transform.position.x > sceneRightEdge)
        {
            transform.position = new Vector2(sceneLeftEdge, transform.position.y);
        }
        if (transform.position.x < sceneLeftEdge)
        {
            transform.position = new Vector2(sceneRightEdge, transform.position.y);
        }
        if (transform.position.y > sceneTopEdge+2)
        {
            transform.position = new Vector2(transform.position.x, sceneBottomEdge);
        }
        if (transform.position.y < sceneBottomEdge-1)
        {
            transform.position = new Vector2(transform.position.x, sceneTopEdge);
        }
    }
}
