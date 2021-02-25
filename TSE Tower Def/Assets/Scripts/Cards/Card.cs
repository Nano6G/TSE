using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    Manager manager;
    int cardType;
    [Header("Fields")]
    public Text cardName;
    public Text cardCost;
    public Text CardType;
    int cardCostVal;
    string cardNameVal;
    public BaseCard cardData;

    GameObject ObjectIn;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("Manager").GetComponent<Manager>();   
        cardCostVal = cardData.Cost;
        cardNameVal = cardData.CardName;
        cardName.text = cardNameVal;
        cardCost.text = cardCostVal.ToString();
    }

    private void OnMouseOver()
    {
        GetComponent<SpriteRenderer>().color = Color.yellow;
    }
    private void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().color = Color.grey;
    }
    private void OnMouseDown()
    {
        if(cardData.ObjectToMake != null)
        {
            manager.setSelection(cardData.ObjectToMake);
        }
        Debug.Log("ClickedCard!");

        /*
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
        */
    }
}
