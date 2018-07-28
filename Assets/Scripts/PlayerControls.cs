using ProgressBar;
using UnityEngine;

public class PlayerControls : MonoBehaviour {
	public GameObject Ship;
	public ProgressBarBehaviour ProgressBar;
	
	private float _forwardAxis;
	private float _sideAxis;

	private Transform _camera;
	private ShipControls _shipControls;

	// Use this for initialization
	private void Start ()
	{
		_camera = Camera.main.transform;
		_shipControls = Ship.GetComponent<ShipControls>();
	}
	
	// Update is called once per frame
	private void Update () {
		Move();
		Aiming();
	}

	private void LateUpdate()
	{
		UpdateHealth();
	}

	private void Move()
	{
		_shipControls.ForwardAxis = Input.GetAxis("Vertical");
		_shipControls.SideAxis = Input.GetAxis("Horizontal");
	}

	private void Aiming()
	{
		var ray = new Ray(_camera.position, _camera.forward);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit))
		{
			_shipControls.Aim = hit.point;
		}
		else
		{
			_shipControls.Aim = _camera.forward * 10000 + _camera.position;
		}
		
		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			_shipControls.FireTurrets();
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
		ProgressBar.Value = healthProgress;
	}
}
