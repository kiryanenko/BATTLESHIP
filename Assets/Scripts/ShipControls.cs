using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class ShipControls : MonoBehaviour
{
	public float Thrust = 100;
	public float Torque = 5;

	public TurretControls[] Turrets;

	public float ForwardAxis { get; set; }
	public float SideAxis { get; set; }
	public Vector3 Aim { get; set; }

	private Rigidbody _rigidbody;

	private float _boostBonusEndTime;
	private float _boost = 1;

	
	// Use this for initialization
	private void Start ()
	{
		_rigidbody = GetComponent<Rigidbody>();
	}

	private void Update()
	{
		TurnTurrets();
		BonusesHandler();
	}

	private void FixedUpdate()
	{
		Move();
		Turn();
	}

	private void Move()
	{
		var trust = Thrust * ForwardAxis * _boost;
		_rigidbody.AddRelativeForce(Vector3.forward * trust);
	}

	private void Turn()
	{
		var torque = Torque * SideAxis * _rigidbody.velocity.magnitude + Torque * SideAxis;
		_rigidbody.AddRelativeTorque(Vector3.left * torque);
	}

	private void TurnTurrets()
	{
		foreach (var turret in Turrets)
		{
			turret.Turn(Aim);
		}
	}
	
	public void FireTurrets()
	{
		foreach (var turret in Turrets)
		{
			turret.Fire();
		}
	}

	private void BonusesHandler()
	{
		if (Time.time > _boostBonusEndTime)
		{
			_boost = 1;
		}
	}

	public void BoostBonus(float boost, float endTime)
	{
		_boost *= boost;
		_boostBonusEndTime = endTime;
	}
}
