using System;
using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    bool isHiden = false;
    bool isInteractingWithSpot = false;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
    }

    // Update is called once per frame
    void Update()
    {
        if(isInteractingWithSpot){
            
            if(Input.GetKeyDown(KeyCode.F) && !isHiden){
                Debug.Log("chujj");
                player.GetComponent<SpriteRenderer>().enabled = false;
                player.GetComponent<PlayerController>().enabled = false;
                isHiden = true;
            }else if(Input.GetKeyDown(KeyCode.F) && isHiden){
                Debug.Log("chujj2");
                player.GetComponent<SpriteRenderer>().enabled = true;
                player.GetComponent<PlayerController>().enabled = true;
                isHiden = false;
            }
            
        }
        
    }

    /// <summary>
    /// Sent each frame where a collider on another object is touching
    /// this object's collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "HidenSpot"){
            isInteractingWithSpot = true;
        }else{
            isInteractingWithSpot = false;
        }
    }
}
