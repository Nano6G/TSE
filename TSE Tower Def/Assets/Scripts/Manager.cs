﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager: MonoBehaviour
{
    Vector2 mousePos;
    //Towers
    public GameObject selectedTower;
    public GameObject selectedSpell;
    public GameObject selectedCard;

    public GameObject ghostObj;
    public Sprite selectedGhost;

    CardManager cardManager;

    private int cardCost;
    public int CardCost
    {
        set { cardCost = value; }
    }

    //Currency
    private int currencyAvailable = 500;
    public int CurrencyAvailable 
    { 
        get { return currencyAvailable; }
    }
    //Health
    private int pHealth = 10;

    //Test Fields assigned in Editor
    public Text currencyText;
    public Text HealthText;

    //Timer used for passive currency adding;
    float currTimerMax = 5f, currTimer;

    //Temporary workaround for spells, otherwise they activate instantly
    int clicktimes = 0;

    //Colours for ghost
    Color gBlue = new Color(0f, 0f, 0.5f, 0.2f);//Tower
    Color gRed = new Color(0.5f, 0f, 0f, 0.2f);//Spell


    private void Start()
    {
        HealthText.text = pHealth.ToString();
        cardManager = GetComponent<CardManager>();
        //currencyText = transform.Find("Currency1").GetComponent<Text>();
        StartCoroutine(UpdateCurrencyRepeat(2));
    }

    void Update()
    {
        currTimer -= Time.deltaTime;
        if (currTimer < 0)
        {
            UpdateCurrency(25);
            currTimer = currTimerMax;
        }
        currencyText.text = CurrencyAvailable.ToString();
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        ghostObj.transform.position = mousePos;
        if (Input.GetMouseButtonDown(3))
        {
            CheckArea(3);
        }
        if (Input.GetMouseButtonDown(0) && selectedCard != null)
        {
            //for SET PLACEMENT 
            if (CheckArea(1) != null && selectedTower != null && cardCost < currencyAvailable)
            {
                GameObject PlacedTower = Instantiate(selectedTower, CheckArea(1).position, transform.rotation);
                //selectedTower = null;
                cardManager.RemoveCard(selectedCard);
                currencyAvailable -= cardCost;
                //ghostObj.GetComponent<SpriteRenderer>().sprite = null;
                Deselct();
            }
            else if (selectedSpell != null && clicktimes > 0 && CheckArea(2) == null && cardCost < currencyAvailable)
            {
                //could swap instantiate for having objects off screen to save resources
                GameObject CastSpell = Instantiate(selectedSpell, mousePos, transform.rotation);
                CastSpell.GetComponent<BaseSpell>().Activate();
                //selectedSpell = null;
                cardManager.RemoveCard(selectedCard);
                currencyAvailable -= cardCost;
                //ghostObj.GetComponent<SpriteRenderer>().sprite = null;
                Deselct();
            }
            clicktimes++;
        }
        if (Input.GetMouseButtonDown(1))
        {
            Deselct();
        }
//ADD CURRENCY FOR TESTING REMOVE IN FINAL BUILD=============================
        if (Input.GetKey(KeyCode.P))
        {
            UpdateCurrency(1000);
        }
//===========================================================================
        if (pHealth == 0)
        {
            LoseEvent();
        }
    }

    //Update currency by the amount input and update text fields
    public void UpdateCurrency(int amount)
    {
        currencyAvailable += amount;
        currencyText.text = currencyAvailable.ToString();
    }
    //Update health by the amount input and update text fields
    public void UpdateHealth(int amount)
    {
        pHealth += amount;
        Mathf.Clamp(pHealth, 0, 100);
        HealthText.text = pHealth.ToString();
    }

    public void setSelection(GameObject cardin, string typein, Sprite ghostSprite, float effectSize)
    {
        //reset selections to prevent errors
        selectedCard = null;
        selectedTower = null;
        selectedSpell = null;
        selectedCard = cardin;
        //IF cardtype Tower
        if (typein == "T")
        {
            selectedTower = cardin;
            ghostObj.GetComponent<SpriteRenderer>().color =  gBlue;
        }
        //IF cardtype Spell
        else if (typein == "S")
        {
            Debug.Log("SETTING SPELL");
            selectedSpell = cardin;
            clicktimes = 0;
            ghostObj.transform.localScale = new Vector2(effectSize * 2, effectSize * 2);
            ghostObj.GetComponent<SpriteRenderer>().color = gRed;
            // Debug.Log("GHOST SCALE: " + ghostObj.transform.localScale);
        }
        //Set the effectZone's sprite, if null the sprite will not be visible, no issues as of yet
        ghostObj.GetComponent<SpriteRenderer>().sprite = ghostSprite;
        if (effectSize > 0)
        {
            ghostObj.transform.localScale = new Vector2(effectSize * 2, effectSize * 2);
           // Debug.Log("GHOST SCALE: " + ghostObj.transform.localScale);
        }
        else
            ghostObj.transform.localScale = new Vector2(1, 1); //Reset the scale for the ghost object
    }
    public void Discard()
    {
        if (selectedCard != null)
        {
            cardManager.RemoveCard(selectedCard);
            UpdateCurrency(cardCost / 2);
            Deselct();
        }
    }
    public void DeleteTower()
    {

    }
    void Deselct()
    {
        selectedTower = null;
        selectedSpell = null;
        selectedCard = null;
        ghostObj.transform.localScale = new Vector2(1, 1);
        selectedGhost = null;
        ghostObj.GetComponent<SpriteRenderer>().sprite = null;
    }
    Transform CheckArea(int typeCheck)
    {
        switch (typeCheck)
        {
            //TOWER 
            case 1:
                if (Physics2D.OverlapBox(mousePos, new Vector2(1, 1), 0, LayerMask.GetMask("Tower")) == false)
                {
                    //checks layers 1 - 9 for collisions
                    Collider2D other = Physics2D.OverlapBox(mousePos, new Vector2(1, 1), 0, 1 << 9);
                    if (other != null)
                        return other.transform;
                }
                break;
            case 2:
                //Checks if mouse is in card area (bottom section of screen)
                if (Physics2D.OverlapBox(mousePos, new Vector2(1, 1), 0, LayerMask.GetMask("Card Area")))
                {
                    Debug.Log("CARD AREA FOUND");
                    return gameObject.transform;
                }
                break;
            case 3:
                if (Physics2D.OverlapBox(mousePos, new Vector2(1, 1), 0, LayerMask.GetMask("Tower")))
                {
                    GameObject other= Physics2D.OverlapBox(mousePos, new Vector2(1, 1), 0, LayerMask.GetMask("Tower")).GetComponent<GameObject>();
                    Destroy(other);
                    Debug.Log("TOWER FOUND");
                    return null;
                }
                break;

        }
        //Check if there is already a tower


        return null;
    }
    //Simple coroutine to constantly update income, could alter the repeatrate with spells maybe?
    IEnumerator UpdateCurrencyRepeat(float repeatRate)
    {
        UpdateCurrency(1);
        yield return new WaitForSeconds(repeatRate);
    }
    //LOSS
    void LoseEvent()
    {
        Debug.Log("THE KINGDOM CRUMBLES");
    }
    //WIN
    public void WinEvent()
    {
        Debug.Log("THE KINGDOM IS SAVED");
    }
}
