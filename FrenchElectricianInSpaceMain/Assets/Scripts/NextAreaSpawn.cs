using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextAreaSpawn : MonoBehaviour
{
    public GameObject background;
    public float spawnOffset;
    [SerializeField] private int spawnCount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if(collision.gameObject.name == "Player" && spawnCount < 1)
        {
            Instantiate(background, transform.parent.transform.position + new Vector3(spawnOffset, 0), background.transform.rotation);
            spawnCount++;
        }
    }
}
