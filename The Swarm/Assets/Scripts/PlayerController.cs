using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public Camera MyCamera;
    [SerializeField]
    private float playerSpeed;
    [SerializeField]
    private float bulletSpeed;
    [SerializeField]
    private float timeBetweenBullets;
    [SerializeField]
    private float stunAbilityCooldown;
    [SerializeField]
    private float stunDuration;

    public TMP_Text stunAbilityCooldownTMP;
    public GameObject bulletPrefab;
    public Transform LaunchOffset;

    GameObject[] units;
    Vector2 _Movement;
    Vector2 direction;
    Vector3 mousePos;
    Rigidbody2D playerRB;
    bool fireContinuously = false;
    bool stunAbilityOnCooldown = false;
    bool stunAbilityUsed = false;
    float lastBulletFired;
    float stunDurationBackup;
    float stunAbilityCooldownBackup;

    private void Awake()
    {
        transform.position = new Vector3(0, 0, 0);
        playerRB = GetComponent<Rigidbody2D>();
        stunDurationBackup = stunDuration;
        stunAbilityCooldownBackup = stunAbilityCooldown;
        stunAbilityCooldownTMP.text = "Stun CD: Ready";
    }
   
    public void OnMovement(InputAction.CallbackContext context)
    {
        _Movement = context.ReadValue<Vector2>();
    }

    public void OnShooting(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            fireContinuously = true;
        }
        if (context.canceled)
        {
            fireContinuously = false;
        }
    }

    public void OnUsingStunAbility(InputAction.CallbackContext context)
    {
        if(context.performed && stunAbilityOnCooldown == false)
        {
            stunAbilityUsed = true;
        }
    }

    private void fireBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, LaunchOffset.position, transform.rotation);
        Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
        bulletRB.velocity = bulletSpeed * transform.up;
    }

    private void FixedUpdate()
    {
        units = GameObject.FindGameObjectsWithTag("SwarmLandUnit");

        // Player facing towards mouse position
        mousePos = Input.mousePosition;
        mousePos = MyCamera.ScreenToWorldPoint(mousePos);
        direction = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
        transform.up = direction;

        //_Rigidbody.velocity = _Movement * speed; //simple movement
        playerRB.AddForce(_Movement * playerSpeed); //smooth movement
        
        // Shooting bullets continuously while holding space
        if (fireContinuously)
        {
            float timeSinceLastFire = Time.time - lastBulletFired;
            if (timeSinceLastFire >= timeBetweenBullets)
            {
                fireBullet();
                lastBulletFired = Time.time;
            }
        }

        if(stunAbilityUsed == true)
        {
            if (stunDuration > 0)
            {
                stunAbilityCooldownTMP.text = "Stun CD: Used";
                foreach (GameObject unit in units)
                {
                    unit.GetComponent<Mover>().enabled = false;
                }
                stunDuration -= Time.fixedDeltaTime;
            }
            else
            {
                foreach (GameObject unit in units)
                {
                    unit.GetComponent<Mover>().enabled = true;
                }
                stunAbilityOnCooldown = true;
            }
            
            if(stunAbilityOnCooldown == true)
            {
                if (stunAbilityCooldown > 0)
                {
                    stunAbilityCooldownTMP.text = "Stun CD: " + Mathf.Round(stunAbilityCooldown);
                    stunAbilityCooldown -= Time.fixedDeltaTime;
                }
                else
                {
                    stunAbilityUsed = false;
                    stunAbilityOnCooldown = false;
                    stunDuration = stunDurationBackup;
                    stunAbilityCooldown = stunAbilityCooldownBackup;
                    stunAbilityCooldownTMP.text = "Stun CD: Ready";
                }
            }
        }
    }
}
