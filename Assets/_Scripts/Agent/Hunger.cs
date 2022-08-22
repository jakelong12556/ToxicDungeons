using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hunger : MonoBehaviour
{
    private Image hungerBar;
    public float currentHunger;
    public float maxHunger;

    Player player;
    private void Start()
    {
        hungerBar = GetComponent<Image>();
        player = FindObjectOfType<Player>();
        maxHunger = player.maxHunger;

    }

    private void Update()
    {
        currentHunger = player.curHunger;
        hungerBar.fillAmount = currentHunger / maxHunger;
    }
}
