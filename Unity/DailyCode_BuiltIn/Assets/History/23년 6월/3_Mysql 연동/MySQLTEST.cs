using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;
using System.Data;
using System;

public class MySQLTEST : MonoBehaviour
{
    private MySqlConnection SqlConn;

    // 만약 원격으로 접속을 하기를 원한다면, MySQL내부에서 원격접속이 가능하게 user의 권한을 변경해야 한다.
    /*
`       select * from mysql.user;
`       CREATE USER 'root'@'%' identified by '비밀번호기입';
`       GRANT ALL PRIVILEGES ON *.* to 'root'@'%';
`       flush privileges;
     */
    // 위의 내용은, 동일한 root이름의 유저를 만들어서, host를 %로 변경하는 쿼리입니다. 모든 경로에서 접속이 가능합니다.
    private string ipAdress = "192.168.0.58";
    private string port = "3306";
    private string dbID = "root";
    private string dbPW = "1234";
    private string dbName = "sakila";

    string strConn = string.Empty;

    private void Awake()
    {
        strConn = $"server={ipAdress};Port={port};uid={dbID};pwd={dbPW};charset=utf8;";

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
        string query = $"use {dbName}; select * from actor;";
        DataSet ds = OnSelectRequest(query, "actor");

        // XML형태로 결과물 보기
        //Debug.Log(ds.GetXml());

        // row단위로 결과물 보기.
        foreach (DataRow item in ds.Tables[0].Rows)
        {
            Debug.Log("데이터 로우 : " + item["first_name"]);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="query">쿼리</param>
    /// <param name="tableName">테이블명</param>
    /// <returns></returns>
    private DataSet OnSelectRequest(string query, string tableName)
    {
        try
        {
            SqlConn.Open();     // DB연결

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = SqlConn;
            cmd.CommandText = query;

            // adapter방식으로 데이터 가져옴.
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
