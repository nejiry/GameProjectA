using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public float RightLimit;
    public float LeftLimit;
    public float UpLimit;
    public float BottomLimit;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player != null){
            Vector3 playerPosition = player.transform.position;
            playerPosition.z = -10;

            if(player.transform.position.x > RightLimit){
                playerPosition.x = RightLimit;
            }
            if(player.transform.position.x < LeftLimit){
                playerPosition.x = LeftLimit;
            }
            if(player.transform.position.y < BottomLimit){
                playerPosition.y = BottomLimit;
            }
            if(player.transform.position.y > UpLimit){
                playerPosition.y = UpLimit;
            }
            this.transform.position =  playerPosition;
        }
    }
}
