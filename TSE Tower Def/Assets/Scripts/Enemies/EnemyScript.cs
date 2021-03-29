using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//EnemyScript can be broken up if too muhc is added
public class EnemyScript : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f, health, MaxHealth = 1, value = 1;

    private Transform target;
    //where on path the enemy is
    private int wavePoint;

    private float frozenTimer = 0f;
    private bool moving;

    private GameObject manager;
    private Manager managerScript;
    private RuntimeAnimatorController animator;
    
    [Header("UI")]
    public Image healthBar;

    //should be called before fully spawned
    public void assignStats(float speedin, float healthin, Sprite spritein, RuntimeAnimatorController controller)
    {
        speed = speedin; MaxHealth = healthin; GetComponent<SpriteRenderer>().sprite = spritein;
        health = MaxHealth;

        animator = GetComponent<Animator>().runtimeAnimatorController;
        animator = controller;
        GetComponent<Animator>().runtimeAnimatorController = controller;
    }
    void Start()
    {
        manager = GameObject.Find("Manager");
        managerScript = manager.GetComponent<Manager>();

        health = MaxHealth;
        target = WaypointsScript.points[0];
    }

    private void Update()
    {
        if (frozenTimer <= 0)
        {
            //direction to move
            Vector2 dir = target.position - transform.position;

            //move towards target, normalized fixes size so speed doesnt change
            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

            if (Vector2.Distance(transform.position, target.position) <= .3f)
            {
                GetNextWayPoint();
            }
        }
        else
        {
            Debug.Log("FROZEN");
            frozenTimer -= 1 * Time.deltaTime;
        }
        if (health <= 0)
        {
            managerScript.UpdateCurrency(1);
            Destroy(gameObject);
        }

    }

    public void GetHit(float PhysDmg)
    {
        health -= PhysDmg;
        healthBar.fillAmount = health / MaxHealth;
    }

    //change move target
    void GetNextWayPoint()
    {
        //has reached player base, add a function for damaging player
        if (wavePoint >= WaypointsScript.points.Length-1)
        {
            Destroy(gameObject);
            managerScript.ChangeHealth(-1);
            return;
        }
        wavePoint++;
        target = WaypointsScript.points[wavePoint];
    }

    public void Frozen(float timeFrozen)
    {
        frozenTimer += timeFrozen;
    }
}
