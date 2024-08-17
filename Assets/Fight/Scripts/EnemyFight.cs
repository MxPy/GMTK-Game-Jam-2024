using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFight : EntityStatistics
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void shortRangeAttack(GameObject target){
        base.shortRangeAttack(target);
    }

    public override void longRangeAttack(GameObject target){
        base.longRangeAttack(target);
    }
}
