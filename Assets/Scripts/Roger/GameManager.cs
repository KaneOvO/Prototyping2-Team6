using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Unity.VisualScripting;
using UnityEngine;

namespace Roger
{
    public class GameManager : MonoBehaviour
    {
        public List<Tree> trees;
        public List<Tree> burningTrees;
        private float _fireSpawnTimer = 4f;
        private float _fireSpawnRateMax = 1f;

        private void Start()
        {
            var treesInScene = GameObject.FindGameObjectsWithTag("Tree");
            foreach (var tree in treesInScene)
            {
                TreePlanted(tree);
            }
            
            StartCoroutine(FireSpawn());
        }
        
        public void TreePlanted(GameObject tree)
        {
            trees.Add(tree.GetComponent<Tree>());
        }

        private IEnumerator FireSpawn()
        {
            while (true)
            {
                yield return new WaitForSeconds(_fireSpawnTimer);
                
                if (trees.Count > 0)
                {
                    var randomIndex = Random.Range(0, trees.Count);

                    trees[randomIndex].TreeStartBurning();
                    
                    burningTrees.Add(trees[randomIndex]);
                    trees.RemoveAt(randomIndex);

                    if (_fireSpawnTimer > _fireSpawnRateMax)
                    {
                        _fireSpawnTimer -= 1;
                    }
                }
            }
        }
        
    }
}
