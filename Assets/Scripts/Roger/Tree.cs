using System.Collections;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;

namespace Roger
{
    public class Tree : MonoBehaviour
    {
        //public float growingTimer;
        private float _growingDuration = 45;
        private float _growingRate = 0.1f;
        
        public bool isOnFire;
        public GameObject firePrefab;
        public GameObject firePlaceHolder;

        public void Start()
        {
            StartCoroutine(TreeGrow());
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Fire"))
            {
                TreeStartBurning();
            }
            else if (other.CompareTag("Water"))
            {
                TreeStopBurning();
            }
        }

        public void TreeStartBurning()
        {
            isOnFire = true;
            
            var fire = Instantiate(firePrefab, transform.position, Quaternion.identity);
            firePlaceHolder = fire;
        }

        public void TreeStopBurning()
        {
            isOnFire = false;
            
            Destroy(firePlaceHolder);
            firePlaceHolder = null;
        }

        private IEnumerator TreeGrow()
        {
            var elapsedTime = 0f;
            
            while (elapsedTime < _growingDuration)
            {
                if (!isOnFire)
                {
                    elapsedTime += Time.deltaTime;
                    
                    var scale = transform.localScale;
                    scale.y += (_growingRate * Time.deltaTime);
                    transform.localScale = scale;
                }
                yield return null;
            }
            
        }
        
    }
}
