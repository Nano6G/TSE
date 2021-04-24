using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolleySpell : BaseSpell
{
    public float effectRadius = 2f;
    int maxVolleys = 5, volleys = 0;
    float timer = 0;

    public override void Activate()
    {
        Explosion();
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 2f)
            Explosion();
    }
    void Hit(GameObject EnemyHit)
    {
        //Apply Effect here
        EnemyScript targetScript = EnemyHit.GetComponent<EnemyScript>();
        targetScript.GetHit(2);
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
        volleys++;
        if (volleys == maxVolleys)
            Destroy(gameObject);
        timer = 0f;
    }
}
