using UnityEngine;

public class EnemyMovement : MonoBehaviour {
	private Rigidbody2D rb;

	private float speed;
	private float targetY;
	private float moveDelay = 0.2f;
	
	private void Move(float s) {
		speed = s;
		rb = GetComponent<Rigidbody2D>();
		
		MoveToYPosition();
	}
	
	private void MoveToYPosition() {
		float screenTop = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y;
		targetY = Random.Range(0, screenTop / 1.5f);

		rb.velocity = new Vector2(0, -(speed * 2.5f));
		
		Invoke(nameof(YPosCheck), moveDelay);
	}

	private void YPosCheck() {
		if (rb.position.y <= targetY) {
			rb.velocity = Vector2.zero;
			gameObject.SendMessage("YReached", speed);
			
			return;
		}
		
		Invoke(nameof(YPosCheck), moveDelay);
	}
}
