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
		_shipControls = Ship.GetComponent<ShipControls>();
		
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
		Vector3 aim;
		if (Physics.Raycast(ray, out hit))
		{
			aim = hit.point;
		}
		else
		{
			aim = Camera.forward * 10000 + Camera.position;
		}
		_shipControls.TurnTurrets(aim);
		
		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			CmdFire(aim);
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
		_shipControls.TurnTurrets(aim);
		_shipControls.FireTurrets();
	}
}
