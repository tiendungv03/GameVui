using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System.Linq;
using static UnityEngine.EventSystems.EventTrigger;
using Unity.VisualScripting;
using UnityEngine.Assertions;
using System;

public class ManageData
{
    //Path create database
    private string dbPath;
    private SqliteConnection connection;

    //Initialization
    public ManageData(string dbPath)
    {
        this.dbPath = dbPath;
        this.connection = new SqliteConnection(dbPath);
    }

    //Open connect to database
    public void OpenConnect()
    {
        connection.Open();
    }

    //Close connect to database
    public void CloseConnect()
    {
        connection.Close();
    }

    public void CreateDb()
    {
        using (SqliteCommand command = connection.CreateCommand())
        {
            //Create Dictionary to store command create table
            Dictionary<string, string> createTableDic;
            createTableDic = new Dictionary<string, string>();

            //Add command create table EnemyStatus
            createTableDic.Add("EnemyStatus", 
                    "CREATE TABLE IF NOT EXISTS EnemyStatus (" +
                    "EnemyID INTEGER PRIMARY KEY NOT NULL," +
                    "EnemyName varchar(20) NOT NULL," +
                    "Health int NOT NULL," +
                    "Damage int NOT NULL," +
                    "Speed float NOT NULL," +
                    "AttackRange float NOT NULL," +
                    "AttackCooldown float NOT NULL," +
                    "SpecialAbility varchar(20));");

            //Add command create table Waves
            createTableDic.Add("Waves",
                    "CREATE TABLE IF NOT EXISTS Waves (" +
                    "WaveID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL," +
                    "WaveNumber int NOT NULL," +
                    "Duration int NOT NULL);");

            //Add command create table WaveSpawns
            createTableDic.Add("WaveSpawns",
                    "CREATE TABLE IF NOT EXISTS WaveSpawns (" +
                    "WaveSpawnID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL," +
                    "WaveID int NOT NULL," +
                    "EnemyID int NOT NULL," +
                    "SpawnCount int NOT NULL," +
                    "SpawnInterval float NOT NULL," +
                    "CONSTRAINT FK_Enemies FOREIGN KEY (EnemyID) REFERENCES EnemyStatus(EnemyID)," +
                    "CONSTRAINT FK_Waves FOREIGN KEY (WaveID) REFERENCES Waves(WaveID));");

            //Add command create table Player
            createTableDic.Add("Player",
                    "CREATE TABLE IF NOT EXISTS Player (" +
                    "PlayerID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL," +
                    "Health int NOT NULL," +
                    "Speed float NOT NULL);");

            CreateTable(createTableDic);    
        }
    }

