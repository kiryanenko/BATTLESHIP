using UnityEngine;
using UnityEngine.AI;

namespace AI
{
	public class ShipAI : MonoBehaviour {
		public float ActionRadius = 1000;

		public Transform[] PatrolPoints;
		public float DistanceForChangePatrolPoint = 500;
	
		private ShipControls _ship;
		private NavMeshAgent _agent;
		private int _currentPoint;
	
		// Use this for initialization
		private void Start ()
		{
			_ship = GetComponent<ShipControls>();
			_agent = GetComponent<NavMeshAgent>();
		}
	
		// Update is called once per frame
		private void Update ()
		{
			FireOnPlayers();
			Patrol();
		}

		private void FireOnPlayers()
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

		private void Patrol()
		{
			if (PatrolPoints.Length == 0) return;
			
			if (_agent.remainingDistance <= DistanceForChangePatrolPoint)
			{
				if (++_currentPoint >= PatrolPoints.Length)
				{
					_currentPoint = 0;
				}
			}
			
			_agent.destination = PatrolPoints[_currentPoint].position;
		}
	}
}
