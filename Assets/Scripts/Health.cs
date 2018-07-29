using UnityEngine.Events;
using UnityEngine.Networking;

public class Health : NetworkBehaviour
{
	public float MaxHealth = 100;
	public bool DestroyObjOnDie = true;
	public UnityEvent OnDie;

	[SyncVar]
	public float CurrentHealth;
	
	// Use this for initialization
	private void Start ()
	{
		CurrentHealth = MaxHealth;
	}
	
	private void LateUpdate()
	{
		if (!(CurrentHealth < 0)) return;
		
		if (DestroyObjOnDie) Destroy(gameObject);
		OnDie.Invoke();
	}

	public void Damage(float damage)
	{
		CurrentHealth -= damage;
	}
}
