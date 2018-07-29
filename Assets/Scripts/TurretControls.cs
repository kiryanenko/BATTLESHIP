using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Networking;

public class TurretControls : NetworkBehaviour
{
	public float ReloadTime;
	public float Accuracy;
	public Rigidbody ShellPrefab;
	public Transform Barrel;
	public float Caliber;
	public float ShellVellocity = 100;

	[Tooltip("Объекты по которым не будет вестись стрельба")]
	public GameObject[] NonAimingObjects;
	
	private Quaternion _rot;
	private bool _isFire;
	private float _reloadEndTime;
	
	// Update is called once per frame
	private void Update () {
		transform.rotation = _rot;
	}

	public void Turn(Vector3 target)
	{
		var relativePos = target - transform.position;
		_rot = Quaternion.LookRotation(relativePos);
	}
	
	private void FixedUpdate()
	{
		TurretFire();
	}

	private void TurretFire()
	{
		if (!_isFire) return;
		_isFire = false;

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

	public void Fire()
	{
		_isFire = true;
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
