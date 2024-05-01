using UnityEngine;

public class EnemyOneMovement : MonoBehaviour {
	private Rigidbody2D rb;

	private float speed;
	
	//called by the generic enemy movement MonoBehaviour
	private void YReached(float s) {
		speed = s;
		rb = GetComponent<Rigidbody2D>();
		
		gameObject.SendMessage("Shoot"); // begins the enemy shooting loop
		Invoke(nameof(XPatrol), 0.01f); //idk why I did this lol
	}
	
	private void XPatrol() {
		rb.velocity = new Vector2(speed, 0);
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.CompareTag("SBoundary")) {
			rb.velocity = Vector2.zero;
			speed = -speed;  // neat refactoring came from this cool line 
			Invoke(nameof(XPatrol), 0.01f);
		}
	}
}
