using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    [SerializeField] private PlayerCon Player;
    [SerializeField] private Enemy enemy;
    [SerializeField] private GameObject GameOverScreen;
    [SerializeField] private GameObject WinScreen;
    [SerializeField] private int state = 1;
    private int[] parryStates;

    void Start()
    {
        enemy = GameObject.FindGameObjectsWithTag("Enemy")[0].GetComponent<Enemy>();
        parryStates = new int[]{ 1, 0};
    }

    void Update()
    {
        if (state == 1)
        {
            
            if (Player.getHealth() <= 0)
            {
                state = -1;
                GameOverScreen.SetActive(true);
            }
            else if (enemy.getHealth() <= 0)
            {
                state = -1;
                WinScreen.SetActive(true);
            }
        }
        if (Input.GetKeyUp("p"))
        {
            Player.parryMode = parryStates[Player.parryMode];
        }
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    public void Reset()
    {
        state = 1;
        Player.Reset();
        enemy.Reset();
        GameOverScreen.SetActive(false);
        WinScreen.SetActive(false);
    }
}
