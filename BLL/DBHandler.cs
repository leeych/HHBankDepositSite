using Common;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Configuration;

namespace BLL
{
    public sealed class DBHandler
    {
        /// <summary>
        /// 根据用户名和密码查询数据库UserInfo表中的所有记录数
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>记录数</returns>
        public int GetUserCount(string userName, string password)
        {
            string sql = @"select count(*) from UserInfo where UserName='{0}' and PassWord='{1}' and 1=1";
            string sqlString = string.Format(sql, userName, password);
            int count = (int)SqlHelper.ExecuteSqlObj(sqlString);
            return count;
        }

        /// <summary>
        /// 返回用户信息
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>用户信息</returns>
        public UserInfo GetUserInfo(string userName, string password)
        {
            string sql = @"select * from UserInfo where UserName='{0}' and PassWord='{1}' and 1=1";
            string sqlString = string.Format(sql, userName, password);
            using (SqlDataReader dr = SqlHelper.ExecuteReader(sqlString))
            {
                if (dr.Read())
                {
                    var userInfo = new UserInfo {
                                        UserName = dr["UserName"].ToString(),
                                        PassWord = dr["Password"].ToString(),
                                        OrgCode = dr["OrgCode"].ToString(),
                                        Role = (UserRole)int.Parse(dr["Priority"].ToString())
                                    };
                    return userInfo;
                }
            }
            return null;
        }

        /// <summary>
        /// 新增一条存款记录
        /// </summary>
        /// <param name="record"></param>
        /// <returns>影响的行数</returns>
        public int AddDepositRecord(DepositRecord record)
        {
            SqlParameter[] parameters = {
                                            new SqlParameter("@ProtocolID", SqlDbType.Char),
                                            new SqlParameter("@BillAccount", SqlDbType.VarChar),
                                            new SqlParameter("@BillCode", SqlDbType.Char),
                                            new SqlParameter("@DepositeDate", SqlDbType.SmallDateTime),
                                            new SqlParameter("@OrgCode", SqlDbType.Char),
                                            new SqlParameter("@TellerCode", SqlDbType.Char),
                                            new SqlParameter("@TellerName", SqlDbType.NVarChar),
                                            new SqlParameter("@DepositorName", SqlDbType.NVarChar),
                                            new SqlParameter("@IDCard", SqlDbType.Char),
                                            new SqlParameter("@DepositMoney", SqlDbType.Money),
                                            new SqlParameter("@DepositPeriod", SqlDbType.Int),
                                            //new SqlParameter("@EarlierDrawDate", SqlDbType.SmallDateTime),
                                            //new SqlParameter("@CalcDueDate", SqlDbType.SmallDateTime),
                                            //new SqlParameter("@EarlierDrawMoney", SqlDbType.Money),
                                            //new SqlParameter("@RemainMoney", SqlDbType.Money),
                                            //new SqlParameter("@EarlierInterest", SqlDbType.Money),
                                            //new SqlParameter("@SystemInterest", SqlDbType.Money),
                                            //new SqlParameter("@MarginInterest", SqlDbType.Money),
                                            new SqlParameter("@BindAccount", SqlDbType.VarChar),
                                            new SqlParameter("@DepositFlag", SqlDbType.Int),
                                            new SqlParameter("@Remark", SqlDbType.NText)
                                        };
            parameters[0].Value = record.ProtocolID;
            parameters[1].Value = record.BillAccount;
            parameters[2].Value = record.BillCode;
            parameters[3].Value = record.DepositDate;
            parameters[4].Value = record.OrgCode;
            parameters[5].Value = record.TellerCode;
            parameters[6].Value = record.TellerName;
            parameters[7].Value = record.DepositorName;
            parameters[8].Value = record.DepositorIDCard;
            parameters[9].Value = record.DepositMoney;
            parameters[10].Value = record.Period;
            //parameters[11].Value = record.EarlierDrawDate;
            //parameters[12].Value = record.CalcDueDate;
            //parameters[13].Value = record.EarlierDrawMoney;
            //parameters[14].Value = record.RemainMoney;
            //parameters[15].Value = record.EarlierInterest;
            //parameters[16].Value = record.SystemInterest;
            //parameters[17].Value = record.MarginInterest;
            parameters[11].Value = record.BindAccount;
            parameters[12].Value = record.DepositFlag;
            parameters[13].Value = record.Remark;

            string sqlString = @"insert into Jiuhuashanlu (ProtocolID, BillAccount, BillCode, DepositDate, OrgCode, TellerCode, TellerName, DepositorName,IDCard,DepositMoney,DepositPeriod,BindAccount,DepositFlag,Remark)" + 
                "values('@ProtocolID', '@BillAccount', '@BillCode','@DepositDate','@OrgCode','@TellerCode','@TellerName'," +
                "'@DepositorName','@IDCard',@DepositMoney,@DepositPeriod,'@BindAccount',@DepositFlag,'@Remark')";

            return SqlHelper.ExecuteSql(sqlString, parameters);
        }

