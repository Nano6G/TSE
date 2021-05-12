using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargedProjectile : BasicProjectile
{

    public bool fired = false;
    // Start is called before the first frame update
    void Start()
    {
        moving = false;
        chargeFire();
    }


    protected override void Update()
    {
        //Destroy projectile if target is gone
        if (target == null && fired)
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

    IEnumerator chargeFire()
    {
        yield return new WaitForSeconds(0.5f);
        moving = true;
    }
}
