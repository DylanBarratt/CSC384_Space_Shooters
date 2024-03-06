using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour {
    [SerializeField] private Transform gunLoc;
    [SerializeField] private GameObject bulletPrefab;
    
    private  float rateOfFire = 0.1f;  //default

    private bool shoot;
    private float lastShotTime;

    private void Start() {
        lastShotTime = 0;
    }

    private void SetROF(float val) {
        rateOfFire = val;
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
            lastShotTime = rateOfFire;
        }

        if (lastShotTime > 0) {
            lastShotTime -= Time.deltaTime;
        }
    }

    private void Shoot() {
        Instantiate(bulletPrefab, gunLoc.position, Quaternion.identity);
    }
}
