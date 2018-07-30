using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class TurretControls : MonoBehaviour
{
	public float ReloadTime;
	public float Accuracy;
	public Rigidbody ShellPrefab;
	public Transform Barrel;
	public float Caliber;
	public float ShellVellocity = 100;

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

		_reloadEndTime = now + ReloadTime;

		var direction = Barrel.transform.forward;
		direction.x += Random.Range(-Accuracy, Accuracy);
		direction.y += Random.Range(-Accuracy, Accuracy);
		var shell = Instantiate(ShellPrefab, Barrel.position, Quaternion.LookRotation(direction));
		shell.velocity = direction * ShellVellocity;
		NetworkServer.Spawn(shell.gameObject);
	}

	private bool IsAimingObject()
	{
		RaycastHit hit;
		if (!Physics.BoxCast(
			Barrel.transform.position,
			new Vector3(Caliber, Caliber, Caliber),
			Barrel.transform.forward,
			out hit))
		{
			return true;
		}

		return hit.transform.gameObject != gameObject && NonAimingObjects.All(obj => hit.transform.gameObject != obj);
	}
}
