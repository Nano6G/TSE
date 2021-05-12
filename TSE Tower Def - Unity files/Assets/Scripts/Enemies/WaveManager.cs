using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public WaveScriptableObject[] waves;
    //set the nodes of spawned objects
    [SerializeField]
    private Vector2[] nodes;
}
