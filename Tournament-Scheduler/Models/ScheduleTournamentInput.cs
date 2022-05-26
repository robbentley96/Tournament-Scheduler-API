using System;
using System.Collections.Generic;
using System.Text;

namespace Tournament_Scheduler
{
	public class ScheduleTournamentInput
	{
		public List<string> TeamNames { get; set; }
		public int GameLength { get; set; }
		public int NumberPitches { get; set; }
		public DateTime StartDate { get; set; }
	}
}
