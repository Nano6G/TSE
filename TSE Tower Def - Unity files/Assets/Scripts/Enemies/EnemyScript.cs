using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//EnemyScript can be broken up if too much is added
public class EnemyScript : MonoBehaviour
{
    [SerializeField]
    protected float speed = 5f, health, MaxHealth = 1;
    [SerializeField]
    protected int value = 1;
    protected Transform target;
    //where on path the enemy is
    protected int wavePoint;

    protected float slowTimer = 0f, SlowAmount = 0f;
    protected bool moving;

    protected GameObject manager;
    protected Manager managerScript;
    protected RuntimeAnimatorController animator;
    protected ParticleSystem particles;
    protected bool facingRight = true;
    [Header("UI")]
    public Image healthBar;

    [Header("debugging")]
    public int ID;

    //should be called before fully spawned
    public void assignSelf(EnemyScriptableObject statObject)
    {
        speed = statObject.speed; MaxHealth = statObject.health; value = statObject.value; ID = statObject.ID;
        animator = statObject.anim;
        GetComponent<Animator>().runtimeAnimatorController = statObject.anim;
    }
    protected void Start()
    {
        manager = GameObject.Find("Manager");
        managerScript = manager.GetComponent<Manager>();
        particles = GetComponentInChildren<ParticleSystem>();
        animator = GetComponent<Animator>().runtimeAnimatorController;
        health = MaxHealth;
        target = WaypointsScript.points[0];
    }

    protected void Update()
    {
        if (slowTimer > 0)
        {
            slowTimer -= Time.deltaTime;
        }
        if (slowTimer < 0 && SlowAmount > 0)
        {
            SlowAmount = 0;
        }

        Move();
        //Deathscript for enemy
        if (health <= 0)
        {
            managerScript.UpdateCurrency(value);
            manager.GetComponentInChildren<WaveSpawner>().enemyCount--;
            Destroy(gameObject);
        }
    }

    protected virtual void Move()
    {
            //direction to move
            Vector2 dir = target.position - transform.position;

            //move towards target, normalized fixes size so speed doesnt change
            transform.Translate(dir.normalized * (speed - (SlowAmount / 100 * speed)) * Time.deltaTime, Space.World);

            if (Vector2.Distance(transform.position, target.position) <= .3f)
            {
                GetNextWayPoint();
            }
            layer();
    }
    public void GetHit(float PhysDmg)
    {
        health -= PhysDmg;
        healthBar.fillAmount = health / MaxHealth;
        particles.Play();
    }

    //change move target
    protected void GetNextWayPoint()
    {
        //has reached player base, add a function for damaging player
        if (wavePoint >= WaypointsScript.points.Length-1)
        {       
            managerScript.UpdateHealth(-1);
            manager.GetComponentInChildren<WaveSpawner>().enemyCount--;
            Destroy(gameObject);
            return;
        }
        wavePoint++;
        target = WaypointsScript.points[wavePoint];
        if (target.transform.position.x < transform.position.x && facingRight)
            Flip();
        else if (target.transform.position.x > transform.position.x && !facingRight)
            Flip();
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    void layer()
    {
        GetComponent<SpriteRenderer>().sortingOrder = 100 - Mathf.RoundToInt(transform.position.y * 10);
    }

    public void Slow(float timeSlowed, float SlowAmnt)
    {
        slowTimer = timeSlowed;
        SlowAmount = SlowAmnt;
    }
}
