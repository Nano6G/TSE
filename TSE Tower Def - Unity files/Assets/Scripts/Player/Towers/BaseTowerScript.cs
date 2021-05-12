﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTowerScript : MonoBehaviour
{
    private Transform target = null;
    //Remove serialize field once everything is implemented correctly
    [Header("Attributes")]
    [SerializeField]
    private float damage;
    [SerializeField]
    private float fireRate = 1;
    private float fireTimer = 0f;
    [SerializeField]
    private float range = 3;


    [Header("Unity Reqs")]
    //could make an array/list and forloop to have multiple shooting points per turret
    public Transform firePoint;
    public GameObject projectile;
    private SpriteRenderer sprite;


    private void Start()
    {
        StartUp();
    }
    public void StartUp()
    {
        //Checks for targets every half second
        InvokeRepeating("UpdateTarget", 0, .5f);
        sprite = GetComponent<SpriteRenderer>();
        firePoint = transform.GetChild(0);
    }

    void UpdateTarget()
    {
        //check all objects in game tagged with "Enemy"
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        //set target if within range
        if (nearestEnemy != null && shortestDistance < range)
        {
            target = nearestEnemy.transform;
        }
        else
            target = null;

    }
    // Update is called once per frame
    void Update()
    {
        //if (target = null)
        //    return;
        //rotate to shoot at target, depending on sprites implemented we can alter this to rotate a sprite or jsut point to change fire direction
        if (target != null)
        {
            Vector3 dir = target.position - transform.position;
            Quaternion shootRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = shootRotation.eulerAngles;
            firePoint.rotation = Quaternion.Euler(Vector3.forward * rotation.y);
        }

        if (fireTimer <= 0 && target != null)
        {
            Shoot();
            fireTimer = 1f / fireRate;
        }
        fireTimer -= Time.deltaTime;
    }
    void Shoot()
    {
        GameObject projectileObject = Instantiate(projectile, firePoint.position, firePoint.rotation);
        BasicProjectile projectileScript = projectileObject.GetComponent<BasicProjectile>();
        if (projectileScript != null)
            projectileScript.Seek(target.transform);
    }

    //Shows range in editor, not in game, need sprite for that?
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
    public void setValues(int damagein, float rangein)
    {
        damage = damagein; range = rangein;
    }  
}