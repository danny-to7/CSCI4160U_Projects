using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelScript : MonoBehaviour
{
    GameObject[] enemies;
    DeadCheck deadCheck;
    bool gameOver;
    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemies.Length; i++) {
            deadCheck = enemies[i].transform.GetComponent<DeadCheck>();
            if (!deadCheck.dead)
            {
                gameOver = false;
            }
        }
        if (gameOver)
        {
            SceneManager.LoadScene("End");
        }

        gameOver = true;
    }
}
