using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderActivate : MonoBehaviour
{
    private BoxCollider2D col;

    void Start()
    {
        col = GetComponent<BoxCollider2D>();
        col.isTrigger = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            col.isTrigger = false;
        }
    }
}
