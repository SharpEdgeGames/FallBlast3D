using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiHandler : MonoBehaviour
{
    public static UiHandler instance;

    public GameObject mainMenu;
    public GameObject ingameMenu;
    public GameObject diedMenu;
    public GameObject wonMenu;

    public ShootHandler shoot;
    public GameHandler  gamehandle;
    public GameObject settingspanel;
    public GameObject skinspanel;
    public int reachedcount=0;

    public float blastupforce=2;


    public GameObject cannon;

    public AudioSource btnclick;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        cannon.SetActive(false);
        mainMenu.SetActive(true);
        ingameMenu.SetActive(false);
        diedMenu.SetActive(false);
        wonMenu.SetActive(false);
        settingspanel.SetActive(false);
        skinspanel.SetActive(false);
    }


    public void startGame()
    {
        mainMenu.SetActive(false);
        ingameMenu.SetActive(true);
        diedMenu.SetActive(false);
        wonMenu.SetActive(false);
        shoot.canshoot = true;
        cannon.SetActive(true);
        gamehandle.showHandTutorial();
    }

    public void OpenSettingMenu()
    {
        btnclick.PlayOneShot(btnclick.clip);
        settingspanel.SetActive(true);
    }
    public void CloseSettingMenu()
    {
        btnclick.PlayOneShot(btnclick.clip);
        settingspanel.SetActive(false);
    }
    public void OpenSskinMenu()
    {
        btnclick.PlayOneShot(btnclick.clip);
        skinspanel.SetActive(true);
    }
    public void CloseskinMenu()
    {
        btnclick.PlayOneShot(btnclick.clip);
        skinspanel.SetActive(false);
    }
    public void Gamelost()
    {
        mainMenu.SetActive(false);
        ingameMenu.SetActive(false);
        diedMenu.SetActive(true);
        wonMenu.SetActive(false);
    }
    public void GameWon()
    {
        mainMenu.SetActive(false);
        ingameMenu.SetActive(false);
        diedMenu.SetActive(false);
        wonMenu.SetActive(true);
    }

    public void ContinueGame()
    {
        btnclick.PlayOneShot(btnclick.clip);
        mainMenu.SetActive(false);
        ingameMenu.SetActive(true);
        diedMenu.SetActive(false);
        wonMenu.SetActive(false);
        shoot.count = shoot.maxAllowed / 2;
        shoot.canshoot = true;
        shoot.ballCount.text = "X" + shoot.count;
    }

    public void RestartLevelifWOn()
    {

        int lvl=PlayerPrefs.GetInt("level", 1);
        lvl++;
        PlayerPrefs.SetInt("level", lvl);
        PlayerPrefs.Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void RestartLeveliflost()
    {
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    // Update is called once per frame
    void Update()
    {
        if(reachedcount>=3)
        {
            GameWon();
            reachedcount = 0;
        }
    }
}
