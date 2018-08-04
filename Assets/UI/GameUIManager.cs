using ProgressBar;
using UnityEngine;
using UnityEngine.Networking;
using Utils;

namespace UI
{
	public class GameUIManager : Singleton<GameUIManager>
	{
		[SerializeField] private NetworkManagerHUD _networkHud;
		[SerializeField] private GameObject _gameMenu;
		[SerializeField] private bool _isGameMenuActive = true;
		[SerializeField] private ProgressBarBehaviour _healthBar;

		private GameObject _player;
		private Health _playerHealth;
		
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
			_healthBar.Value = healthProgress;
		}
	}
}
