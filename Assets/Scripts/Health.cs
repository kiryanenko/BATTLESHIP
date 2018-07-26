using UnityEngine;

public class Health : MonoBehaviour
{
	public float MaxHealth = 100;

	public float CurrentHealth { get; set; }
	
	// Use this for initialization
	void Start ()
	{
		CurrentHealth = MaxHealth;
	}
	
	private void FixedUpdate()
	{
		if (CurrentHealth < 0)
		{
			Destroy(gameObject);
		}
	}

	public void Damage(float damage)
	{
		CurrentHealth -= damage;
	}
}
