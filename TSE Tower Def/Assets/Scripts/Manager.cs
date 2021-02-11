using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager: MonoBehaviour
{
    Vector2 mousePos;
    public GameObject selectedTower;

    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //temporary code to place a tower at mouse position, tower has no function yet
        if(Input.GetMouseButtonDown(0))
        {
            GameObject PlacedTower = Instantiate(selectedTower, mousePos, transform.rotation);
        }
    }
}
