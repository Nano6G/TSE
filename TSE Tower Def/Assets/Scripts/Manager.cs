using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager: MonoBehaviour
{
    Vector2 mousePos;
    //Towers
    public GameObject towerBase;
    public GameObject selectedTower;

    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //temporary code to place a tower at mouse position, tower has no function yet
        if(Input.GetMouseButtonDown(0))
        {
            //for FREE PLACEMENT
            if (checkArea() == false && selectedTower != null)
            {
                GameObject PlacedTower = Instantiate(selectedTower, mousePos, transform.rotation);
                selectedTower = null;
            }
            else if (checkArea() == true && selectedTower!= null)
            {
                selectedTower = null;
            }
        }
    }
    public void setSelection(GameObject towerin)
    {
        selectedTower = towerin;
    }
    bool checkArea()
    {
        //Checks to see if colliding with ANYTHING
        return(Physics2D.OverlapBox(mousePos, new Vector2(1,1), 0));
    }
}
