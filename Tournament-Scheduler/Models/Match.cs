using System;
using System.Collections.Generic;
using System.Text;

namespace Tournament_Scheduler
{
	public class Match
	{
		public string Team1 { get; set; }
		public string Team2 { get; set; }
		public DateTime StartTime { get; set; }
		public string PoolName { get; set; }
		public int PitchNumber { get; set; }
	}
}
