using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tournament_Scheduler
{
	public class TournamentService : ITournamentService
	{
		public List<Match> ScheduleTournament(ScheduleTournamentInput input)
		{
			if (!ValidateTeamList(input.TeamNames)) { return null; }
			int numberPools = CalculateNumberPools(input.TeamNames);
			List<List<string>> pools = DistributeTeamsToPools(input.TeamNames, numberPools);
			List<Match> matchList = new List<Match>();
			for (int i = 0; i < pools.Count; i++)
			{
				List<Match> poolMatches = OrganisePoolMatches(pools[i],$"Pool {i+1}");
				matchList.AddRange(poolMatches);
			}
			matchList = SchedulePoolMatches(matchList, input.TeamNames, input.StartDate, input.GameLength, 10, input.NumberPitches, numberPools);
			return matchList;
		}
		private bool ValidateTeamList(List<string> teamList)
		{
			return teamList.Count == 16 || teamList.Count == 18 || teamList.Count == 20;
		}
		private int CalculateNumberPools(List<string> teamList)
		{
			if (teamList.Count % 3 == 0)
			{
				return 3;
			}
			else if (teamList.Count % 4 == 0)
			{
				return 4;
			}
			else return -1;
		}
		private List<List<string>> DistributeTeamsToPools(List<string> teamList, int numberPools)
		{
			List<List<string>> pools = new List<List<string>>();
			for (int i = 0; i < numberPools; i++)
			{
				pools.Add(new List<string>());
			}
			for (int i = 0; i < teamList.Count; i++)
			{
				pools[i % numberPools].Add(teamList[i]);
			}
			return pools;
		}

		private List<Match> OrganisePoolMatches(List<string> pool, string poolName)
		{
			List<Match> matches = new List<Match>();
			for (int i = 0; i < pool.Count - 1; i++)
			{
				for (int j = i + 1; j < pool.Count; j++)
				{
					Match match = new Match() { Team1 = pool[i], Team2 = pool[j],PoolName = poolName };
					matches.Add(match);
				}
			}
			return matches;
		}
		private List<Match> SchedulePoolMatches(List<Match> matchList, List<string> teamList, DateTime startDate, int minsPerGame,int delayBetweenGames, int numberPitches, int numberPools)
		{
			DateTime currentMatchStartTime = startDate + new TimeSpan(9, 0, 0);
			while (matchList.Select(x => x).Where(x => x.PitchNumber <= 0 || x.StartTime == null).ToList().Count > 0)
			{
				List<Match> matchesToSchedule = new List<Match>();
				for (int i = 0; i < numberPitches && matchList.Select(x => x).Where((x => (x.PitchNumber <= 0 || x.StartTime == null) && !matchesToSchedule.Select(y => y.Team1).Contains(x.Team1) && !matchesToSchedule.Select(y => y.Team1).Contains(x.Team2) && !matchesToSchedule.Select(y => y.Team2).Contains(x.Team1) && !matchesToSchedule.Select(y => y.Team2).Contains(x.Team2))).ToList().Count > 0; i++)
				{
					Match matchToSchedule = matchList.Select(x => x).Where((x => (x.PitchNumber <= 0 || x.StartTime == null) && !matchesToSchedule.Select(y => y.Team1).Contains(x.Team1) && !matchesToSchedule.Select(y => y.Team1).Contains(x.Team2) && !matchesToSchedule.Select(y => y.Team2).Contains(x.Team1) && !matchesToSchedule.Select(y => y.Team2).Contains(x.Team2))).FirstOrDefault();
					if (matchToSchedule != null) { matchToSchedule.PitchNumber = i + 1; }
					if (matchToSchedule != null) { matchToSchedule.StartTime = currentMatchStartTime; }
					matchesToSchedule?.Add(matchToSchedule);
				}
				currentMatchStartTime += new TimeSpan(0, minsPerGame + delayBetweenGames, 0);
			}
			return matchList;
		}
	}
}
