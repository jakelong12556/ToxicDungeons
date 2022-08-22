using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    private Image healthBar;
    public float currentHealth;
    public float maxHealth;

    Player player;
    private void Start()
    {
        healthBar = GetComponent<Image>();
        player = FindObjectOfType<Player>();
        maxHealth = player.maxHealth;

    }

    private void Update()
    {
        currentHealth = player.curHealth;
        healthBar.fillAmount = currentHealth / maxHealth;
    }


}
