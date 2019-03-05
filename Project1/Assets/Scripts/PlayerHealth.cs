using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] public int playerHP = 4;
    [SerializeField] public int invulnerableTime = 3;
    public bool playerDead;
    private Animator animator;

    public Image[] hearts;

    // Start is called before the first frame update
    void Start()
    {
        playerDead = false;
        animator = this.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < playerHP)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }

        //check if player falls off map
        if (gameObject.transform.position.y < -10)
        {
            playerDead = true;
            StartCoroutine(Die());
        }
    }

    public void GetHit()
    {
        playerHP--;
        StartCoroutine(HitBlinker(invulnerableTime));
        Debug.Log(playerHP);
        if (playerHP <= 0)
        {
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        SceneManager.LoadScene("Level1");
        playerHP = 5;
        yield return null;
    }

    IEnumerator HitBlinker(float invulnTime)
    {
        int enemyLayer = LayerMask.NameToLayer("Enemy");
        int playerLayer = LayerMask.NameToLayer("Player");
        Physics2D.IgnoreLayerCollision(enemyLayer, playerLayer);
        animator.SetLayerWeight(1, 1);

        yield return new WaitForSeconds(invulnTime);
        Physics2D.IgnoreLayerCollision(enemyLayer, playerLayer, false);
        animator.SetLayerWeight(1, 0);
    }
}
