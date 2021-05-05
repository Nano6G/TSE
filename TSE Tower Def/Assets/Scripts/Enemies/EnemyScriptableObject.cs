using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Enemy", order = 1)]
public class EnemyScriptableObject : ScriptableObject
{
    // enemy stats, can be built on but currently very basic
    public float speed, health;
    public int value;
    public Sprite sprite;
    public RuntimeAnimatorController anim;
    //0 = basic 1 = boss, more enemy types can be made with this sytem instead of a bool
    public int enemytype;
    public int ID;
}
