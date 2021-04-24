using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialTower : Tower
{
    [SerializeField]
    float chargeTimer = 3f, chargeTimeMax = 2f;
    bool charging = false;
    int maxShots = 3;
    [SerializeField]
    List<GameObject> Shots;

    protected override void Update()
    {
        if (Shots.Count < 3)
        {
            if (fireTimer <= 0 && target != null)
            { Shoot(); }
            if (!charging)
            { chargeTimer -= Time.deltaTime; }
        }
        if (Shots.Count > 0 && fireTimer <= 0 && target != null)
        { Shoot(); }

        if (chargeTimer <= 0)
        {
            StartCoroutine("ChargeShot");
            charging = true;
            chargeTimer = chargeTimeMax;
            Debug.Log("TIMER");
        }
        fireTimer -= Time.deltaTime;
    }
    void makeShot()
    {
        GameObject projectileObject = Instantiate(projectile, new Vector2(firePoint.position.x + Shots.Count * 0.5f, firePoint.position.y), firePoint.rotation);
        Shots.Add(projectileObject);
        charging = false;
        Debug.Log("MakeShot");
    }
    protected override void Shoot()
    {

        if (Shots.Count > 0)
        {
            Debug.Log("Shot");
            ChargedProjectile projectileScript = Shots[0].GetComponent<ChargedProjectile>();
            if (projectileScript != null)
            {
                projectileScript.Dmg = damage;
                projectileScript.Seek(target.transform);
                projectileScript.fired = true;
                projectileScript.moving = true;
            }
            Shots.RemoveAt(0);
            fireTimer = 0.2f;
        }
    }
    
    IEnumerator ChargeShot()
    {
        Debug.Log("ChargeShot");
        yield return new WaitForSeconds(0.5f);
        makeShot();
    }
}
