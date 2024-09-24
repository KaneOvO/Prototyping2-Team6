using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestSkill : MonoBehaviour
{

	private Image imageFilled;
	public float coldTime;   
	private float timer = 0;    
	public bool isCold = false;
	public PlantingTool plantingTool;
	public WateringTool wateringTool;

	void Start()
	{
		imageFilled = gameObject.GetComponent<Image>();
		imageFilled.fillAmount = 0;
		if (plantingTool != null)
		{
			coldTime = plantingTool.plantingCooldown;
		}
		else if (wateringTool != null)
		{
			coldTime = wateringTool.wateringCooldown;
		}

	}

	void Update()
	{
		if (isCold == true)
		{
			timer += Time.deltaTime;
			if (timer > coldTime)
			{
				isCold = false;
				timer = 0;
				imageFilled.fillAmount = 0;
			}
			else
			{
				imageFilled.fillAmount = (coldTime - timer) / coldTime;
			}
		}
	}
}
