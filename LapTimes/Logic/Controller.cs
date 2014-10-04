using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;

using LapTimes.Builder;
using LapTimes.Model;

namespace LapTimes.Logic
{
    class Controller : IController
    {
        private const int avgLapTime = 21; //minutes
        private IRace race;

        public Controller()
        {
            timer.Interval = 100;
            timer.Elapsed += TimerElapsed;
            timer.Start();

            // Temporary for testing: instantiate the race and create some laps
            int lap = 0;
            DateTime now = DateTime.Now;
            race = IOC.Get<IRace>();
            race.raceName = "TestRace";
            race.laps.Add(new Lap(race, 1, new TeamMember("Piet"), now + new TimeSpan(00, (avgLapTime * lap++), 00), true, false));
            race.laps.Add(new Lap(race, 2, new TeamMember("Jaap"), now + new TimeSpan(00, (avgLapTime * lap++), 07), false, false));
            race.laps.Add(new Lap(race, 3, new TeamMember("Kees"), now + new TimeSpan(00, (avgLapTime * lap++), 07), false, false));
            race.laps.Add(new Lap(race, 4, new TeamMember("Hans"), now + new TimeSpan(00, (avgLapTime * lap++), 07), false, false));
            race.laps.Add(new Lap(race, 5, new TeamMember("Piet"), now + new TimeSpan(00, (avgLapTime * lap++), 07), false, false));
            race.laps.Add(new Lap(race, 6, new TeamMember("Jaap"), now + new TimeSpan(00, (avgLapTime * lap++), 07), false, false));
            race.laps.Add(new Lap(race, 7, new TeamMember("Kees"), now + new TimeSpan(00, (avgLapTime * lap++), 07), false, false));
            race.laps.Add(new Lap(race, 8, new TeamMember("Hans"), now + new TimeSpan(00, (avgLapTime * lap++), 07), false, false));
            race.laps.Add(new Lap(race, 9, new TeamMember("Piet"), now + new TimeSpan(00, (avgLapTime * lap++), 00), false, false));
            race.laps.Add(new Lap(race, 10, new TeamMember("Jaap"), now + new TimeSpan(00, (avgLapTime * lap++), 07), false, false));
            race.laps.Add(new Lap(race, 11, new TeamMember("Kees"), now + new TimeSpan(00, (avgLapTime * lap++), 07), false, false));
            race.laps.Add(new Lap(race, 12, new TeamMember("Hans"), now + new TimeSpan(00, (avgLapTime * lap++), 07), false, false));
            race.laps.Add(new Lap(race, 13, new TeamMember("Piet"), now + new TimeSpan(00, (avgLapTime * lap++), 07), false, false));
            race.laps.Add(new Lap(race, 14, new TeamMember("Jaap"), now + new TimeSpan(00, (avgLapTime * lap++), 07), false, false));
            race.laps.Add(new Lap(race, 15, new TeamMember("Kees"), now + new TimeSpan(00, (avgLapTime * lap++), 07), false, false));
            race.laps.Add(new Lap(race, 16, new TeamMember("Hans"), now + new TimeSpan(00, (avgLapTime * lap++), 07), false, false));
        }

        public void HandOver()
        {
            int count = 1;
            bool startnext = false;
            foreach (ILap lap in race.laps)
            {
                if (lap.started)
                {
                    if (!lap.finished)
                    {
                        FinishLap(lap);
                        startnext = true;            
                    }
                }
                // lap.started == false  
                else if (startnext) 
                {
                    StartLap(lap);
                    startnext = false;
                }                
                else
                {
                    // Update expected start times
                    DateTime now = DateTime.Now;
                    lap.startTime = now + new TimeSpan(00, (avgLapTime * count++), 00);
                }
            }
        }

        private void FinishLap(ILap lap)
        {
            lap.finished = true;
            lap.finishTime = DateTime.Now;
            lap.lapTime = lap.finishTime - lap.startTime;
        }

        private static void StartLap(ILap lap)
        {
            lap.startTime = DateTime.Now;
            lap.started = true;
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            foreach (ILap lap in race.laps)
            {
                if (lap.started && !lap.finished)
                {
                    lap.lapTime = DateTime.Now - lap.startTime;
                }
            }
        }

        private Timer timer = new Timer();
    }
}
