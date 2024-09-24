using UnityEngine;

namespace Roger
{
    public class Fire : MonoBehaviour
    {
        public ParticleSystem fireParticles;

        public void Awake()
        {
            fireParticles = gameObject.GetComponent<ParticleSystem>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Water"))
            {
                ExtinguishFire();
            }
        }

        private void ExtinguishFire()
        {
            if (fireParticles != null)
            {
                fireParticles.Stop();
                
                Destroy(gameObject, 1f);
            }
        }
        
    }
}
