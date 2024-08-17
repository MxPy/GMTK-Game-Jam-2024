using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    public string type;
    public void interaction(){
        if(type == "gun"){
            gameObject.GetComponent<Animator>().Play("sht");
        }
    }
}
