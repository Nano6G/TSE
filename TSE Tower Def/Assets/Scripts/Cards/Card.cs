using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    Manager manager;

    [Header("Fields")]
    public Text cardName;
    public Text cardCost;
    public Text CardType;
    int cardCostVal;
    string cardNameVal;
    public BaseCard cardData;

    public int cardPlace = -1;

    GameObject ObjectIn;

    //maybe have a list of empties to hold the transform of card spots
    Vector2 startPos;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("Manager").GetComponent<Manager>();   
        cardCostVal = cardData.cost;
        cardNameVal = cardData.cardName;
        cardName.text = cardNameVal;
        cardCost.text = cardCostVal.ToString();
    }

    private void OnMouseEnter()
    {
        startPos = transform.position;
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
        if(cardData.objectToMake != null)
        {
            if (manager.CurrencyAvailable >= cardCostVal)
            {
                manager.CardCost = cardCostVal;
                manager.setSelection(cardData.objectToMake, cardData.type);
                manager.selectedCard = this.gameObject;
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
