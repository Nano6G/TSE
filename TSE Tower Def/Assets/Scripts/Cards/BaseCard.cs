using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BaseCard : ScriptableObject
{
    [Header("Card")]
    public string CardName;
    public string CardDescription;
    public Sprite CardSprite;
    [Header("Standard Stats")]
    public int Cost;
    public GameObject ObjectToMake;
}
