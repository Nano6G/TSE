using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager: MonoBehaviour
{
    Vector2 mousePos;
    //Towers
    public GameObject selectedTower;
    public GameObject selectedCard;

    CardManager cardManager;

    int towerCost = 0;
    public int TowerCost
    {
        set { towerCost = value; }
    }

    //Currency
    private int currencyAvailable;
    public int CurrencyAvailable 
    { 
        get { return currencyAvailable; }
        set { towerCost = value; } 
    }

    private int currencyAvailable;
    public Text currencyText;
    float currTimerMax = 5f, currTimer;

    private void Start()
    {
        cardManager = GetComponent<CardManager>();
        //currencyText = transform.Find("Currency1").GetComponent<Text>();
        StartCoroutine(UpdateCurrencyRepeat(2));
    }

    void Update()
    {
        currTimer -= Time.deltaTime;
        if (currTimer < 0)
        {
            UpdateCurrency(1);
            currTimer = currTimerMax;
        }
        currencyText.text = CurrencyAvailable.ToString();
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            //for SET PLACEMENT 
            if (CheckArea() != null && selectedTower != null)
            { 
                GameObject PlacedTower = Instantiate(selectedTower, CheckArea().position, transform.rotation);
                selectedTower = null;
                cardManager.RemoveCard(selectedCard);
            }
            //Maybe change to click a placement zone and then highlight usable cards?

        }
    }

    //Update currency by the amount input
    public void UpdateCurrency(int amount)
    {
        currencyAvailable += amount;
        currencyText.text = currencyAvailable.ToString();
    }

    public void UpdateHealth(int amount)
    {
         += amount;
         = currencyAvailable.ToString();
    }

    public void setSelection(GameObject towerin)
    {
        selectedTower = towerin;
    }

    //checks layers 1 - 9 for collisions
    Transform CheckArea()
    {
        //Check if there is already a tower
        if (Physics2D.OverlapBox(mousePos, new Vector2(1, 1), 0,LayerMask.GetMask("Tower")) == false)
        {
            Collider2D other = Physics2D.OverlapBox(mousePos, new Vector2(1, 1), 0, 1 << 9);
            if (other != null)
                return other.transform;

            else return null;
        }
        else return null;
    }
    //Simple coroutine to constantly update income, could alter the repeatrate with spells maybe?
    IEnumerator UpdateCurrencyRepeat(float repeatRate)
    {
        UpdateCurrency(1);
        yield return new WaitForSeconds(repeatRate);
    }
}
