using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrigger : MonoBehaviour {
	public float coolDownTimer=0f;

	void Update(){
		if (coolDownTimer > 0) {
			PathFinding.check = false;
			coolDownTimer -= Time.deltaTime;
		}
		if (coolDownTimer < 0) {
			PathFinding.check = true;
			coolDownTimer = 0;
		}
	}
	void OnTriggerEnter(Collider other){
		if (other.tag == "Fire") {
			Destroy(other.gameObject);
			coolDownTimer = 5;
		}
	}

}
