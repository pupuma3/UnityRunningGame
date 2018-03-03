using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeightSlider : MonoBehaviour
{
    public GameObject Indicator;
    public GameObject WeightSider;
	// Use this for initialization
	void Start ()
    {
        float maxWeight = GameManager.Instance.GetPlayer().GetMaxWeight();
        float goalWeight = GameManager.Instance.GetPlayer().GetGoalWeight();
        float rate = goalWeight / maxWeight;

        float sliderWidth = 500.0f;
        float posX = sliderWidth * rate - (sliderWidth/2.0f);
        Indicator.transform.localPosition = new Vector3(posX, 0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update ()
    {
        float maxWeight = GameManager.Instance.GetPlayer().GetMaxWeight();
        float currentWeight = GameManager.Instance.GetPlayer().GetCurrentWeight();
        float rate = currentWeight / maxWeight;

        gameObject.GetComponent<Slider>().value = rate;

        //
        {
            if(50 > rate )
            {
                WeightSider.GetComponent<Image>().color = new Color(0, 10, 0);

            }
            else
            {
                WeightSider.GetComponent<Image>().color = new Color(0, 255, 0);
            }
        }
    }
}
