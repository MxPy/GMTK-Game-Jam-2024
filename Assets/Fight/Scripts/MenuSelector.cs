using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class MenuSelector : MonoBehaviour
{
    PlayerFight player;

    public GameObject[] menuElements;

    int selector = 0;


    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        menuElements[selector].SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S)){
            selector +=1;
            if (selector>= menuElements.Length){
                selector = 0;
            }
            menuElements[selector].SetActive(true);
            if(selector != 0){
                menuElements[selector-1].SetActive(false);
            }else{
                menuElements[^1].SetActive(false);
            }
        }else if(Input.GetKeyDown(KeyCode.W)){
            selector -=1;
            if (selector < 0){
                selector = menuElements.Length-1;
            }
            menuElements[selector].SetActive(true);
            if(selector != menuElements.Length-1){
                menuElements[selector+1].SetActive(false);
            }else{
                menuElements[0].SetActive(false);
            }
        }

    }
}
