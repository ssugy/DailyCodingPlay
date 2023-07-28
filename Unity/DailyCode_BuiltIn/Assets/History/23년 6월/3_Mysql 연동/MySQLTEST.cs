using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;
using System.Data;
using System;

public class MySQLTEST : MonoBehaviour
{
    private MySqlConnection SqlConn;

    // ���� �������� ������ �ϱ⸦ ���Ѵٸ�, MySQL���ο��� ���������� �����ϰ� user�� ������ �����ؾ� �Ѵ�.
    /*
`       select * from mysql.user;
`       CREATE USER 'root'@'%' identified by '��й�ȣ����';
`       GRANT ALL PRIVILEGES ON *.* to 'root'@'%';
`       flush privileges;
     */
    // ���� ������, ������ root�̸��� ������ ����, host�� %�� �����ϴ� �����Դϴ�. ��� ��ο��� ������ �����մϴ�.
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
            Debug.Log("���� ���� �߻� : " + e.Message);
        }
    }

    private void Start()
    {
        string query = $"use {dbName}; select * from actor;";
        DataSet ds = OnSelectRequest(query, "actor");

        // XML���·� ����� ����
        //Debug.Log(ds.GetXml());

        // row������ ����� ����.
        foreach (DataRow item in ds.Tables[0].Rows)
        {
            Debug.Log("������ �ο� : " + item["first_name"]);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="query">����</param>
    /// <param name="tableName">���̺��</param>
    /// <returns></returns>
    private DataSet OnSelectRequest(string query, string tableName)
    {
        try
        {
            SqlConn.Open();     // DB����

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = SqlConn;
            cmd.CommandText = query;

            // adapter������� ������ ������.
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds, tableName);

            SqlConn.Close();

            return ds;
        }
        catch (Exception e)
        {
            Debug.Log("�����߻� : " + e.Message);
            return null;
        }
    }

    private void OnApplicationQuit()
    {
        SqlConn.Close();
    }
}
