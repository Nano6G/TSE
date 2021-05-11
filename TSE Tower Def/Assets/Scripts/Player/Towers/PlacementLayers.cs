using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementLayers : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sortingOrder = 100 - Mathf.RoundToInt(transform.position.y * 10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
