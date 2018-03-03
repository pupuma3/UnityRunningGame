﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HpSlider : MonoBehaviour
{


	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        // 캐릭터의 체력 정보를 가져와서, UI에 반영해주면 된다.
        float maxHP = GameManager.Instance.GetPlayer().GetMaxHP();
        float currentHP = GameManager.Instance.GetPlayer().GetCurrentHp();
        float rate = currentHP / maxHP;

        gameObject.GetComponent<Slider>().value = rate;
	}


}
