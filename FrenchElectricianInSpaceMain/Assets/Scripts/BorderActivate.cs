using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderActivate : MonoBehaviour
{
    [SerializeField] private GameObject border;
    private BoxCollider2D borderCollider;

    void Start()
    {
        borderCollider = border.GetComponent<BoxCollider2D>();
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("activate border");

        if (collision.gameObject.CompareTag("Player"))
        {
            float horizontal = collision.gameObject.GetComponent<PlayerController>().horizontalInput;

            if(gameObject.name.Contains("Camera"))
            {
                if (horizontal > 0)
                {
                    borderCollider.isTrigger = false;
                }
            }

            if(gameObject.name.Contains("Next"))
            {
                borderCollider.isTrigger = true;
            }
        }
    }
}
