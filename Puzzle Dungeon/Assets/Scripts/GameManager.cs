using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool PlayerTurn = true;
    public float block;
    public float damageReceived;
    private bool[] playerAbilities;
    private int enemies;
    bool end = false;
    private int playersForCast;
    public static GameManager instance;
    public int turn = 0;

    // UI objets
    public TextMeshProUGUI defense;
    public TextMeshProUGUI damage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        playerAbilities = new bool[Enum.GetNames(typeof(CharacterClass)).Length];
        playersForCast = playerAbilities.Length;
    }
    public void ApplyEffect(CharacterClass caster)
    {
        // Implementation for applying the effect
        int index = (int)caster;
        playerAbilities[index] = true;
        playersForCast--;
        if (playersForCast == 0)
        {
            PlayerTurn = false;
            enemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
            return;
        }
        foreach (GameObject card in GameObject.FindGameObjectsWithTag("Card").Where(card => card.GetComponent<Spell>().casterClass == caster))
        {
            card.GetComponent<BoxCollider2D>().enabled = false;
        }
        
    }
    public void EnemyAttacked()
    {

        
        if (enemies == 0 && damageReceived <= block)
        {
            if (SceneManager.GetActiveScene().name.Equals("InfiniteLevel")&& enemies==0) { 
            
                Instantiate(Resources.Load<GameObject>("Prefabs/Enemies/EnemyInfinite"), new Vector3(7.42999983f, -0.779999971f, 0f), Quaternion.identity);
                Instantiate(Resources.Load<GameObject>("Prefabs/Enemies/EnemyInfinite"), new Vector3(7.42999983f, 3.41000009f, 0f), Quaternion.identity);
                Instantiate(Resources.Load<GameObject>("Prefabs/Enemies/EnemyInfinite"), new Vector3(5.6500001f, 1.35000002f, 0f), Quaternion.identity);

            }
            PlayerTurn = true;
            playersForCast = playerAbilities.Length;
            for (int i = 0; i < playerAbilities.Length; i++)
            {
                playerAbilities[i] = false;
            }
            foreach (GameObject card in GameObject.FindGameObjectsWithTag("Card"))
            {
                card.GetComponent<BoxCollider2D>().enabled = true;
                card.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
    }
        // Update is called once per frame
        void Update()
    {
        defense.text = "Block: " + block;
        damage.text = "Damage: " + damageReceived;
        if (damageReceived>block && !end) {
            StartCoroutine(Wait());
            end = true;
        }
        enemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (enemies == 0)
        {
            SceneManager.LoadScene("YouWin");//Aquí habria que hacer que avance a la asiguiente escena, para que pase de nivel 1 a cinematica, 2 a cinematica, 3 a cinematica final y al modo infinito(postgame), y que ahí
            //cdo mueras te lleve a la pantalla de puntuación, por cierto, copilot sigue tratando de autocompletar con sinsentidos
        }
    }
    IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        SceneManager.LoadScene("GameOver");
    }
}