        /// <summary>
        /// 新增一条存款记录
        /// </summary>
        /// <param name="record">存款记录</param>
        /// <param name="orgCode">机构号</param>
        /// <returns>影响的行数</returns>
        public int AddDepositRecord(DepositRecord record, string orgCode)
        {
            string tableName = Constants.OrgCodeToTableName[orgCode];
            string sql = @"insert into {0} (ProtocolID, BillAccount, BillCode, DepositDate, OrgCode, TellerCode, TellerName, DepositorName,IDCard,DepositMoney,DepositPeriod,BindAccount,DepositFlag,Remark)" + 
                "values('{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}',{10},{11},'{12}',{13},'{14}')";
            string sqlString = string.Format(sql, tableName, record.ProtocolID, record.BillAccount, record.BillCode, record.DepositDate.ToString("yyyy-MM-dd"), record.OrgCode, record.TellerCode, record.TellerName, record.DepositorName, record.DepositorIDCard, record.DepositMoney, record.Period,
                record.BindAccount, record.DepositFlag, record.Remark);
            return SqlHelper.ExecuteSql(sqlString);
        }

        /// <summary>
        /// 根据协议号查询“保利存”产品
        /// </summary>
        /// <param name="protocolID">协议号</param>
        /// <param name="orgCode">机构号</param>
        /// <returns>“保利存”产品</returns>
        public DepositRecord GetRecordByProtocolID(string protocolID, string orgCode)
        {
            string tableName = Constants.OrgCodeToTableName[orgCode];
            string sql = @"select * from {0} where ProtocolID='{1}'";
            string sqlString = string.Format(sql, tableName, protocolID);
            using (SqlDataReader dr = SqlHelper.ExecuteReader(sqlString))
            {
                var record = new DepositRecord 
                                    {
                                        ProtocolID = dr["ProtocolID"].ToString(),
                                        BillAccount = dr["BillAccount"].ToString(),
                                        BillCode = dr["BillCode"].ToString(),
                                        DepositDate = DateTime.Parse(dr["DepositDate"].ToString()),
                                        OrgCode = dr["OrgCode"].ToString(),
                                        TellerCode = dr["TellerCode"].ToString(),
                                        TellerName = dr["TellerName"].ToString(),
                                        DepositorName = dr["DepositorName"].ToString(),
                                        DepositorIDCard = dr["IDCard"].ToString(),
                                        DepositMoney = decimal.Parse(dr["DepositMoney"].ToString()),
                                        Period = int.Parse(dr["DepositPeriod"].ToString()),
                                        EarlierDrawDate = DateTime.Parse(dr["EarlierDrawDate"].ToString()),
                                        CalcDueDate = DateTime.Parse(dr["CalcDueDate"].ToString()),
                                        EarlierDrawMoney = decimal.Parse(dr["EarlierDrawMoney"].ToString()),
                                        RemainMoney = decimal.Parse(dr["RemainMoney"].ToString()),
                                        EarlierInterest = decimal.Parse(dr["EarlierInterest"].ToString()),
                                        SystemInterest = decimal.Parse(dr["SystemInterest"].ToString()),
                                        MarginInterest = decimal.Parse(dr["MarginInsterest"].ToString()),
                                        BindAccount = dr["BindAccount"].ToString(),
                                        DepositFlag = int.Parse(dr["DepositFlag"].ToString()),
                                        Remark = dr["Remark"].ToString()
                                    };
                return record;
            }
        }

