using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;
using System.Data;
using System;

public class MySQLTEST : MonoBehaviour
{
    private MySqlConnection SqlConn;

    private string ipAdress = "127.0.0.1";
    private string dbID = "root";
    private string dbPW = "1234";
    private string dbName = "sakila";

    string strConn = string.Empty;

    private void Awake()
    {
        strConn = $"server={ipAdress};uid={dbID};pwd={dbPW};charset=utf8;";

        try
        {
            SqlConn = new MySqlConnection(strConn);
        }
        catch (System.Exception e)
        {
            Debug.Log("접속 에러 발생 : " + e.Message);
        }
    }

    private void Start()
    {
        string query = "select * from actor";
        DataSet ds = OnSelectRequest(query, "actor");

        Debug.Log(ds.GetXml()); // 결과물 확인
    }

    private DataSet OnSelectRequest(string query, string tableName)
    {
        try
        {
            SqlConn.Open();     // DB연결

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = SqlConn;
            cmd.CommandText = query;

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds, tableName);

            SqlConn.Close();

            return ds;
        }
        catch (Exception e)
        {
            Debug.Log("에러발생 : " + e.Message);
            return null;
        }
    }

    private void OnApplicationQuit()
    {
        SqlConn.Close();
    }
}