    public void InsertDb()
    {
        //Create Dictionary to store command insert table
        Dictionary<string, List<string>> insertTableDic;
        insertTableDic = new Dictionary<string, List<string>>();

        //Add command insert table EnemyStatus
        insertTableDic.Add("EnemyStatus", new List<string>()
        {
            "INSERT INTO EnemyStatus (EnemyID, EnemyName, Health, Damage, Speed, AttackRange, AttackCooldown, SpecialAbility) " +
            "VALUES (1, 'Melee', 300, 2, 5, 4, 2, 'Melee')",
            "INSERT INTO EnemyStatus (EnemyID, EnemyName, Health, Damage, Speed, AttackRange, AttackCooldown, SpecialAbility) " +
            "VALUES (2, 'Ranged', 150, 2, 5, 15, 1, 'Ranged')",
            "INSERT INTO EnemyStatus (EnemyID, EnemyName, Health, Damage, Speed, AttackRange, AttackCooldown, SpecialAbility) " +
            "VALUES (3, 'Speeder', 150, 3, 10, 4, 0.5, 'Melee')",
            "INSERT INTO EnemyStatus (EnemyID, EnemyName, Health, Damage, Speed, AttackRange, AttackCooldown, SpecialAbility) " +
            "VALUES (4, 'Mutant', 600, 5, 5, 5, 3, 'Mutant')",
        });

        //Add command insert table EnemyStatus
        insertTableDic.Add("Waves", new List<string>()
        {
            "INSERT INTO Waves (WaveNumber, Duration) " +
            "VALUES (1, 60)",
            "INSERT INTO Waves (WaveNumber, Duration) " +
            "VALUES (2, 90)",
            "INSERT INTO Waves (WaveNumber, Duration) " +
            "VALUES (3, 180)",
            "INSERT INTO Waves (WaveNumber, Duration) " +
            "VALUES (4, 180)",
            "INSERT INTO Waves (WaveNumber, Duration) " +
            "VALUES (5, 0)",
        });

        //Add command insert table WaveSpawns
        insertTableDic.Add("WaveSpawns", new List<string>()
        {
            //wave 1
            "INSERT INTO WaveSpawns (WaveID, EnemyID, SpawnCount, SpawnInterval) " +
            "VALUES (1, 1, 30, 0.5)",
            "INSERT INTO WaveSpawns (WaveID, EnemyID, SpawnCount, SpawnInterval) " +
            "VALUES (1, 2, 15, 0.5)",

            //wave 2
            "INSERT INTO WaveSpawns (WaveID, EnemyID, SpawnCount, SpawnInterval) " +
            "VALUES (2, 1, 40, 0.25)",
            "INSERT INTO WaveSpawns (WaveID, EnemyID, SpawnCount, SpawnInterval) " +
            "VALUES (2, 3, 20, 0.5)",
            "INSERT INTO WaveSpawns (WaveID, EnemyID, SpawnCount, SpawnInterval) " +
            "VALUES (2, 2, 15, 0.5)",

            //wave 3
            "INSERT INTO WaveSpawns (WaveID, EnemyID, SpawnCount, SpawnInterval) " +
            "VALUES (3, 3, 60, 0.5)",
            "INSERT INTO WaveSpawns (WaveID, EnemyID, SpawnCount, SpawnInterval) " +
            "VALUES (3, 2, 30, 0.5)",

            //wave 4
            "INSERT INTO WaveSpawns (WaveID, EnemyID, SpawnCount, SpawnInterval) " +
            "VALUES (4, 1, 60, 0.25)",
            "INSERT INTO WaveSpawns (WaveID, EnemyID, SpawnCount, SpawnInterval) " +
            "VALUES (4, 3, 30, 0.5)",
            "INSERT INTO WaveSpawns (WaveID, EnemyID, SpawnCount, SpawnInterval) " +
            "VALUES (4, 2, 20, 0.5)",
            "INSERT INTO WaveSpawns (WaveID, EnemyID, SpawnCount, SpawnInterval) " +
            "VALUES (4, 4, 10, 1)",
            
            //wave 5
            "INSERT INTO WaveSpawns (WaveID, EnemyID, SpawnCount, SpawnInterval) " +
            "VALUES (4, 4, 150, 1)",
        });

        //Add command insert table Player
        insertTableDic.Add("Player", new List<string>()
        {
            "INSERT INTO Player (Health, Speed) " +
            "VALUES (100, 7.5)",
        });

        InsertTable(insertTableDic);
    }

    //Create table with name and create command
    public void CreateTable(Dictionary<string, string> insertTableDic)
    {
        using (SqliteCommand command = connection.CreateCommand())
        {
            foreach (var create in insertTableDic)
            {
                bool flag = create.Value.ToLower().Contains(create.Key.ToLower());
                if (!flag)
                {
                    Debug.Log("Sai ten bang" + create.Key);
                    return;
                }
                command.CommandText = create.Value.ToString();
                command.ExecuteNonQuery();
            }
        }
    }

    //First, check table already have data
    //If not, insert data to table
    public void InsertTable(Dictionary<string, List<string>> insertTableDic) 
    {
        using (SqliteCommand command = connection.CreateCommand())
        {
            foreach (var insert in insertTableDic)
            {
                // Check if the table has data
                command.CommandText = $"SELECT COUNT(1) FROM {insert.Key}";
                int rowCount = Convert.ToInt32(command.ExecuteScalar());

                if (rowCount > 0)
                {
                    Debug.Log("Da co du lieu bang " + insert.Key);
                    continue;
                }

                foreach (var cmd in insert.Value)
                {
                    if (!cmd.ToLower().Contains(insert.Key.ToLower()))
                    {
                        Debug.Log(cmd.ToLower() + " " + insert.Key.ToLower());
                        Debug.Log("Sai ten bang" + insert.Key);
                        continue;
                    }
                    command.CommandText = cmd;
                    command.ExecuteNonQuery();               
                }
            }
        }
    }

    //Take data from table
    public List<T> GetDataFromTable<T>(string tableName, Func<IDataReader, T> mapFunction)
    {
        List<T> data = new List<T>();

        using (SqliteCommand command = connection.CreateCommand())
        {
            string strCmd = "SELECT * FROM " + tableName;
            command.CommandText = strCmd;

            using (IDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    T item = mapFunction(reader);
                    data.Add(item);
                }
                reader.Close();
            }
        }

        return data;
    }
}
