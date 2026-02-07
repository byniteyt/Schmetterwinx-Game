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

    }
    public EnemyBasic(GameObject a) { }
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
            GetComponent<EnemyWarning>().UpdateIntention(i);
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

    public void TakeDamage(float damage)
    {

        float effectiveDamage = Mathf.Max(damage - block, 1); // we want to ensure that the enemy takes at least 1 damage
        hp -= effectiveDamage;
        block = Mathf.Max(block - damage, 0);
        Debug.Log("Enemy takes " + effectiveDamage + " damage. Remaining HP: " + hp);
        if (hp <= 0)
        {
            Destroy(gameObject);
            return;
        }
        GetComponent<EnemyWarning>().UpdateHealth();
    }
}