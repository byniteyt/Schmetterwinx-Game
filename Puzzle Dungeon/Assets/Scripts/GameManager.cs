using System;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool PlayerTurn = true;
    public float block;
    public float damageRecieved;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (damageRecieved>block) {
           SceneManager.LoadScene("GameOver");
        }
    }
}
