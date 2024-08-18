using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drabajna : MonoBehaviour
{
    public GameObject dabTop;
    public GameObject drabBot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void actttic(){
        Debug.Log("3333333333333333333333333333333333");
        dabTop.SetActive(true);
        drabBot.SetActive(true);
    }

    public void actttic2(){
        if(dabTop.activeSelf == false){
            dabTop.SetActive(true);
        }else{
            dabTop.SetActive(false);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
