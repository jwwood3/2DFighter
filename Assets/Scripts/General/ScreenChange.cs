using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenChange : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    public void StartOver()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ElephantFight()
    {
        PlayerCon.player.resetAbilities();
        SceneManager.LoadScene("ElephantFight");
    }

    public void DemonFight()
    {
        SceneManager.LoadScene("DemonFight");
    }

    public void BomberFight()
    {
        SceneManager.LoadScene("BomberFight");
    }

    public void WinScreen()
    {
        SceneManager.LoadScene("WinScreen");
    }

    public void ControlScreen()
    {
        SceneManager.LoadScene("ControlScreen");
    }

    
}
