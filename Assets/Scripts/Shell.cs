using UnityEngine;

public class Shell : MonoBehaviour
{
	public float Damage = 50;
	public float DamageRadius = 5;
	public float TimeLive = 3f;
	public GameObject Explosion;
	
	// Use this for initialization
	private void Start () {
		Destroy(gameObject, TimeLive);
	}

	private void OnCollisionEnter(Collision collision)
	{
		Instantiate(Explosion, transform.position, transform.rotation);
		Destroy(gameObject);

		var objHealth = collision.gameObject.GetComponent<Health>();
		if (objHealth)
		{
			objHealth.Damage(Damage);
		}

		var hits = Physics.SphereCastAll(transform.position, DamageRadius, transform.rotation.eulerAngles);
		foreach (var hit in hits)
		{
			var health = hit.transform.GetComponent<Health>();
			if (health)
			{
				health.Damage(Damage);
			}
		}
	}
}
