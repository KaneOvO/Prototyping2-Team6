using System.Collections;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;

namespace Roger
{
    public class Tree : MonoBehaviour
    {
        public bool isOnFire;
        public GameObject firePrefab;
        public GameObject firePlaceHolder;

        private float _fireTimer;
        private float _treeHp;
        private float _treeHpMax = 10f;

        public void Update()
        {
            
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

        private void TreeHpCalculation()
        {
            if (isOnFire)
            {
                _treeHp -= Time.deltaTime;
            }
            else if (_treeHp < _treeHpMax)
            {
                _treeHp += Time.deltaTime;
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

        public void TreeBurnedDown()
        {
            GameManager.Instance.TreeBurnedDown(GetComponent<Tree>());
            
            Destroy(firePlaceHolder);
            Destroy(gameObject);
        }

        /*private IEnumerator TreeGrow()
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
            
        }*/
        
    }
}
