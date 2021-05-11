using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//PROJECTILES HITSCAN POSSIBLE WITH ANIMATIONS INSTEAD OF OBJECTS?
public class BasicProjectile : MonoBehaviour
{
    public Transform target;
    protected float speed = 50f;
    protected float dmg;
    SpriteRenderer spriteRenderer;
    public float Dmg 
    {
        get { return dmg; }
        set { dmg = value; }
    }
    public float explodeRadius;
    public bool moving = true;
    public float slowamount, slowtime;
    protected void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    //Set the projectile target, called on creation in basic tower
    public void Seek(Transform targetin)
    {
        target = targetin;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        //Destroy projectile if target is gone
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        if (moving == true)
        {
            //Bullet following target
            Vector3 dir = target.position - transform.position;
            float distThisFrame = speed * Time.deltaTime;
            //stop the bullet if in contact with enemy, removes need for colliders which could get messy
            if (dir.magnitude <= distThisFrame)
            {
                HitTarget();
                return;
            }
            transform.Translate(dir.normalized * distThisFrame, Space.World);
        }
    }

    protected void HitTarget()
    {
        //if the target explodes the special effect of an explosion is triggered
        if (explodeRadius > 0)
        {
            moving = false;
            Explosion();
        }
        else
            Hit(target.gameObject,Dmg);
    }

    protected void Hit(GameObject EnemyHit, float damage)
    {
        //Apply damage here
        EnemyScript targetScript = EnemyHit.GetComponent<EnemyScript>();
        targetScript.GetHit(damage);
        //Add effects on hit? Simple particle effect assigned in object/prefab
        if (slowamount > 0)
        {
            targetScript.Slow(slowtime, slowamount);
        }
        Destroy(gameObject);
    }

    protected void Explosion()
    {
        LayerMask Mask = LayerMask.GetMask("Gameplay");
        //Collider2D[] hitObjs = Physics.OverlapSphere(transform.position, explodeRadius, 8);
        Collider2D[] hitObjs = Physics2D.OverlapCircleAll(transform.position, explodeRadius);
        //CREATE EXPLOSION HERE

        //damage all objects
        foreach (Collider2D collider in hitObjs)
        {
            if (collider.transform.tag == "Enemy")
            {
                Hit(collider.gameObject, Dmg/2);
            }
        }
        //make co routine to stop instant destroy
        Destroy(gameObject);
    }
}