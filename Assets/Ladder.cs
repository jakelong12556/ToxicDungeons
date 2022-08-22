using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    public TextMesh enter;
    private bool isNextLevel; 

    private void Awake()
    {
        isNextLevel = false;
        enter.text = "";
    }

    private void Update()
    {

        if (isNextLevel)
        {
            if (Input.GetButtonDown("Submit"))
            {

                LevelManager.levelManager.NextLevel();

            }
        } 
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            enter.text = "Press Enter";
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            isNextLevel = true;
        }
    }



    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            enter.text = "";
            isNextLevel = false;
        }
    }
}
