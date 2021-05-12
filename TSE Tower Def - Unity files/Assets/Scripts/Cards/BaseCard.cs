using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BaseCard : ScriptableObject
{
    [Header("Card")]
    public string cardName;
    public string cardDescription;
    public Sprite cardSprite;
    public Sprite ghost;
    [Header("Standard Stats")]
    public int cost;
    public string type;
    public GameObject objectToMake;
    [Header("Special Stats")]
    public float effectRadius;
}
