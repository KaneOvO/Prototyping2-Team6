using UnityEngine;

namespace Roger
{
    public class Tree : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Fire"))
            {
                Debug.Log(other.name);
                
                TreeBurnDown();
            }
        }

        private void TreeBurnDown()
        {
            Destroy(gameObject);
        }

        private void TreeGrow()
        {
            
        }
    }
}
