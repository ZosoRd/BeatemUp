using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : MonoBehaviour
{
    public int healthAmount = 100;
    private Health playerHealth; 
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerHealth = other.gameObject.GetComponent<Health>();

            if (playerHealth != null)
            {
                playerHealth.RestoreHealth(healthAmount); 

                gameObject.SetActive(false);
            }
        }
    }
}
