using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Dati di Gioco")]
    public float cookies = 0;
    public float cookiesPerSecond = 0;
    public float upgradeCost = 15;
    public int winCondition = 100;


    [Header("Riferimenti UI")]
    public TextMeshProUGUI cookieText;
    public TextMeshProUGUI perSecondText;
    public TextMeshProUGUI upgradeCostText;
    public Button buyUpgradeButton;
    public Button winButton;
    public GameObject UiBase;
    public GameObject WinScreen;

    [Header("Riferimenti Prefabs")]
    public GameObject cursorPrefab;
    public List<GameObject> cursors = new List<GameObject>();
    public int Xspawn = 0;
    public float Yspawn = 3.5f;


    [Header("COokie rimbalzo")]
    public Transform CookieTransfrom;
    private float scale = 1.5f;
    void Start()
    {
        WinScreen.SetActive(false);
    }

    
    public void Update()
    {
        if (cookiesPerSecond>0)
        {
            cookies += cookiesPerSecond * Time.deltaTime;
            UiUpdate();
        }
        buyUpgradeButton.interactable = cookies >= upgradeCost;

        winButton.interactable = cookies >= winCondition;


        bool clickedThisFrame = Mouse.current.leftButton.wasPressedThisFrame;
        Vector3 mousePos = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
        if (clickedThisFrame)
        {
           
            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);
            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Cookie"))
                {
                    CookieTransfrom.localScale = Vector3.one * scale;
                    ClickCookie();
                    UiUpdate();
                    Invoke("resetforma", 0.2f);
                }
            }

        }
    }

    public void resetforma()
    {
        CookieTransfrom.localScale = Vector3.one;
    }

    public void UiUpdate()
    {
        cookieText.text = "Cookies: " + Mathf.RoundToInt(cookies).ToString();
        perSecondText.text = "Per second: " + cookiesPerSecond.ToString("");
        upgradeCostText.text = "Upgrade Cost: " + Mathf.RoundToInt(upgradeCost).ToString();
    }
    public void ClickCookie()
    {
        cookies++;
        UiUpdate();
    }
    public void BuyUpgrade()
    {
        if (cookies >= upgradeCost)
        {
            cookies -= upgradeCost;
            cookiesPerSecond += 0.4f;
            upgradeCost *= 1.15f;
            
            GameObject newCursor = Instantiate(cursorPrefab, new Vector3(Xspawn, Yspawn, 0), Quaternion.identity);
            cursors.Add(newCursor);
            UiUpdate();
        }
    }
    public void Win()
    {
        UiBase.SetActive(false);
        WinScreen.SetActive(true);
    }

}
