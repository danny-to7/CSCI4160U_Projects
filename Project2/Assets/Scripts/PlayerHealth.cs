using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int startingHealth = 100;
    [SerializeField] private Slider healthBar;
    [SerializeField] private Image damageImage;
    [SerializeField] private float flashingSpeed = 4f;
    [SerializeField] private Color flashColour = new Color(1f, 0f, 0f, 0.1f);

    private bool isDead;
    private bool isDamaged;
    private int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDamaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashingSpeed * Time.deltaTime);
        }
        isDamaged = false;
    }

    public void ReceiveHit(int damageAmount)
    {
        isDamaged = true;
        currentHealth -= damageAmount;
        healthBar.value = currentHealth;
        Debug.Log(currentHealth);
    }
}
