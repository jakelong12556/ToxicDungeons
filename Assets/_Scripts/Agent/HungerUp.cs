using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HungerUp : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            AgentGenerator.manager.currentPlayer.Eat(20);
            Destroy(gameObject);
        }
    }
}
