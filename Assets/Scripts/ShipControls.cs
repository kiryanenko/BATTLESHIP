using Bonuses;
using UnityEngine;
using UnityEngine.Networking;

public class ShipControls : NetworkBehaviour
{
	[SerializeField] private float _thrust = 600;
	[SerializeField] private float _torque = 3000;

	public TurretControls[] Turrets;
	[SerializeField] private BonusController _bonusController;

	public float DestructionTime = 8;

	public float ForwardAxis { get; set; }
	public float SideAxis { get; set; }

	private Rigidbody _rigidbody;

	[SyncVar]
	private bool _destroyed;
	
	
	// Use this for initialization
	private void Start ()
	{
		_rigidbody = GetComponent<Rigidbody>();
		if (!_bonusController) GetComponent<BonusController>();
	}

	private void FixedUpdate()
	{
		if (_destroyed) return;

		Move();
		Turn();
	}

	private void Move()
	{
		var trust = _thrust * ForwardAxis;
		if (_bonusController) trust *= _bonusController.Boost;
		_rigidbody.AddRelativeForce(Vector3.forward * trust);
	}

	private void Turn()
	{
		var torque = _torque * SideAxis * _rigidbody.velocity.magnitude + _torque * SideAxis;
		_rigidbody.AddRelativeTorque(Vector3.left * torque);
	}

	public void TurnTurrets(Vector3 aim)
	{
		foreach (var turret in Turrets)
		{
			turret.Turn(aim);
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
