using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
	public float LiveTime = 5f;
	
	// Use this for initialization
	void Start () {
		if (LiveTime >= 0)
		{
			Destroy(gameObject, LiveTime);
		}
	}
}
