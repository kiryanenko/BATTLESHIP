using System.Collections.Generic;
using System.Linq;
using ProgressBar;
using UnityEngine;
using UnityEngine.Networking;
using Utils;

namespace UI
{
	public class GameUIManager : Singleton<GameUIManager>
	{
		[SerializeField] private Canvas _canvas;
		[SerializeField] private NetworkManagerHUD _networkHud;
		[SerializeField] private GameObject _gameMenu;
		[SerializeField] private bool _isGameMenuActive = true;
		[SerializeField] private ProgressBarBehaviour _playerHealthBar;
		[SerializeField] private GameObject _HealthBarPrefab;

		public float HealthBarDistance = 2000;

		private GameObject _player;
		private Health _playerHealth;
		private Dictionary<Health, GameObject> _healthBars = new Dictionary<Health, GameObject>();
		
		// Use this for initialization
		private void Start () {
			SetMenuActive(_isGameMenuActive);
		}
	
		private void LateUpdate () {
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				SetMenuActive(!_isGameMenuActive);
			}

			UpdateHealth();
			UpdateEnymesHealthBars();
		}
		
		public void SetMenuActive(bool isActive)
		{
			_isGameMenuActive = isActive;
			Cursor.lockState = isActive ? CursorLockMode.None : CursorLockMode.Locked;
			Cursor.visible = isActive;
			_networkHud.showGUI = isActive;
			_gameMenu.SetActive(isActive);
		}

		public void ShowMenu()
		{
			SetMenuActive(true);
		}
		
		public void DisableMenu()
		{
			SetMenuActive(false);
		}

		public void OnStartLocalPlayer()
		{
			_player = CustomNetworkManager.Instance.LocalPlayer;
			_playerHealth = _player.GetComponent<Health>();
		}
		
		private void UpdateHealth()
		{
			if (!_player) return;
			
			var healthProgress = _playerHealth.CurrentHealth / _playerHealth.MaxHealth * 100;
			_playerHealthBar.Value = healthProgress;
		}

		private void UpdateEnymesHealthBars()
		{
			if (!_player) return;

			var pos = _player.transform.position;
			var cam = Camera.main;
			var healths = FindObjectsOfType<Health>();
			foreach (var health in healths)
			{
				if (health == _playerHealth) continue;

				var objPos = health.transform.position;
				if (Vector3.Distance(objPos, pos) < HealthBarDistance && 
				    GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(Camera.main), health.GetComponent<Collider>().bounds))
				{
					GameObject healthBar;
					if (_healthBars.ContainsKey(health))
					{
						healthBar = _healthBars[health];
						healthBar.SetActive(true);
					}
					else
					{
						healthBar = Instantiate(_HealthBarPrefab, _canvas.transform);
						_healthBars.Add(health, healthBar);
						health.DieEvent.AddListener(OnDieEnyme);
					}
					var healthBarPos = cam.WorldToScreenPoint(objPos);
					healthBarPos.z = 10;
					healthBar.transform.position = healthBarPos;
					healthBar.GetComponentInChildren<SimpleHealthBar>().UpdateBar(health.CurrentHealth, health.MaxHealth);
				}
				else
				{
					if (_healthBars.ContainsKey(health))
					{
						_healthBars[health].SetActive(false);
					}
				}
			}
		}

		public void OnDieEnyme(GameObject enyme)
		{
			var health = enyme.GetComponent<Health>();
			if (!_healthBars.ContainsKey(health)) return;
			
			Destroy(_healthBars[health]);
			_healthBars.Remove(health);
		}
	}
}
