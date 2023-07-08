using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class SaveData : MonoBehaviour
{

    [Header("mainmenu level setter objects")]
    public TextMeshProUGUI mainleveltext;
    [Header("Main coins holder")]
    public TextMeshProUGUI coins;



    [Header("Main Menu data objects")]

    [Header(".......................")]
    [Header("Increase bomb level objects")]
    public TextMeshProUGUI bomb_levelcount;
    public TextMeshProUGUI bomb_amount;

    public Button bomb_button;

    public GameObject bomb_disablebtn;
    public GameObject bomb_upgradelable;

    [Header("Increase coins level objects")]
    public TextMeshProUGUI coin_levelcount;
    public TextMeshProUGUI coin_amount;
    
    public Button coin_button;

    public GameObject coin_disablebtn;
    public GameObject coin_upgradelable;

    [Header("Increase arrow level objects")]
    public TextMeshProUGUI arrow_levelcount;
    public TextMeshProUGUI arrow_amount;

    public Button arrow_button;

    public GameObject arrow_disablebtn;
    public GameObject arrow_upgradelable;


    [Header("button click sfx")]
    public AudioSource btnclick;
    public AudioSource btnclickUpgrade;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("coins", 2000);
        PlayerPrefs.Save();
        initializeAllData();
    }

    public void initializeAllData()
    {
        checkIfToDisable();
      
        mainleveltext.text="LEVEL "+ PlayerPrefs.GetInt("level", 1);
        coins.text = "" + PlayerPrefs.GetInt("coins", 0);


        bomb_amount.text = ""+PlayerPrefs.GetInt("bomb_amount",100);
        bomb_levelcount.text = "LEVEL "+PlayerPrefs.GetInt("bomb_levelcount",1);


        coin_amount.text = "" + PlayerPrefs.GetInt("coin_amount", 100);
        coin_levelcount.text = "LEVEL " + PlayerPrefs.GetInt("coin_levelcount", 1);

        arrow_amount.text = "" + PlayerPrefs.GetInt("arrow_amount", 100);
        arrow_levelcount.text = "LEVEL " + PlayerPrefs.GetInt("arrow_levelcount", 1);


    }
    public void checkIfToDisable()
    {
        int bombamount = PlayerPrefs.GetInt("bomb_amount", 100);
        int coinamount = PlayerPrefs.GetInt("coin_amount", 100);
        int arrowamount = PlayerPrefs.GetInt("arrow_amount", 100);

        int mycoins = PlayerPrefs.GetInt("coins", 0);
        
        if(mycoins<bombamount)
        {
            //disable bomb btn
            bomb_disablebtn.SetActive(true);
            bomb_upgradelable.SetActive(false);
            bomb_button.interactable = false;
        }
        else
        {
            bomb_disablebtn.SetActive(false);
            bomb_upgradelable.SetActive(true);
            bomb_button.interactable = true;
        }
        if (mycoins < coinamount)
        {
            //disable coin btn
            coin_disablebtn.SetActive(true);
            coin_upgradelable.SetActive(false);
            coin_button.interactable = false;
        }
        else
        {
            coin_disablebtn.SetActive(false);
            coin_upgradelable.SetActive(true);
            coin_button.interactable = true;
        }
        if (mycoins < arrowamount)
        {
            //disable arrow btn
            arrow_disablebtn.SetActive(true);
            arrow_upgradelable.SetActive(false);
            arrow_button.interactable = false;
        }
        else
        {
            arrow_disablebtn.SetActive(false);
            arrow_upgradelable.SetActive(true);
            arrow_button.interactable = true;
        }



    }

    public void bomb_IncreaseLevel()
    {
       int currlevel= PlayerPrefs.GetInt("bomb_levelcount", 1);
        int curramount = PlayerPrefs.GetInt("bomb_amount", 100);

        int mycoins = PlayerPrefs.GetInt("coins", 0);

        if(mycoins>=curramount)
        {
            //buy level
            btnclickUpgrade.PlayOneShot(btnclickUpgrade.clip);
            mycoins -= curramount;
            currlevel++;
            curramount += 50;

            PlayerPrefs.SetInt("coins", mycoins);
            PlayerPrefs.SetInt("bomb_levelcount", currlevel);
            PlayerPrefs.SetInt("bomb_amount", curramount);

            PlayerPrefs.Save();


        }else
        {
            Debug.Log("Not enough cash");
            //not enough cash
        }

        initializeAllData();

    }
    public void coin_IncreaseLevel()
    {
        int currlevel = PlayerPrefs.GetInt("coin_levelcount", 1);
        int curramount = PlayerPrefs.GetInt("coin_amount", 100);

        int mycoins = PlayerPrefs.GetInt("coins", 0);

        if (mycoins >= curramount)
        {
            //buy level
            btnclickUpgrade.PlayOneShot(btnclickUpgrade.clip);
            mycoins -= curramount;
            currlevel++;
            curramount += 50;

            PlayerPrefs.SetInt("coins", mycoins);
            PlayerPrefs.SetInt("coin_levelcount", currlevel);
            PlayerPrefs.SetInt("coin_amount", curramount);

            PlayerPrefs.Save();


        }
        else
        {
            Debug.Log("Not enough cash");
            //not enough cash
        }

        initializeAllData();

    }
    public void arrow_IncreaseLevel()
    {
        int currlevel = PlayerPrefs.GetInt("arrow_levelcount", 1);
        int curramount = PlayerPrefs.GetInt("arrow_amount", 100);

        int mycoins = PlayerPrefs.GetInt("coins", 0);

        if (mycoins >= curramount)
        {
            //buy level
            btnclickUpgrade.PlayOneShot(btnclickUpgrade.clip);
            mycoins -= curramount;
            currlevel++;
            curramount += 50;

            PlayerPrefs.SetInt("coins", mycoins);
            PlayerPrefs.SetInt("arrow_levelcount", currlevel);
            PlayerPrefs.SetInt("arrow_amount", curramount);

            PlayerPrefs.Save();


        }
        else
        {
            Debug.Log("Not enough cash");
            //not enough cash
        }


        initializeAllData();
    }



    public void restorePurchases()
    {

        btnclick.PlayOneShot(btnclick.clip);
        PlayerPrefs.SetInt("bomb_amount", 100);
       PlayerPrefs.SetInt("bomb_levelcount", 1);


       PlayerPrefs.SetInt("coin_amount", 100);
       PlayerPrefs.SetInt("coin_levelcount", 1);

       PlayerPrefs.SetInt("arrow_amount", 100);
       PlayerPrefs.SetInt("arrow_levelcount", 1);
        PlayerPrefs.Save();
        initializeAllData();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
