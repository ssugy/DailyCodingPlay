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

    // ���� �������� ������ �ϱ⸦ ���Ѵٸ�, MySQL���ο��� ���������� �����ϰ� user�� ������ �����ؾ� �Ѵ�.
    /*
`       select * from mysql.user;
`       CREATE USER 'root'@'%' identified by '��й�ȣ����';
`       GRANT ALL PRIVILEGES ON *.* to 'root'@'%';
`       flush privileges;
     */
    // ���� ������, ������ root�̸��� ������ ����, host�� %�� �����ϴ� �����Դϴ�. ��� ��ο��� ������ �����մϴ�.
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
            SqlConn = new MySqlConnection(strConn); // �̹� ������ �Ǿ�����

        }
        catch (System.Exception e)
        {
            Debug.Log("���� ���� �߻� : " + e.Message);
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

            // XML���·� ����� ����
            //Debug.Log(ds.GetXml());

            // row������ ����� ����.
            foreach (DataRow item in ds.Tables[0].Rows)
            {
                Debug.Log("������ �ο� : " + item["first_name"]);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SqlConn.Open();     // DB����
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            string query = $"use {dbName}; select * from actor;";
            DataSet ds = OnSelectRequest(query, "actor");

            // XML���·� ����� ����
            //Debug.Log(ds.GetXml());

            // row������ ����� ����.
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
    /// <param name="query">����</param>
    /// <param name="tableName">���̺��</param>
    /// <returns></returns>
    private DataSet OnSelectRequest(string query, string tableName)
    {
        try
        {
            SqlConn.Open();     // DB����
            DataSet DS = SelectLogic(query, tableName);
            SqlConn.Close();

            return DS;
        }
        catch (Exception e)
        {
            // �̷��� ó�� �� �� ����.
            if (e.Message.Equals("The connection is already open."))
            {
                Debug.Log("�����߻� : " + e.Message);

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

        // adapter������� ������ ������.
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
