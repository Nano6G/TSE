using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public List<BaseCard> Deck = new List<BaseCard>();
    public List<BaseCard> Hand = new List<BaseCard>();
    public List<BaseCard> Discarded = new List<BaseCard>();

    void DrawCard()
    {
        Hand.Add(Deck[Deck.Count-1]);
        Deck.Remove(Deck[Deck.Count - 1]);
    }
    void RemoveCard(GameObject card)
    {
        Discarded.Add(card);
        Hand.Remove(Hand[Hand.Count - 1]);
    }
    void RefreshHand()
    {

    }
}
