using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour
{
    [SerializeField]
    protected Transform target = null;
    //Remove serialize field once everything is implemented correctly
    [Header("Attributes")]
    [SerializeField]
    protected float damage;
    [SerializeField]
    protected float fireRate = 1;
    [SerializeField]
    protected float fireTimer = 0f;
    [SerializeField]
    protected float range = 3;
    [SerializeField]
    protected int Value;
    [SerializeField]
    protected GameObject TowerCard;

    [SerializeField]
    private Manager manager;

    [Header("Unity Reqs")]
    //could make an array/list and forloop to have multiple shooting points per turret
    public Transform firePoint;
    public GameObject projectile;
    protected SpriteRenderer spriteRenderer;
    public Sprite sprite;

    protected void Start()
    {

        manager = GameObject.Find("Manager").GetComponent<Manager>();
        InvokeRepeating("UpdateTarget", 0, .5f);
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;
        GetComponent<SpriteRenderer>().sortingOrder = 110 - Mathf.RoundToInt(transform.position.y * 10);
        firePoint = transform.GetChild(0);
    }
    protected void UpdateTarget()
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
    protected virtual void Update()
    {
        if (target != null)
        {
            Vector3 dir = target.position - transform.position;
            Quaternion shootRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = shootRotation.eulerAngles;
            firePoint.rotation = Quaternion.Euler(Vector3.forward * rotation.y);

            if (fireTimer <= 0)
            {
                Shoot();
            }
        }
        fireTimer -= Time.deltaTime;
    }
    protected virtual void Shoot()
    {
        //Create a projectile and assign fields
        GameObject projectileObject = Instantiate(projectile, firePoint.position, firePoint.rotation);
        BasicProjectile projectileScript = projectileObject.GetComponent<BasicProjectile>();
        if (projectileScript != null)
        {
            //assign damage from tower
            projectileScript.Dmg = damage;
            projectileScript.Seek(target.transform);
        }
        fireTimer = 1f / fireRate;
    }

    private void OnMouseDown()
    {
        Debug.Log("Clicked");
        if (TowerCard != null && !TowerCard.activeSelf)
        {
            TowerCard.SetActive(true);
            TowerCard.transform.Find("number holder").transform.Find("DAMAGE").GetComponent<Text>().text = damage.ToString();
            TowerCard.transform.Find("number holder").transform.Find("FIRERATE").GetComponent<Text>().text = fireRate.ToString();
        }
        else if (TowerCard != null && TowerCard.activeSelf)
            TowerCard.SetActive(false);
    }
    public void Delete()
    {
        manager.UpdateCurrency(Value);
        Destroy(gameObject);
    }

}
