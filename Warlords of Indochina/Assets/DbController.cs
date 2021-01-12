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

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        conn = "URI=file:" + Application.dataPath + "/Database.db";
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbcmd = dbconn.CreateCommand();
        GetProvinceInfo();
    }

    void GetProvinceInfo()
    {
        dbconn.Open();
        sqlQuery = "SELECT p.Name, p.BuildingSlots, p.AvailableBuildingSlots, n.Color, n.NationId, n.Name, t.Name, t.Attrition, t.DeffenderBonus " 
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
                reader.GetString(5),
                reader.GetString(6),
                reader.GetInt32(7),
                reader.GetInt32(8)));
        }
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbconn.Close();
    }
}
