using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;
using System.Threading.Tasks;
using GlobalDatas;
using Provinces;

public class DbController : MonoBehaviour
{
    public static DbController Instance { get; private set; }
    private string _conn;
    private IDbConnection _dbconn;
    private IDbCommand _dbcmd;
    private IDataReader _reader;
    private string _sqlQuery;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
        _conn = "URI=file:" + Application.dataPath + "/Database.db";
        _dbconn = (IDbConnection)new SqliteConnection(_conn);
        _dbcmd = _dbconn.CreateCommand();
    }

    private void Start()
    {
    }

    public async Task<List<ProvinceData>> GetProvinceInfo()
    {
        var provinces = new List<ProvinceData>();
        await Task.Run(() => {
            _dbconn.Open();
            _sqlQuery = "SELECT p.Name, p.BuildingSlots, p.AvailableBuildingSlots, n.Color, n.NationId, n.Name, t.Name, t.Attrition, t.DeffenderBonus " 
                        + "FROM Provinces p "
                        + "INNER JOIN Nations n ON p.NationId = n.NationId "
                        + "INNER JOIN Terrains t ON p.TerrainId = t.TerrainId ";
            _dbcmd.CommandText = _sqlQuery;
            _reader = _dbcmd.ExecuteReader();

            while (_reader.Read())
            {
                provinces.Add(new ProvinceData(
                    _reader.GetString(0),
                    _reader.GetInt32(1),
                    _reader.GetInt32(2),
                    _reader.GetString(3),
                    _reader.GetString(4),
                    _reader.GetString(5),
                    _reader.GetString(6),
                    _reader.GetInt32(7),
                    _reader.GetInt32(8)));
            }

            _reader.Close();
            _reader = null;
            _dbcmd.Dispose();
            _dbconn.Close(); 
        });
        Debug.Log("Provinces fetched from db");
        return provinces;
    }
}
