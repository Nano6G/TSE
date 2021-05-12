using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossScript : EnemyScript
{
    protected override void Move()
    {
        //direction to move
        Vector2 dir = target.position - transform.position;

        //move towards target, normalized fixes size so speed doesnt change
        transform.Translate(dir.normalized * (speed - ((SlowAmount / 2) / 100 * speed)) * Time.deltaTime, Space.World);

        if (Vector2.Distance(transform.position, target.position) <= .3f)
        {
            GetNextWayPoint();
        }
    }
}
