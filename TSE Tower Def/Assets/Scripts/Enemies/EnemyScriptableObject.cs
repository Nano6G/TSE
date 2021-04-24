using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Enemy", order = 1)]
public class EnemyScriptableObject : ScriptableObject
{
    // enemy stats, can be built on but currently very basic
    public float speed, health;

    public Sprite sprite;
    public RuntimeAnimatorController anim;
}
