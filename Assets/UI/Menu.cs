using UnityEngine;
using UnityEngine.Networking;

namespace UI
{
	public class Menu : MonoBehaviour
	{
		public GameObject[] Buttons;
		public NetworkManagerHUD NetworkHud;
		public bool IsActive;
		
		// Use this for initialization
		private void Start () {
			Cursor.visible = IsActive;
			NetworkHud.showGUI = IsActive;
		}
	
		// Update is called once per frame
		private void LateUpdate () {
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				IsActive = !IsActive;
				SetState(IsActive);
			}
		}

		public void Quit()
		{
			Application.Quit();
		}
		
		public void Resume()
		{
			IsActive = false;
			SetState(false);
		}

		private void SetState(bool isActive)
		{
			Cursor.visible = isActive;
			NetworkHud.showGUI = isActive;
			foreach (var btn in Buttons)
			{
				btn.SetActive(isActive);
			}
		}
	}
}
