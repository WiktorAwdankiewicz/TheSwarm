using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyByContact : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (gameObject.CompareTag("Bullet") && other.CompareTag("MapObjects"))
        {
            Destroy(gameObject);
        }

        if (gameObject.CompareTag("Bullet") && other.CompareTag("SwarmLandUnit"))
        {
            GameController.remainingUnits -= 1;
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (gameObject.CompareTag("SwarmLandUnit") && other.gameObject.CompareTag("Player"))
        {
            GameController.lives -= 1;
            GameController.remainingUnits -= 1;
            Destroy(gameObject);
            
            if (GameController.lives == 0)
            {
                Destroy(gameObject);
                Destroy(GameObject.FindWithTag("Player"));
                SceneManager.LoadScene(2);
            }
        }
    }
}
