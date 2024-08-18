using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheatCode : MonoBehaviour
{
    private string[] meme, tpChest, tpFight, tpEmpty, invisible, strengh, money, active, boss, hub;
    bool isActive;
    private int index,index1,index2,index3,index4,index5,index6, index7, index8, index9;
    public Sprite spriteImage;

    public GameObject memeI;
     
     void Start() {
         // Code is "idkfa", user needs to input this in the right order
         isActive = true;
         active = new string[] {"m","a","r","v","i","n"};
         meme = new string[] {"n", "u", "l", "l", "p","o","i","n","t","e","r" };
         tpChest = new string[] {"s","u","p","r","e","m","e"};
         tpFight = new string[] {"h","a","u","n","t","e","d"};
         tpEmpty = new string[] {"e","n","j","o","y","t","h","e","s","i","l","e","n","c","e"};
         invisible = new string[] {"y","o","u","c","a","n","t","l","e","a","v","e","m","e","a","l","o","n","e"};
         strengh = new string[] {"t","n","e","c","o","n","n","i"};
         money = new string[] {"m","o","t","h","e","r","l","o","d","e"};
         boss = new string[] {"t","h","e","i","n","s","i","d","e","w","a","l","t","z"};
         hub = new string[] {"/","h","o","m","e"};
         index = 0; 
         index1 = 0;
         index2 = 0;   
         index3 = 0;
         index4 = 0;
         index5 = 0;
         index6 = 0;
         index7 = 0;
         index8 = 0;
         index9 = 0;
     }
     
     void Update() {
         
         if (Input.anyKeyDown) {
                if (Input.GetKeyDown(active[index1])) {index1++;}else {index1 = 0;}
            }
         if(isActive){
            if (Input.anyKeyDown) {
                if (Input.GetKeyDown(meme[index])) {index++;}else {index = 0;}
                if (Input.GetKeyDown(money[index2])) {index2++;}else {index2 = 0;}
                if (Input.GetKeyDown(invisible[index3])) {index3++;}else {index3 = 0;}
                if (Input.GetKeyDown(tpChest[index4])) {index4++;}else {index4 = 0;}
                if (Input.GetKeyDown(tpFight[index5])) {index5++;}else {index5 = 0;}
                if (Input.GetKeyDown(tpEmpty[index6])) {index6++;}else {index6 = 0;}
                if (Input.GetKeyDown(strengh[index7])) {index7++;}else {index7 = 0;}
                if (Input.GetKeyDown(boss[index8])) {index8++;}else {index8 = 0;}
                if (Input.GetKeyDown(hub[index9])) {index9++;}else {index9 = 0;}
            }
         }
         
   
         if (index == meme.Length) {
            memeI.SetActive(true);
            index = 0;
         }
        if (index1 == active.Length) {
            isActive = true;
            index1 = 0;
        }
        if (index2 == money.Length) {
            index2 = 0;
        }
        if (index3 == invisible.Length) {
            index3 = 0;
        }
        if (index4 == tpChest.Length) {
            SceneManager.LoadScene("Chest");
            index4 = 0;
        }
        if (index5 == tpFight.Length) {
            SceneManager.LoadScene("Fight");
            index5 = 0;
        }
        if (index6 == tpEmpty.Length) {
            SceneManager.LoadScene("Empty");
            index6 = 0;
        }
        if (index8 == boss.Length) {
            SceneManager.LoadScene("boss");
            index8 = 0;
        }
        if (index7 == strengh.Length) {
            index7 = 0;
        }
        if (index9 == hub.Length) {
            SceneManager.LoadScene("Game");;
            index9 = 0;
        }
     }
}
