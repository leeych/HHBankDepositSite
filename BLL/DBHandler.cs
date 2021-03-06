﻿using Common;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Configuration;
using HHBankDepositSite;

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
                    dr.Close();
                    return userInfo;
                }
            }
            return null;
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>返回用户信息</returns>
        public UserInfo GetUserInfo(string userName)
        {
            string sql = @"select * from UserInfo where UserName='{0}'";
            string sqlString = string.Format(sql, userName);
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
                    dr.Close();
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
                                            new SqlParameter("@BillPeriod", SqlDbType.Int),
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
            parameters[11].Value = record.BindAccount;
            parameters[12].Value = record.DepositFlag;
            parameters[13].Value = record.Remark;

            string sqlString = @"insert into Jiuhuashanlu (ProtocolID, BillAccount, BillCode, DepositDate, OrgCode, TellerCode, TellerName, DepositorName,IDCard,DepositMoney,BillPeriod,BindAccount,DepositFlag,Remark)" + 
                "values('@ProtocolID', '@BillAccount', '@BillCode','@DepositDate','@OrgCode','@TellerCode','@TellerName'," +
                "'@DepositorName','@IDCard',@DepositMoney,@BillPeriod,'@BindAccount',@DepositFlag,'@Remark')";

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
            string sql = @"if not exists (select * from {0} where ProtocolID='{1}') begin insert into {0} (ProtocolID, BillAccount, BillCode, DepositDate, OrgCode, TellerCode, TellerName, DepositorName,DepositorIDCard,DepositMoney,DueDate,BillPeriod,BindAccount,DepositFlag,Remark,CurrentRate,D01Rate,M03Rate, M06Rate, Y01Rate,Y02Rate,Y03Rate,Y05Rate,RemainMoney)" + 
                                    "values('{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', {10}, '{11}', {12}, '{13}', {14}, '{15}', {16}, {17}, {18}, {19}, {20}, {21}, {22}, {23},{24}) end";
            string sqlString = string.Format(sql, tableName, record.ProtocolID, record.BillAccount, record.BillCode, record.DepositDate.ToString("yyyy-MM-dd"), record.OrgCode, record.TellerCode, record.TellerName, record.DepositorName, record.DepositorIDCard, record.DepositMoney, record.DueDate.ToString("yyyy-MM-dd"), record.Period,
                record.BindAccount, record.DepositFlag, record.Remark, record.Rate.CurrRate, record.Rate.D01, record.Rate.M03, record.Rate.M06, record.Rate.Y01, record.Rate.Y02, record.Rate.Y03, record.Rate.Y05, record.DepositMoney);
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
                                        Period = int.Parse(dr["BillPeriod"].ToString()),
                                        BindAccount = dr["BindAccount"].ToString(),
                                        DepositFlag = int.Parse(dr["DepositFlag"].ToString()),
                                        Remark = dr["Remark"].ToString()
                                    };
                dr.Close();
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
                            Period = int.Parse(dr["BillPeriod"].ToString()),
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
                            Period = int.Parse(dr["BillPeriod"].ToString()),
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
                            Period = int.Parse(dr["BillPeriod"].ToString()),
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
                            Period = int.Parse(dr["BillPeriod"].ToString()),
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
        /// 根据协议号、存单账号、凭证号查询保利存记录
        /// </summary>
        /// <param name="protocolId">协议号</param>
        /// <param name="account">存单账号</param>
        /// <param name="code">凭证号</param>
        /// <returns>存款记录</returns>
        public DepositRecord GetRecordByProtocolIdAccountCode(string protocolId, string account, string code, string orgCode)
        {
            string tableName = Constants.OrgCodeToTableName[orgCode];
            string sql = @"select * from {0} where ProtocolID='{1}' and BillAccount='{2}' and BillCode='{3}' and 1=1";
            string sqlString = string.Format(sql, tableName, protocolId, account, code);
            using (SqlDataReader dr = SqlHelper.ExecuteReader(sqlString))
            {
                var record = new DepositRecord { 
                                    ProtocolID = dr["ProtocolID"].ToString(),
                                    BillAccount = dr["BillAccount"].ToString(),
                                    BillCode = dr["BillCode"].ToString(),
                                    DepositDate = DateTime.Parse(dr["DepositDate"].ToString()),
                                    OrgCode = dr["OrgCode"].ToString(),
                                    TellerCode = dr["TellerCode"].ToString(),
                                    DepositorName = dr["DepositorName"].ToString(),
                                    DepositorIDCard = dr["IDCard"].ToString(),
                                    DepositMoney = decimal.Parse(dr["DepositMoney"].ToString()),
                                    BindAccount = dr["BindAccount"].ToString(),
                                    DepositFlag = int.Parse(dr["DepositFlag"].ToString()),
                                    Remark = dr["Remark"].ToString()
                                };
                dr.Close();
                return record;
            }
        }

        /// <summary>
        /// 协议编号、存单账号、凭证号查询存款记录
        /// </summary>
        /// <param name="protocolId">协议编号</param>
        /// <param name="account">存单账号</param>
        /// <param name="code">凭证号</param>
        /// <param name="orgCode">机构号</param>
        /// <returns>存款记录</returns>
        public DrawRecord GetDrawRecordByProtocolIdAccountCode(string protocolId, string account, string orgCode)
        { 
            string tableName = Constants.OrgCodeToTableName[orgCode];
            string sql = @"select DepositDate,BillPeriod,DueDate,DepositMoney,DepositorName,DepositorIDCard,DepositFlag,TellerCode,TellerName,BindAccount,Remark,CurrentRate,D01Rate,M03Rate,M06Rate,Y01Rate,Y02Rate,Y03Rate,Y05Rate,FirstDrawDate,FirstDrawMoney,RemainMoney,FirstSysInterest,FirstCalcInterest,FirstMarginInterest,FinalDrawDate,FinalDrawMoney,FinalSysInterest,FinalCalcInterest,FinalMarginInterest from "
                + "{0} where ProtocolID='{1}' and BillAccount='{2}' and 1=1";
            string sqlString = string.Format(sql, tableName, protocolId, account);
            using (SqlDataReader dr = SqlHelper.ExecuteReader(sqlString))
            {
                if (dr.Read())
                {
                    DrawRecord record = new DrawRecord();
                    record.ProtocolID = protocolId;
                    record.BillAccount = account;
                    record.DepositDate = DateTime.Parse(dr["DepositDate"].ToString());
                    record.BillPeriod = (Period)int.Parse(dr["BillPeriod"].ToString());
                    record.DueDate = DateTime.Parse(dr["DueDate"].ToString());
                    record.CapticalMoney = decimal.Parse(dr["DepositMoney"].ToString());
                    record.DepositorName = dr["DepositorName"].ToString();
                    record.DepositorIDCard = dr["DepositorIDCard"].ToString();
                    record.Status = (DrawFlag)int.Parse(dr["DepositFlag"].ToString());
                    record.RemainMoney = decimal.Parse(dr["RemainMoney"].ToString());

                    record.FirstDrawDate = (dr["FirstDrawDate"] == DBNull.Value) ? DateTime.MaxValue : DateTime.Parse(dr["FirstDrawDate"].ToString());
                    record.FirstDrawMoney = (dr["FirstDrawMoney"] == DBNull.Value) ? decimal.Zero : decimal.Parse(dr["FirstDrawMoney"].ToString());
                    record.FirstSysInterest = (dr["FirstSysInterest"] == DBNull.Value) ? decimal.Zero : decimal.Parse(dr["FirstSysInterest"].ToString());
                    record.FirstSectionInterest = (dr["FirstCalcInterest"] == DBNull.Value) ? decimal.Zero : decimal.Parse(dr["FirstCalcInterest"].ToString());
                    record.FirstMarginInterest = (dr["FirstMarginInterest"] == DBNull.Value) ? decimal.Zero : decimal.Parse(dr["FirstMarginInterest"].ToString());

                    record.FinalDrawDate = (dr["FinalDrawDate"] == DBNull.Value) ? DateTime.MaxValue : DateTime.Parse(dr["FinalDrawDate"].ToString());
                    record.FinalDrawMoney = (dr["FinalDrawMoney"] == DBNull.Value) ? decimal.Zero : decimal.Parse(dr["FinalDrawMoney"].ToString());
                    record.FinalSysInterest = (dr["FinalSysInterest"] == DBNull.Value) ? decimal.Zero : decimal.Parse(dr["FinalSysInterest"].ToString());
                    record.FinalSectionInterest = (dr["FinalCalcInterest"] == DBNull.Value) ? decimal.Zero : decimal.Parse(dr["FinalCalcInterest"].ToString());
                    record.FinalMarginInterest = (dr["FinalMarginInterest"] == DBNull.Value) ? decimal.Zero : decimal.Parse(dr["FinalMarginInterest"].ToString());

                    record.TellerCode = dr["TellerCode"].ToString();
                    record.BindAccount = dr["BindAccount"].ToString();
                    record.Rate = new BankRate()
                                    {
                                        CurrRate = decimal.Parse(dr["CurrentRate"].ToString()),
                                        D01 = decimal.Parse(dr["D01Rate"].ToString()),
                                        M03 = decimal.Parse(dr["M03Rate"].ToString()),
                                        M06 = decimal.Parse(dr["M06Rate"].ToString()),
                                        Y01 = decimal.Parse(dr["Y01Rate"].ToString()),
                                        Y02 = decimal.Parse(dr["Y02Rate"].ToString()),
                                        Y03 = decimal.Parse(dr["Y03Rate"].ToString()),
                                        Y05 = decimal.Parse(dr["Y05Rate"].ToString())
                                    };
                    record.Remark = dr["Remark"].ToString();
                    dr.Close();
                    return record;
                }
                return null;
            }
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

        /// <summary>
        /// 修改登录密码
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="oldPwd">原密码</param>
        /// <param name="newPwd">新密码</param>
        /// <returns>返回影响行数</returns>
        public int ChangePassword(string userName, string oldPwd, string newPwd)
        {
            string sql = @"update UserInfo set Password='{0}' where UserName='{1}' and Password='{2}'";
            string sqlString = string.Format(sql, newPwd, userName, oldPwd);
            int rows = SqlHelper.ExecuteSql(sqlString);
            return rows;
        }

        /// <summary>
        /// 获取机构信息
        /// </summary>
        /// <param name="orgCode">机构号</param>
        /// <returns>机构信息表</returns>
        public OrgInfo GetOrgInfoByOrgCode(string orgCode)
        {
            string sql = @"select * from OrgInfo where OrgCode='{0}'";
            string sqlString = string.Format(sql, orgCode);
            using (SqlDataReader dr = SqlHelper.ExecuteReader(sqlString))
            {
                if (dr.Read())
                {
                    var orgInfo = new OrgInfo { 
                                        OrgCode = orgCode,
                                        OrgName= dr["OrgName"].ToString(),
                                        OrgAddress= dr["OrgAddress"].ToString(),
                                        OrgPhone = dr["OrgPhone"].ToString()
                                       };
                    dr.Close();
                    return orgInfo;
                }
            }
            return null;
        }

        public bool FirstDrawRecord(DrawInfo info, string orgCode)
        {
            string tableName = Constants.OrgCodeToTableName[orgCode];
            string sql = @"update {0} set FirstDrawDate='{1}', FirstDrawMoney={2}, RemainMoney={3}, FirstCalcInterest={4}, FirstSysInterest={5}, " +
                " FirstMarginInterest={6}, DepositFlag={7} where ProtocolID='{8}' and 1=1";
            string sqlString = string.Format(sql, tableName, info.DrawDate.ToString("yyyy-MM-dd"), info.DrawMoney, info.RemainMoney, info.SectionInterest, info.SystemInterest, info.MarginInterest, (int)info.DrawStatus,
                info.ProtocolId);
            int rows = SqlHelper.ExecuteSql(sqlString);
            if (rows == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool FinalDrawDepositRecord(DrawInfo info, string orgCode)
        {
            string tableName = Constants.OrgCodeToTableName[orgCode];
            string sql = @"update {0} set FinalDrawDate='{1}', FinalDrawMoney={2}, RemainMoney={3}, FinalCalcInterest={4}, FinalSysInterest={5}, " +
                " FinalMarginInterest={6}, DepositFlag={7} where ProtocolID='{8}' and 1=1";
            string sqlString = string.Format(sql, tableName, info.DrawDate.ToString("yyyy-MM-dd"), info.DrawMoney, info.RemainMoney, info.SectionInterest, info.SystemInterest, info.MarginInterest, (int)info.DrawStatus,
                info.ProtocolId);
            int rows = SqlHelper.ExecuteSql(sqlString);
            if (rows == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 查询最大协议编号
        /// </summary>
        /// <param name="orgCode"></param>
        /// <returns></returns>
        public string GetMaxProtocolID(string orgCode)
        {
            string tableName = Constants.OrgCodeToTableName[orgCode];
            string sql = @"select max(ProtocolID) from {0}";
            string sqlString = string.Format(sql, tableName);
            string protocolId = SqlHelper.ExecuteSqlObj(sqlString).ToString();
            return protocolId;
        }

        /// <summary>
        /// 查询机构所办业务总数
        /// </summary>
        /// <param name="orgCode"></param>
        /// <returns></returns>
        public SearchDraftInfo GetDraftSearchInfo(string orgCode)
        {
            string tableName = Constants.OrgCodeToTableName[orgCode];
            string sql = @"select max(ProtocolID) as MaxID, count(ProtocolID) as TotalID, org.OrgName from {0} as r inner join " +
                "OrgInfo as org on r.OrgCode=org.OrgCode group by org.OrgName";
            string sqlString = string.Format(sql, tableName);
            using (SqlDataReader dr = SqlHelper.ExecuteReader(sqlString))
            {
                if (dr.Read())
                {
                    SearchDraftInfo info = new SearchDraftInfo { 
                                                    OrgCode = orgCode,
                                                    OrgName = dr["OrgName"].ToString(),
                                                    MaxProtocolId = dr["MaxID"].ToString(),
                                                    RecordCount = long.Parse(dr["TotalID"].ToString())
                                                };
                    dr.Close();
                    return info;
                }
                return null;
            }
        }

        /// <summary>
        /// 查询指定协议编号的“保利存”记录
        /// </summary>
        /// <param name="protocolId">协议编号</param>
        /// <param name="orgCode">机构号</param>
        /// <returns></returns>
        public SearchInfo GetSearchRecordByProtocolID(string protocolId, string orgCode)
        {
            string tableName = Constants.OrgCodeToTableName[orgCode];
            string sql = @"select * from {0} where ProtocolID='{1}' and 1=1";
            string sqlString = string.Format(sql, tableName, protocolId);
            using (SqlDataReader dr = SqlHelper.ExecuteReader(sqlString))
            {
                if (dr.Read())
                {
                    SearchInfo info = new SearchInfo();
                    
                    info.ProtocolID = protocolId;
                    info.BillAccount = dr["BillAccount"].ToString();
                    info.BillCode = dr["BillCode"].ToString();
                    info.DepositMoney = decimal.Parse(dr["DepositMoney"].ToString());
                    info.DepositDate = DateTime.Parse(dr["DepositDate"].ToString());
                    info.BillPeriod = (Period)int.Parse(dr["BillPeriod"].ToString());
                    info.ClientName = dr["DepositorName"].ToString();
                    info.ClientID = dr["DepositorIDCard"].ToString();
                    info.Status = (DrawFlag)int.Parse(dr["DepositFlag"].ToString());
                    info.TellerCode = dr["TellerCode"].ToString();
                    info.TellerName = dr["TellerName"].ToString();
                    info.BindAccount = dr["BindAccount"].ToString();
                    info.ExecRate = new BankRate
                    {
                        CurrRate = decimal.Parse(dr["CurrentRate"].ToString()),
                        D01 = decimal.Parse(dr["D01Rate"].ToString()),
                        M03 = decimal.Parse(dr["M03Rate"].ToString()),
                        M06 = decimal.Parse(dr["M06Rate"].ToString()),
                        Y01 = decimal.Parse(dr["Y01Rate"].ToString()),
                        Y02 = decimal.Parse(dr["Y02Rate"].ToString()),
                        Y03 = decimal.Parse(dr["Y03Rate"].ToString()),
                        Y05 = decimal.Parse(dr["Y05Rate"].ToString())
                    };
                    info.FirstDrawDate = (dr["FirstDrawDate"] == DBNull.Value) ? DateTime.MaxValue : DateTime.Parse(dr["FirstDrawDate"].ToString());
                    info.FirstDrawMoney = (dr["FirstDrawMoney"] == DBNull.Value) ? decimal.Zero : decimal.Parse(dr["FirstDrawMoney"].ToString());
                    info.FirstSysInterest = (dr["FirstSysInterest"] == DBNull.Value) ? decimal.Zero : decimal.Parse(dr["FirstSysInterest"].ToString());
                    info.FirstCalcInterest = (dr["FirstCalcInterest"] == DBNull.Value) ? decimal.Zero : decimal.Parse(dr["FirstCalcInterest"].ToString());
                    info.FirstMarginInterest = (dr["FirstMarginInterest"] == DBNull.Value) ? decimal.Zero : decimal.Parse(dr["FirstMarginInterest"].ToString());

                    info.FinalDrawDate = (dr["FinalDrawDate"] == DBNull.Value) ? DateTime.MaxValue : DateTime.Parse(dr["FinalDrawDate"].ToString());
                    info.FinalDrawMoney = (dr["FinalDrawMoney"] == DBNull.Value) ? decimal.Zero : decimal.Parse(dr["FinalDrawMoney"].ToString());
                    info.FinalSysInterest = (dr["FinalSysInterest"] == DBNull.Value) ? decimal.Zero : decimal.Parse(dr["FinalSysInterest"].ToString());
                    info.FinalCalcInterest = (dr["FinalCalcInterest"] == DBNull.Value) ? decimal.Zero : decimal.Parse(dr["FinalCalcInterest"].ToString());
                    info.FinalMarginInterest = (dr["FinalMarginInterest"] == DBNull.Value) ? decimal.Zero : decimal.Parse(dr["FinalMarginInterest"].ToString());

                    dr.Close();
                    return info;
                }
                return null;
            }
        }

        /// <summary>
        /// 查询指定存单账号的“保利存”存款记录
        /// </summary>
        /// <param name="account">存单账号</param>
        /// <param name="orgCode">机构号</param>
        /// <returns></returns>
        public SearchInfo GetSearchRecordByBillAccount(string account, string orgCode)
        {
            string tableName = Constants.OrgCodeToTableName[orgCode];
            string sql = @"select * from {0} where BillAccount='{1}' and 1=1";
            string sqlString = string.Format(sql, tableName, account);
            using (SqlDataReader dr = SqlHelper.ExecuteReader(sqlString))
            {
                if (dr.Read())
                {
                    SearchInfo info = new SearchInfo();
                    info.ProtocolID = dr["ProtocolID"].ToString();
                    info.BillAccount = dr["BillAccount"].ToString();
                    info.BillCode = dr["BillCode"].ToString();
                    info.DepositMoney = decimal.Parse(dr["DepositMoney"].ToString());
                    info.DepositDate = DateTime.Parse(dr["DepositDate"].ToString());
                    info.BillPeriod = (Period)int.Parse(dr["BillPeriod"].ToString());
                    info.ClientName = dr["DepositorName"].ToString();
                    info.ClientID = dr["DepositorIDCard"].ToString();
                    info.BindAccount = dr["BindAccount"].ToString();
                    info.Status = (DrawFlag)int.Parse(dr["DepositFlag"].ToString());
                    info.TellerCode = dr["TellerCode"].ToString();
                    info.TellerName = dr["TellerName"].ToString();
                    info.ExecRate = new BankRate
                    {
                        CurrRate = decimal.Parse(dr["CurrentRate"].ToString()),
                        D01 = decimal.Parse(dr["D01Rate"].ToString()),
                        M03 = decimal.Parse(dr["M03Rate"].ToString()),
                        M06 = decimal.Parse(dr["M06Rate"].ToString()),
                        Y01 = decimal.Parse(dr["Y01Rate"].ToString()),
                        Y02 = decimal.Parse(dr["Y02Rate"].ToString()),
                        Y03 = decimal.Parse(dr["Y03Rate"].ToString()),
                        Y05 = decimal.Parse(dr["Y05Rate"].ToString())
                    };

                    info.FirstDrawDate = (dr["FirstDrawDate"] == DBNull.Value) ? DateTime.MaxValue : DateTime.Parse(dr["FirstDrawDate"].ToString());
                    info.FirstDrawMoney = (dr["FirstDrawMoney"] == DBNull.Value) ? decimal.Zero : decimal.Parse(dr["FirstDrawMoney"].ToString());
                    info.FirstSysInterest = (dr["FirstSysInterest"] == DBNull.Value) ? decimal.Zero : decimal.Parse(dr["FirstSysInterest"].ToString());
                    info.FirstCalcInterest = (dr["FirstCalcInterest"] == DBNull.Value) ? decimal.Zero : decimal.Parse(dr["FirstCalcInterest"].ToString());
                    info.FirstMarginInterest = (dr["FirstMarginInterest"] == DBNull.Value) ? decimal.Zero : decimal.Parse(dr["FirstMarginInterest"].ToString());

                    info.FinalDrawDate = (dr["FinalDrawDate"] == DBNull.Value) ? DateTime.MaxValue : DateTime.Parse(dr["FinalDrawDate"].ToString());
                    info.FinalDrawMoney = (dr["FinalDrawMoney"] == DBNull.Value) ? decimal.Zero : decimal.Parse(dr["FinalDrawMoney"].ToString());
                    info.FinalSysInterest = (dr["FinalSysInterest"] == DBNull.Value) ? decimal.Zero : decimal.Parse(dr["FinalSysInterest"].ToString());
                    info.FinalCalcInterest = (dr["FinalCalcInterest"] == DBNull.Value) ? decimal.Zero : decimal.Parse(dr["FinalCalcInterest"].ToString());
                    info.FinalMarginInterest = (dr["FinalMarginInterest"] == DBNull.Value) ? decimal.Zero : decimal.Parse(dr["FinalMarginInterest"].ToString());

                    dr.Close();
                    return info;
                }
                return null;
            }
        }

        /// <summary>
        /// 查询指定身份证号码在本机构办理保利存记录
        /// </summary>
        /// <param name="idCard"></param>
        /// <param name="orgCode"></param>
        /// <returns></returns>
        public List<SearchInfo> GetSearchRecordByIDCard(string idCard, string orgCode)
        {
            string tableName = Constants.OrgCodeToTableName[orgCode];
            string sql = @"select ProtocolID, BillAccount, BillCode, DepositMoney, DepositDate, BillPeriod,DepositorName, DepositorIDCard,TellerCode,TellerName,DepositFlag,CurrentRate,D01Rate,M03Rate,M06Rate,Y01Rate,Y02Rate,Y03Rate,Y05Rate " +
                " from {0} where DepositorIDCard='{1}' and 1=1";
            string sqlString = string.Format(sql, tableName, idCard);
            using (SqlDataReader dr = SqlHelper.ExecuteReader(sqlString))
            {
                List<SearchInfo> infoList = new List<SearchInfo>();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        SearchInfo info = new SearchInfo();

                        info.ProtocolID = dr["ProtocolID"].ToString();
                        info.BillAccount = dr["BillAccount"].ToString();
                        info.BillCode = dr["BillCode"].ToString();
                        info.DepositMoney = decimal.Parse(dr["DepositMoney"].ToString());
                        info.DepositDate = DateTime.Parse(dr["DepositDate"].ToString());
                        info.BillPeriod = (Period)int.Parse(dr["BillPeriod"].ToString());
                        info.ClientName = dr["DepositorName"].ToString();
                        info.ClientID = dr["DepositorIDCard"].ToString();
                        info.BindAccount = dr["BindAccount"].ToString();
                        info.Status = (DrawFlag)int.Parse(dr["DepositFlag"].ToString());
                        info.TellerCode = dr["TellerCode"].ToString();
                        info.TellerName = dr["TellerName"].ToString();
                        info.ExecRate = new BankRate
                        {
                            CurrRate = decimal.Parse(dr["CurrentRate"].ToString()),
                            D01 = decimal.Parse(dr["D01Rate"].ToString()),
                            M03 = decimal.Parse(dr["M03Rate"].ToString()),
                            M06 = decimal.Parse(dr["M06Rate"].ToString()),
                            Y01 = decimal.Parse(dr["Y01Rate"].ToString()),
                            Y02 = decimal.Parse(dr["Y02Rate"].ToString()),
                            Y03 = decimal.Parse(dr["Y03Rate"].ToString()),
                            Y05 = decimal.Parse(dr["Y05Rate"].ToString())
                        };

                        info.FirstDrawDate = (dr["FirstDrawDate"] == DBNull.Value) ? DateTime.MaxValue : DateTime.Parse(dr["FirstDrawDate"].ToString());
                        info.FirstDrawMoney = (dr["FirstDrawMoney"] == DBNull.Value) ? decimal.Zero : decimal.Parse(dr["FirstDrawMoney"].ToString());
                        info.FirstSysInterest = (dr["FirstSysInterest"] == DBNull.Value) ? decimal.Zero : decimal.Parse(dr["FirstSysInterest"].ToString());
                        info.FirstCalcInterest = (dr["FirstCalcInterest"] == DBNull.Value) ? decimal.Zero : decimal.Parse(dr["FirstCalcInterest"].ToString());
                        info.FirstMarginInterest = (dr["FirstMarginInterest"] == DBNull.Value) ? decimal.Zero : decimal.Parse(dr["FirstMarginInterest"].ToString());

                        info.FinalDrawDate = (dr["FinalDrawDate"] == DBNull.Value) ? DateTime.MaxValue : DateTime.Parse(dr["FinalDrawDate"].ToString());
                        info.FinalDrawMoney = (dr["FinalDrawMoney"] == DBNull.Value) ? decimal.Zero : decimal.Parse(dr["FinalDrawMoney"].ToString());
                        info.FinalSysInterest = (dr["FinalSysInterest"] == DBNull.Value) ? decimal.Zero : decimal.Parse(dr["FinalSysInterest"].ToString());
                        info.FinalCalcInterest = (dr["FinalCalcInterest"] == DBNull.Value) ? decimal.Zero : decimal.Parse(dr["FinalCalcInterest"].ToString());
                        info.FinalMarginInterest = (dr["FinalMarginInterest"] == DBNull.Value) ? decimal.Zero : decimal.Parse(dr["FinalMarginInterest"].ToString());
                        infoList.Add(info);
                    }
                    dr.Close();
                    return infoList;
                }
                return null;
            }
        }

        /// <summary>
        /// 查询指定期限内的所有交易记录
        /// </summary>
        /// <param name="start">起始时间</param>
        /// <param name="end">截止时间</param>
        /// <param name="orgCode">机构号</param>
        /// <returns>所有交易记录列表</returns>
        public List<SearchInfo> GetSearchRecordByDuration(DateTime start, DateTime end, string orgCode)
        {
            string tableName = Constants.OrgCodeToTableName[orgCode];
            string sql = @"select * from {0} where DepositDate between '{1}' and '{2}' and 1=1 order by ProtocolID asc";
            string sqlString = string.Format(sql, tableName, start.ToString("yyyy-MM-dd"), end.ToString("yyyy-MM-dd"));
            using (SqlDataReader dr = SqlHelper.ExecuteReader(sqlString))
            {
                List<SearchInfo> infoList = new List<SearchInfo>();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        SearchInfo info = new SearchInfo();

                        info.ProtocolID = dr["ProtocolID"].ToString();
                        info.BillAccount = dr["BillAccount"].ToString();
                        info.BillCode = dr["BillCode"].ToString();
                        info.DepositMoney = decimal.Parse(dr["DepositMoney"].ToString());
                        info.DepositDate = DateTime.Parse(dr["DepositDate"].ToString());
                        info.BillPeriod = (Period)int.Parse(dr["BillPeriod"].ToString());
                        info.ClientName = dr["DepositorName"].ToString();
                        info.ClientID = dr["DepositorIDCard"].ToString();
                        info.BindAccount = dr["BindAccount"].ToString();
                        info.Status = (DrawFlag)int.Parse(dr["DepositFlag"].ToString());
                        info.TellerCode = dr["TellerCode"].ToString();
                        info.TellerName = dr["TellerName"].ToString();
                        info.ExecRate = new BankRate
                        {
                            CurrRate = decimal.Parse(dr["CurrentRate"].ToString()),
                            D01 = decimal.Parse(dr["D01Rate"].ToString()),
                            M03 = decimal.Parse(dr["M03Rate"].ToString()),
                            M06 = decimal.Parse(dr["M06Rate"].ToString()),
                            Y01 = decimal.Parse(dr["Y01Rate"].ToString()),
                            Y02 = decimal.Parse(dr["Y02Rate"].ToString()),
                            Y03 = decimal.Parse(dr["Y03Rate"].ToString()),
                            Y05 = decimal.Parse(dr["Y05Rate"].ToString())
                        };

                        info.FirstDrawDate = (dr["FirstDrawDate"] == DBNull.Value) ? DateTime.MaxValue : DateTime.Parse(dr["FirstDrawDate"].ToString());
                        info.FirstDrawMoney = (dr["FirstDrawMoney"] == DBNull.Value) ? decimal.Zero : decimal.Parse(dr["FirstDrawMoney"].ToString());
                        info.FirstSysInterest = (dr["FirstSysInterest"] == DBNull.Value) ? decimal.Zero : decimal.Parse(dr["FirstSysInterest"].ToString());
                        info.FirstCalcInterest = (dr["FirstCalcInterest"] == DBNull.Value) ? decimal.Zero : decimal.Parse(dr["FirstCalcInterest"].ToString());
                        info.FirstMarginInterest = (dr["FirstMarginInterest"] == DBNull.Value) ? decimal.Zero : decimal.Parse(dr["FirstMarginInterest"].ToString());

                        info.FinalDrawDate = (dr["FinalDrawDate"] == DBNull.Value) ? DateTime.MaxValue : DateTime.Parse(dr["FinalDrawDate"].ToString());
                        info.FinalDrawMoney = (dr["FinalDrawMoney"] == DBNull.Value) ? decimal.Zero : decimal.Parse(dr["FinalDrawMoney"].ToString());
                        info.FinalSysInterest = (dr["FinalSysInterest"] == DBNull.Value) ? decimal.Zero : decimal.Parse(dr["FinalSysInterest"].ToString());
                        info.FinalCalcInterest = (dr["FinalCalcInterest"] == DBNull.Value) ? decimal.Zero : decimal.Parse(dr["FinalCalcInterest"].ToString());
                        info.FinalMarginInterest = (dr["FinalMarginInterest"] == DBNull.Value) ? decimal.Zero : decimal.Parse(dr["FinalMarginInterest"].ToString());
                        infoList.Add(info);
                    }
                    dr.Close();
                    return infoList;
                }
                return null;
            }
        }

        public DataTable GetOrgRecordByDuration(DateTime start, DateTime end, string orgCode)
        {
            string tableName = Constants.OrgCodeToTableName[orgCode];
            string sql = @"select * from {0} where DepositDate between '{1}' and '{2}' and 1=1 order by ProtocolID asc";
            string sqlString = string.Format(sql, tableName, start.ToString("yyyy-MM-dd"), end.ToString("yyyy-MM-dd"));
            DataTable dt = SqlHelper.QueryTable(sqlString);
            return dt;
        }

        public DataTable GetDataSourceByProtocolID(string protocolID, string orgCode)
        {
            string tableName = Constants.OrgCodeToTableName[orgCode];
            string sql = @"select * from {0} where ProtocolID='{1}' and 1=1";
            string sqlString = string.Format(sql, tableName, protocolID);
            DataTable dt = SqlHelper.QueryTable(sqlString);
            return dt;
        }

        public DataTable GetDataSourceByBillAccount(string billAccount, string orgCode)
        {
            string tableName = Constants.OrgCodeToTableName[orgCode];
            string sql = @"select * from {0} where BillAccount='{1}' and 1=1";
            string sqlString = string.Format(sql, tableName, billAccount);
            DataTable dt = SqlHelper.QueryTable(sqlString);
            return dt;
        }

        public DataTable GetDataSourceByIDCard(string idCard, string orgCode)
        {
            string tableName = Constants.OrgCodeToTableName[orgCode];
            string sql = @"select * from {0} where DepositorIDCard='{1}' and 1=1";
            string sqlString = string.Format(sql, tableName, idCard);
            DataTable dt = SqlHelper.QueryTable(sqlString);
            return dt;
        }

        /// <summary>
        /// 查询所有机构的存款记录，返回DataTable
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public DataTable GetAllOrgRecordByDuration(DateTime start, DateTime end)
        {
            string sql = @"select * from {0} where DepositDate between '{1}' and '{2}'";
            string startStr = start.ToString("yyyy-MM-dd");
            string endStr = end.ToString("yyyy-MM-dd");
            StringBuilder sqlSb = new StringBuilder();
            sqlSb.AppendFormat(sql + " union all ", Constants.OrgCodeToTableName["3404151477"], startStr, endStr);
            sqlSb.AppendFormat(sql + " union all ", Constants.OrgCodeToTableName["3404151479"], startStr, endStr);
            sqlSb.AppendFormat(sql + " union all ", Constants.OrgCodeToTableName["3404151481"], startStr, endStr);
            sqlSb.AppendFormat(sql + " union all ", Constants.OrgCodeToTableName["3404151483"], startStr, endStr);
            sqlSb.AppendFormat(sql + " union all ", Constants.OrgCodeToTableName["3404151485"], startStr, endStr);
            sqlSb.AppendFormat(sql + " union all ", Constants.OrgCodeToTableName["3404151487"], startStr, endStr);
            sqlSb.AppendFormat(sql + " union all ", Constants.OrgCodeToTableName["3404151489"], startStr, endStr);
            sqlSb.AppendFormat(sql + " union all ", Constants.OrgCodeToTableName["3404151491"], startStr, endStr);
            sqlSb.AppendFormat(sql + " union all ", Constants.OrgCodeToTableName["3404151493"], startStr, endStr);
            sqlSb.AppendFormat(sql + " union all ", Constants.OrgCodeToTableName["3404151501"], startStr, endStr);
            sqlSb.AppendFormat(sql + " union all ", Constants.OrgCodeToTableName["3404151503"], startStr, endStr);
            sqlSb.AppendFormat(sql + " union all ", Constants.OrgCodeToTableName["3404151505"], startStr, endStr);
            sqlSb.AppendFormat(sql + " union all ", Constants.OrgCodeToTableName["3404151507"], startStr, endStr);
            sqlSb.AppendFormat(sql + " union all ", Constants.OrgCodeToTableName["3404151511"], startStr, endStr);
            sqlSb.AppendFormat(sql + " order by ProtocolID asc", Constants.OrgCodeToTableName["3404157871"], startStr, endStr);

            string sqlString = sqlSb.ToString();
            DataTable dt = SqlHelper.QueryTable(sqlString);
            return dt;
        }

        /// <summary>
        /// 查询机构名
        /// </summary>
        /// <param name="orgCode"></param>
        /// <returns></returns>
        public string GetOrgName(string orgCode)
        {
            string tableName = Constants.OrgCodeToTableName[orgCode];
            string sql = @"select OrgName from OrgInfo where OrgCode='{1}' and 1=1";
            string sqlString = string.Format(sql, tableName, orgCode);
            string orgName = SqlHelper.ExecuteSqlObj(sqlString).ToString();
            return orgName;
        }

        /// <summary>
        /// 查询指定机构的所有柜员
        /// </summary>
        /// <param name="orgCode"></param>
        /// <returns></returns>
        public List<TellerInfo> GetTellerInfoList(string orgCode)
        {
            string sql = @"select * from TellerInfo where OrgCode='{0}' and 1=1 order by TellerCode asc";
            string sqlString = string.Format(sql, orgCode);
            using (SqlDataReader dr = SqlHelper.ExecuteReader(sqlString))
            {
                List<TellerInfo> tellerList = new List<TellerInfo>();
                while (dr.Read())
                {
                    TellerInfo teller = new TellerInfo();
                    teller.TellerCode = dr["TellerCode"].ToString();
                    teller.TellerName = dr["TellerName"].ToString();
                    teller.OrgCode = orgCode;
                    teller.Role = (RoleFlag)int.Parse(dr["Role"].ToString());

                    tellerList.Add(teller);
                }
                dr.Close();
                return tellerList;
            }
        }

        /// <summary>
        /// 柜员调动
        /// </summary>
        /// <param name="teller"></param>
        /// <returns></returns>
        public bool ChangeTellerOrg(TellerInfo teller)
        {
            string sql = @"update TellerInfo set OrgCode='{0}' where TellerCode='{1}' and 1=1";
            string sqlString = string.Format(sql, teller.OrgCode, teller.TellerCode);
            int rows = SqlHelper.ExecuteSql(sqlString);
            if (rows == 1)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 新增柜员
        /// </summary>
        /// <param name="teller"></param>
        /// <returns></returns>
        public bool AddTellerInfo(TellerInfo teller)
        {
            string sql = @"if not exists (select * from TellerInfo where TellerCode='{0}') begin insert into TellerInfo(TellerCode, TellerName, OrgCode, Role) values('{1}','{2}','{3}', {4}) end";
            string sqlString = string.Format(sql, teller.TellerCode, teller.TellerCode, teller.TellerName, teller.OrgCode, (int)teller.Role);
            int rows = (int)SqlHelper.ExecuteSqlObj(sqlString);
            if (rows == 1)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 密码重置
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="orgCode"></param>
        /// <returns></returns>
        public bool ResetUserPassword(string userName, string password, string orgCode)
        {
            string sql = @"if exists (select * from UserInfo where UserName='{0}') update UserInfo set Password='{1}' where UserName='{2}' and OrgCode='{3}' and 1=1";
            string sqlString = string.Format(sql, userName, password, userName, orgCode);
            int rows = SqlHelper.ExecuteSql(sqlString);
            if (rows == 1)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 查询所有利率
        /// </summary>
        /// <returns></returns>
        public List<BankRateInfo> GetAllBankRate()
        {
            string sql = @"select * from BankRateInfo where 1=1";
            using (SqlDataReader dr = SqlHelper.ExecuteReader(sql))
            {
                List<BankRateInfo> rateList = new List<BankRateInfo>();
                while (dr.Read())
                {
                    BankRateInfo rate = new BankRateInfo();
                    rate.EffectDate = DateTime.Parse(dr["EffectDate"].ToString());
                    rate.Rate.CurrRate = decimal.Parse(dr["CurrentRate"].ToString());
                    rate.Rate.M03 = decimal.Parse(dr["M03Rate"].ToString());
                    rate.Rate.M06 = decimal.Parse(dr["M06Rate"].ToString());
                    rate.Rate.Y01 = decimal.Parse(dr["Y01Rate"].ToString());
                    rate.Rate.Y02 = decimal.Parse(dr["Y02Rate"].ToString());
                    rate.Rate.Y03 = decimal.Parse(dr["Y03Rate"].ToString());
                    rate.Rate.Y05 = decimal.Parse(dr["Y05Rate"].ToString());
                    rateList.Add(rate);
                }
                dr.Close();
                return rateList;
            }
        }

        /// <summary>
        /// 查询所有支行
        /// </summary>
        /// <returns></returns>
        public List<OrgInfo> GetOrgInfoList()
        {
            string sql = @"select * from OrgInfo where 1=1 order by OrgCode";
            using (SqlDataReader dr = SqlHelper.ExecuteReader(sql))
            {
                List<OrgInfo> orgList = new List<OrgInfo>();
                while (dr.Read())
                {
                    OrgInfo org = new OrgInfo();
                    org.OrgCode = dr["OrgCode"].ToString();
                    org.OrgName = dr["OrgName"].ToString();
                    org.OrgAddress = dr["OrgAddress"].ToString();
                    org.OrgPhone = dr["OrgPhone"].ToString();
                    orgList.Add(org);
                }
                dr.Close();
                return orgList;
            }
        }

        /// <summary>
        /// 查询所有柜员信息
        /// </summary>
        /// <returns></returns>
        public List<TellerInfo> GetAllTellerInfo()
        {
            string sql = @"select * from TellerInfo where 1=1 order by TellerCode asc";
            using (SqlDataReader dr = SqlHelper.ExecuteReader(sql))
            {
                List<TellerInfo> tellerList = new List<TellerInfo>();
                while (dr.Read())
                {
                    TellerInfo teller = new TellerInfo();
                    teller.TellerCode = dr["TellerCode"].ToString();
                    teller.TellerName = dr["TellerName"].ToString();
                    teller.OrgCode = dr["OrgCode"].ToString();
                    teller.Role = (RoleFlag)int.Parse(dr["Role"].ToString());
                    tellerList.Add(teller);
                }
                dr.Close();
                return tellerList;
            }
        }

        /// <summary>
        /// 查询所有利率表
        /// </summary>
        /// <returns></returns>
        public List<BankRateInfo> GetAllBankRateInfoList()
        {
            string sql = @"select * from BankRateInfo where 1=1 order by EffectDate desc";
            using (SqlDataReader dr = SqlHelper.ExecuteReader(sql))
            {
                List<BankRateInfo> rateList = new List<BankRateInfo>();
                while (dr.Read())
                {
                    BankRateInfo rate = new BankRateInfo();
                    rate.EffectDate = DateTime.Parse(dr["EffectDate"].ToString());
                    rate.Rate = new BankRate();
                    rate.Rate.CurrRate = decimal.Parse(dr["CurrentRate"].ToString());
                    rate.Rate.M03 = decimal.Parse(dr["M03Rate"].ToString());
                    rate.Rate.M06 = decimal.Parse(dr["M06Rate"].ToString());
                    rate.Rate.Y01 = decimal.Parse(dr["Y01Rate"].ToString());
                    rate.Rate.Y02 = decimal.Parse(dr["Y02Rate"].ToString());
                    rate.Rate.Y03 = decimal.Parse(dr["Y03Rate"].ToString());
                    rate.Rate.Y05 = decimal.Parse(dr["Y05Rate"].ToString());
                    rateList.Add(rate);
                }
                dr.Close();
                return rateList;
            }
        }
        
        /// <summary>
        /// 新增利率表
        /// </summary>
        /// <param name="rateInfo"></param>
        /// <returns></returns>
        public bool AddBankRateInfo(BankRateInfo rateInfo)
        {
            string sql = @"insert into BankRateInfo(EffectDate,CurrentRate,M03Rate,M06Rate,Y01Rate,Y02Rate,Y03Rate,Y05Rate) values('{0}',{1},{2},{3},{4},{5},{6},{7})";
            string sqlString = string.Format(sql, rateInfo.EffectDate.ToString("yyyy-MM-dd"), rateInfo.Rate.CurrRate, rateInfo.Rate.M03, rateInfo.Rate.M06, rateInfo.Rate.Y01,
                rateInfo.Rate.Y02, rateInfo.Rate.Y03, rateInfo.Rate.Y05);
            int rows = SqlHelper.ExecuteSql(sqlString);
            if (rows == 1)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 新增柜员
        /// </summary>
        /// <param name="tellerCode"></param>
        /// <param name="tellerName"></param>
        /// <param name="orgCode"></param>
        /// <returns></returns>
        public bool AddNewTeller(string tellerCode, string tellerName, string orgCode)
        {
            string sql = @"if not exists (select * from TellerInfo where TellerCode='{0}') begin insert into TellerInfo(TellerCode,TellerName,OrgCode,Role) values('{0}','{1}','{2}',0) end";
            string sqlString = string.Format(sql, tellerCode, tellerName, orgCode);
            int rows = SqlHelper.ExecuteSql(sqlString);
            if (rows == 1)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 调动柜员
        /// </summary>
        /// <param name="tellerCode"></param>
        /// <param name="tellerName"></param>
        /// <param name="orgCode"></param>
        /// <returns></returns>
        public bool ChangeTellerOrg(string tellerCode, string tellerName, string orgCode)
        {
            string sql = @"if exists (select * from TellerInfo where TellerCode='{0}' and TellerName='{1}') update TellerInfo set OrgCode='{2}' where TellerCode='{0}' and TellerName='{1}'";
            string sqlString = string.Format(sql, tellerCode, tellerName, orgCode);
            int rows = SqlHelper.ExecuteSql(sqlString);
            if (rows == 1)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 查询所有机构的存款记录
        /// </summary>
        /// <param name="start">起始时间</param>
        /// <param name="end">结束时间</param>
        /// <returns>所有分支机构的存款记录</returns>
        public List<SearchInfo> GetAllOrgRecord(DateTime start, DateTime end)
        {
            string sql = @"select * from {0} where DepositDate between '{1}' and '{2}'";
            string startStr = start.ToString("yyyy-MM-dd");
            string endStr = end.ToString("yyyy-MM-dd");
            StringBuilder sqlSb = new StringBuilder();
            sqlSb.AppendFormat(sql + " union all ", Constants.OrgCodeToTableName["3404151477"], startStr, endStr);
            sqlSb.AppendFormat(sql + " union all ", Constants.OrgCodeToTableName["3404151479"], startStr, endStr);
            sqlSb.AppendFormat(sql + " union all ", Constants.OrgCodeToTableName["3404151481"], startStr, endStr);
            sqlSb.AppendFormat(sql + " union all ", Constants.OrgCodeToTableName["3404151483"], startStr, endStr);
            sqlSb.AppendFormat(sql + " union all ", Constants.OrgCodeToTableName["3404151485"], startStr, endStr);
            sqlSb.AppendFormat(sql + " union all ", Constants.OrgCodeToTableName["3404151487"], startStr, endStr);
            sqlSb.AppendFormat(sql + " union all ", Constants.OrgCodeToTableName["3404151489"], startStr, endStr);
            sqlSb.AppendFormat(sql + " union all ", Constants.OrgCodeToTableName["3404151491"], startStr, endStr);
            sqlSb.AppendFormat(sql + " union all ", Constants.OrgCodeToTableName["3404151493"], startStr, endStr);
            sqlSb.AppendFormat(sql + " union all ", Constants.OrgCodeToTableName["3404151501"], startStr, endStr);
            sqlSb.AppendFormat(sql + " union all ", Constants.OrgCodeToTableName["3404151503"], startStr, endStr);
            sqlSb.AppendFormat(sql + " union all ", Constants.OrgCodeToTableName["3404151505"], startStr, endStr);
            sqlSb.AppendFormat(sql + " union all ", Constants.OrgCodeToTableName["3404151507"], startStr, endStr);
            sqlSb.AppendFormat(sql + " union all ", Constants.OrgCodeToTableName["3404151511"], startStr, endStr);
            sqlSb.AppendFormat(sql + " order by ProtocolID asc", Constants.OrgCodeToTableName["3404157871"], startStr, endStr);
            string sqlString = sqlSb.ToString();
            using (SqlDataReader dr = SqlHelper.ExecuteReader(sqlString))
            {
                List<SearchInfo> infoList = new List<SearchInfo>();
                while (dr.Read())
                {
                    SearchInfo info = new SearchInfo();
                    info.ProtocolID = dr["ProtocolID"].ToString();
                    info.BillAccount = dr["BillAccount"].ToString();
                    info.BillCode = dr["BillCode"].ToString();
                    info.DepositDate = DateTime.Parse(dr["DepositDate"].ToString());
                    info.DepositMoney = decimal.Parse(dr["DepositMoney"].ToString());
                    info.BillPeriod = (Period)int.Parse(dr["BillPeriod"].ToString());
                    info.ClientID = dr["DepositorIDCard"].ToString();
                    info.ClientName = dr["DepositorName"].ToString();
                    info.TellerCode = dr["TellerCode"].ToString();
                    info.TellerName = dr["TellerName"].ToString();
                    info.Status = (DrawFlag)int.Parse(dr["DepositFlag"].ToString());
                    info.BindAccount = dr["BindAccount"].ToString();

                    info.ExecRate = new BankRate
                    {
                        CurrRate = decimal.Parse(dr["CurrentRate"].ToString()),
                        D01 = decimal.Parse(dr["D01Rate"].ToString()),
                        M03 = decimal.Parse(dr["M03Rate"].ToString()),
                        M06 = decimal.Parse(dr["M06Rate"].ToString()),
                        Y01 = decimal.Parse(dr["Y01Rate"].ToString()),
                        Y02 = decimal.Parse(dr["Y02Rate"].ToString()),
                        Y03 = decimal.Parse(dr["Y03Rate"].ToString()),
                        Y05 = decimal.Parse(dr["Y05Rate"].ToString())
                    };

                    info.FirstDrawDate = (dr["FirstDrawDate"] == DBNull.Value) ? DateTime.MaxValue : DateTime.Parse(dr["FirstDrawDate"].ToString());
                    info.FirstDrawMoney = (dr["FirstDrawMoney"] == DBNull.Value) ? decimal.Zero : decimal.Parse(dr["FirstDrawMoney"].ToString());
                    info.FirstSysInterest = (dr["FirstSysInterest"] == DBNull.Value) ? decimal.Zero : decimal.Parse(dr["FirstSysInterest"].ToString());
                    info.FirstCalcInterest = (dr["FirstCalcInterest"] == DBNull.Value) ? decimal.Zero : decimal.Parse(dr["FirstCalcInterest"].ToString());
                    info.FirstMarginInterest = (dr["FirstMarginInterest"] == DBNull.Value) ? decimal.Zero : decimal.Parse(dr["FirstMarginInterest"].ToString());

                    info.FinalDrawDate = (dr["FinalDrawDate"] == DBNull.Value) ? DateTime.MaxValue : DateTime.Parse(dr["FinalDrawDate"].ToString());
                    info.FinalDrawMoney = (dr["FinalDrawMoney"] == DBNull.Value) ? decimal.Zero : decimal.Parse(dr["FinalDrawMoney"].ToString());
                    info.FinalSysInterest = (dr["FinalSysInterest"] == DBNull.Value) ? decimal.Zero : decimal.Parse(dr["FinalSysInterest"].ToString());
                    info.FinalCalcInterest = (dr["FinalCalcInterest"] == DBNull.Value) ? decimal.Zero : decimal.Parse(dr["FinalCalcInterest"].ToString());
                    info.FinalMarginInterest = (dr["FinalMarginInterest"] == DBNull.Value) ? decimal.Zero : decimal.Parse(dr["FinalMarginInterest"].ToString());
                    infoList.Add(info);
                }
                dr.Close();
                return infoList;
            }
        }

        public bool SetRecordInfo(SearchInfo info, string orgCode)
        {
            string tableName = Constants.OrgCodeToTableName[orgCode];
            string sql = @"update {0} set BillAccount='{1}',BillCode='{2}',DepositMoney={3},DepositDate='{4}',BillPeriod={5},DepositStatus={6},DepositorName='{7}',DepositorIDCard='{8}',BindAccount='{9}'," +
                "CurrentRate={10},D01Rate={11},M03Rate={12},M06Rate={13},Y01Rate={14},Y02Rate={15},Y03Rate={16},Y05Rate={17} where ProtocolID='{18}' and 1=1";
            string sqlString = string.Format(sql, tableName, info.BillAccount, info.BillCode, info.DepositMoney, info.DepositDate.ToString("yyyy-MM-dd"), (int)info.BillPeriod, (int)info.Status, info.ClientName, info.ClientID, info.BindAccount,
                info.ExecRate.CurrRate, info.ExecRate.D01, info.ExecRate.M03, info.ExecRate.M06, info.ExecRate.Y01, info.ExecRate.Y02, info.ExecRate.Y03, info.ExecRate.Y05, info.ProtocolID);
            int rows = SqlHelper.ExecuteSql(sqlString);
            if (rows == 1)
            {
                return true;
            }
            return false;
        }

        public bool SetElseDrawRecord(SearchInfo info, string orgCode)
        {
            string tableName = Constants.OrgCodeToTableName[orgCode];
            string sql = @"update {0} set FinalDrawDate='{1}',DepositStatus={2} where ProtocolID='{3}' and 1=1";
            string sqlString = string.Format(sql, tableName, info.FinalDrawDate.ToString("yyyy-MM-dd"), (int)info.Status, info.ProtocolID);
            int rows = SqlHelper.ExecuteSql(sqlString);
            return (rows == 1);
        }

        public bool SetRecordWithoutDrawDate(SearchInfo info, string orgCode)
        {
            string tableName = Constants.OrgCodeToTableName[orgCode];
            string sql = @"update {0} set BillAccount='{1}',BillCode='{2}',DepositMoney={3},DepositDate='{4}',BillPeriod={5},DepositFlag={6}," +
                "DepositorName='{7}',DepositorIDCard='{8}',BindAccount='{9}',CurrentRate={10},D01Rate={11},M03Rate={12},M06Rate={13},Y01Rate={14},Y02Rate={15},Y03Rate={16},Y05Rate={17}, " + 
                "TellerCode='{18}',TellerName='{19}' where ProtocolID='{20}' and 1=1";
            string sqlString = string.Format(sql, tableName, info.BillAccount, info.BillCode, info.DepositMoney, info.DepositDate.ToString("yyyy-MM-dd"), (int)info.BillPeriod, (int)info.Status,
                info.ClientName, info.ClientID, info.BindAccount, info.ExecRate.CurrRate, info.ExecRate.D01, info.ExecRate.M03, info.ExecRate.M06, info.ExecRate.Y01, info.ExecRate.Y02, info.ExecRate.Y03, info.ExecRate.Y05, 
                info.TellerCode, info.TellerName, info.ProtocolID);
            int rows = SqlHelper.ExecuteSql(sqlString);
            return (rows == 1);
        }

        public List<SearchInfo> GetEachOrgDataPagerRecord(DataPagerInfo pageInfo, string orgCode)
        {
            string tableName = Constants.OrgCodeToTableName[orgCode];
            string sql = @"select top {0} * from {1} where ProtocolID not in (select top {2} ProtocolID from {1} order by ProtocolID asc) order by ProtocolID asc";
            string sqlString = string.Format(sql, pageInfo.PageSize, tableName, pageInfo.PageSize * pageInfo.CurrengPage);
            using (SqlDataReader dr = SqlHelper.ExecuteReader(sqlString))
            {
                List<SearchInfo> infoList = new List<SearchInfo>();
                while (dr.Read())
                {
                    SearchInfo info = new SearchInfo();
                    info.ProtocolID = dr["ProtocolID"].ToString();
                    info.BillAccount = dr["BillAccount"].ToString();
                    info.BillCode = dr["BillCode"].ToString();
                    info.DepositMoney = decimal.Parse(dr["DepositMoney"].ToString());
                    info.DepositDate = DateTime.Parse(dr["DepositDate"].ToString());
                    info.BillPeriod = (Period)int.Parse(dr["BillPeriod"].ToString());
                    info.ClientName = dr["DepositorName"].ToString();
                    info.ClientID = dr["DepositorIDCard"].ToString();
                    info.BindAccount = dr["BindAccount"].ToString();
                    info.Status = (DrawFlag)int.Parse(dr["DepositFlag"].ToString());
                    info.TellerCode = dr["TellerCode"].ToString();
                    info.TellerName = dr["TellerName"].ToString();
                    info.ExecRate = new BankRate
                    {
                        CurrRate = decimal.Parse(dr["CurrentRate"].ToString()),
                        D01 = decimal.Parse(dr["D01Rate"].ToString()),
                        M03 = decimal.Parse(dr["M03Rate"].ToString()),
                        M06 = decimal.Parse(dr["M06Rate"].ToString()),
                        Y01 = decimal.Parse(dr["Y01Rate"].ToString()),
                        Y02 = decimal.Parse(dr["Y02Rate"].ToString()),
                        Y03 = decimal.Parse(dr["Y03Rate"].ToString()),
                        Y05 = decimal.Parse(dr["Y05Rate"].ToString())
                    };

                    info.FirstDrawDate = (dr["FirstDrawDate"] == DBNull.Value) ? DateTime.MaxValue : DateTime.Parse(dr["FirstDrawDate"].ToString());
                    info.FirstDrawMoney = (dr["FirstDrawMoney"] == DBNull.Value) ? decimal.Zero : decimal.Parse(dr["FirstDrawMoney"].ToString());
                    info.FirstSysInterest = (dr["FirstSysInterest"] == DBNull.Value) ? decimal.Zero : decimal.Parse(dr["FirstSysInterest"].ToString());
                    info.FirstCalcInterest = (dr["FirstCalcInterest"] == DBNull.Value) ? decimal.Zero : decimal.Parse(dr["FirstCalcInterest"].ToString());
                    info.FirstMarginInterest = (dr["FirstMarginInterest"] == DBNull.Value) ? decimal.Zero : decimal.Parse(dr["FirstMarginInterest"].ToString());

                    info.FinalDrawDate = (dr["FinalDrawDate"] == DBNull.Value) ? DateTime.MaxValue : DateTime.Parse(dr["FinalDrawDate"].ToString());
                    info.FinalDrawMoney = (dr["FinalDrawMoney"] == DBNull.Value) ? decimal.Zero : decimal.Parse(dr["FinalDrawMoney"].ToString());
                    info.FinalSysInterest = (dr["FinalSysInterest"] == DBNull.Value) ? decimal.Zero : decimal.Parse(dr["FinalSysInterest"].ToString());
                    info.FinalCalcInterest = (dr["FinalCalcInterest"] == DBNull.Value) ? decimal.Zero : decimal.Parse(dr["FinalCalcInterest"].ToString());
                    info.FinalMarginInterest = (dr["FinalMarginInterest"] == DBNull.Value) ? decimal.Zero : decimal.Parse(dr["FinalMarginInterest"].ToString());
                    infoList.Add(info);
                }
                dr.Close();
                return infoList;
            }
        }
    }
}