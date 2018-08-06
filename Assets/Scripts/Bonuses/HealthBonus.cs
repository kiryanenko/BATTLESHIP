using UnityEngine;

namespace Bonuses
{
	public class HealthBonus : MonoBehaviour 
	{
		public float Health = 1000;

		private void OnTriggerEnter(Collider objCollider)
		{
			var bonuseController = objCollider.gameObject.GetComponent<BonusController>();
			if (!bonuseController) return;
		
			bonuseController.HelthBonuse(Health);
			Destroy(gameObject);
		}
	}
}
