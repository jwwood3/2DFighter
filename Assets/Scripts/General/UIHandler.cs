using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIHandler : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI PHealth;
    [SerializeField] TextMeshProUGUI EHealth;
    [SerializeField] PlayerCon player;
    [SerializeField] Enemy enemy;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.FindGameObjectsWithTag("Enemy")[0].GetComponent<Enemy>();
        player = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerCon>();
    }

    // Update is called once per frame
    void Update()
    {
        PHealth.text = "" + player.getHealth() + "/" + player.getMaxHealth();
        EHealth.text = "" + enemy.getHealth() + "/" + enemy.getMaxHealth();
    }
}
