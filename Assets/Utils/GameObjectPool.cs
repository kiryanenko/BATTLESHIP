using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public class GameObjectPool
    {
        private readonly Dictionary<GameObject, Stack<GameObject>> _freeInstances = new Dictionary<GameObject, Stack<GameObject>>();
        private readonly Dictionary<GameObject, GameObject> _instances = new Dictionary<GameObject, GameObject>();
        
        public GameObject Take(GameObject prefab)
        {
            GameObject instance;
            if (_freeInstances.ContainsKey(prefab))
            {
                var pool = _freeInstances[prefab];
                if (pool.Count > 0)
                {
                    instance = pool.Pop();
                    instance.SetActive(true);
                    return instance;
                }
                
                instance = Object.Instantiate(prefab);
                _instances.Add(instance, prefab);
                return instance;
            }

            _freeInstances.Add(prefab, new Stack<GameObject>());
            instance = Object.Instantiate(prefab);
            _instances.Add(instance, prefab);
            return instance;
        }

        public void Release(GameObject gameObject)
        {
            if (!_instances.ContainsKey(gameObject))
            {
                Debug.LogErrorFormat("Prefab not found for instance '{0}'", gameObject.name);
                Object.Destroy(gameObject);
                return;
            }
            
            gameObject.SetActive(false);
            var prefab = _instances[gameObject];
            _freeInstances[prefab].Push(gameObject);
        }

        public void Clear()
        {
            _freeInstances.Clear();
        }
    }
}
