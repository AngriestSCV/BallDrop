
using System;
using TMPro;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;

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

        [UdonSynced]
        public int add_seconds;

        [UdonSynced]
        public int add_days;

        public BallDropLogic ballDropLogic;
        //public TimeZoneDisplay timeZoneDisplay;
        public DisplayNextTimeZone displayNextTimeZone;
        private int _lastState;


        public void Start()
        {
            DateTime dt = Networking.GetNetworkDateTime();
            dt = dt.AddSeconds(add_seconds).AddDays(add_days);

            SendCustomEventDelayedSeconds(nameof(Tick), 1.0f);
            displayNextTimeZone.Tick(dt);

            _lastState = GetStateHash();
        }

        private int GetStateHash()
        {
            return add_seconds.GetHashCode() ^ add_days.GetHashCode();
        }

        public void Tick()
        {
            DateTime dt = Networking.GetNetworkDateTime();
            dt = dt.AddSeconds(add_seconds).AddDays(add_days);
            int newHash = GetStateHash();

            DateTime nextHour = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, 0, 0, DateTimeKind.Utc)
                .AddHours(1);

            TimeSpan tilNext = nextHour - dt;

            int minutes = tilNext.Minutes;
            int seconds = tilNext.Seconds;

            if(minutes == 0 && seconds == 20 && ballDropLogic != null)
            {
                ballDropLogic.Trigger();
            }

            bool timeReady = seconds == 0 && minutes != 0 && displayNextTimeZone != null;
            if(timeReady || _lastState != newHash)
            {
                _lastState = newHash;
                displayNextTimeZone.Tick(dt);
                RequestSerialization(); 
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