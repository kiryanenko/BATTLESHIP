using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class TurretControls : MonoBehaviour
{
	[SerializeField] private float _reloadTime = 0.5f;
	[SerializeField] private float _accuracy = 0.002f;
	[SerializeField] private GameObject _shellPrefab;
	[SerializeField] private Transform _barrel;
	[SerializeField] private float _caliber = 1;
	[SerializeField] private float _shellVellocity = 700;

	[Tooltip("Объекты по которым не будет вестись стрельба")]
	public GameObject[] NonAimingObjects;
	
	private float _reloadEndTime;

	public void Turn(Vector3 target)
	{
		var relativePos = target - transform.position;
		transform.rotation = Quaternion.LookRotation(relativePos);
	}

	public void Fire()
	{
		var now = Time.time;
		if (now < _reloadEndTime) return;
		if (!IsAimingObject()) return;

		_reloadEndTime = now + _reloadTime;

		var direction = _barrel.transform.forward;
		direction.x += Random.Range(-_accuracy, _accuracy);
		direction.y += Random.Range(-_accuracy, _accuracy);

		// FIXME: Object pool
		// var shell = BattleGameManager.Pool.Take(ShellPrefab);
		var shell = Instantiate(_shellPrefab);
		shell.transform.position = _barrel.position;
		shell.transform.rotation = Quaternion.LookRotation(direction);
		shell.GetComponent<Rigidbody>().velocity = direction * _shellVellocity;
		NetworkServer.Spawn(shell.gameObject);
	}

	private bool IsAimingObject()
	{
		RaycastHit hit;
		if (!Physics.BoxCast(
			_barrel.transform.position,
			new Vector3(_caliber, _caliber, _caliber),
			_barrel.transform.forward,
			out hit))
		{
			return true;
		}

		return hit.transform.gameObject != gameObject && NonAimingObjects.All(obj => hit.transform.gameObject != obj);
	}
}
