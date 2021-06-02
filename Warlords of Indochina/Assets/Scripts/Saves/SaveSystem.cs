using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Saves
{
	public class SaveSystem : MonoBehaviour
	{
		public static void SaveData(SaveData saveData)
		{
			var formatter = new BinaryFormatter();
			var path = Path.Combine(Application.persistentDataPath, "player.bin");

			var stream = new FileStream(path, FileMode.Create);

			formatter.Serialize(stream, JsonUtility.ToJson(saveData));
			stream.Close();
		}

		/*public static SaveData LoadData()
		{
			string path = Path.Combine(Application.persistentDataPath, "player.bin");
			if (File.Exists(path))
			{
				BinaryFormatter formatter = new BinaryFormatter();
				FileStream stream = new FileStream(path, FileMode.Open);

				PlayerData data = formatter.Deserialize(stream) as PlayerData;
				stream.Close();

				return data;
			}
			else
			{
				return null;
			}
		}*/
	}
}