
using System;
using System.Collections;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace AngryLabs.Props.BallDrop {
    public class Constants : UdonSharpBehaviour
    {
        public void Start()
        {
            Test();
        }

        public static void Test()
        {
            var res = HoursUntilNewYears(2026, new DateTime(2025, 12, 31, 23, 30, 0, DateTimeKind.Utc));
            if(res != 0)
            {
                Debug.LogError($"Hours until new years should have returned 0 at half till, returned: {res}");
            }

            res = HoursUntilNewYears(2026, new DateTime(2026, 1, 1, 0, 30, 0, DateTimeKind.Utc));
            if(res != -1)
            {
                Debug.LogError($"Hours until new years should have returned -1 at 1:30, returned: {res}");
            }

            res = HoursUntilNewYears(2026, new DateTime(2026, 1, 1, 1, 1, 0, DateTimeKind.Utc));
            if(res != -2)
            {
                Debug.LogError($"Hours until new years should have returned -1 at 1:01, returned: {res}");
            }

            DateTime when = new DateTime(2025, 12, 31, 23, 1, 0, DateTimeKind.Utc);
            res = HoursUntilNewYears(2026, when);
            if(res != 0)
            {
                Debug.LogError($"Hours until new years should have returned 0 at {when}, returned: {res}");
            }

            when = new DateTime(2025, 12, 31, 22, 59, 0, DateTimeKind.Utc);
            res = HoursUntilNewYears(2026, when);
            if(res != 1)
            {
                Debug.LogError($"Hours until new years should have returned 1 at {when}, returned: {res}");
            }
    
        }

        public static int HoursUntilNewYears(int year, DateTime toTest)
        {
            var startOfYear = new DateTime(year, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var timeDiff = startOfYear - toTest;
            int hours = (int) Math.Floor(timeDiff.TotalMinutes / 60.0);
            // Debug.Log($"Time diff: {startOfYear} - {toTest} = {timeDiff}");
            // Debug.Log($"hours = {hours} = {timeDiff.TotalMinutes} / 60.0");
            return hours;
        }

        public static bool GetUpcomingZone(int offset, out string str)
        {
            switch (offset)
            {
                case -12: str = "Baker Islands"; return true;
                case -11: str = "Niue"; return true;
                case -10: str = "Honolulu"; return true;
                case -9: str = "Gambier Islands"; return true;
                case -8: str = "Los Angeles"; return true;
                case -7: str = "Denver, Calgary, and Ciudad Juárez"; return true;
                case -6: str = "Mexico City, Chicago, San Salvador"; return true;
                case -5: str = "New York, Toronto, Lima"; return true;
                case -4: str = "Santiago, San Juan, Santo Domingo"; return true;
                case -3: str = "São Paulo, Buenos Aires, Montevideo"; return true;
                case -2: str = "Fernando de Noronha, Greenland"; return true;
                case -1: str = "Cape Verde"; return true;
                case 0: str = "London, Dublin, Lisbon"; return true;
                case 1: str = "Berlin, Rome, Stockholm, Paris"; return true;
                case 2: str = "Athens, Bucharest, Cairo, Helsinki"; return true;
                case 3: str = "Moscow, Istanbul, Bagdad, Kuwait City"; return true;
                case 4: str = "Dubai, Baku, Samara"; return true;
                case 5: str = "Karachi, Astana, Tashkent"; return true;
                case 6: str = "Dhaka, Omsk, Bishkek"; return true;
                case 7: str = "Jakarta, Ho Chi Minh City"; return true;
                case 8: str = "Shanghai, Taipei, Hong Kong"; return true;
                case 9: str = "Tokyo, Seoul, Pyongyang"; return true;
                case 10: str = "Sydney, Melbourne, Port Moresby"; return true;
                case 11: str = "Nouméa"; return true;
                case 12: str = "Auckland, Suva"; return true;
                default: str = $"Unknown value of {offset}"; return false;
            }
        }
    }

}
