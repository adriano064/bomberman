using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickups : MonoBehaviour
{
    public enum ItemType
    {
        ExtraBomb,
        BlastRadius,
        SpeedIncrease,
    }

    public ItemType type;

    private void OnItemPickup(GameObject player)
    {
        switch (type)
        {
            case ItemType.ExtraBomb:
                player.GetComponent<bomba>().AddBomb();
                break;
            
            case ItemType.BlastRadius:
                player.GetComponent<bomba>().explosionRadius++;
                break;
            
            case ItemType.SpeedIncrease:
                player.GetComponent<movimento>().speed++;
                break;
        }
        
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            OnItemPickup(other.gameObject);
        }
    }
}
