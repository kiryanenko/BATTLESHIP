using UnityEngine;

namespace Utils
{
	public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
	{
		private static T _instance;

		public static T Instance
		{
			get
			{
				if (_instance)
				{
					return _instance;
				}

				var instances = FindObjectsOfType<T>();
				if (instances.Length > 1)
				{
					Debug.LogErrorFormat("More than one instance of type {0}", typeof(T).Name);
				}

				if (instances.Length == 0)
				{
					var someGameObject = new GameObject(typeof(T).Name);
					_instance = someGameObject.AddComponent<T>();
					return _instance;
				}

				_instance = instances[0];
				return _instance;
			}
		}
	}
}
