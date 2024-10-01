using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Roger
{
    public class Tree : MonoBehaviour
    {
        public bool isOnFire;
        public GameObject firePrefab;
        public GameObject firePlaceHolder;

        private float _fireTimer;
        public float treeHp = 15f;
        public float treeHpMax = 20f;
        public bool _treeWatered;
        public float _extinguishTimeThreshold = 2f;
        public float _extinguishTime = 0f;

        public void Update()
        {
            TreeHpCalculation();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Water"))
            {
                if (isOnFire) GameManager.Instance.TreeStopBurning(GetComponent<Tree>());
            }
        }

        private void TreeHpCalculation()
        {
            if (isOnFire && !_treeWatered)
            {
                treeHp -= Time.deltaTime;
            }
            else if (treeHp < treeHpMax)
            {
                treeHp += Time.deltaTime;
            }

            if (treeHp <= 0)
            {
                TreeBurnedDown();
            }

            if(_treeWatered && isOnFire)
            {
                _extinguishTime += Time.deltaTime;
                if (_extinguishTime >= _extinguishTimeThreshold)
                {
                    GameManager.Instance.TreeStopBurning(GetComponent<Tree>());
                    //TreeStopBurning();
                }
            }
        }

        public void TreeStartBurning()
        {
            isOnFire = true;
            
            var fire = Instantiate(firePrefab, transform.position, transform.rotation);
            firePlaceHolder = fire;
            fire.transform.SetParent(transform);
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
