using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostBonus : MonoBehaviour
{
	public float Boost = 2;
	public int ActionTime = 15;

	private void OnTriggerEnter(Collider objCollider)
	{
		var ship = objCollider.gameObject.GetComponent<ShipControls>();
		if (ship)
		{
			ship.BoostBonus(Boost, Time.time + ActionTime);
			Destroy(gameObject);
		}
	}
}
