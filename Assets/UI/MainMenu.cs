using System.Collections;
using ProgressBar;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
	public class MainMenu : MonoBehaviour
	{
		public GameObject[] Buttons;
		public GameObject ProgressBar;

		private void Start()
		{
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;

			ProgressBar.SetActive(false);
			foreach (var btn in Buttons)
			{
				btn.SetActive(true);
			}
		}

		public void StartGame()
		{
			ProgressBar.SetActive(true);
			foreach (var btn in Buttons)
			{
				btn.SetActive(false);
			}

			StartCoroutine(LoadScene("Scene1"));
		}
	
		public void Quit()
		{
			Application.Quit();
		}

		private IEnumerator LoadScene(string scene)
		{
			var loadingSceneOperation = SceneManager.LoadSceneAsync(scene);
			while (!loadingSceneOperation.isDone)
			{
				Debug.Log(loadingSceneOperation.progress);
				ProgressBar.GetComponent<ProgressRadialBehaviour>().Value = loadingSceneOperation.progress * 100;
				yield return null;
			}
		}
	}
}
