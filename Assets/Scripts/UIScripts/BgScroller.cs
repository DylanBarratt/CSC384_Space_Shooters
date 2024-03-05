using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
 
public class BgScroller : MonoBehaviour { 
	[SerializeField] private RawImage bgImg;
	[SerializeField] private float y = 0.1f;
 
	private void Update() {
		bgImg.uvRect = new Rect(bgImg.uvRect.position + new Vector2(0, y) * Time.deltaTime,bgImg.uvRect.size);
	}

	private void SetSpeed(float speed) {
		this.y = speed;
	}
}
