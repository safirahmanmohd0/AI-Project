using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollision : MonoBehaviour {
	
	void OnTriggerEnter(Collider coll)
	{
		if (coll.tag == "Zombie") {
			Application.LoadLevel("Game Over");
		}
	}


}
