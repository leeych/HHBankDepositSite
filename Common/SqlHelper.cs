using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

namespace Common
{
    public static class SqlHelper
    {
        //public static readonly string ConnectionString = ConfigurationManager.AppSettings["ConnectString"];
        public static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        private static ILogger s_Logger;

        public static void SetLogger(ILogger logger)
        {
            s_Logger = logger;
        }

        private static void WriteLog(string format, params object[] args)
        {
            if (s_Logger != null)
            {
                s_Logger.Log(format, args);
            }
        }

        #region 公用方法

        public static int GetMaxID(string fieldName, string tableName)
        {
            string strsql = "select max(" + fieldName + ")+1 from " + tableName;
            object obj = GetSingle(strsql);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return int.Parse(obj.ToString());
            }
        }

        public static bool Exists(string strSql, params SqlParameter[] cmdParams)
        {
            object obj = GetSingle(strSql, cmdParams);
            int cmdResult = 0;
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, DBNull.Value)))
            {
                cmdResult = 0;
            }
            else
            {
                cmdResult = int.Parse(obj.ToString());
            }
            if (cmdResult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        #endregion


        #region 执行简单SQL语句

        /// <summary>
        /// 执行简单语句
        /// </summary>
        /// <param name="sqlString"></param>
        /// <returns>影响行数</returns>
        public static int ExecuteSql(string sqlString)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sqlString, connection))
                {
                    try
                    {
                        connection.Open();
                        int rows = cmd.ExecuteNonQuery();
                        connection.Close();
                        return rows;
                    }
                    catch (Exception exception)
                    {
                        WriteLog("sql语句[{0}]：错误信息[{1}]调用堆栈[{2}]", sqlString, exception.Message, exception.StackTrace);
                        connection.Close();
                        return -1;
                    }
                    finally
                    {
                        cmd.Dispose();
                        connection.Close();
                    }
                }
            }
        }

        /// <summary>
        /// 执行sql语句，返回结果第一行第一列
        /// </summary>
        /// <param name="sqlString">sql语句</param>
        /// <returns></returns>
        public static object ExecuteSqlObj(string sqlString)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand(sqlString, connection);
            try
            {
                connection.Open();
                object obj = cmd.ExecuteScalar();
                connection.Close();
                return obj;
            }
            catch (Exception exception)
            {
                WriteLog("sql语句[{0}]:错误信息[{1}]调用堆栈[{2}]", sqlString, exception.Message, exception.StackTrace);
                connection.Close();
                return null;
            }
            finally
            {
                cmd.Dispose();
                connection.Close();
            }
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务
        /// </summary>
        /// <param name="sqlStringList"></param>
        public static void ExecuteSqlTran(List<string> sqlStringList)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    SqlTransaction transaction = null;
                    try
                    {
                        conn.Open();
                        cmd.Connection = conn;
                        transaction = conn.BeginTransaction();
                        cmd.Transaction = transaction;

                        foreach (string s in sqlStringList)
                        {
                            if (!string.IsNullOrEmpty(s.Trim()))
                            {
                                cmd.CommandText = s;
                                cmd.ExecuteNonQuery();
                            }
                        }
                        transaction.Commit();
                        conn.Close();
                    }
                    catch (Exception exception)
                    {
                        WriteLog("ExecuteSqlTran：错误信息[{0}]调用堆栈[{1}]", exception.Message, exception.StackTrace);
                        try
                        {
                            transaction.Rollback();
                        }
                        catch (Exception exception2)
                        {
                            WriteLog("Rollback Exception: Type[{0}]Message[{0}]", exception2.GetType(), exception2.Message);
                        }
                        conn.Close();
                    }
                    finally
                    {
                        if (transaction != null)
                        {
                            transaction.Dispose();
                        }
                        cmd.Dispose();
                        conn.Close();
                    }
                }
            }
        }

        /// <summary>
        /// 执行带一个存储过程参数的SQL语句
        /// </summary>
        /// <param name="sqlString">SQL语句</param>
        /// <param name="content">参数内容，比如的篇有特殊符号的文章内容，可以通过这个方式增加</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string sqlString, string content)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand(sqlString, connection);
            SqlParameter myParameter = new SqlParameter("@content", SqlDbType.NText);
            myParameter.Value = content;
            cmd.Parameters.Add(myParameter);
            try
            {
                connection.Open();
                int rows = cmd.ExecuteNonQuery();
                connection.Close();
                return rows;
            }
            catch (SqlException exception)
            {
                WriteLog("sql语句[{0}]content[{1}]：错误信息[{2}]调用堆栈[{3}]", sqlString, content, exception.Message, exception.StackTrace);
                connection.Close();
                return -1;
            }
            finally
            {
                cmd.Dispose();
                connection.Close();
            }
        }

        /// <summary>
        /// 向数据库里插入图像格式的字段
        /// </summary>
        /// <param name="sqlString">SQL语句</param>
        /// <param name="fs">图像字节，数据库的字段类型为image的情况</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSqlInsertImg(string sqlString, byte[] fs)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sqlString, connection))
                {
                    SqlParameter myParameter = new SqlParameter("@fs", SqlDbType.Image);
                    myParameter.Value = fs;
                    cmd.Parameters.Add(myParameter);
                    try
                    {
                        connection.Open();
                        int rows = cmd.ExecuteNonQuery();
                        connection.Close();
                        return rows;
                    }
                    catch (SqlException exception)
                    {
                        connection.Close();
                        throw new Exception(exception.Message);
                    }
                    finally
                    {
                        cmd.Dispose();
                        connection.Close();
                    }
                }
            }
        }

        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果(object)
        /// </summary>
        /// <param name="sqlString">计算查询结果语句</param>
        /// <returns>查询结果(object)</returns>
        public static object GetSingle(string sqlString)
        {
            using (SqlConnection connection= new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sqlString, connection))
                {
                    try
                    {
                        connection.Open();
                        object obj = cmd.ExecuteScalar();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, DBNull.Value)))
                        {
                            connection.Close();
                            return null;
                        }
                        else
                        {
                            connection.Close();
                            return obj;
                        }
                    }
                    catch (SqlException exception)
                    {
                        connection.Close();
                        throw new Exception(exception.Message);
                    }
                }
            }
        }

        /// <summary>
        /// 执行查询语句，返回SqlDataReader
        /// </summary>
        /// <param name="sqlString">SQL语句</param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader ExecuteReader(string sqlString)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand(sqlString, connection);
            try
            {
                connection.Open();
                SqlDataReader myReader = cmd.ExecuteReader();
                return myReader;
            }
            catch (SqlException exception)
            {
                connection.Close();
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="sqlString">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(string sqlString)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    DataSet ds = new DataSet();
                    SqlDataAdapter adapter = new SqlDataAdapter(sqlString, connection);
                    adapter.Fill(ds, "ds");
                    connection.Close();
                    return ds;
                }
                catch (Exception exception)
                {
                    WriteLog("sql语句[{0}]错误信息：[{1}]调用堆栈[{2}]", sqlString, exception.Message, exception.StackTrace);
                    connection.Close();
                    return null;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public static DataTable QueryTable(string sqlString)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    DataTable dt = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter(sqlString, connection);
                    adapter.Fill(dt);
                    connection.Close();
                    return dt;
                }
                catch (Exception exception)
                {
                    WriteLog("sql语句[{0}]错误信息：[{1}]调用堆栈[{2}]", sqlString, exception.Message, exception.StackTrace);
                    connection.Close();
                    return null;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        #endregion


        #region 执行带参数的SQL语句

        /// <summary>
        /// 执行SQL语句，返回影响的行数
        /// </summary>
        /// <param name="sqlString">SQL语句</param>
        /// <param name="cmdParams">参数</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string sqlString, params SqlParameter[] cmdParams)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, sqlString, cmdParams);
                        int rows = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        connection.Close();
                        return rows;
                    }
                    catch (SqlException exception)
                    {
                        connection.Close();
                        throw new Exception(exception.Message);
                    }
                }
            }
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务
        /// </summary>
        /// <param name="sqlStringList"></param>
        public static void ExecuteSqlTran(Hashtable sqlStringList)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    SqlCommand cmd = new SqlCommand();
                    try
                    {
                        foreach (DictionaryEntry myDE in sqlStringList)
                        {
                            string cmdText = myDE.Key.ToString();
                            SqlParameter[] cmdParams = (SqlParameter[])myDE.Value;
                            PrepareCommand(cmd, conn, trans, cmdText, cmdParams);
                            int val = cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                            trans.Commit();
                        }
                    }
                    catch
                    {
                        trans.Rollback();
                        conn.Close();
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果(object)
        /// </summary>
        /// <param name="sqlString">计算查询结果语句</param>
        /// <param name="cmdParams"></param>
        /// <returns>查询结果</returns>
        public static object GetSingle(string sqlString, params SqlParameter[] cmdParams)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, sqlString, cmdParams);
                        object obj = cmd.ExecuteScalar();
                        cmd.Parameters.Clear();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, DBNull.Value)))
                        {
                            connection.Close();
                            return null;
                        }
                        else
                        {
                            connection.Close();
                            return obj;
                        }
                    }
                    catch (SqlException e)
                    {
                        connection.Close();
                        throw new Exception(e.Message);
                    }
                }
            }
        }

        /// <summary>
        /// 执行查询语句，返回SqlDataReader
        /// </summary>
        /// <param name="sqlString">查询语句</param>
        /// <param name="cmdParams"></param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader ExecuteReader(string sqlString, params SqlParameter[] cmdParams)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, sqlString, cmdParams);
                        SqlDataReader myReader = cmd.ExecuteReader();
                        cmd.Parameters.Clear();
                        return myReader;
                    }
                    catch (SqlException e)
                    {
                        connection.Close();
                        throw new Exception(e.Message);
                    }
                }
            }
        }

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="sqlString"></param>
        /// <param name="cmdParams"></param>
        /// <returns></returns>
        public static DataSet Query(string sqlString, params SqlParameter[] cmdParams)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                PrepareCommand(cmd, connection, null, sqlString, cmdParams);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        da.Fill(ds, "ds");
                        cmd.Parameters.Clear();
                    }
                    catch (SqlException ex)
                    {
                        connection.Close();
                        throw new Exception(ex.Message);
                    }
                    connection.Close();
                    return ds;
                }
            }
        }


        public static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, string cmdText, SqlParameter[] cmdParams)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
            {
                cmd.Transaction = trans;
            }
            cmd.CommandType = CommandType.Text;
            if (cmdParams != null)
            {
                foreach (SqlParameter param in cmdParams)
                {
                    cmd.Parameters.Add(param);
                }
            }
        }

        #endregion

        #region 存储过程

        /// <summary>
        /// 构建SqlCommand对象（用来返回一个结果集，而不是一个整数）
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SqlCommand</returns>
        private static SqlCommand BuildQueryCommand(SqlConnection connection, string storedProcName, SqlParameter[] parameters)
        {
            SqlCommand command = new SqlCommand(storedProcName, connection);
            command.CommandType = CommandType.StoredProcedure;
            foreach (SqlParameter parameter in parameters)
            {
                command.Parameters.Add(parameter);
            }
            return command;
        }

        /// <summary>
        /// 执行存储过程，返回SqlDataReader
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns></returns>
        public static SqlDataReader RunProcedure(string storedProcName, SqlParameter[] parameters)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            SqlCommand cmd = BuildQueryCommand(connection, storedProcName, parameters);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader returnReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return returnReader;
        }

        /// <summary>
        /// 执行存储过程，返回DataSet
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="tableName">DataSet中的表名</param>
        /// <returns>DataSet</returns>
        public static DataSet RunProcedure(string storedProcName, SqlParameter[] parameters, string tableName)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            try
            {
                DataSet dataSet = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = BuildQueryCommand(connection, storedProcName, parameters);
                adapter.Fill(dataSet, tableName);
                return dataSet;
            }
            catch (Exception exception)
            {
                WriteLog("RunProcedure错误信息：[{0}]调用堆栈[{1}]", exception.Message, exception.StackTrace);
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// 创建SqlCommand对象实例（用来返回一个整数值）
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SqlCommand对象实例</returns>
        private static SqlCommand BuildIntCommand(SqlConnection connection, string storedProcName, SqlParameter[] parameters)
        {
            SqlCommand cmd = BuildQueryCommand(connection, storedProcName, parameters);
            cmd.Parameters.Add(new SqlParameter("ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, string.Empty, DataRowVersion.Default, null));
            return cmd;
        }

        /// <summary>
        /// 执行存储过程，返回影响的行数
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="rowsAffected">影响的行数</param>
        /// <returns>影响的行数</returns>
        public static int RunProcedure(string storedProcName, SqlParameter[] parameters, out int rowsAffected)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                int result = 0;
                connection.Open();
                SqlCommand cmd = BuildIntCommand(connection, storedProcName, parameters);
                rowsAffected = cmd.ExecuteNonQuery();
                result = (int)cmd.Parameters["ReturnValue"].Value;
                return result;
            }
        }

        #endregion
    }
}
