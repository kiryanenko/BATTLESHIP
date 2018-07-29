using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Scripting.APIUpdating;

public class ShipControls : NetworkBehaviour
{
	public float Thrust = 100;
	public float Torque = 5;

	public TurretControls[] Turrets;

	public float DestructionTime = 8;

	public float ForwardAxis { get; set; }
	public float SideAxis { get; set; }
	public Vector3 Aim { get; set; }

	private Rigidbody _rigidbody;

	[SyncVar]
	private float _boostBonusEndTime;
	[SyncVar]
	private float _boost = 1;
	[SyncVar]
	private bool _destroyed;

	
	// Use this for initialization
	private void Start ()
	{
		_rigidbody = GetComponent<Rigidbody>();
	}

	private void Update()
	{
		if (_destroyed) return;

		TurnTurrets();
		BonusesHandler();
	}

	private void FixedUpdate()
	{
		if (_destroyed) return;

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
		if (_destroyed) return;

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

	public void OnDie()
	{
		gameObject.tag = "Untagged";
		_rigidbody.constraints = RigidbodyConstraints.None;
		_destroyed = true;
		Destroy(gameObject, DestructionTime);
	}

	public bool IsDestroyed()
	{
		return _destroyed;
	}
}
