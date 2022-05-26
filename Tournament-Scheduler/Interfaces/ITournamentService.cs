using System;
using System.Collections.Generic;
using System.Text;

namespace Tournament_Scheduler
{
	public interface ITournamentService
	{
		public List<Match> ScheduleTournament(ScheduleTournamentInput input);
	}
}
