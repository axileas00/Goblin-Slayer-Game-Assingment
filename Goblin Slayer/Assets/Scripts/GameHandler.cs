using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public CameraFollows cameraFollow;
    public Transform playerTransform;
    
    void Start()
    {
        cameraFollow.Setup(() => playerTransform.position);
    }//sets camera to fallow the given transform
}
