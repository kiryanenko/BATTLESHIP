using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {
	private float _forwardAxis;
	private float _sideAxis;

	private Transform _camera;
	public ShipControls Ship;

	// Use this for initialization
	void Start ()
	{
		_camera = Camera.main.transform;
	}
	
	// Update is called once per frame
	void Update () {
		Move();
		Aiming();
	}

	private void Move()
	{
		Ship.ForwardAxis = Input.GetAxis("Vertical");
		Ship.SideAxis = Input.GetAxis("Horizontal");
	}

	private void Aiming()
	{
		var ray = new Ray(_camera.position, _camera.forward);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit))
		{
			Ship.Aim = hit.point;
		}
		else
		{
			Ship.Aim = _camera.forward * 10000 + _camera.position;
		}
		
		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			Ship.FireTurrets();
		}
	}
}
