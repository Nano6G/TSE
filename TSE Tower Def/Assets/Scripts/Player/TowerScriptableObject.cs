using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//creates a scriptable object to be created
[CreateAssetMenu(fileName = "Tower", menuName = "ScriptableObjects/Tower", order = 1)]
public class TowerScriptableObject : ScriptableObject
{
    [SerializeField]
    float damage, range;

    public Sprite sprite;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
