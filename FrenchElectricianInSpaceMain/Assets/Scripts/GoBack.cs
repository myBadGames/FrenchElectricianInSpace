using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoBack : MonoBehaviour
{
    private float originalX = 184.2f;

    public void GoBackToFront()
    {
        transform.position = new Vector2(originalX, 0);
    }
}
