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
            //for FREE PLACEMENT
            if (checkArea() == false)
            {
                GameObject PlacedTower = Instantiate(selectedTower, mousePos, transform.rotation);
            }
        }
    }

    bool checkArea()
    {
        //Checks to see if colliding with ANYTHING
        return(Physics2D.OverlapBox(mousePos, new Vector2(1,1), 0));
    }
}
