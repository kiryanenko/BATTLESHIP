using UnityEngine;
using UnityEngine.Networking;

namespace Bonuses
{
    public class BonusController : NetworkBehaviour {
        [SerializeField] private Health _health;
    
        [SyncVar]
        private float _boost = 1;
        public float Boost
        {
            get
            {
                if (Time.time > _boostBonusEndTime)
                {
                    _boost = 1;
                }
                return _boost;
            }
        }
    
        [SyncVar]
        private float _boostBonusEndTime;
        public float BoostBonusEndTime
        {
            get { return _boostBonusEndTime; }
        }

        private void Start()
        {
            if (!_health) GetComponent<Health>();
        }

        public void HelthBonuse(float health)
        {
            _health.CurrentHealth = Mathf.Clamp(_health.CurrentHealth + health, _health.CurrentHealth, _health.MaxHealth);
        }
    
        public void BoostBonus(float boost, float endTime)
        {
            _boost *= boost;
            _boostBonusEndTime = endTime;
        }
    }
}
