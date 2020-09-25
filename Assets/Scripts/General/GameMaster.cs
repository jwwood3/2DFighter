using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    [SerializeField] private PlayerCon Player;
    [SerializeField] private Enemy enemy;
    [SerializeField] private GameObject GameOverScreen;
    [SerializeField] private GameObject PowerUpScreen;
    [SerializeField] private int state = 1;
    private int[] parryStates;

    void Start()
    {
        enemy = GameObject.FindGameObjectsWithTag("Enemy")[0].GetComponent<Enemy>();
        parryStates = new int[] { 1, 0 };
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
                //PowerUpScreen.SetActive(true);
                BreakDamageSources();
            }
        }
    }

    void BreakDamageSources()
    {
        foreach(GameObject i in GameObject.FindGameObjectsWithTag("damage"))
        {
            Destroy(i);
        }
    }

    public void Reset()
    {
        state = 1;
        Player.Reset();
        enemy.Reset();
        GameOverScreen.SetActive(false);
    }

    public void powerUp()
    {
        PowerUpScreen.SetActive(true);
    }
}
