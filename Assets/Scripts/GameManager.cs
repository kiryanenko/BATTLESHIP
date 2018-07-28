using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public GameObject WinText;
	public GameObject LoseText;
	[Tooltip("Время через сколько будет загружено главное меню после окончания игры")]
	public float MainMenuLoadingTime = 5;
	
	private Health _playerHealth;
	private bool _gameFinished;
	private float _timeLoadMainMenu;
	
	// Use this for initialization
	private void Start ()
	{
		_playerHealth = GetComponent<PlayerControls>().Ship.GetComponent<Health>();
		WinText.SetActive(false);
		LoseText.SetActive(false);
	}
	
	// Update is called once per frame
	private void LateUpdate ()
	{
		var enymes = GameObject.FindGameObjectsWithTag("AI");
		if (enymes.Length == 0)
		{
			WinText.SetActive(true);
			FinishGame();
		}
		else if (!_playerHealth || _playerHealth.CurrentHealth <= 0)
		{
			LoseText.SetActive(true);
			FinishGame();
		}

		if (_gameFinished && Time.time > _timeLoadMainMenu)
		{
			SceneManager.LoadScene("UI/MainMenu");
		}
	}

	private void FinishGame()
	{
		if (_gameFinished) return;
		
		_gameFinished = true;
		_timeLoadMainMenu = Time.time + MainMenuLoadingTime;
	}
}
