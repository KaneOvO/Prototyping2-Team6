using UnityEngine;

namespace Roger
{
    public class PlanetAlign : MonoBehaviour
    {
        public Transform planet;
        
        private void Awake()
        {
            planet = GameObject.FindGameObjectWithTag("Planet").transform;
            
            Vector3 toCenter = planet.position - transform.position;
            toCenter.Normalize();

            Quaternion q = Quaternion.FromToRotation(transform.up, -toCenter);
            q = q * transform.rotation;
            transform.rotation = Quaternion.Slerp(transform.rotation, q, 1);
        }
    }
}
