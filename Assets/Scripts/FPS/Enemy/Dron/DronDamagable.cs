using UnityEngine;




namespace FPS
{
    public class DronDamagable : Damagable
    {
        [SerializeField] private Renderer _lightRenderer;



        private float _currentPercentage;
        public override void HealthUpdated()
        {
            base.HealthUpdated();
            GetPercantageOfHealth();


        }
        public void GetPercantageOfHealth()
        {
            _currentPercentage = _currentHealth / _health;
            _lightRenderer.material.SetColor("_EmissionColor", new Color(_currentPercentage, 0, 0));
        }
    }
}

