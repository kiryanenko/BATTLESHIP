using ProgressBar;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerControls : NetworkBehaviour {
	public GameObject Ship;
	public Transform TPSCamera;
	
	private float _forwardAxis;
	private float _sideAxis;

	private ShipControls _shipControls;

	// Use this for initialization
	private void Start ()
	{
		_shipControls = Ship.GetComponent<ShipControls>();

		if (!isLocalPlayer) return;
		
		foreach (var cam in Camera.allCameras)
		{
			cam.tag = "Untagged";
			cam.enabled = false;
		}
			
		TPSCamera.tag = "MainCamera";
		TPSCamera.GetComponent<Camera>().enabled = true;
			
		CustomNetworkManager.Instance.OnStartLocalPlayer(gameObject);
	}
	
	// Update is called once per frame
	private void Update () {
		if (!isLocalPlayer) return;
		
		Move();
		Aiming();
	}

	private void Move()
	{
		_shipControls.ForwardAxis = Input.GetAxis("Vertical");
		_shipControls.SideAxis = Input.GetAxis("Horizontal");
	}

	private void Aiming()
	{
		var ray = new Ray(TPSCamera.position, TPSCamera.forward);
		RaycastHit hit;
		Vector3 aim;
		if (Physics.Raycast(ray, out hit))
		{
			aim = hit.point;
		}
		else
		{
			aim = TPSCamera.forward * 10000 + TPSCamera.position;
		}
		_shipControls.TurnTurrets(aim);
		
		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			CmdFire(aim);
		}
	}

	[Command]
	private void CmdFire(Vector3 aim)
	{
		_shipControls.TurnTurrets(aim);
		_shipControls.FireTurrets();
	}
}
