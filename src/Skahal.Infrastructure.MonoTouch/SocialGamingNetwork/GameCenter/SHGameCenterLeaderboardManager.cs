#region Usings
using System;
using System.Collections.Generic;
using MonoTouch.GameKit;
using Skahal.MonoTouch.Common;
using Skahal.MonoTouch.Logging;
#endregion

namespace Skahal.MonoTouch.SocialGamingNetwork.GameCenter
{
	public class SHGameCenterLeaderboardManager : ISGNLeaderboardManager
	{
		#region Events
		public event EventHandler<LeaderboardUpdatingEventArgs> LeaderboardUpdating;
		public event EventHandler<LeaderboardUpdatingFailedEventArgs> LeaderboardUpdatingFailed;
		public event EventHandler<LeaderboardUpdatedEventArgs> LeaderboardUpdated;
		#endregion
		
		#region Fields	
		private static Dictionary<string, int> s_rankCache = new Dictionary<string, int>();
		#endregion
		
		#region Properties
		public bool Supported { get { return true; }}
		#endregion
		
		#region Methods
	
		#region UpdateLeaderboard
		
		public void UpdateLeaderboard(SGNLeaderboard leaderboard)
		{
			Log("UpdateLeaderboard", 
				"Updating leaderboard {0} with score {1}", 
				leaderboard.ID, 
				leaderboard.Score);
					
			var score = (int) leaderboard.Score;
			AddOrUpdateCache(leaderboard, SGN.PlayerManager.Player, score, -1);
			
			if (LeaderboardUpdating != null)
			{
				LeaderboardUpdating(this, new LeaderboardUpdatingEventArgs(leaderboard));
			}
			
			var gkScore = new GKScore(leaderboard.ID);
			gkScore.Value = score;
			gkScore.ReportScore((error) => 
			{
				if(error == null)
				{
					Log("UpdateLeaderboard", "Finished: {0}", leaderboard.ID);
			
					var player = SGN.PlayerManager.Player;
					var cacheKey = GetCacheKey(leaderboard, player);
					
					if (s_rankCache.ContainsKey(cacheKey))
					{
						s_rankCache.Remove(cacheKey);
					}
					
					GetPlayerRank(leaderboard, player, delegate(int rank) { });
		
					LeaderboardUpdated.Raise(this, new LeaderboardUpdatedEventArgs(leaderboard));
				}
				else
				{
					Log("UpdateLeaderboard", "Failed: {0}", error);			
					LeaderboardUpdatingFailed.Raise(this, new LeaderboardUpdatingFailedEventArgs(leaderboard));
				}
			});
		}
	
		#endregion
		
		#region GetPlayerScore
		
		public void GetPlayerScore(SGNLeaderboard leaderboard, SGNPlayer player, Action<int> scoreReceived)
		{
			if (player.ID.Equals(SGN.PlayerManager.Player.ID) && HasCache(leaderboard, player))
			{
				var value = GetScoreCached(leaderboard, player);
				Log("GetPlayerScore", "Scores {0} for player {1} found on the cache.", value, player.ID);
				scoreReceived(value);
			}
			else
			{
				RetrieveGameCenterScore(leaderboard, player, 
					delegate(GKScore score)
					{
						AddOrUpdateCache(leaderboard, player, (int) score.Value, score.Rank);
						scoreReceived((int)score.Value);
					}
				);
			}
		}
	
		#endregion
		
		#region GetPlayerRank
		
		public void GetPlayerRank(SGNLeaderboard leaderboard, SGNPlayer player, Action<int> rankReceived)
		{
			var cacheKey = GetCacheKey(leaderboard, player);
			
			if (s_rankCache.ContainsKey(cacheKey))
			{
				rankReceived(s_rankCache[cacheKey]);
			}
			else
			{
				RetrieveGameCenterScore(leaderboard, player, 
				delegate(GKScore score)
				{ 
					AddOrUpdateCache(leaderboard, player, -1, score.Rank); 
					rankReceived(score.Rank);
				});
			}
		}
		#endregion
		
		#endregion	
		
		#region Private methods
		
		private static string GetCacheKey(SGNLeaderboard leaderboard, SGNPlayer player)
		{
			return String.Format("{0}_{1}", leaderboard.ID, player.ID);	
		}
		
		private static bool HasCache(SGNLeaderboard leaderboard, SGNPlayer player)
		{
			return SHAppSettings.HasKey(GetCacheKey(leaderboard, player));
		}
		
		private static int GetScoreCached(SGNLeaderboard leaderboard, SGNPlayer player)
		{
			return SHAppSettings.GetInt(GetCacheKey(leaderboard, player), 0);
		}
		
		private static int GetRankCached(SGNLeaderboard leaderboard, SGNPlayer player)
		{
			try
			{
				return s_rankCache[GetCacheKey(leaderboard, player)];
			}
			catch
			{
				SHLog.Debug("Rank for player '{0}' was not found on cache.", player.ID);
				return 0;
			}
		}
		
		private static void AddOrUpdateCache(SGNLeaderboard leaderboard, SGNPlayer player, int score, int rank)
		{
			var key = GetCacheKey(leaderboard, player);
			
			if (score > -1)
			{
				SHAppSettings.SetInt(key, score);
			}
			
			if (rank > -1)
			{
				s_rankCache[key] = rank;
			}
		}
		private static void Log(string methodName, string message, params object[] args)
		{
			SHGameCenterHelper.Log("SHGameCenterLeaderboardManager", methodName, message, args);
		}
		
		private void RetrieveGameCenterScore(SGNLeaderboard leaderboard, SGNPlayer player, Action<GKScore> scoreReceived)
		{
			if (SGN.PlayerManager.IsLogged)
			{
//				Action<List<GameCenterScore>> call = delegate(List<GameCenterScore> scores) 
//				{
//					int value = 0;
//					string playerId = "unknown";
//				
//					Log("RetrieveGameCenterScore", "{0} scores received for player {1}", scores.Count, playerId);
//					
//					if (scores.Count > 0)
//					{
//						var score = scores[0];
//						value = score.value;
//						playerId = score.playerId;
//						
//						Log("RetrieveGameCenterScore", "Score {0} received for player {1}", value, playerId);
//						
//						scoreReceived(score);
//					}
//				
//					GameCenterManager.scoresForPlayerIdLoaded -= call;
//				};
//			
//				Log("RetrieveGameCenterScore", "Requesting score for player {0}", player);
//				GameCenterManager.retrieveScoresForPlayerIdFailed -= HandleGameCenterManagerretrieveScoresForPlayerIdFailed;
//				GameCenterManager.retrieveScoresForPlayerIdFailed += HandleGameCenterManagerretrieveScoresForPlayerIdFailed;
//			
//				GameCenterManager.scoresForPlayerIdLoaded += call;	
//				GameCenterBinding.retrieveScoresForPlayerId(player.ID, leaderboard.ID);
//				
				var gkl = new GKLeaderboard(new string[] { player.ID } );
				gkl.LoadScores((scoreArray, error) => 
				{
					if(error == null)
					{
						int value = 0;
						string playerId = "unknown";
					
						Log("RetrieveGameCenterScore", "{0} scores received for player {1}", scoreArray.Length, player.ID);
						
						if (scoreArray.Length > 0)
						{
							var score = scoreArray[0];
							Log("RetrieveGameCenterScore", "Score {0} received for player {1}", score.Value, player.ID);
							
							scoreReceived(score);
						}
					}
					else
					{
						Log("GetPlayerScore", "Failed: {0}", error); 
					}
				});
			}
		}
		#endregion
	}
}