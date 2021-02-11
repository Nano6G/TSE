using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//EnemyScript can be broken up if too muhc is added
public class EnemyScript : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f, health = 1;

    private Transform target;
    //where on path the enemy is
    private int wavePoint;

    //should be called before fully spawned
    public void assignStats(float speedin, float healthin, Sprite spritein)
    {
        speed = speedin; health = healthin; GetComponent<SpriteRenderer>().sprite = spritein;
    }
    void Start()
    {
        target = WaypointsScript.points[0];
    }

    private void Update()
    {
        //direction to move
        Vector2 dir = target.position - transform.position;

        //move towards target, normalized fixes size so speed doesnt change
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if(Vector2.Distance(transform.position, target.position) <= .3f)
        {
            GetNextWayPoint();
        }
    }

    //change move target
    void GetNextWayPoint()
    {
        //has reached player base, add a function for damaging player
        if (wavePoint >= WaypointsScript.points.Length-1)
        {
            Destroy(gameObject);
            return;
        }
        wavePoint++;
        target = WaypointsScript.points[wavePoint];
    }


}
