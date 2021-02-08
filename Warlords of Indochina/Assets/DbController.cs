using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;
using System.Threading.Tasks;
using GlobalDatas;
using Player;
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
        _dbconn.Open();
    }

    public async Task<List<ProvinceData>> GetProvinceInfo()
    {
        var provinces = new List<ProvinceData>();
        await Task.Run(() => {
            _sqlQuery = "SELECT p.Name, " +
                        "p.BuildingSlots, " +
                        "p.AvailableBuildingSlots, " +
                        "n.Color, " +
                        "n.NationId, " +
                        "n.Name, " +
                        "t.Name, " +
                        "t.Attrition, " +
                        "t.DefenderBonus, " +
                        "(SELECT pr.Amount FROM ProvinceResources pr WHERE pr.ProvinceId = p.ProvinceId AND pr.ResourceId = 1), " +
                        "(SELECT pr.Amount FROM ProvinceResources pr WHERE pr.ProvinceId = p.ProvinceId AND pr.ResourceId = 2) " 
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
                    _reader.GetInt32(8),
                     _reader.GetFloat(9),
                    (int)_reader.GetFloat(10)));
            }

            _reader.Close();
            _reader = null;
        });
        return provinces;
    }

    public List<NationData> GetNationInfo()
    {
        var nationDatas = new List<NationData>();
        
        _sqlQuery = "SELECT n.NationId, " +
                    "n.Name, " +
                    "n.Color, " +
                    "n.Capital, " +
                    "n.StartingTroops "
                    + "FROM Nations n";
        _dbcmd.CommandText = _sqlQuery;
        _reader = _dbcmd.ExecuteReader();
        
        while (_reader.Read())
        {
            nationDatas.Add(new NationData(
                _reader.GetString(0), 
                _reader.GetString(1),
                _reader.GetString(2),
                _reader.GetString(3),
                _reader.GetInt32(4)));
        }
        
        _reader.Close();
        _reader = null;

        return nationDatas;
    }
    
    private void OnDestroy()
    {
        _dbcmd.Dispose();
        _dbcmd = null;
        _dbconn.Close();
        _dbconn = null;
    }
}
