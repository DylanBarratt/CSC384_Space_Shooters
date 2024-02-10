using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerBullet : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D coll) {
        if (coll.gameObject.CompareTag("Enemy")) {
            gameObject.SendMessage("DestroyBullet");
        }
    }
}
