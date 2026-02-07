using System;
using UnityEngine;

public class EnemyInfinite : MonoBehaviour
{

    public float hp;
    public float block;
    public GameObject gm;
    public bool Enemyturn;
    public float power;
    public bool hasPlayed = false;
    public float intention;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       intention = UnityEngine.Random.Range(0, 1);
        power = UnityEngine.Random.Range(gm.GetComponent<GameManager>().turn, (int)(gm.GetComponent<GameManager> ().turn*1.5));
    }
    public void setGM(GameObject a) 
    {
       this.gm = a;
        
    }
    public EnemyInfinite(GameObject a) { }
    // Update is called once per frame
    void Update(){
        Enemyturn = !GameManager.instance.PlayerTurn;
        if (Enemyturn) {
            gm.GetComponent<GameManager>().turn++;
            int turno = gm.GetComponent<GameManager>().turn;
            int potencia= (int)(turno * 1.2f);
            switch (intention) {
                case 0:                
                EnemyAttack();
                    break;
                case 1:
                    EnemyBlock();
                    break;
            }
            gm.GetComponent<GameManager>().enemyplayed++;
            intention = UnityEngine.Random.Range(0, 1);
            power= UnityEngine.Random.Range(potencia,potencia+turno);
            GameManager.instance.EnemyAttacked();
            
            GetComponent<EnemyWarning>().UpdateIntention(turno);
            
            GameManager.instance.PlayerTurn = true;
        }

    }
    void EnemyAttack() {
        if (Enemyturn && !hasPlayed)
        {
            Debug.Log("Enemy attacks for " + power + " damage.");
            GameManager.instance.damageReceived += power;
            hasPlayed = true;
        }
    }
    void EnemyBlock() { 
        if (Enemyturn && !hasPlayed)
        {
            block += power;
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
        }
        GetComponent<EnemyWarning>().UpdateHealth();
    }
}