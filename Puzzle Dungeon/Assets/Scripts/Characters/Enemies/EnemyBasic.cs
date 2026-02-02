using System;
using UnityEngine;

public class EnemyBasic : MonoBehaviour
{

    public float hp;
    public float block;
    public GameObject gm;
    public bool Enemyturn;
    public float[] power;
    public bool hasPlayed = false;
    public float[] intention;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    { 
        hp = 10;
    }

    // Update is called once per frame
    void Update(){
        Enemyturn = !GameManager.instance.PlayerTurn;
        int i=0;
        if (Enemyturn) {
            switch (intention[i]) {
                case 0:                
                EnemyAttack(i);
                    break;
                case 1:
                    EnemyBlock(i);
                    break;
            }
            GameManager.instance.EnemyAttacked();
            i++;
        }

    }
    void EnemyAttack(int i) {
        if (Enemyturn && !hasPlayed)
        {
            Debug.Log("Enemy attacks for " + power[i] + " damage.");
            GameManager.instance.damageReceived += power[i];
            hasPlayed = true;
        }
    }
    void EnemyBlock(int i) { 
        if (Enemyturn && !hasPlayed)
        {
            block += power[i];
            hasPlayed= true;
        }
    }
}