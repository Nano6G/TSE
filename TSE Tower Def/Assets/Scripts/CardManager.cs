using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//I tried splitting the regular Manager and card manager but it may be easier to put them together and work fully from Manager script.

//Class used to store data on card slots, e.g. where the spot is, is it empty
public class HandItem
{
    public Transform pos;
    public bool occupied = false;
    public HandItem(Transform position)
    {
        pos = position;
    }
}

public class CardManager : MonoBehaviour
{
    //------------HEADER--------------//
    int maxHand = 5;

    public Transform createCard;

    public List<BaseCard> deck = new List<BaseCard>();
    public List<GameObject> hand = new List<GameObject>();
    public List<BaseCard> discarded = new List<BaseCard>();

    //May be redundant soon
    public List<Transform> CardLocs = new List<Transform>();


    //public List<GameObject> HandObjs = new List<GameObject>();
    public GameObject cardObj;

    //ARRAYS FOR HAND
    public HandItem[] handSpots;
    public GameObject[] handArr;
    //---------------------------------//

    private void Start()
    {
        shuffle();
        CreateHand(5);

        DrawCard();
        DrawCard();
        DrawCard();

        //Debug.Log(handSpots[1].occupied);
    }

    void CreateHand(int handSize)
    {
        handArr = new GameObject[handSize];
        handSpots = new HandItem[handSize];
        for (int i = 0; i < handSize; i++)
        {
            HandItem handItem = new HandItem(CardLocs[i]);
            handSpots[i] = handItem;
        }
    }

    //draw a card from a deck into the hand
    public void DrawCard()
    {
        if (deck.Count > 0)
        {
            if (hand.Count < maxHand)
            {
                HandItem spotToPlace = CheckHandSpots();

                if (spotToPlace != null)
                {
                    //create a cardobject and assign its values 
                    GameObject newCard = Instantiate(cardObj, spotToPlace.pos.position, transform.rotation);

                    BaseCard dataFromCard = deck[0];
                    newCard.GetComponent<Card>().cardData = dataFromCard;
                    //remove card data from the deck
                    deck.Remove(deck[0]);

                    spotToPlace.occupied = true;
                    //update the hand array to fill the spot
                    for (int i = 0; i < handArr.Length; i++)
                    {
                        if(handArr[i] == null)
                        {
                            handArr[i] = newCard;
                            break;
                        }
                    }
                    //Debug.Log("Hand size: " + hand.Count);
                }
            }
        }
        else
        {
            ShuffleDisc();
            DrawCard();
        }
    }

    //Check for empty spots in hand array
    HandItem CheckHandSpots()
    {
        for (int i = 0; i < handSpots.Length; i++)
        {
            if (handSpots[i].occupied == false)
                return handSpots[i];
        }
        return null;
    }

    //remove a card from the player hand
    public void RemoveCard(GameObject toRemove)
    {
        discarded.Add(toRemove.GetComponent<Card>().cardData);
        RefreshHand(toRemove);
    }

    //Check the hand and move cards back at a certain point
    void RefreshHand(GameObject objectRemoved)
    {
        int index = Array.IndexOf(handArr, objectRemoved);
        Debug.Log(objectRemoved);
        Debug.Log(index);
        Destroy(handArr[index]);
        handArr[index] = null;
        handSpots[index].occupied = false;
    }

    //Shuffle the discard pile into the main deck
    void ShuffleDisc()
    {
        Debug.Log("SHUFFLING");
        //Add to deck
        if (discarded.Count > 0)
        {
            //likely a more efficient way to copy the list over
            for (int i = 0; i < discarded.Count - 1; i++)
            {
                deck.Add(discarded[i]);
            }
            discarded.Clear();
            shuffle();
        }
    }

    void shuffle()
    {
        for (int i = 0; i < deck.Count; i++)
        {
            BaseCard temp = deck[i];
            int randomIndex = UnityEngine.Random.Range(i, deck.Count);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }
    }
}
