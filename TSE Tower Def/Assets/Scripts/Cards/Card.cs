using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    Manager manager;
    int cardType;
    public enum CardType
    {
        Tower,
        Upgrade,
        Spell
    }

    // Start is called before the first frame update
    void Start()
    {
        //manager = transform.Find("Manager").gameObject.GetComponent<Manager>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        switch (cardType)
        {   
            //TOWER
            case 1:
                //SET TOWER
                break;
            //UPGRADE
            case 2:
                //SET UPGRADE
                break;
            //SPELL
        }
    }
}
