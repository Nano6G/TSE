using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeSpell : BaseSpell
{
    public float effectTime = 3f;
    public float effectRadius = 0.5f;

    public override void Activate()
    {
        Explosion(); 
    }

    void Hit(GameObject EnemyHit)
    {
        //Apply Effect here
        EnemyScript targetScript = EnemyHit.GetComponent<EnemyScript>();
        targetScript.Frozen(effectTime);
    }

    void Explosion()
    {
        LayerMask Mask = LayerMask.GetMask("Gameplay");
        Collider2D[] hitObjs = Physics2D.OverlapCircleAll(transform.position, effectRadius);
        //CREATE ANIMATION HERE

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
