using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpItemScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IItemInterface ItemInter))
        {
            ItemInter.PowerUp();
        }
    }
}
