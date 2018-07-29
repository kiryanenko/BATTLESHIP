using UnityEngine;
using UnityEngine.Networking;

namespace AI
{
	public class TurretAI : NetworkBehaviour
	{
		public float ActionRadius = 1000;
	
		private TurretControls _turret;
	
		// Use this for initialization
		private void Start ()
		{
			_turret = GetComponent<TurretControls>();
		}
	
		// Update is called once per frame
		private void Update ()
		{
			if (!isServer) return;

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
			
				_turret.Turn(playerPosition);
				_turret.Fire();
				return;
			}
		}
	}
}
