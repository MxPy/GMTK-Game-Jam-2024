using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFight : EntityStatistics
{
    public GameObject menu;
    public bool isMenuShown = false;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if(isMenuShown){
            menu.SetActive(true);
        }else{
            menu.SetActive(false);
        }
    }

    public override void shortRangeAttack(GameObject target){
        base.shortRangeAttack(target);
    }

    public override void longRangeAttack(GameObject target){
        base.longRangeAttack(target);
    }
}
