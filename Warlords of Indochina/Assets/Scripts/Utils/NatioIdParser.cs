namespace Utils
{
	public class NatioIdParser
	{
		public static string ParseId(string nationId)
		{
			var name = "";

			switch (nationId)
			{
				case "KHM":
					name = "Khmer";
					break;
				case "AYU":
					name = "Ayutthaya";
					break;
				case "CHM":
					name = "Champa";
					break;
				case "DVT":
					name = "Dai Viet";
					break;
				case "LXG":
					name = "Lan Xang";
					break;
				case "LGR":
					name = "Ligor";
					break;
				case "SKT":
					name = "Sukkothai";
					break;
				case "PEG":
					name = "Pegu";
					break;
				case "LNA":
					name = "Lan Na";
					break;
				case "PRM":
					name = "Prome";
					break;
				case "TNG":
					name = "Taungu";
					break;
				case "MNP":
					name = "Mong Pai";
					break;
					
			}
			
			return name;
		}
	}
}