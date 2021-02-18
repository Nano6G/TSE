using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//creates a scriptable object to be created
[CreateAssetMenu(fileName = "Tower", menuName = "ScriptableObjects/Tower", order = 1)]
public class TowerScriptableObject : ScriptableObject
{
    public float damage;
    public float range;
    public Sprite sprite;
    public GameObject projectile;
}
