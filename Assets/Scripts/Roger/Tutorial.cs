using UnityEngine;
using UnityEngine.UI;

namespace Roger
{
    public class Tutorial : MonoBehaviour
    {
        public enum TutorialState
        {
            SwitchWateringTool,
            ExtinguishFire,
            SwitchPlantingTool,
            PlantTree,
            Complete
        }

        public TutorialState curState;
        public Tree tutorialTree;
        public GameObject msgBoard;
        public TMPro.TMP_Text msgBoardText;

        private void Awake()
        {
            curState = TutorialState.SwitchWateringTool;
            UpdateText();

            tutorialTree.treeHpMax = 3600;
            tutorialTree.treeHp = 3600;
        }

        private void Update()
        {
            switch (curState)
            {
                case TutorialState.SwitchWateringTool:
                    if (Input.GetKeyDown(KeyCode.Alpha2))
                    {
                        UpdateState(TutorialState.ExtinguishFire);
                    }
                    break;
                case TutorialState.ExtinguishFire:
                    if (!tutorialTree.isOnFire)
                    {
                        UpdateState(TutorialState.SwitchPlantingTool);
                    }
                    break;
                case TutorialState.SwitchPlantingTool:
                    if (Input.GetKeyDown(KeyCode.Alpha1))
                    {
                        UpdateState(TutorialState.PlantTree);
                    }
                    break;
                case TutorialState.PlantTree:
                    if (GameManager.Instance.treePlantedFlag)
                    {
                        UpdateState(TutorialState.Complete);
                    }
                    break;
                case TutorialState.Complete:
                    UIFade();
                    tutorialTree.treeHpMax = 20;
                    tutorialTree.treeHp = 20;
                    break;
            }
        }

        public void GameStart()
        {
            GameManager.Instance.TreeStartBurning(GameManager.Instance.trees[0]);
        }

        private void UpdateState(TutorialState state)
        {
            curState = state;
            UpdateText();
        }

        private void UpdateText()
        {
            switch (curState)
            {
                case TutorialState.SwitchWateringTool:
                    msgBoardText.text = "Press 2 Switch to Watering Tools";
                    break;
                case TutorialState.ExtinguishFire:
                    msgBoardText.text = "Left Click to Extinguish Fire";
                    break;
                case TutorialState.SwitchPlantingTool:
                    msgBoardText.text = "Press 1 Switch to Planting Tools";
                    break;
                case TutorialState.PlantTree:
                    msgBoardText.text = "Hold ALT and Left Click to Plant Tree";
                    break;
                case TutorialState.Complete:
                    msgBoardText.text = "Tutorial Completed";
                    break;
            }
        }

        private void UIFade()
        {
            if (msgBoard.GetComponent<Image>().color.a > 0)
            {
                msgBoard.GetComponent<Image>().color = new Color(1, 1, 1,
                    msgBoard.GetComponent<Image>().color.a - Time.deltaTime * 0.5f);
                msgBoardText.color = new Color(1, 1, 1, 
                    msgBoardText.color.a - Time.deltaTime * 0.5f);
            }
        }
    }
}
