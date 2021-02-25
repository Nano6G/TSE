using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//PROJECTILES HITSCAN POSSIBLE WITH ANIMATIONS INSTEAD OF OBJECTS?
public class BasicProjectile : MonoBehaviour
{
    public Transform target;
    private float speed = 50f;
    private float dmg;
    SpriteRenderer spriteRenderer;
    public float Dmg 
    {
        get { return dmg; }
        set { dmg = value; }
    }
    public float explodeRadius;
    bool moving = true;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    //Set the projectile target, called on creation in basic tower
    public void Seek(Transform targetin)
    {
        target = targetin;
    }

    // Update is called once per frame
    void Update()
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

    void HitTarget()
    {
        if (explodeRadius > 0)
        {
            moving = false;
            Explosion();
        }
        else
            Hit(target.gameObject);
    }
    void Hit(GameObject EnemyHit)
    {
        //Apply damage here
        EnemyScript targetScript = EnemyHit.GetComponent<EnemyScript>();
        targetScript.GetHit(Dmg);
        //Add effects on hit? Simple particle effect assigned in object/prefab
        Destroy(gameObject);
    }

    void Explosion()
    {
        LayerMask Mask = LayerMask.GetMask("Gameplay");
        //Collider2D[] hitObjs = Physics.OverlapSphere(transform.position, explodeRadius, 8);
        Collider2D[] hitObjs = Physics2D.OverlapCircleAll(transform.position, explodeRadius);
        Debug.Log(hitObjs.Length);
        //CREATE EXPLOSION HERE

        //damage all objects
        foreach (Collider2D collider in hitObjs)
        {
            if (collider.transform.tag == "Enemy")
            {
                Hit(collider.gameObject);
            }
        }
        //make co routine to stop instant destroy
        Destroy(gameObject);
    }
}