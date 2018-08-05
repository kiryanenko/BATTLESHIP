using UnityEngine;

public class Shell : MonoBehaviour
{
	public float Damage = 50;
	public float DamageRadius = 5;
	public float TimeLive = 3f;
	public GameObject Explosion;

	private float _endLiveTime;
	
	// Use this for initialization
	private void OnEnable()
	{
		_endLiveTime = Time.time + TimeLive;
	}

	private void Update()
	{
		if (Time.time > _endLiveTime)
		{
			BattleGameManager.Pool.Release(gameObject);
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
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
		
		var exposion =  Instantiate(Explosion, transform.position, transform.rotation);
		Destroy(exposion, TimeLive);
		BattleGameManager.Pool.Release(gameObject);		
	}
}
