using System;
using System.Collections;
using System.Collections.Generic;
using TarodevController;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    bool isHiden = false;
    public bool isInteractingWithSpot = false;
    public bool isInteractingWithInteraction = false;
    public Animator animator;
    GameObject player;
    GameObject lastTarget;
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
                animator.Play("succ");
                Debug.Log("chujj");
                //player.GetComponent<SpriteRenderer>().enabled = false;
                //player.GetComponent<PlayerController>().enabled = false;
                player.GetComponent<PlayerController>().lastTarget = lastTarget;
                isHiden = true;
            }else if(Input.GetKeyDown(KeyCode.F) && isHiden){
                animator.Play("idle");
                Debug.Log("chujj2");
                player.GetComponent<SpriteRenderer>().enabled = true;
                player.GetComponent<PlayerController>().enabled = true;
                lastTarget.GetComponent<change>().exchange();
                player.GetComponent<PlayerController>().lastTarget = null;
                isHiden = false;
            }
            
        }

        if(Input.GetKeyDown(KeyCode.F) && isInteractingWithInteraction){
            Debug.Log("digkfhjnnli");
            lastTarget.GetComponent<InteractiveObject>().interaction();
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
            lastTarget = other.gameObject;
        }
        else if (other.gameObject.tag == "Interaction"){
            isInteractingWithInteraction = true;
            lastTarget = other.gameObject;
        }
    }

    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionEnter2D(Collision2D other)
    {
         if (other.gameObject.tag == "Kill"){
            SceneManager.LoadScene("gtmktest");
         }
    }

     /// <summary>
    /// Sent when a collider on another object stops touching this
    /// object's collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionExit2D(Collision2D other)
    {
        isInteractingWithSpot = false;
        isInteractingWithInteraction = false;
        lastTarget = null;
    }
}
