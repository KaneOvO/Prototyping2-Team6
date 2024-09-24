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
            transform.up = toCenter;

            if (gameObject.CompareTag("Fire"))
            {
                transform.up = -toCenter.normalized;
            }

            /*Quaternion q = Quaternion.FromToRotation(transform.up, -toCenter);
            q = q * transform.rotation;
            transform.rotation = Quaternion.Slerp(transform.rotation, q, 1);*/
        }
    }
}
