using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;
using System.Data;
using System;
using UnityEditor.Search;

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
    private string ipAdress = "172.33.32.138";
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
            SqlConn = new MySqlConnection(strConn); // 이미 접속이 되어있음

        }
        catch (System.Exception e)
        {
            Debug.Log("접속 에러 발생 : " + e.Message);
        }
    }

    private void Start()
    {
        

        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
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
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SqlConn.Open();     // DB연결
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            string query = $"use {dbName}; select * from actor;";
            DataSet ds = OnSelectRequest(query, "actor");

            // XML형태로 결과물 보기
            //Debug.Log(ds.GetXml());

            // row단위로 결과물 보기.
            foreach (DataTable item in ds.Tables)
            {
                Debug.Log(item.GetType().ToString());
            }

            foreach (DataColumn item in ds.Tables[0].Rows)
            {
                Debug.Log(item.ColumnName);
                
            }
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
            DataSet DS = SelectLogic(query, tableName);
            SqlConn.Close();

            return DS;
        }
        catch (Exception e)
        {
            // 이렇게 처리 할 수 있음.
            if (e.Message.Equals("The connection is already open."))
            {
                Debug.Log("에러발생 : " + e.Message);

                SqlConn.Close();
                SqlConn.Open();
                DataSet DS = SelectLogic(query, tableName);
                SqlConn.Close();
                
                return DS;
            }
            return null;
        }
    }

    private DataSet SelectLogic(string _query, string _tableName)
    {
        MySqlCommand cmd = new MySqlCommand();
        cmd.Connection = SqlConn;
        cmd.CommandText = _query;

        // adapter방식으로 데이터 가져옴.
        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adapter.Fill(ds, _tableName);
        return ds;
    }

    private void OnApplicationQuit()
    {
        SqlConn.Close();
    }
}
