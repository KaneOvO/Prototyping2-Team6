using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Roger
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        
        public List<Tree> trees;
        public List<Tree> burningTrees;
        private float _fireSpawnTimer = 8f;
        private float _fireSpawnRateMax = 3f;

        private void Start()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }
            
            var treesInScene = GameObject.FindGameObjectsWithTag("Tree");
            foreach (var tree in treesInScene)
            {
                TreePlanted(tree.GetComponent<Tree>());
            }
            
            StartCoroutine(FireSpawn());
        }
        
        public void TreePlanted(Tree tree)
        {
            trees.Add(tree);
        }

        public void TreeStartBurning(Tree tree)
        {
            burningTrees.Add(tree);
            trees.Remove(tree);
            
            tree.TreeStartBurning();
        }

        public void TreeStopBurning(Tree tree)
        {
            trees.Add(tree);
            burningTrees.Remove(tree);
            
            tree.TreeStopBurning();
        }

        public void TreeBurnedDown(Tree tree)
        {
            burningTrees.Remove(tree);
        }

        private IEnumerator FireSpawn()
        {
            while (true)
            {
                yield return new WaitForSeconds(_fireSpawnTimer);
                
                if (trees.Count > 0)
                {
                    var randomIndex = Random.Range(0, trees.Count);
                    
                    TreeStartBurning(trees[randomIndex]);

                    if (_fireSpawnTimer > _fireSpawnRateMax)
                    {
                        _fireSpawnTimer -= 1;
                    }
                }
            }
        }
        
    }
}
