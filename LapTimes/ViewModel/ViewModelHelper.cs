using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LapTimes.ViewModel
{
    class ViewModelHelper
    {
        public delegate void ShowMessage(string message);

        public static string ToString(TimeSpan timespan)
        {
            // Can't use TimeSpan.ToString() because it can't display 24:00:00
            return string.Format("{0:d2}:{1:d2}:{2:d2}",
                timespan.Days * 24 + timespan.Hours,
                timespan.Minutes,
                timespan.Seconds);
        }

        public static bool ParseTime(string timeString, out TimeSpan parsedTime)
        {
            // Can't use TimeSpan.TryParse() because it doesn't support "24:00:00"

            bool success = true;
            parsedTime = new TimeSpan();

            try
            {
                string[] parts = timeString.Split(':');
                int hours = 0;
                int minutes = 0;
                int seconds = 0;

                if (parts.Count() >= 2)
                {
                    success &= int.TryParse(parts[0], out hours);
                    success &= int.TryParse(parts[1], out minutes);
                    if (parts.Count() >= 3)
                    {
                        success &= int.TryParse(parts[2], out seconds);
                    }
                }
                else
                {
                    success = false;
                }

                if (success)
                {
                    parsedTime = new TimeSpan(hours, minutes, seconds);
                }
            }
            catch (SystemException )
            {
                success = false;
            }

            return success;
        }
    }
}
