using ProgressBar;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerControls : NetworkBehaviour {
	public GameObject Ship;
	public ProgressBarBehaviour HealthProgressBar;
	
	private float _forwardAxis;
	private float _sideAxis;

	public Transform Camera;
	private ShipControls _shipControls;

	// Use this for initialization
	private void Start ()
	{
		if (isLocalPlayer)
		{
			Camera.tag = "MainCamera";
			Camera.GetComponent<Camera>().enabled = true;
		}
		else
		{
			Camera.GetComponent<Camera>().enabled = false;
			Destroy(Camera.gameObject);
			return;
		}
		
		_shipControls = Ship.GetComponent<ShipControls>();

		if (!HealthProgressBar)
		{
			HealthProgressBar = GameObject.Find("HealthProgressBar").GetComponent<ProgressBarBehaviour>();
		}
	}
	
	// Update is called once per frame
	private void Update () {
		if (!isLocalPlayer) return;
		
		Move();
		Aiming();
	}

	private void LateUpdate()
	{
		if (!isLocalPlayer) return;

		UpdateHealth();
	}

	private void Move()
	{
		_shipControls.ForwardAxis = Input.GetAxis("Vertical");
		_shipControls.SideAxis = Input.GetAxis("Horizontal");
	}

	private void Aiming()
	{
		var ray = new Ray(Camera.position, Camera.forward);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit))
		{
			_shipControls.Aim = hit.point;
		}
		else
		{
			_shipControls.Aim = Camera.forward * 10000 + Camera.position;
		}
		
		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			CmdFire(_shipControls.Aim);
		}
	}

	private void UpdateHealth()
	{
		float healthProgress = 0;
		if (Ship)
		{
			var health = Ship.GetComponent<Health>();
			healthProgress = health.CurrentHealth / health.MaxHealth * 100;
		}
		HealthProgressBar.Value = healthProgress;
	}

	[Command]
	private void CmdFire(Vector3 aim)
	{
		_shipControls.Aim = aim;
		_shipControls.FireTurrets();
	}
}
