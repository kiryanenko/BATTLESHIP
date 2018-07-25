using UnityEngine;

public class Shell : MonoBehaviour
{
	public float TimeLive = 3f;
	public GameObject Explosion;
	
	// Use this for initialization
	private void Start () {
		Destroy(gameObject, TimeLive);
	}

	private void OnCollisionEnter(Collision other)
	{
		Instantiate(Explosion, transform.position, transform.rotation);
		Destroy(gameObject);
	}
}
