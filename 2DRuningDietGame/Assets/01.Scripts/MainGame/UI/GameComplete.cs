using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameComplete : MonoBehaviour
{
    public GameObject GameCompleteText;
    public Text SuccessFailText;

    float _goalWeight ;
    float _currentWeight;
    // Use this for initialization
    void Start()
    {
        GameCompleteText.SetActive(false);
        SuccessFailText.gameObject.SetActive(false);

        _goalWeight = GameManager.Instance.GetPlayer().GetGoalWeight();
        _currentWeight = GameManager.Instance.GetPlayer().GetCurrentWeight();
    }


    float _currentTime = 0;
    // Update is called once per frame
    void Update()
    {

        //
        {
            _goalWeight = GameManager.Instance.GetPlayer().GetGoalWeight();
            _currentWeight = GameManager.Instance.GetPlayer().GetCurrentWeight();

            if (_goalWeight - 5.0f <= _currentWeight &&
                    _currentWeight <= _goalWeight + 5.0f)
            {

                _currentTime+= Time.deltaTime;
                if(3.0f < _currentTime )
                {
                    _currentTime = 0;
                    GameManager.Instance.GetPlayer().IsFever(true);
                }
            }
        }
        


        if (GameManager.Instance.GetPlayer().IsComplete())
        {
            GameCompleteText.SetActive(true);

            //
            _goalWeight = GameManager.Instance.GetPlayer().GetGoalWeight();
             _currentWeight = GameManager.Instance.GetPlayer().GetCurrentWeight();

            if (_goalWeight - 5.0f <= _currentWeight &&
                _currentWeight <= _goalWeight + 5.0f)
            {
                SuccessFailText.text = "SUCCESS";
                SuccessFailText.color = Color.yellow;

            }
            else
            {
                SuccessFailText.text = "FALE";
                SuccessFailText.color = Color.red;
            }
            SuccessFailText.gameObject.SetActive(true);

        }
    }
}
