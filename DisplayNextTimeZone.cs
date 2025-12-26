
using System;
using AngryLabs.Props.BallDrop;
using TMPro;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class DisplayNextTimeZone : UdonSharpBehaviour
{
    public TMP_Text TextObject;

    public void Tick(DateTime now)
    {
        int hours = Constants.HoursUntilNewYears(2026, now);
        if(Constants.GetUpcomingZone(hours, out string cities))
        {
            TextObject.text = cities;
        }
        else
        {
            TextObject.text = "Soon";
            Debug.Log($"Long time until new years. {hours} hours");
        }
    }
}
