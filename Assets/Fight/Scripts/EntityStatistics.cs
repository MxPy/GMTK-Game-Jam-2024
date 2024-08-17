using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStatistics : MonoBehaviour
{
    public int hp;
    public int maxHp = 100;
    public int mp;
    public int maxMp = 100;
    public int stamina;
    public int maxStamina = 0;
    public int dmg = 10;
    public int speed = 5;
    public int speedCounter = 1;
    public bool isDead = false;
    public bool isSelected = false;
    public GameObject indicator;
    


    // Start is called before the first frame update
    protected virtual void Start()
    {
        hp = maxHp;
        mp = maxMp;
        stamina = maxStamina;
        indicator.SetActive(false);
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(hp<maxHp){
            isDead = true;
        }

        if(isSelected){
            indicator.SetActive(true);
        }else{
            indicator.SetActive(false);
        }
        
    }

    void goToEnemy(GameObject target){

    }

    public void getDmg(int enemyDmg){
        hp -= enemyDmg;
    }
    public void getHeal(int heal){
        hp += heal;
    }

    virtual public void shortRangeAttack(GameObject target){
        goToEnemy(target);
    }

    virtual public void longRangeAttack(GameObject target){
        
    }
}
