using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCamera : MonoBehaviour {
	public Transform Target;
	public float RotSpeed = 1f;
	public float ZoomSpeed = 15f;
	
	private float _rotX, _rotY;
	private Vector3 _offset;
	
	void Start() {
		_rotY = transform.eulerAngles.y;
		_offset = Target.position - transform.position;
		Cursor.visible = false;
	}
	
	void LateUpdate() {
		Zoom();
		RotateCamera();
	}

	private void RotateCamera()
	{
		_rotY += Input.GetAxis("Mouse X") * RotSpeed;
		_rotX += - Input.GetAxis("Mouse Y") * RotSpeed;
		
		var rotation = Quaternion.Euler(_rotX, _rotY, 0);
		transform.position = Target.position - (rotation * _offset);
		transform.rotation = rotation;
	}
	
	private void Zoom()
	{
		_offset.z += - Input.GetAxis("Mouse ScrollWheel") * ZoomSpeed;
	}
}
