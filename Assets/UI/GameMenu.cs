using UnityEngine;

namespace UI
{
	public class GameMenu : MonoBehaviour
	{
		public void Quit()
		{
			Application.Quit();
		}
		
		public void Resume()
		{
			GameUIManager.Instance.DisableMenu();
		}
	}
}
