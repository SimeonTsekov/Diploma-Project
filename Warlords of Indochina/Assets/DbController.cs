using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;
using GlobalDatas;

public class DbController : MonoBehaviour
{
    public static DbController Instance { get; private set; }
    string conn;
    IDbConnection dbconn;
    IDbCommand dbcmd;
    IDataReader reader;
    string sqlQuery;

    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        conn = "URI=file:" + Application.dataPath + "/Database.db";
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbcmd = dbconn.CreateCommand();
        GetProvinceInfo();
    }

    void GetProvinceInfo()
    {
        dbconn.Open();
        sqlQuery = "SELECT p.Name, p.BuildingSlots, p.AvailableBuildingSlots, n.Color, t.Name, t.Attrition, t.DeffenderBonus " 
            + "FROM Provinces p "
            + "INNER JOIN Nations n ON p.NationId = n.NationId "
            + "INNER JOIN Terrains t ON p.TerrainId = t.TerrainId ";
        dbcmd.CommandText = sqlQuery;
        reader = dbcmd.ExecuteReader();

        while (reader.Read())
        {
            ProvinceManagement.Instance.AddProvince(new ProvinceData(
                reader.GetString(0),
                reader.GetInt32(1),
                reader.GetInt32(2),
                reader.GetString(3),
                reader.GetString(4),
                reader.GetInt32(5),
                reader.GetInt32(6)));
        }
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbconn.Close();
    }
}
