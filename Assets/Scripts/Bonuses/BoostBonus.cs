using UnityEngine;

namespace Bonuses
{
	public class BoostBonus : MonoBehaviour
	{
		public float Boost = 2;
		public int ActionTime = 15;

		private void OnTriggerEnter(Collider objCollider)
		{
			var ship = objCollider.gameObject.GetComponent<BonusController>();
			if (!ship) return;
		
			ship.BoostBonus(Boost, Time.time + ActionTime);
			Destroy(gameObject);
		}
	}
}
