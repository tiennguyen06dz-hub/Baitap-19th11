using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ProjectManagement.DAL
{
    public class DatabaseHelper
    {
        private static string _connectionString = string.Empty;

        public static string ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionString))
                {
                    try
                    {
                        var setting = ConfigurationManager.ConnectionStrings["ProjectTaskDBConn"];
                        if (setting != null && !string.IsNullOrWhiteSpace(setting.ConnectionString))
                        {
                            _connectionString = setting.ConnectionString;
                        }
                    }
                    catch { }

                    string[] candidates = new string[]
                    {
                        _connectionString,
                        "Server=.\\MSSQLSERVER01;Database=ProjectTaskDB;Integrated Security=True;TrustServerCertificate=True;Connect Timeout=3;",
                        "Server=.;Database=ProjectTaskDB;Integrated Security=True;TrustServerCertificate=True;Connect Timeout=3;",
                        "Server=localhost;Database=ProjectTaskDB;Integrated Security=True;TrustServerCertificate=True;Connect Timeout=3;",
                        "Server=(localdb)\\mssqllocaldb;Database=ProjectTaskDB;Integrated Security=True;TrustServerCertificate=True;Connect Timeout=3;"
                    };

                    foreach (var connStr in candidates)
                    {
                        if (string.IsNullOrWhiteSpace(connStr)) continue;
                        try
                        {
                            using (var testConn = new SqlConnection(connStr))
                            {
                                testConn.Open();
                                _connectionString = connStr;
                                break;
                            }
                        }
                        catch { }
                    }

                    if (string.IsNullOrEmpty(_connectionString))
                    {
                        _connectionString = "Server=.\\MSSQLSERVER01;Database=ProjectTaskDB;Integrated Security=True;TrustServerCertificate=True;";
                    }
                }
                return _connectionString;
            }
            set
            {
                _connectionString = value;
            }
        }

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        public static DataTable ExecuteQuery(string query, SqlParameter[]? parameters = null)
        {
            using (var conn = GetConnection())
            {
                using (var cmd = new SqlCommand(query, conn))
                {
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);

                    using (var adapter = new SqlDataAdapter(cmd))
                    {
                        var dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        public static int ExecuteNonQuery(string query, SqlParameter[]? parameters = null)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(query, conn))
                {
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public static object? ExecuteScalar(string query, SqlParameter[]? parameters = null)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(query, conn))
                {
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteScalar();
                }
            }
        }
    }
}
