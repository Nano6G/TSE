using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BaseCard : ScriptableObject
{
    [Header("Card")]
    public string cardName;
    public string cardDescription;
    public Sprite cardSprite;
    [Header("Standard Stats")]
    public int cost;
    public GameObject objectToMake;
}
