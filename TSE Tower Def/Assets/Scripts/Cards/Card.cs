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
    public Text cardType;
    public float cardEffectRadius;
    public Sprite ghost;
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
        ghost = cardData.ghost;
        cardName.text = cardNameVal;
        cardCost.text = cardCostVal.ToString();
        cardEffectRadius = cardData.effectRadius;
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
                manager.selectedGhost = ghost;
                manager.setSelection(cardData.objectToMake, cardData.type, ghost, cardEffectRadius);
                //Debug.Log("Card Rad = " + cardEffectRadius);
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
