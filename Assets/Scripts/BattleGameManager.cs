using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using Utils;

public class BattleGameManager : Singleton<BattleGameManager>
{
	[SerializeField] private GameObject _winText;
	[SerializeField] private GameObject _loseText;
	[Tooltip("Время через сколько будет загружено главное меню после окончания игры")]
	[SerializeField] private float _mainMenuLoadingTime = 5;
	
	private bool _gameStarted;
	private bool _gameFinished;
	private float _timeLoadMainMenu;
	
	// Use this for initialization
	private void Start ()
	{
		if (!_winText) _winText = GameObject.Find("WinText");
		if (!_loseText) _loseText = GameObject.Find("LoseText");
		
		_winText.SetActive(false);
		_loseText.SetActive(false);
	}
	
	// Update is called once per frame
	private void LateUpdate ()
	{
		if (!_gameStarted) return;
		
		var enymes = GameObject.FindGameObjectsWithTag("Enyme");
		if (enymes.Length == 0)
		{
			_winText.SetActive(true);
			FinishGame();
		}

		if (_gameFinished && Time.time > _timeLoadMainMenu)
		{
			SceneManager.LoadScene("UI/MainMenu");
		}
	}

	public void StartGame()
	{
		_gameStarted = true;

		var player = CustomNetworkManager.Instance.LocalPlayer;
		player.GetComponent<Health>().OnDie.AddListener(OnDie);
	}

	private void FinishGame()
	{
		if (_gameFinished) return;
		
		_gameFinished = true;
		_timeLoadMainMenu = Time.time + _mainMenuLoadingTime;
	}

	public void OnDie()
	{
		_loseText.SetActive(true);
		FinishGame();
	}
}
