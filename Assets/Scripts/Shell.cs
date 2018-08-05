using UnityEngine;

public class Shell : MonoBehaviour
{
	public float Damage = 50;
	public float DamageRadius = 5;
	[SerializeField] private float _timeLive = 3f;
	[SerializeField] private GameObject _explosion;

	private float _endLiveTime;
	
	// Use this for initialization
	private void OnEnable()
	{
		_endLiveTime = Time.time + _timeLive;
	}

	private void Update()
	{
		if (Time.time > _endLiveTime)
		{
			// FIXME: Game object pool
			// BattleGameManager.Pool.Release(gameObject);
			Destroy(gameObject);
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

		var explosion = BattleGameManager.Pool.Take(_explosion);
		explosion.transform.position = transform.position;
		explosion.transform.rotation = transform.rotation;
//		var exposion =  Instantiate(Explosion, transform.position, transform.rotation);
//		Destroy(exposion, TimeLive);
		// FIXME: Game object pool
		// BattleGameManager.Pool.Release(gameObject);
		Destroy(gameObject);
	}
}
