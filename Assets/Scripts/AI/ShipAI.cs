using UnityEngine;

namespace AI
{
	public class ShipAI : MonoBehaviour {
		public float ActionRadius = 1000;
	
		private ShipControls _ship;
	
		// Use this for initialization
		private void Start ()
		{
			_ship = GetComponent<ShipControls>();
		}
	
		// Update is called once per frame
		private void Update ()
		{
			var players = GameObject.FindGameObjectsWithTag("Player");
			foreach (var player in players)
			{
				var playerPosition = player.transform.position;
				if (!(Vector3.Distance(playerPosition, transform.position) < ActionRadius)) continue;
			
				var ray = new Ray(transform.position, playerPosition - transform.position);
				RaycastHit hit;
				if (Physics.Raycast(ray, out hit))
				{
					if (hit.transform.gameObject != player) continue;
				}

				_ship.Aim = playerPosition;
				_ship.FireTurrets();
				return;
			}
		}
	}
}
