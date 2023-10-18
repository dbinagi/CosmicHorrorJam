using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TurtleGames.Framework.Runtime.Core;
using TurtleGames.Framework.Runtime.UI;
using UnityEngine.UI;

public class TimeManager : Singleton<TimeManager>
{

    int currentHour;
    int currentDay;

    float lastHour;

    void Start()
    {
        UIManager.Instance.SetText("TxtDays", "Days: " + currentDay);
    }

    void Update()
    {
        if (Time.time - lastHour >= GameManager.Instance.balance.hourDuration)
        {
            HourPassed();
        }
    }

    void HourPassed()
    {
        currentHour += 1;
        lastHour = Time.time;
        if (currentHour > 23)
        {
            currentHour = 0;
            DayPassed();
        }
        GameObject slider = UIManager.Instance.FindInCanvas("SliderHour");
        slider.GetComponent<Slider>().value = currentHour;
    }

    void DayPassed()
    {
        currentDay++;
        UIManager.Instance.SetText("TxtDays", "Days: " + currentDay);

        if (currentDay == GameManager.Instance.balance.daysForLevel1)
        {
            GameManager.Instance.pet.SetLevel(1);
        }
        else if (currentDay == GameManager.Instance.balance.daysForLevel2)
        {
            GameManager.Instance.pet.SetLevel(2);
        }
        else if (currentDay == GameManager.Instance.balance.daysForEnd)
        {
            GameManager.Instance.pet.SetLevel(3);
        }
    }
}
