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
        private IRace race;

        public Controller()
        {
            // Temporary for testing: instantiate the race and create some laps
            DateTime now = DateTime.Now;
            race = IOC.Get<IRace>();
            race.raceName = "TestRace";

            race.setup.lapDistance = 8; // km
            race.setup.lapsPerShift = 1; // more is not yet supported
            race.setup.startTime = DateTime.Now;
            race.setup.raceDuration = new TimeSpan(24, 00, 00);
            race.setup.team = new List<ITeamMember>();
            race.setup.team.Add(new TeamMember("Piet", new TimeSpan(00, 21, 00)));
            race.setup.team.Add(new TeamMember("Jaap", new TimeSpan(00, 21, 00)));
            race.setup.team.Add(new TeamMember("Kees", new TimeSpan(00, 21, 00)));
            race.setup.team.Add(new TeamMember("Hans", new TimeSpan(00, 21, 00)));
            race.setup.team.Add(new TeamMember("Bart", new TimeSpan(00, 21, 00)));
            race.setup.team.Add(new TeamMember("Fred", new TimeSpan(00, 21, 00)));

            GenerateLaps();
            race.laps.First().started = true;
            // End temporary

            timer.Interval = 100;
            timer.Elapsed += TimerElapsed;
            timer.Start();
        }

        public void GenerateLaps()
        {
            // Generate or regenerate laps based on settings in race Setup.
            // Leave already started lap untouched.

            // Find last started lap, clear the others
            ILap lastStartedLap = null;
            IList<ILap> lapsToRemove = new List<ILap>();  
            foreach (ILap lap in race.laps)
            {
                if (lap.started)
                {
                    lastStartedLap = lap;
                }
                else
                {
                    lapsToRemove.Add(lap);
                }
            }
            foreach(ILap lap in lapsToRemove)
            {
                race.laps.Remove(lap);
            }
            lapsToRemove.Clear();


            // (Re)generate laps to do
            DateTime raceEndTime = race.setup.startTime + race.setup.raceDuration;
            DateTime lastStartTime = (lastStartedLap == null) ? new DateTime(0) : lastStartedLap.startTime;
            uint lapNumber = (uint)race.laps.Count;
            ILap previousLap = lastStartedLap;
            bool lastStartedMemberFound = (lastStartedLap == null);
            while (lastStartTime < raceEndTime)
            {
                foreach (TeamMember member in race.setup.team)
                {
                    if (!lastStartedMemberFound)
                    {
                        if ((lastStartedLap == null)  || (member == lastStartedLap.teamMember))
                            lastStartedMemberFound = true;
                        // TODO support for multiple laps per shift
                    }
                    else
                    {
                        lapNumber++;
                        DateTime startTime = (previousLap == null) ? (race.setup.startTime) : (previousLap.startTime + previousLap.teamMember.expectedLapTime);
                        Lap newLap = new Lap(race, lapNumber, member, startTime, false, false);
                        race.laps.Add(newLap);
                        previousLap = newLap;
                        lastStartTime = newLap.startTime;
                    }
                }
            }

            // Calculate correct starttimes
            UpdateStartTimes();

            // Notify the world has changed
            race.OnRaceChanged();
        }

        public void HandOver()
        {
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
            }

            // Calculate new starttimes
            UpdateStartTimes();
        }

        // Update start times for laps to do, based on running average of each team member
        private void UpdateStartTimes()
        {
            // Calculate running averages for each member
            CalculateRunningAverageLaptimes();

            // Update start times in race
            ILap previousLap = null;
            foreach(ILap lap in race.laps)
            {
                if ((previousLap != null) && (lap.started == false))
                {
                    lap.startTime = previousLap.startTime + previousLap.teamMember.averageLapTime;
                }
                previousLap = lap;
            }
        }

        private void CalculateRunningAverageLaptimes()
        {
            // For each member create a list of laptimes
            Dictionary<ITeamMember, List<TimeSpan>> laptimesPerMember = new Dictionary<ITeamMember, List<TimeSpan>>();
            foreach (ILap lap in race.laps)
            {
                if (lap.started && lap.finished)
                {
                    if (!laptimesPerMember.ContainsKey(lap.teamMember))
                    {
                        laptimesPerMember.Add(lap.teamMember, new List<TimeSpan>());
                    }

                    laptimesPerMember[lap.teamMember].Add(lap.lapTime);
                }
            }

            // For each member calculate average laptime
            const long runningAvgLength = 3;
            foreach (ITeamMember member in race.setup.team)
            {
                List<TimeSpan> allLaps;
                if (laptimesPerMember.TryGetValue(member, out allLaps))
                {
                    // Pick last n laps for average calculation, where n is runningAvgLength.
                    TimeSpan sumOfLaps = new TimeSpan(0);
                    for (int idx = 0; idx < runningAvgLength; idx++)
                    {
                        int item = allLaps.Count - 1 - idx;
                        if (item >= 0)
                        {
                            sumOfLaps += allLaps[item];
                        }
                        else // less laps in list than runningAvgLength
                        {
                            sumOfLaps += member.expectedLapTime;
                        }
                    }

                    // Average
                    member.averageLapTime = new TimeSpan(sumOfLaps.Ticks / runningAvgLength);
                }
                else
                {
                    member.averageLapTime = member.expectedLapTime;
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
