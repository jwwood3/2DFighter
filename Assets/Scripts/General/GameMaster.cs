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
    [SerializeField] private static System.Random gen;
    private int[] parryStates;

    void Start()
    {
        enemy = GameObject.FindGameObjectsWithTag("Enemy")[0].GetComponent<Enemy>();
        Player = PlayerCon.player;
        parryStates = new int[] { 1, 0 };
        gen = new System.Random();
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

    public static Vector3 getRandomPosition(float y_max, float y_min, float x_max, float x_min)
    {
        if (gen == null)
        {
            gen = new System.Random();
        }
        float y_pos = ((float)gen.Next((int)(y_min * 100.0f), (int)(y_max * 100.0f))) / 100.0f;
        float x_pos = ((float)gen.Next((int)(x_min * 100.0f), (int)(x_max * 100.0f))) / 100.0f;
        return new Vector3(x_pos, y_pos, 0);
    }

    public static Vector3 getGroundPosition(Entity go, Vector3 pos)
    {
        if (gen == null)
        {
            gen = new System.Random();
        }
        float x_min = go.gameObject.transform.position.x < pos.x ? go.gameObject.transform.position.x : pos.x;
        float x_max = go.gameObject.transform.position.x > pos.x ? go.gameObject.transform.position.x : pos.x;
        float x_pos = ((float)gen.Next((int)(x_min * 100.0f), (int)(x_max * 100.0f))) / 100.0f;
        return new Vector3(x_pos, go.getGround(), 0);
    }
}
