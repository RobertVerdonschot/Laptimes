using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LapTimes.Builder;
using LapTimes.Model;

namespace LapTimes.Logic
{
    class Controller
    {
        private IRace race;

        public Controller()
        {
            // Temporary for testing: instantiate the race and create some laps
            race = IOC.Get<IRace>();
            race.raceName = "TestRace";
            race.laps.Add(new Lap(race, 1, new TeamMember("Piet"), new DateTime(2014, 8, 24, 13, 00, 0), true, true));
            race.laps.Add(new Lap(race, 2, new TeamMember("Jaap"), new DateTime(2014, 8, 24, 13, 21, 0), true, true));
            race.laps.Add(new Lap(race, 3, new TeamMember("Kees"), new DateTime(2014, 8, 24, 13, 42, 0), true, true));
            race.laps.Add(new Lap(race, 4, new TeamMember("Hans"), new DateTime(2014, 8, 24, 14, 03, 0), true, true));
            race.laps.Add(new Lap(race, 5, new TeamMember("Piet"), new DateTime(2014, 8, 24, 14, 24, 0), true, false));
            race.laps.Add(new Lap(race, 6, new TeamMember("Jaap"), new DateTime(2014, 8, 24, 14, 45, 0), false, false));
            race.laps.Add(new Lap(race, 7, new TeamMember("Kees"), new DateTime(2014, 8, 24, 15, 06, 0), false, false));
            race.laps.Add(new Lap(race, 8, new TeamMember("Hans"), new DateTime(2014, 8, 24, 15, 27, 0), false, false));
        }

    }
}
