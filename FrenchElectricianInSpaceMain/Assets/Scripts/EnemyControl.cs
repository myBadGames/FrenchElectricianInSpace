using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    [SerializeField] private float movingSpeed;
    [SerializeField] private float direction;

    void Start()
    {
        direction = 1;
    }

    void Update()
    {
        transform.Translate(Vector2.left * movingSpeed * direction * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("GroundTrigger"))
        {
            direction = -direction;
        }
    } 
}
