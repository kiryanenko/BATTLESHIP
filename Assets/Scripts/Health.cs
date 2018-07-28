using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
	public float MaxHealth = 100;
	public bool DestroyObjOnDie = true;
	public UnityEvent OnDie;

	public float CurrentHealth { get; set; }
	
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
