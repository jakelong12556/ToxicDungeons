using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUp : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            AgentGenerator.manager.currentPlayer.Heal(10);
            Destroy(gameObject);
        }
    }
}
