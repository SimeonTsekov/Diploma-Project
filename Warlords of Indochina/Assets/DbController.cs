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
        dbconn.Open();
        dbcmd = dbconn.CreateCommand();

    }

    ProvinceData GetProvinceInfo(string name)
    {
        sqlQuery = "SELECT p.Name, p.BuildingSlots, p.AvailableBuildingSlots, n.Color, t.Terrain, t.Attrition, t.DeffenderBonus" 
            + "FROM Provinces p WHERE p.Name = " + name
            + "INNER JOIN Nations n ON p.NationId = n.NationId"
            + "INNER JOIN Terrain t ON p.TerrainId = t.TerrainId";
        dbcmd.CommandText = sqlQuery;
        reader = dbcmd.ExecuteReader;

        while (reader.Read())
        {

        }
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
    }
}
