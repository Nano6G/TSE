using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//PROJECTILES HITSCAN POSSIBLE WITH ANIMATIONS INSTEAD OF OBJECTS?
public class BasicProjectile : MonoBehaviour
{
    public Transform target;
    private float speed = 50f;

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

    void HitTarget()
    {
        //Apply damage here
        EnemyScript targetScript = target.GetComponent<EnemyScript>();
        targetScript.GetHit(1);

        //Add effects on hit? Simple particle effect assigned in object/prefab
        Destroy(gameObject);
    }
}
