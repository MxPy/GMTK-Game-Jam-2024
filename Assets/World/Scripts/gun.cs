using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun : MonoBehaviour
{

    public GameObject flashbang;

    public GameObject exit;
    public GameObject exit2;


    public float flashhh = 1;

    bool doIt = false;


    // Start is called before the first frame update
    void Start()
    {
       
        
    }

    public void shoooot(){
        doIt = true;

    }

    // Update is called once per frame
    void Update()
    {
        if(flashhh>0 && doIt){
            flashbang.SetActive(true);
            exit.SetActive(true);
            exit2.SetActive(true);
            gameObject.GetComponent<Animator>().Play("idle2");
            flashbang.GetComponent<SpriteRenderer>().color = new Color(1,1,1,flashhh);
            gameObject.GetComponent<Shini>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            gameObject.tag = "Untagged";
            flashhh-=0.1f*Time.deltaTime;

        }
    }
}
