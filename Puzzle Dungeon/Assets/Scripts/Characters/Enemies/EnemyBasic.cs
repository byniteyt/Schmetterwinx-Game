using System;
using UnityEngine;

public class EnemyBasic : MonoBehaviour
{

    public float hp;
    public float block;
    public GameObject gm;
    GameObject[] uiElements = new GameObject[2];
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
    public void AddUILife(GameObject life)
    {
        uiElements[0] = life;
    }
    public void AddUIAction(GameObject action)
    {
        uiElements[1] = action;
    }
    public GameObject[] GetUIElements()
    {
        return uiElements;
    }
    public GameObject GetUILife()
    {
        return uiElements[0];
    }

    public GameObject GetUIAction()
    {
        return uiElements[1];
    }

    public void TakeDamage(float damage)
    {
        float effectiveDamage = Mathf.Max(damage - block, 0);
        hp -= effectiveDamage;
        block = Mathf.Max(block - damage, 0);
        Debug.Log("Enemy takes " + effectiveDamage + " damage. Remaining HP: " + hp);
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        foreach (GameObject uiElement in uiElements)
        {
            if (uiElement != null)
            {
                Destroy(uiElement);
            }
        }
    }
}