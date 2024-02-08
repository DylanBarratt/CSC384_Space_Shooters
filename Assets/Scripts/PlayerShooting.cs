using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour {
    [SerializeField] private Transform gunLoc;
    [SerializeField] private GameObject bulletPrefab;

    //these consts are attributes that should be able to be changed with player upgrades
    private const float RATE_OF_FIRE = 0.5f; 

    private bool shoot;
    private float lastShotTime;

    private void Start() {
        lastShotTime = 0;
    }

    private void Update() {
        if (Input.GetButtonDown("Fire1")){
            shoot = true ;
        }

        if (Input.GetButtonUp("Fire1")){
            shoot = false;
        }
    }

    private void FixedUpdate() {
        if (shoot && lastShotTime <= 0){
            Shoot();
            lastShotTime = RATE_OF_FIRE;
        }

        if (lastShotTime > 0) {
            lastShotTime -= Time.deltaTime;
        }
    }

    private void Shoot() {
        Instantiate(bulletPrefab, gunLoc.position, Quaternion.identity);
    }
}
