using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    // Start is called before the first frame update

    public CameraFollows cameraFollow;
    public Transform playerTransform;
    
    

    void Start()
    {
        cameraFollow.Setup(() => playerTransform.position);
   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
