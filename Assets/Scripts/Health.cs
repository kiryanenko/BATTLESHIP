using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

[System.Serializable]
public class DieEvent : UnityEvent<GameObject>
{
}

public class Health : NetworkBehaviour
{
	public float MaxHealth = 100;
	public bool DestroyObjOnDie = true;
	public UnityEvent OnDie;
	public DieEvent DieEvent;

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
		
		DieEvent.Invoke(gameObject);
		OnDie.Invoke();
		
		if (DestroyObjOnDie) Destroy(gameObject);
	}

	public void Damage(float damage)
	{
		CurrentHealth -= damage;
	}
}
