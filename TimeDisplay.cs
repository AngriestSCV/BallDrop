
using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls;
using TMPro;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace AngryLabs.Props.BallDrop
{
    public enum TimeDisplayState
    {
        Time,
        Happy,
    }

    public class TimeDisplay : UdonSharpBehaviour
    {
        public TimeDisplayState DisplayState = TimeDisplayState.Time;

        public TMP_Text TextObject;

        public int add_seconds;
        public int add_days;

        public BallDropLogic ballDropLogic;
        //public TimeZoneDisplay timeZoneDisplay;
        public DisplayNextTimeZone displayNextTimeZone;

        public void Start()
        {
            SendCustomEventDelayedSeconds(nameof(Tick), 1.0f);
        }

        public void Tick()
        {
            DateTime dt = Networking.GetNetworkDateTime();
            dt = dt.AddSeconds(add_seconds).AddDays(add_days);

            DateTime nextHour = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, 0, 0, DateTimeKind.Utc)
                .AddHours(1);

            TimeSpan tilNext = nextHour - dt;

            int minutes = tilNext.Minutes;
            int seconds = tilNext.Seconds;

            if(minutes == 0 && seconds == 20 && ballDropLogic != null)
            {
                ballDropLogic.Trigger();
            }

            if(seconds == 0 && minutes != 0 && displayNextTimeZone != null)
            {
                displayNextTimeZone.Tick(dt);
            }

            if(TextObject == null)
            {
                Debug.LogError($"{nameof(TimeDisplay)}::Tick - TextObject was null");
                return;
            }

            DisplayState = (minutes == 59 || (minutes == 0 && seconds == 0) ) ? TimeDisplayState.Happy : TimeDisplayState.Time;

            if(DisplayState == TimeDisplayState.Happy)
            {
                TextObject.text = "Happy\nNewYear!";
            }
            else
            {
                string formated = minutes == 0 ? $"{seconds}": $"{minutes:00}:{seconds:00}";
                TextObject.text = formated;
            }
            SendCustomEventDelayedSeconds(nameof(Tick), 1.0f);
        }

    }

}