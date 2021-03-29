using System.Collections;
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

    CardManager cardManager;

    int towerCost = 0;
    private int cardCost;
    public int CardCost
    {
        set { cardCost = value; }
    }

    //Currency
    private int currencyAvailable = 5;
    public int CurrencyAvailable 
    { 
        get { return currencyAvailable; }
        set { towerCost = value; } 
    }

    private int pHealth = 10;

    public Text currencyText;
    public Text HealthText;

    float currTimerMax = 5f, currTimer;

    //Temporary workaround for spells, otherwise they activate instantly
    int clicktimes = 0;

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
                currencyAvailable -= cardCost;
            }
            else if (selectedSpell != null && clicktimes > 0)
            {
                GameObject CastSpell = Instantiate(selectedSpell, mousePos, transform.rotation);
                CastSpell.GetComponent<BaseSpell>().Activate();
                selectedSpell = null;
                cardManager.RemoveCard(selectedCard);
                currencyAvailable -= cardCost;
            }
            clicktimes++;
            //Maybe change to click a placement zone and then highlight usable cards?
        }
        if (pHealth == 0)
        {
            LoseEvent();
        }
    }

    //Update currency by the amount input
    public void UpdateCurrency(int amount)
    {
        currencyAvailable += amount;
        currencyText.text = currencyAvailable.ToString();
    }
    public void ChangeHealth(int amount)
    {
        pHealth -= amount;
        Mathf.Clamp(pHealth, 0, 100);
        HealthText.text = pHealth.ToString();
    }

    public void setSelection(GameObject cardin, string typein)
    {
        if (typein == "T")
            selectedTower = cardin;
        else if (typein == "S")
        {
            Debug.Log("SETTING SPELL");
            selectedSpell = cardin;
            clicktimes = 0;
        }
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
        }
        return null;
    }
    //Simple coroutine to constantly update income, could alter the repeatrate with spells maybe?
    IEnumerator UpdateCurrencyRepeat(float repeatRate)
    {
        UpdateCurrency(1);
        yield return new WaitForSeconds(repeatRate);
    }

    void LoseEvent()
    {
        Debug.Log("THE KINGDOM CRUMBLES");
    }
}
