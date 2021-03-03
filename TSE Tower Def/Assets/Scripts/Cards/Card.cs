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
    bool selected = false;

    //maybe have a list of empties to hold the transform of card spots
    Vector2 startPos;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("Manager").GetComponent<Manager>();   
        cardCostVal = cardData.Cost;
        cardNameVal = cardData.CardName;
        cardName.text = cardNameVal;
        cardCost.text = cardCostVal.ToString();
        startPos = transform.position;
    }

    private void OnMouseEnter()
    {
        highlight();
    }
    private void OnMouseExit()
    {

        Lower();
        GetComponent<SpriteRenderer>().color = Color.grey;
    }
    private void OnMouseDown()
    {
        //error checking to ensure there is data in the card
        if(cardData.ObjectToMake != null)
        {
            if (manager.CurrencyAvailable >= cardCostVal)
            {
                manager.TowerCost = cardCostVal;
                manager.setSelection(cardData.ObjectToMake);
                selected = true;
            }
        }
    }

    void highlight()
    {
        Raise();
        GetComponent<SpriteRenderer>().color = Color.yellow;
    }
    //Highlighting mechanics
    private void Raise()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y + .1f);
    }
    void Lower()
    {
        transform.position = startPos;
    }
}
