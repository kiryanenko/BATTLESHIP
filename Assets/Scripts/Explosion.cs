using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
	[SerializeField] private float _liveTime = 5f;

	private float _endLiveTime;
	
	// Use this for initialization
	private void OnEnable()
	{
		_endLiveTime = Time.time + _liveTime;
	}

	private void Update()
	{
		if (Time.time > _endLiveTime)
		{
			BattleGameManager.Pool.Release(gameObject);
		}
	}
}
