using System.Collections;
using System.Collections.Generic;

using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Wave/Wave", order = 1)]
public class WaveScriptableObject : ScriptableObject
{
    // will be usable to create seperate waves for levels
    /*
    //enemies to be spawned
    public EnemyScriptableObject[] enemies;
    //number of enemies to be spawned, See if possible to tie to enemy entry above
    public int[] numToSpawn;
    */
    public waveValues[] wave;
}

[Serializable]
public class waveValues
{
    [SerializeField]
    //enemies to be spawned
    public EnemyScriptableObject enemy;
    //number of enemies to be spawned, See if possible to tie to enemy entry above
    [SerializeField]
    public int numToSpawn;
}
