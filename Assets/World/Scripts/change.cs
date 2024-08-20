using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class change : MonoBehaviour
{

    public Sprite s1;
    public Sprite s2;
    public bool swt = true;
    public void exchange(int how = 0){
        if(how == 0){
            gameObject.GetComponent<SpriteRenderer>().sprite = s1;
            gameObject.GetComponent<Shini>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            swt = false;
        }else{
            gameObject.GetComponent<SpriteRenderer>().sprite = s2;
            gameObject.GetComponent<Shini>().enabled = true;
            swt = true;
        }
        
    }
}