        /// <summary>
        /// 根据客户身份证号码查询其名下所有“保利存”产品
        /// </summary>
        /// <param name="idCard">身份证号码</param>
        /// <param name="orgCode">机构号</param>
        /// <returns>“保利存”列表</returns>
        public List<DepositRecord> GetRecordByIDCardOrgCode(string idCard, string orgCode)
        {
            string tableName = Constants.OrgCodeToTableName[orgCode];
            string sql = @"select * from {0} where IDCard='{1}'";
            string sqlString = string.Format(sql, tableName, idCard);
            List<DepositRecord> recordList = new List<DepositRecord>();
            using (SqlDataReader dr = SqlHelper.ExecuteReader(sqlString))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var record = new DepositRecord
                        {
                            ProtocolID = dr["ProtocolID"].ToString(),
                            BillAccount = dr["BillAccount"].ToString(),
                            BillCode = dr["BillCode"].ToString(),
                            DepositDate = DateTime.Parse(dr["DepositDate"].ToString()),
                            OrgCode = dr["OrgCode"].ToString(),
                            TellerCode = dr["TellerCode"].ToString(),
                            TellerName = dr["TellerName"].ToString(),
                            DepositorName = dr["DepositorName"].ToString(),
                            DepositorIDCard = dr["IDCard"].ToString(),
                            DepositMoney = decimal.Parse(dr["DepositMoney"].ToString()),
                            Period = int.Parse(dr["DepositPeriod"].ToString()),
                            EarlierDrawDate = DateTime.Parse(dr["EarlierDrawDate"].ToString()),
                            CalcDueDate = DateTime.Parse(dr["CalcDueDate"].ToString()),
                            EarlierDrawMoney = decimal.Parse(dr["EarlierDrawMoney"].ToString()),
                            RemainMoney = decimal.Parse(dr["RemainMoney"].ToString()),
                            EarlierInterest = decimal.Parse(dr["EarlierInterest"].ToString()),
                            SystemInterest = decimal.Parse(dr["SystemInterest"].ToString()),
                            MarginInterest = decimal.Parse(dr["MarginInsterest"].ToString()),
                            BindAccount = dr["BindAccount"].ToString(),
                            DepositFlag = int.Parse(dr["DepositFlag"].ToString()),
                            Remark = dr["Remark"].ToString()
                        };
                        recordList.Add(record);
                    }
                }
            }

            if (recordList.Count == 0)
            {
                return null;
            }
            return recordList;
        }

        /// <summary>
        /// 查询某存单账号下所有的“保利存”
        /// </summary>
        /// <param name="account">存单账号</param>
        /// <param name="orgCode">机构号</param>
        /// <returns>“保利存”列表</returns>
        public List<DepositRecord> GetRecordByAccount(string account, string orgCode)
        {
            string tableName = Constants.OrgCodeToTableName[orgCode];
            string sql = @"select * from {0} where BillAccount='{1}'";
            string sqlString = string.Format(sql, tableName, account);
            List<DepositRecord> recordList = new List<DepositRecord>();
            using (SqlDataReader dr = SqlHelper.ExecuteReader(sqlString))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        string temp = dr["DepositMoney"].ToString();
                        decimal d = decimal.Parse(temp);
                        var record = new DepositRecord
                        {
                            ProtocolID = dr["ProtocolID"].ToString(),
                            BillAccount = dr["BillAccount"].ToString(),
                            BillCode = dr["BillCode"].ToString(),
                            DepositDate = DateTime.Parse(dr["DepositDate"].ToString()),
                            OrgCode = dr["OrgCode"].ToString(),
                            TellerCode = dr["TellerCode"].ToString(),
                            TellerName = dr["TellerName"].ToString(),
                            DepositorName = dr["DepositorName"].ToString(),
                            DepositorIDCard = dr["IDCard"].ToString(),
                            DepositMoney = decimal.Parse(dr["DepositMoney"].ToString()),
                            Period = int.Parse(dr["DepositPeriod"].ToString()),
                            //EarlierDrawDate = (dr["EalierDrawDate"] == null ? DateTime.MaxValue : DateTime.Parse(dr["EarlierDrawDate"].ToString())),
                            //CalcDueDate = (dr["CalcDueDate"] == null ? DateTime.MaxValue : DateTime.Parse(dr["CalcDueDate"].ToString())),
                            //EarlierDrawMoney = decimal.Parse(dr["EarlierDrawMoney"].ToString()),
                            RemainMoney = decimal.Parse(dr["RemainMoney"].ToString()),
                            EarlierInterest = decimal.Parse(dr["EarlierInterest"].ToString()),
                            SystemInterest = decimal.Parse(dr["SystemInterest"].ToString()),
                            MarginInterest = decimal.Parse(dr["MarginInsterest"].ToString()),
                            BindAccount = dr["BindAccount"].ToString(),
                            DepositFlag = int.Parse(dr["DepositFlag"].ToString()),
                            Remark = dr["Remark"].ToString()
                        };
                        recordList.Add(record);
                    }
                }
            }
            if (recordList.Count == 0)
            {
                return null;
            }
            return recordList;
        }

        /// <summary>
        /// 查询某柜员办理的所有“保利存”业务
        /// </summary>
        /// <param name="tellerCode">柜员号</param>
        /// <param name="orgCode">机构号</param>
        /// <returns>“保利存”列表</returns>
        public List<DepositRecord> GetRecordByTellerCode(string tellerCode, string orgCode)
        {
            string tableName = Constants.OrgCodeToTableName[orgCode];
            string sql = @"select * from {0} where TellerCode='{1}'";
            string sqlString = string.Format(sql, tableName, orgCode);
            List<DepositRecord> recordList = new List<DepositRecord>();
            using (SqlDataReader dr = SqlHelper.ExecuteReader(sqlString))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var record = new DepositRecord
                        {
                            ProtocolID = dr["ProtocolID"].ToString(),
                            BillAccount = dr["BillAccount"].ToString(),
                            BillCode = dr["BillCode"].ToString(),
                            DepositDate = DateTime.Parse(dr["DepositDate"].ToString()),
                            OrgCode = dr["OrgCode"].ToString(),
                            TellerCode = dr["TellerCode"].ToString(),
                            TellerName = dr["TellerName"].ToString(),
                            DepositorName = dr["DepositorName"].ToString(),
                            DepositorIDCard = dr["IDCard"].ToString(),
                            DepositMoney = decimal.Parse(dr["DepositMoney"].ToString()),
                            Period = int.Parse(dr["DepositPeriod"].ToString()),
                            EarlierDrawDate = DateTime.Parse(dr["EarlierDrawDate"].ToString()),
                            CalcDueDate = DateTime.Parse(dr["CalcDueDate"].ToString()),
                            EarlierDrawMoney = decimal.Parse(dr["EarlierDrawMoney"].ToString()),
                            RemainMoney = decimal.Parse(dr["RemainMoney"].ToString()),
                            EarlierInterest = decimal.Parse(dr["EarlierInterest"].ToString()),
                            SystemInterest = decimal.Parse(dr["SystemInterest"].ToString()),
                            MarginInterest = decimal.Parse(dr["MarginInsterest"].ToString()),
                            BindAccount = dr["BindAccount"].ToString(),
                            DepositFlag = int.Parse(dr["DepositFlag"].ToString()),
                            Remark = dr["Remark"].ToString()
                        };
                        recordList.Add(record);
                    }
                }
            }
            if (recordList.Count == 0)
            {
                return null;
            }
            return recordList;
        }

        /// <summary>
        /// 查询某支行所办理的所有“保利存”产品 
        /// </summary>
        /// <param name="orgCode">机构号</param>
        /// <returns>保利存列表</returns>
        public List<DepositRecord> GetRecordByOrgCode(string orgCode)
        {
            string tableName = Constants.OrgCodeToTableName[orgCode];
            string sql = @"select * from {0} where OrgCode='{1}'";
            string sqlString = string.Format(sql, tableName, orgCode);
            List<DepositRecord> recordList = new List<DepositRecord>();
            using (SqlDataReader dr = SqlHelper.ExecuteReader(sqlString))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var record = new DepositRecord
                        {
                            ProtocolID = dr["ProtocolID"].ToString(),
                            BillAccount = dr["BillAccount"].ToString(),
                            BillCode = dr["BillCode"].ToString(),
                            DepositDate = DateTime.Parse(dr["DepositDate"].ToString()),
                            OrgCode = dr["OrgCode"].ToString(),
                            TellerCode = dr["TellerCode"].ToString(),
                            TellerName = dr["TellerName"].ToString(),
                            DepositorName = dr["DepositorName"].ToString(),
                            DepositorIDCard = dr["IDCard"].ToString(),
                            DepositMoney = decimal.Parse(dr["DepositMoney"].ToString()),
                            Period = int.Parse(dr["DepositPeriod"].ToString()),
                            EarlierDrawDate = DateTime.Parse(dr["EarlierDrawDate"].ToString()),
                            CalcDueDate = DateTime.Parse(dr["CalcDueDate"].ToString()),
                            EarlierDrawMoney = decimal.Parse(dr["EarlierDrawMoney"].ToString()),
                            RemainMoney = decimal.Parse(dr["RemainMoney"].ToString()),
                            EarlierInterest = decimal.Parse(dr["EarlierInterest"].ToString()),
                            SystemInterest = decimal.Parse(dr["SystemInterest"].ToString()),
                            MarginInterest = decimal.Parse(dr["MarginInsterest"].ToString()),
                            BindAccount = dr["BindAccount"].ToString(),
                            DepositFlag = int.Parse(dr["DepositFlag"].ToString()),
                            Remark = dr["Remark"].ToString()
                        };
                        recordList.Add(record);
                    }
                }
            }
            if (recordList.Count == 0)
            {
                return null;
            }
            return recordList;
        }

        /// <summary>
        /// 取款
        /// </summary>
        /// <param name="protocolID">协议号</param>
        /// <param name="orgCode">机构号</param>
        /// <param name="billAccount">存单账号</param>
        /// <param name="billCode">存单凭证</param>
        /// <param name="drawDate">取款日期</param>
        /// <param name="drawMoney">取款金额</param>
        /// <param name="remainMoney">剩余金额</param>
        /// <returns>影响的行数</returns>
        public int DrawDepositByProtocolID(string protocolID, string orgCode, string billAccount, string billCode, DateTime drawDate, decimal drawMoney, decimal remainMoney)
        {
            string tableName = Constants.OrgCodeToTableName[orgCode];
            string sql = @"update {0} set EarlierDrawDate='{1}',EarlierDrawMoney={2},RemainMoney={3},DrawFlg={4}" +
                " where ProtocolID='{5}' and BillAccount='{6}' and BillCode='{7}' and OrgCode='{8}'";

            string sqlString = string.Format(sql, tableName, drawDate.ToString("yyyy-MM-dd"), drawMoney.ToString(), remainMoney.ToString(), DrawFlag.Draw.ToString(),
                protocolID, billAccount, billCode, orgCode);
            int rowsAffected = SqlHelper.ExecuteSql(sqlString);
            return rowsAffected;
        }

        /// <summary>
        /// 根据协议号修改存款记录
        /// </summary>
        /// <param name="protocolID"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        public int SetRecordByProtocolID(string protocolID, DepositRecord record)
        {
            string tableName = Constants.OrgCodeToTableName[record.OrgCode];
            string sql = @"update {0} set BillAccount='{1}',BillCode='{2}',DepositDate='{3}',OrgCode='{4}',"
                + "TellerCode='{5}',TellerName='{6}',DepositorName='{7}',IDCard='{8}',DepositMoney='{9}',DepositPeriod='{10}'" 
                + "EarlierDrawDate='{11}',CalcDrawDate='{12}',EarlierDrawMoney={13},RemainMoney={14},EarlierInterest={15},SystemInterest={16},"
                + "MarginInterest={17},BindAccount='{18}',DepositFlag={19},Remark='{20}' where ProtocolID='{21}'";
            string sqlString = string.Format(sql, tableName, record.BillAccount, record.BillCode, record.DepositDate.ToString("yyyy-MM-dd"), record.OrgCode,
                record.TellerCode, record.TellerName, record.DepositorName, record.DepositorIDCard, record.DepositMoney, record.Period,
                record.EarlierDrawDate.ToString("yyyy-MM-dd"), record.CalcDueDate.ToString("yyyy-MM-dd"), record.EarlierDrawMoney, record.RemainMoney, record.EarlierInterest, record.SystemInterest,
                record.MarginInterest, record.BillAccount, record.DepositFlag, record.Remark, protocolID);
            int rows = SqlHelper.ExecuteSql(sqlString);
            return rows;
        }

        /// <summary>
        /// 根据协议号和机构号删除记录
        /// </summary>
        /// <param name="protocolID"></param>
        /// <param name="orgCode"></param>
        /// <returns></returns>
        public int DeleteRecordByProtocolID(string protocolID, string orgCode)
        {
            string tableName = Constants.OrgCodeToTableName[orgCode];
            string sql = @"delete from {0} where ProtocolID='{1}' and OrgCode='{2}'";
            string sqlString = string.Format(sql, tableName, protocolID, orgCode);
            int rows = SqlHelper.ExecuteSql(sqlString);
            return rows;
        }
    }
}
