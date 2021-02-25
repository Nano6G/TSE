using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager: MonoBehaviour
{
    Vector2 mousePos;
    //Towers
    GameObject towerBase;
    public GameObject selectedTower;

    //Currency
    private int currency1;
    public int Currency1 
    { 
        get { return currency1; }
        set { currency1 = value; } 
    }
    public Text currencyText;
    float CurrTimerMax = 5f, CurrTimer;

    private void Start()
    {
        //currencyText = transform.Find("Currency1").GetComponent<Text>();
        StartCoroutine(UpdateCurrencyRepeat(2));
    }

    void Update()
    {
        CurrTimer -= Time.deltaTime;
        if (CurrTimer < 0)
        {
            UpdateCurrency(1);
            CurrTimer = CurrTimerMax;
        }
        currencyText.text = Currency1.ToString();
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //temporary code to place a tower at mouse position, tower has no function yet
        if (Input.GetMouseButtonDown(0))
        {
            //for FREE PLACEMENT
            //if (checkArea() == false && selectedTower != null)
            //{
            //    GameObject PlacedTower = Instantiate(selectedTower, mousePos, transform.rotation);
            //    selectedTower = null;
            //}
            //else if (checkArea() == true && selectedTower != null)
            //{
            //    selectedTower = null;
            //}


            //for SET PLACEMENT 
            if (CheckArea() != null && selectedTower != null)
            { 
                GameObject PlacedTower = Instantiate(selectedTower, CheckArea().position, transform.rotation);
                selectedTower = null;
            }
            //Maybe change to click a placement zone and then highlight usable cards?

        }
    }
    public void UpdateCurrency(int amount)
    {
        currency1 += amount;
        currencyText.text = currency1.ToString();
    }
    public void setSelection(GameObject towerin)
    {
        selectedTower = towerin;
    }

    //bool checkArea()
    //{
    //    //Checks to see if colliding with ANYTHING
    //    return(Physics2D.OverlapBox(mousePos, new Vector2(1,1), 0));
    //}

    Transform CheckArea()
    {
       Collider2D other = Physics2D.OverlapBox(mousePos, new Vector2(1, 1), 0, 1 << 9);
        if (other != null)
            return other.transform;
        else return null;
    }
    IEnumerator UpdateCurrencyRepeat(float repeatRate)
    {
        UpdateCurrency(1);
        yield return new WaitForSeconds(repeatRate);
    }
}
