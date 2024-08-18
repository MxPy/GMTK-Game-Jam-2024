using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class InteractiveObject : MonoBehaviour
{
    public string type;


    public void interaction(){
        if(type == "gun"){
            gameObject.GetComponent<Animator>().Play("sht");
        }else if(type == "drabajna"){
            gameObject.GetComponent<drabajna>().actttic();
            gameObject.SetActive(false);
        }else if(type == "dabbot"){
            gameObject.GetComponent<drabajna>().actttic2();
        }
    }
}
