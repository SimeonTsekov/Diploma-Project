﻿namespace Utils
{
	public class Constants
	{
		public const float TimeStep = 0.2f;
		public const float Pause = 0.0f;
		public const float Unpause = 1.0f;
		public const int MinSpeed = 1;
		public const int MaxSpeed = 5;
		public static readonly int StartingProvinceManpower = 1000;
		public const int ManpowerIncrease = 10000;
		public static readonly int MonthsInAnYear = 12;
		public const int ManpowerRecoverySpeedDivider = 120;
		public const string MineButtonIdentifier = "Mine";
		public const string BarracksButtonIdentifier = "Barracks";
		public const string FortButtonIdentifier = "Fort";
		public const int BaseBuildingCost = 100;
		public const float BaseProduction = 50f;
		public const int ArmyOffset = 1;
		public const int RegimentTroops = 1000;
		public const int TravelTime = 15;
		public const int RetreatTravelTime = 10;
		public const float MaximumMorale = 2.5f;
		public const int BaseDefenderAdvantage = 2;
		public const int MoraleLossDivisor = 200;
		public const float MaxMoraleLossDivisor = 2.7f;
		public const float DailyMoraleLoss = 0.3f;
		public const float VictoryMoraleRecovery = 0.5f;
		public const float BaseMonthlyMoraleRecovery = 0.15f;
		public const float BaseMonthlyMoraleRecoveryOnFriendlyTerritory = 0.05f;
		public const int MonthlyReinforcements = 100;
		public const int ProvinceSiegeDuration = 30;
		public const int RegimentBuildTime = 60;
		public const float RegimentCost = 10.00f;
		public const float RegimentMonthlyCostConstant = 0.02f;
		public const float BattleReward = 25f;
		public const int BaseCasualties = 15;
		public const int DiceAmplifier = 5;
		public const int CasualtiesDivisor = 100;
		public const int TroopsCasualtiesAmplifier = 10;
	}
}