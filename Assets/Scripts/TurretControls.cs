using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretControls : MonoBehaviour
{
	public Rigidbody ShellPrefab;
	public Transform Barrel;

	public float ShellVellocity = 100;
	
	private Quaternion _rot;
	private bool _isFire;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
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
		
		var shell = Instantiate(ShellPrefab, Barrel.position, Barrel.rotation);
		shell.velocity = transform.forward * ShellVellocity;
		_isFire = false;
	}

	public void Fire()
	{
		_isFire = true;
	}
}
