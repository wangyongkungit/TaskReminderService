﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using TipService.Model;

namespace TipService.DAL
{
    /// <summary>
    /// **************************************
    /// 描    述：Project数据访问层
    /// 作    者：Yongkun Wang
    /// 创建时间：2016-05-16
    /// 修改历史：2017-05-16 Yongkun Wang 创建
    /// **************************************
    /// </summary>
    public class ProjectDAL
    {
        public ProjectDAL()
        { }

        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from project");
            strSql.Append(" where ISEDLETED <> 1 AND ID=@ID ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.VarChar,40)			};
            parameters[0].Value = ID;

            return DbHelperMySQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Project model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into project(");
            strSql.Append(@"ID,TASKNO,PROJECTNAME,ORDERDATE,EXPIREDATE,TIMENEEDED,SHOP,ORDERAMOUNT,VALUATEMODE,PROVINCE,MODELINGSOFTWARE,VALUATESOFTWARE,
                        SPECIALTYCATEGORY,WANGWANGNAME,EMAIL,FLOORS,CONSTRUCTIONAREA,STRUCTUREFORM,BUILDINGTYPE,TRANSACTIONSTATUS,REFUND,MOBILEPHONE,QQ,PAYMENTMETHOD,EXTRAREQUIREMENT,
                        REFERRER,CASHBACK,REMARKS,ASSIGNMENTBOOK,ISFINISHED,FINISHEDPERSON,ENTERINGPERSON,CREATEDATE,ISCREATEDFOLDER,ISDELETED)");
            strSql.Append(" values (");
            strSql.Append(@"@ID,@TASKNO,@PROJECTNAME,@ORDERDATE,@EXPIREDATE,@TIMENEEDED,@SHOP,@ORDERAMOUNT,@VALUATEMODE,@PROVINCE,@MODELINGSOFTWARE,@VALUATESOFTWARE,
                        @SPECIALTYCATEGORY,@WANGWANGNAME,@EMAIL,@FLOORS,@CONSTRUCTIONAREA,@STRUCTUREFORM,@BUILDINGTYPE,@TRANSACTIONSTATUS,@REFUND,@MOBILEPHONE,@QQ,
                        @PAYMENTMETHOD,@EXTRAREQUIREMENT,@REFERRER,@CASHBACK,@REMARKS,@ASSIGNMENTBOOK,@ISFINISHED,@FINISHEDPERSON,@ENTERINGPERSON,@CREATEDATE,@ISCREATEDFOLDER,@ISDELETED)");
            MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.VarChar,40),
					new MySqlParameter("@TASKNO", MySqlDbType.VarChar,120),
					new MySqlParameter("@PROJECTNAME", MySqlDbType.VarChar,200),
					new MySqlParameter("@ORDERDATE", MySqlDbType.DateTime),
					new MySqlParameter("@EXPIREDATE", MySqlDbType.DateTime),
					new MySqlParameter("@TIMENEEDED", MySqlDbType.Decimal,3),
					new MySqlParameter("@SHOP", MySqlDbType.VarChar,2),
					new MySqlParameter("@ORDERAMOUNT", MySqlDbType.Double,8),
					new MySqlParameter("@VALUATEMODE", MySqlDbType.VarChar,2),
					new MySqlParameter("@PROVINCE", MySqlDbType.VarChar,2),
					new MySqlParameter("@MODELINGSOFTWARE", MySqlDbType.VarChar,2),
					new MySqlParameter("@VALUATESOFTWARE", MySqlDbType.VarChar,2),
					new MySqlParameter("@SPECIALTYCATEGORY", MySqlDbType.VarChar,2),
					new MySqlParameter("@WANGWANGNAME", MySqlDbType.VarChar,100),
					new MySqlParameter("@EMAIL", MySqlDbType.VarChar,120),
					new MySqlParameter("@FLOORS", MySqlDbType.Decimal,3),
					new MySqlParameter("@CONSTRUCTIONAREA", MySqlDbType.Double,8),
					new MySqlParameter("@STRUCTUREFORM", MySqlDbType.VarChar,2),
					new MySqlParameter("@BUILDINGTYPE", MySqlDbType.VarChar,2),
					new MySqlParameter("@TRANSACTIONSTATUS", MySqlDbType.VarChar,2),
					new MySqlParameter("@REFUND", MySqlDbType.Double,10),
					new MySqlParameter("@MOBILEPHONE", MySqlDbType.VarChar,18),
					new MySqlParameter("@QQ", MySqlDbType.VarChar,12),
					new MySqlParameter("@PAYMENTMETHOD", MySqlDbType.VarChar,2),
					new MySqlParameter("@EXTRAREQUIREMENT", MySqlDbType.VarChar,255),
					new MySqlParameter("@REFERRER", MySqlDbType.VarChar,120),
					new MySqlParameter("@CASHBACK", MySqlDbType.Decimal,4),
					new MySqlParameter("@REMARKS", MySqlDbType.VarChar,255),
					new MySqlParameter("@ASSIGNMENTBOOK", MySqlDbType.VarChar,3000),
					new MySqlParameter("@ISFINISHED", MySqlDbType.Decimal,1),
					new MySqlParameter("@FINISHEDPERSON", MySqlDbType.VarChar,40),
					new MySqlParameter("@ENTERINGPERSON", MySqlDbType.VarChar,40),
					new MySqlParameter("@CREATEDATE", MySqlDbType.DateTime),
					new MySqlParameter("@ISCREATEDFOLDER", MySqlDbType.Decimal,1),
					new MySqlParameter("@ISDELETED", MySqlDbType.Decimal,1)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.TASKNO;
            parameters[2].Value = model.PROJECTNAME;
            parameters[3].Value = model.ORDERDATE;
            parameters[4].Value = model.EXPIREDATE;
            parameters[5].Value = model.TIMENEEDED;
            parameters[6].Value = model.SHOP;
            parameters[7].Value = model.ORDERAMOUNT;
            parameters[8].Value = model.VALUATEMODE;
            parameters[9].Value = model.PROVINCE;
            parameters[10].Value = model.MODELINGSOFTWARE;
            parameters[11].Value = model.VALUATESOFTWARE;
            parameters[12].Value = model.SPECIALTYCATEGORY;
            parameters[13].Value = model.WANGWANGNAME;
            parameters[14].Value = model.EMAIL;
            parameters[15].Value = model.FLOORS;
            parameters[16].Value = model.CONSTRUCTIONAREA;
            parameters[17].Value = model.STRUCTUREFORM;
            parameters[18].Value = model.BUILDINGTYPE;
            parameters[19].Value = model.TRANSACTIONSTATUS;
            parameters[20].Value = model.REFUND;
            parameters[21].Value = model.MOBILEPHONE;
            parameters[22].Value = model.QQ;
            parameters[23].Value = model.PAYMENTMETHOD;
            parameters[24].Value = model.EXTRAREQUIREMENT;
            parameters[25].Value = model.REFERRER;
            parameters[26].Value = model.CASHBACK;
            parameters[27].Value = model.REMARKS;
            parameters[28].Value = model.ASSIGNMENTBOOK;
            parameters[29].Value = model.ISFINISHED;
            parameters[30].Value = model.FINISHEDPERSON;
            parameters[31].Value = model.ENTERINGPERSON;
            parameters[32].Value = model.CREATEDATE;
            parameters[33].Value = model.ISCREATEDFOLDER;
            parameters[34].Value = model.ISDELETED;

            int rows = DbHelperMySQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Project model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update project set ");
            strSql.Append("TASKNO=@TASKNO,");
            strSql.Append("PROJECTNAME=@PROJECTNAME,");
            strSql.Append("ORDERDATE=@ORDERDATE,");
            strSql.Append("EXPIREDATE=@EXPIREDATE,");
            strSql.Append("TIMENEEDED=@TIMENEEDED,");
            strSql.Append("SHOP=@SHOP,");
            strSql.Append("ORDERAMOUNT=@ORDERAMOUNT,");
            strSql.Append("VALUATEMODE=@VALUATEMODE,");
            strSql.Append("PROVINCE=@PROVINCE,");
            strSql.Append("MODELINGSOFTWARE=@MODELINGSOFTWARE,");
            strSql.Append("VALUATESOFTWARE=@VALUATESOFTWARE,");
            strSql.Append("SPECIALTYCATEGORY=@SPECIALTYCATEGORY,");
            strSql.Append("WANGWANGNAME=@WANGWANGNAME,");
            strSql.Append("EMAIL=@EMAIL,");
            strSql.Append("FLOORS=@FLOORS,");
            strSql.Append("CONSTRUCTIONAREA=@CONSTRUCTIONAREA,");
            strSql.Append("STRUCTUREFORM=@STRUCTUREFORM,");
            strSql.Append("BUILDINGTYPE=@BUILDINGTYPE,");
            strSql.Append("TRANSACTIONSTATUS=@TRANSACTIONSTATUS,");
            strSql.Append("REFUND=@REFUND,");
            strSql.Append("MOBILEPHONE=@MOBILEPHONE,");
            strSql.Append("QQ=@QQ,");
            strSql.Append("PAYMENTMETHOD=@PAYMENTMETHOD,");
            strSql.Append("EXTRAREQUIREMENT=@EXTRAREQUIREMENT,");
            strSql.Append("REFERRER=@REFERRER,");
            strSql.Append("CASHBACK=@CASHBACK,");
            strSql.Append("REMARKS=@REMARKS,");
            strSql.Append("ASSIGNMENTBOOK=@ASSIGNMENTBOOK,");
            strSql.Append("ISFINISHED=@ISFINISHED,");
            strSql.Append("FINISHEDPERSON=@FINISHEDPERSON,");
            strSql.Append("ENTERINGPERSON=@ENTERINGPERSON,");
            strSql.Append("CREATEDATE=@CREATEDATE,");
            strSql.Append("ISCREATEDFOLDER=@ISCREATEDFOLDER,");
            strSql.Append("ISDELETED=@ISDELETED");
            strSql.Append(" where ID=@ID ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@TASKNO", MySqlDbType.VarChar,120),
					new MySqlParameter("@PROJECTNAME", MySqlDbType.VarChar,200),
					new MySqlParameter("@ORDERDATE", MySqlDbType.DateTime),
					new MySqlParameter("@EXPIREDATE", MySqlDbType.DateTime),
					new MySqlParameter("@TIMENEEDED", MySqlDbType.Decimal, 3),
					new MySqlParameter("@SHOP", MySqlDbType.VarChar,2),
					new MySqlParameter("@ORDERAMOUNT", MySqlDbType.Double,8),
					new MySqlParameter("@VALUATEMODE", MySqlDbType.VarChar,2),
					new MySqlParameter("@PROVINCE", MySqlDbType.VarChar,2),
					new MySqlParameter("@MODELINGSOFTWARE", MySqlDbType.VarChar,2),
					new MySqlParameter("@VALUATESOFTWARE", MySqlDbType.VarChar,2),
					new MySqlParameter("@SPECIALTYCATEGORY", MySqlDbType.VarChar,2),
					new MySqlParameter("@WANGWANGNAME", MySqlDbType.VarChar,100),
					new MySqlParameter("@EMAIL", MySqlDbType.VarChar,120),
					new MySqlParameter("@FLOORS", MySqlDbType.Decimal,3),
					new MySqlParameter("@CONSTRUCTIONAREA", MySqlDbType.Double,8),
					new MySqlParameter("@STRUCTUREFORM", MySqlDbType.VarChar,2),
					new MySqlParameter("@BUILDINGTYPE", MySqlDbType.VarChar,2),
					new MySqlParameter("@TRANSACTIONSTATUS", MySqlDbType.VarChar,2),
					new MySqlParameter("@REFUND", MySqlDbType.Double,10),
					new MySqlParameter("@MOBILEPHONE", MySqlDbType.VarChar,18),
					new MySqlParameter("@QQ", MySqlDbType.VarChar,12),
					new MySqlParameter("@PAYMENTMETHOD", MySqlDbType.VarChar,2),
					new MySqlParameter("@EXTRAREQUIREMENT", MySqlDbType.VarChar,255),
					new MySqlParameter("@REFERRER", MySqlDbType.VarChar,120),
					new MySqlParameter("@CASHBACK", MySqlDbType.Decimal,4),
					new MySqlParameter("@REMARKS", MySqlDbType.VarChar,255),
					new MySqlParameter("@ASSIGNMENTBOOK", MySqlDbType.VarChar,3000),
					new MySqlParameter("@ISFINISHED", MySqlDbType.Decimal,1),
					new MySqlParameter("@FINISHEDPERSON", MySqlDbType.VarChar,40),
					new MySqlParameter("@ENTERINGPERSON", MySqlDbType.VarChar,40),
					new MySqlParameter("@CREATEDATE", MySqlDbType.DateTime),
					new MySqlParameter("@ISCREATEDFOLDER", MySqlDbType.Decimal,1),
					new MySqlParameter("@ISDELETED", MySqlDbType.Decimal,1),
					new MySqlParameter("@ID", MySqlDbType.VarChar,40)};
            parameters[0].Value = model.TASKNO;
            parameters[1].Value = model.PROJECTNAME;
            parameters[2].Value = model.ORDERDATE;
            parameters[3].Value = model.EXPIREDATE;
            parameters[4].Value = model.TIMENEEDED;
            parameters[5].Value = model.SHOP;
            parameters[6].Value = model.ORDERAMOUNT;
            parameters[7].Value = model.VALUATEMODE;
            parameters[8].Value = model.PROVINCE;
            parameters[9].Value = model.MODELINGSOFTWARE;
            parameters[10].Value = model.VALUATESOFTWARE;
            parameters[11].Value = model.SPECIALTYCATEGORY;
            parameters[12].Value = model.WANGWANGNAME;
            parameters[13].Value = model.EMAIL;
            parameters[14].Value = model.FLOORS;
            parameters[15].Value = model.CONSTRUCTIONAREA;
            parameters[16].Value = model.STRUCTUREFORM;
            parameters[17].Value = model.BUILDINGTYPE;
            parameters[18].Value = model.TRANSACTIONSTATUS;
            parameters[19].Value = model.REFUND;
            parameters[20].Value = model.MOBILEPHONE;
            parameters[21].Value = model.QQ;
            parameters[22].Value = model.PAYMENTMETHOD;
            parameters[23].Value = model.EXTRAREQUIREMENT;
            parameters[24].Value = model.REFERRER;
            parameters[25].Value = model.CASHBACK;
            parameters[26].Value = model.REMARKS;
            parameters[27].Value = model.ASSIGNMENTBOOK;
            parameters[28].Value = model.ISFINISHED;
            parameters[29].Value = model.FINISHEDPERSON;
            parameters[30].Value = model.ENTERINGPERSON;
            parameters[31].Value = model.CREATEDATE;
            parameters[32].Value = model.ISCREATEDFOLDER;
            parameters[33].Value = model.ISDELETED;
            parameters[34].Value = model.ID;

            int rows = DbHelperMySQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from project ");
            strSql.Append(" where ID=@ID ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.VarChar,40)			};
            parameters[0].Value = ID;

            int rows = DbHelperMySQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from project ");
            strSql.Append(" where ID in (" + IDlist + ")  ");
            int rows = DbHelperMySQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Project GetModel(string ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select ID,TASKNO,PROJECTNAME,ORDERDATE,EXPIREDATE,TIMENEEDED,SHOP,ORDERAMOUNT,VALUATEMODE,PROVINCE,MODELINGSOFTWARE,VALUATESOFTWARE,
                            SPECIALTYCATEGORY,WANGWANGNAME,EMAIL,FLOORS,CONSTRUCTIONAREA,STRUCTUREFORM,BUILDINGTYPE,TRANSACTIONSTATUS,REFUND,MOBILEPHONE,QQ,
                            PAYMENTMETHOD,EXTRAREQUIREMENT,REFERRER,CASHBACK,REMARKS,ASSIGNMENTBOOK,ISFINISHED,FINISHEDPERSON,ENTERINGPERSON,CREATEDATE,ISCREATEDFOLDER,ISDELETED from project ");
            strSql.Append(" where ID=@ID ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.VarChar,40)			};
            parameters[0].Value = ID;

            Project model = new Project();
            DataSet ds = DbHelperMySQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Project DataRowToModel(DataRow row)
        {
            Project model = new Project();
            if (row != null)
            {
                if (row["ID"] != null)
                {
                    model.ID = row["ID"].ToString();
                }
                if (row["TASKNO"] != null)
                {
                    model.TASKNO = row["TASKNO"].ToString();
                }
                if (row["PROJECTNAME"] != null)
                {
                    model.PROJECTNAME = row["PROJECTNAME"].ToString();
                }
                if (row["ORDERDATE"] != null && row["ORDERDATE"].ToString() != "")
                {
                    model.ORDERDATE = DateTime.Parse(row["ORDERDATE"].ToString());
                }
                if (row["EXPIREDATE"] != null && row["EXPIREDATE"].ToString() != "")
                {
                    model.EXPIREDATE = DateTime.Parse(row["EXPIREDATE"].ToString());
                }
                if (row["TIMENEEDED"] != null && row["TIMENEEDED"].ToString() != "")
                {
                    model.TIMENEEDED = decimal.Parse(row["TIMENEEDED"].ToString());
                }
                if (row["SHOP"] != null)
                {
                    model.SHOP = row["SHOP"].ToString();
                }
                if (row["ORDERAMOUNT"] != null)
                {
                    double orderAmount = 0.00;
                    if (double.TryParse(row["ORDERAMOUNT"].ToString(), out orderAmount))
                    {
                        model.ORDERAMOUNT = orderAmount;
                    }
                    else
                    {
                        model.ORDERAMOUNT = 0;
                    }
                }
                else
                {
                    model.ORDERAMOUNT = 0.00;
                }
                if (row["VALUATEMODE"] != null)
                {
                    model.VALUATEMODE = row["VALUATEMODE"].ToString();
                }
                if (row["PROVINCE"] != null)
                {
                    model.PROVINCE = row["PROVINCE"].ToString();
                }
                if (row["MODELINGSOFTWARE"] != null)
                {
                    model.MODELINGSOFTWARE = row["MODELINGSOFTWARE"].ToString();
                }
                if (row["VALUATESOFTWARE"] != null)
                {
                    model.VALUATESOFTWARE = row["VALUATESOFTWARE"].ToString();
                }
                if (row["SPECIALTYCATEGORY"] != null)
                {
                    model.SPECIALTYCATEGORY = row["SPECIALTYCATEGORY"].ToString();
                }
                if (row["WANGWANGNAME"] != null)
                {
                    model.WANGWANGNAME = row["WANGWANGNAME"].ToString();
                }
                if (row["EMAIL"] != null)
                {
                    model.EMAIL = row["EMAIL"].ToString();
                }
                if (row["FLOORS"] != null && row["FLOORS"].ToString() != "")
                {
                    model.FLOORS = decimal.Parse(row["FLOORS"].ToString());
                }
                if (row["CONSTRUCTIONAREA"] != null)
                {
                    int constructionArea = 0;
                    if (int.TryParse(row["CONSTRUCTIONAREA"].ToString(), out constructionArea))
                    {
                        model.CONSTRUCTIONAREA = constructionArea;
                    }
                }
                if (row["STRUCTUREFORM"] != null)
                {
                    model.STRUCTUREFORM = row["STRUCTUREFORM"].ToString();
                }
                if (row["BUILDINGTYPE"] != null)
                {
                    model.BUILDINGTYPE = row["BUILDINGTYPE"].ToString();
                }
                if (row["TRANSACTIONSTATUS"] != null)
                {
                    model.TRANSACTIONSTATUS = row["TRANSACTIONSTATUS"].ToString();
                }
                if (row["REFUND"] != null)
                {
                    double refund = 0.00;
                    if (double.TryParse(row["REFUND"].ToString(), out refund))
                    {
                        model.REFUND = refund;
                    }
                    else
                    {
                        model.REFUND = 0;
                    }
                }
                else
                {
                    model.REFUND = 0.00;
                }
                if (row["MOBILEPHONE"] != null)
                {
                    model.MOBILEPHONE = row["MOBILEPHONE"].ToString();
                }
                if (row["QQ"] != null)
                {
                    model.QQ = row["QQ"].ToString();
                }
                if (row["PAYMENTMETHOD"] != null)
                {
                    model.PAYMENTMETHOD = row["PAYMENTMETHOD"].ToString();
                }
                if (row["EXTRAREQUIREMENT"] != null)
                {
                    model.EXTRAREQUIREMENT = row["EXTRAREQUIREMENT"].ToString();
                }
                if (row["REFERRER"] != null)
                {
                    model.REFERRER = row["REFERRER"].ToString();
                }
                if (row["CASHBACK"] != null && row["CASHBACK"].ToString() != "")
                {
                    model.CASHBACK = decimal.Parse(row["CASHBACK"].ToString());
                }
                if (row["REMARKS"] != null)
                {
                    model.REMARKS = row["REMARKS"].ToString();
                }
                if (row["ASSIGNMENTBOOK"] != null)
                {
                    model.ASSIGNMENTBOOK = row["ASSIGNMENTBOOK"].ToString();
                }
                if (row["ISFINISHED"] != null && row["ISFINISHED"].ToString() != "")
                {
                    model.ISFINISHED = decimal.Parse(row["ISFINISHED"].ToString());
                }
                if (row["FINISHEDPERSON"] != null)
                {
                    model.FINISHEDPERSON = row["FINISHEDPERSON"].ToString();
                }
                if (row["ENTERINGPERSON"] != null)
                {
                    model.ENTERINGPERSON = row["ENTERINGPERSON"].ToString();
                }
                if (row["CREATEDATE"] != null && row["CREATEDATE"].ToString() != "")
                {
                    model.CREATEDATE = DateTime.Parse(row["CREATEDATE"].ToString());
                }
                if (row["ISCREATEDFOLDER"] != null && row["ISCREATEDFOLDER"].ToString() != "")
                {
                    model.ISCREATEDFOLDER = decimal.Parse(row["ISCREATEDFOLDER"].ToString());
                }
                if (row["ISDELETED"] != null && row["ISDELETED"].ToString() != "")
                {
                    model.ISDELETED = decimal.Parse(row["ISDELETED"].ToString());
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select ID,TASKNO,PROJECTNAME,ORDERDATE,EXPIREDATE,TIMENEEDED,SHOP,ORDERAMOUNT,VALUATEMODE,PROVINCE,MODELINGSOFTWARE,VALUATESOFTWARE,
                        SPECIALTYCATEGORY,WANGWANGNAME,EMAIL,FLOORS,CONSTRUCTIONAREA,STRUCTUREFORM,BUILDINGTYPE,TRANSACTIONSTATUS,REFUND,MOBILEPHONE,QQ,
                        PAYMENTMETHOD,EXTRAREQUIREMENT,REFERRER,CASHBACK,REMARKS,ASSIGNMENTBOOK,ISFINISHED,FINISHEDPERSON,ENTERINGPERSON,CREATEDATE,ISCREATEDFOLDER,ISDELETED ");
            strSql.Append(" FROM project WHERE (ISDELETED IS NULL OR ISDELETED = 0) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(strWhere);
            }
            return DbHelperMySQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表，简单关联
        /// </summary>
        public DataSet GetListSimpleUnion(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select ID,TASKNO,PROJECTNAME,dp.configvalue SHOP,WANGWANGNAME FROM project p
                        LEFT JOIN
                        (select configkey,configvalue from configvalue c where configtypeid=(select configtypeid from configtype WHERE configtypeName='店铺编号名称')) dp
                        on p.SHOP=dp.configkey
                        WHERE (ISDELETED IS NULL OR ISDELETED = 0)");
            //strSql.Append(" FROM project WHERE (ISDELETED IS NULL OR ISDELETED = 0) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(strWhere);
            }
            return DbHelperMySQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表（关联）
        /// </summary>
        public DataSet GetListUnion(Dictionary<string, string> dicCondition, string sortExpression, int pageIndex, int pageSize, out int totalAmount)
        {
            StringBuilder sbWhere = new StringBuilder(" where (ISDELETED IS NULL OR ISDELETED = 0) ");
            foreach (KeyValuePair<string, string> pair in dicCondition)
            {
                if (!pair.Key.Contains("Start") && !pair.Key.Contains("End") && !pair.Key.Contains("Max") && !pair.Key.Contains("Min")
                    && pair.Key != "finishedPerson" && pair.Key != "finishedStatus" && pair.Key != "enteringPerson")
                {
                    sbWhere.AppendFormat(" and {0} like '%{1}%'", pair.Key, pair.Value);
                }
            }
            if (dicCondition.ContainsKey("expireDateStart"))
            {
                sbWhere.AppendFormat(" and expireDate >= '{0}'", dicCondition["expireDateStart"]);
            }
            if (dicCondition.ContainsKey("expireDateEnd"))
            {
                sbWhere.AppendFormat(" and expireDate <= '{0}'", dicCondition["expireDateEnd"]);
            }
            if (dicCondition.ContainsKey("orderDateStart"))
            {
                sbWhere.AppendFormat(" and orderDate >= '{0}'", dicCondition["orderDateStart"]);
            }
            if (dicCondition.ContainsKey("orderDateEnd"))
            {
                sbWhere.AppendFormat(" and orderDate <= '{0}'", dicCondition["orderDateEnd"]);
            }
            if (dicCondition.ContainsKey("constructionAreaMin"))
            {
                sbWhere.AppendFormat(" and constructionArea >= {0}", dicCondition["constructionAreaMin"]);
            }
            if (dicCondition.ContainsKey("constructionAreaMax"))
            {
                sbWhere.AppendFormat(" and constructionArea <= {0}", dicCondition["constructionAreaMax"]);
            }
            if (dicCondition.ContainsKey("orderAmountMin"))
            {
                sbWhere.AppendFormat(" and orderAmount >= {0}", dicCondition["orderAmountMin"]);
            }
            if (dicCondition.ContainsKey("orderAmountMax"))
            {
                sbWhere.AppendFormat(" and orderAmount <= {0}", dicCondition["orderAmountMax"]);
            }
            if (dicCondition.ContainsKey("floorsMin"))
            {
                sbWhere.AppendFormat(" AND floors >= {0}", dicCondition["floorsMin"]);
            }
            if (dicCondition.ContainsKey("floorsMax"))
            {
                sbWhere.AppendFormat(" AND floors <= {0}", dicCondition["floorsMax"]);
            }
            //if (dicCondition.ContainsKey("finishedPerson"))
            //{
            //    sbWhere.AppendFormat(" AND finishedperson IN (SELECT ID from employee WHERE EMPLOYEENO like '%{0}%')",dicCondition["finishedPerson"]);
            //}
            if (dicCondition.ContainsKey("isfinished"))
            {
                sbWhere.AppendFormat(" AND isfinished= {0}", dicCondition["isfinished"]);
            }
            if (dicCondition.ContainsKey("enteringPerson"))
            {
                sbWhere.AppendFormat(" AND enteringPerson IN (SELECT ID FROM employee WHERE EMPLOYEENO LIKE '%{0}%')", dicCondition["enteringPerson"]);
            }
            totalAmount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select 
                        p.ID,p.REFERRER,p.CASHBACK,emp.employeeno finishedperson,dp.configvalue SHOP,
                        zffs.configvalue PAYMENTMETHOD,
                        SHOP SHOP_,TRANSACTIONSTATUS TRANSACTIONSTATUS_,PAYMENTMETHOD PAYMENTMETHOD_,

                        p.ORDERDATE,p.WANGWANGNAME,p.TASKNO,p.EXPIREDATE,p.TIMENEEDED,p.ORDERAMOUNT,p.QQ,
                        p.MOBILEPHONE,p.EMAIL,p.isFinished,jyzt.configvalue TRANSACTIONSTATUS,p.REFUND,p.REMARKS,p.ISCREATEDFOLDER,
                        re.PROVINCENAME,jjrj.configvalue VALUATESOFTWARE,
                        zylb.configvalue SPECIALTYCATEGORY,jmrj.configvalue MODELINGSOFTWARE,
                        jjms.configvalue VALUATEMODE,p.EXTRAREQUIREMENT,
                        jzlx.configvalue BUILDINGTYPE,p.PROJECTNAME,p.CONSTRUCTIONAREA,
                        jglx.configvalue STRUCTUREFORM,p.FLOORS

                        from project p
                        LEFT JOIN 
                        (select configkey,configvalue from configvalue c where configtypeid=(select configtypeid from configtype WHERE configtypeName='店铺编号名称')) dp
                        on p.SHOP=dp.configkey
                        left JOIN
                        (select configkey,configvalue from configvalue c where configtypeid=(select configtypeid from configtype WHERE configtypeName='计价模式')) jjms
                        on p.VALUATEMODE=jjms.configkey
                        LEFT JOIN
                        (SELECT CODE procode,NAME PROVINCENAME from region) re
                        on p.PROVINCE=re.procode
                        left JOIN
                        (select configkey,configvalue from configvalue c where configtypeid=(select configtypeid from configtype WHERE configtypeName='建模软件')) jmrj
                        on p.MODELINGSOFTWARE=jmrj.configkey
                        left JOIN
                        (select configkey,configvalue from configvalue c where configtypeid=(select configtypeid from configtype WHERE configtypeName='计价软件')) jjrj
                        on p.VALUATESOFTWARE=jjrj.configkey
                        left JOIN
                        (select configkey,configvalue from configvalue c where configtypeid=(select configtypeid from configtype WHERE configtypeName='结构类型')) jglx
                        on p.STRUCTUREFORM=jglx.configkey
                        left JOIN
                        (select configkey,configvalue from configvalue c where configtypeid=(select configtypeid from configtype WHERE configtypeName='建筑类型')) jzlx
                        on p.BUILDINGTYPE=jzlx.configkey
                        left JOIN
                        (select configkey,configvalue from configvalue c where configtypeid=(select configtypeid from configtype WHERE configtypeName='专业类别')) zylb
                        on p.SPECIALTYCATEGORY=zylb.configkey
                        left JOIN
                        (select configkey,configvalue from configvalue c where configtypeid=(select configtypeid from configtype WHERE configtypeName='交易状态')) jyzt
                        on p.TRANSACTIONSTATUS=jyzt.configkey
                        left JOIN
                        (select configkey,configvalue from configvalue c where configtypeid=(select configtypeid from configtype WHERE configtypeName='支付方式')) zffs
                        on p.PAYMENTMETHOD=zffs.configkey");

            //LEFT JOIN
            //(select ID,employeeno from employee) emp
            //on p.FINISHEDPERSON = emp.ID");

            //LEFT JOIN 
            //(SELECT FINISHEDPERSON,PROJECTID FROM projectsharing WHERE
            // finishedperson IN (SELECT ID from employee WHERE EMPLOYEENO like '%{0}%')) PS
            //ON P.ID = PS.PROJECTID

            //LEFT JOIN 
            //(select ID,employeeno from employee) emp
            // on PS.FINISHEDPERSON = emp.ID");

            //2017-04-13 11-00-42，Edit by Wang Yongkun，因优化了任务的多人完成功能，故修改完成人的关联查询
            if (dicCondition.ContainsKey("finishedPerson"))
            {
                strSql.AppendFormat(@" INNER JOIN 
                    (SELECT FINISHEDPERSON,PROJECTID FROM projectsharing WHERE
                     finishedperson IN (SELECT ID from employee WHERE EMPLOYEENO like '%{0}%')) PS
                    ON P.ID = PS.PROJECTID", dicCondition["finishedPerson"]);
            }
            else
            {
                strSql.Append(@" LEFT JOIN
                    (
                        SELECT id,projectid,finishedperson,proportion,CREATEdate
                        FROM projectsharing
                        GROUP BY projectid
                    ) PS
                    ON P.ID = PS.PROJECTID");
            }
            strSql.Append(@" LEFT JOIN 
                    (select ID,employeeno from employee) emp
                     on PS.FINISHEDPERSON = emp.ID");
            //2017-04-13 11-00-42 End

            strSql.Append(sbWhere);

            /*
            //待用
            strSql.Append(" ORDER BY ");
            //pageSize为0，不进行分页，以获取全部数据
            if (pageSize == 0)
            {
                //排序表达式为空，默认按创建时间排序
                if (string.IsNullOrEmpty(sortExpression))
                {
                    strSql.Append(" CREATEDATE DESC ");
                }
                //否则按排序表达式进行排序
                else
                {
                    strSql.Append(sortExpression);
                }
            }
            //否则分页获取
            else
            {
                //获取记录总数，供分页控件计算使用
                DataSet dsRowsCount = MySqlHelper.GetDataSet(strSql.ToString());
                if (dsRowsCount != null && dsRowsCount.Tables.Count > 0)
                {
                    totalAmount = dsRowsCount.Tables[0].Rows.Count;
                }
                //排序表达式为空，默认按创建时间排序
                if (string.IsNullOrEmpty(sortExpression))
                {
                    strSql.Append(" CREATEDATE DESC ");
                }
                //否则按排序表达式进行排序
                else
                {
                    strSql.Append(sortExpression);
                }
                strSql.Append(" LIMIT {0},{1}", (pageIndex - 1) * pageSize, pageSize);
            }
             * */

            #region Old         //wait for discard
            //PageSize为0时不分页
            if (pageSize == 0)
            {
                strSql.Append(" ORDER BY ORDERDATE DESC ");
            }
            else
            {
                DataSet dsRowsCount = DbHelperMySQL.Query(strSql.ToString());
                if (dsRowsCount != null && dsRowsCount.Tables.Count > 0)
                {
                    totalAmount = dsRowsCount.Tables[0].Rows.Count;
                }
                strSql.AppendFormat(" ORDER BY ORDERDATE DESC LIMIT {0},{1}", (pageIndex - 1) * pageSize, pageSize);
            }
            #endregion
            return DbHelperMySQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM project ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperMySQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /*/// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.ID desc");
            }
            strSql.Append(")AS Row, T.*  from project T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperMySQL.Query(strSql.ToString());
        }*/

        /*
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        {
            MySqlParameter[] parameters = {
                    new MySqlParameter("@tblName", MySqlDbType.VarChar, 255),
                    new MySqlParameter("@fldName", MySqlDbType.VarChar, 255),
                    new MySqlParameter("@PageSize", MySqlDbType.Int32),
                    new MySqlParameter("@PageIndex", MySqlDbType.Int32),
                    new MySqlParameter("@IsReCount", MySqlDbType.Bit),
                    new MySqlParameter("@OrderType", MySqlDbType.Bit),
                    new MySqlParameter("@strWhere", MySqlDbType.VarChar,1000),
                    };
            parameters[0].Value = "project";
            parameters[1].Value = "ID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperMySQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod
        #region  ExtensionMethod

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool LogicDelete(string ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update project set ISDELETED = 1");
            strSql.Append(" where ID=@ID ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.VarChar,40)			};
            parameters[0].Value = ID;

            int rows = DbHelperMySQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获取完成人
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        public string GetFinishedPerson(string projectID)
        {
            string sql = string.Format("select e.employeeno from project p LEFT join employee e ON p.finishedperson=e.ID WHERE P.ID='{0}'", projectID);
            DataSet ds = DbHelperMySQL.Query(sql);
            string employeeNO = string.Empty;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                employeeNO = ds.Tables[0].Rows[0][0].ToString();
            }
            return employeeNO;
        }

        /// <summary>
        /// 获取完成稿
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        public DataTable GetFinalScript(string projectID)
        {
            //            string sql = string.Format(@"SELECT P.ID,TASKNO,EMPLOYEENO FROM PROJECT P
            //                                INNER JOIN EMPLOYEE E 
            //                                ON P.FINISHEDPERSON = E.ID
            //                                WHERE ISFINISHED=1 AND P.ID='{0}'", projectID);
            string sql = string.Format(@"SELECT p.id,taskno,employeeno,ps.finishedperson from project p
                    INNER JOIN projectsharing ps
                    on p.id = ps.projectid
                    INNER JOIN employee e
                    on ps.finishedperson = e.id
                    WHERE p.isfinished = 1 AND p.id = '{0}'", projectID);
            DataSet ds = DbHelperMySQL.Query(sql);
            return ds.Tables[0];
        }

        /// <summary>
        /// 根据任务ID和完成人ID获取完成稿
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="finishedPerson">完成人ID</param>
        /// <returns></returns>
        public DataTable GetFinalScript(string projectID, string finishedPerson)
        {
            //            string sql = string.Format(@"SELECT P.ID,TASKNO,EMPLOYEENO FROM PROJECT P
            //                                INNER JOIN EMPLOYEE E 
            //                                ON P.FINISHEDPERSON = E.ID
            //                                WHERE ISFINISHED=1 AND P.ID='{0}'", projectID);
            string sql = string.Format(@"SELECT p.id,taskno,employeeno,ps.finishedperson from project p
                    INNER JOIN projectsharing ps
                    on p.id = ps.projectid
                    INNER JOIN employee e
                    on ps.finishedperson = e.id
                    WHERE p.isfinished = 1 AND p.id = '{0}' AND PS.FINISHEDPERSON = '{1}'", projectID, finishedPerson);
            DataSet ds = DbHelperMySQL.Query(sql);
            return ds.Tables[0];
        }

        /// <summary>
        /// 根据任务ID获取员工编号和任务名
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        public DataTable GetEmployeeNoAndTaskNo(string projectID)
        {
            string sql = string.Format(@"select employeeno,taskno from project P
                             inner JOIN employee E
                             ON p.FINISHEDPERSON = e.ID
                             AND P.ID = '{0}'", projectID);
            DataSet ds = DbHelperMySQL.Query(sql);
            return ds.Tables[0];
        }

        /// <summary>
        /// 根据任务ID获取修改记录稿
        /// </summary>
        /// <param name="projectModifyID">任务</param>
        /// <returns></returns>
        public DataTable GetProjectModifyByPrjID(string projectID)
        {
            string sql = string.Format(@"SELECT pm.ID,pm.foldername,pm.ISUPLOADATTACH,pm.REVIEWSTATUS,pm.ISFINISHED,p.taskno,e.employeeno
                             FROM projectmodify pm 
                             inner JOIN project p
                             on pm.projectid=p.id
                                INNER JOIN projectsharing ps
                                on pm.projectid = ps.projectid
                             INNER JOIN employee e
                             on ps.finishedperson=e.id
                             WHERE p.ID='{0}' Order by pm.foldername", projectID);
            DataSet ds = DbHelperMySQL.Query(sql);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0];
            }
            return null;
        }

        /// <summary>
        /// 根据修改记录的ID获取修改记录稿
        /// </summary>
        /// <param name="projectModifyID"></param>
        /// <returns></returns>
        public DataTable GetProjectModifyByModifyID(string projectModifyID)
        {
            string sql = string.Format(@"SELECT pm.ID,pm.foldername,pm.ISFINISHED,p.taskno,e.employeeno
                             FROM projectmodify pm 
                             inner JOIN project p
                             on pm.projectid = p.id
                             INNER JOIN projectsharing ps
                             on pm.projectid = ps.projectid
                             INNER JOIN employee e
                             on ps.finishedperson = e.id
                             WHERE pm.ID='{0}'", projectModifyID);
            DataSet ds = DbHelperMySQL.Query(sql);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0];
            }
            return null;
        }

        /// <summary>
        /// 删除售后任务
        /// </summary>
        /// <param name="ID">售后任务ID</param>
        /// <returns></returns>
        public bool DeleteProjectModifyTask(string ID)
        {
            string sql = string.Format("DELETE FROM PROJECTMODIFY WHERE ID='{0}'", ID);
            int r = DbHelperMySQL.ExecuteSql(sql);
            return r > 0;
        }

        /// <summary>
        /// 判断待审核的修改任务是否存在
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        public bool IsExistModifyTaskWaitforReview(string projectID)
        {
            string sql = string.Format("SELECT count(*) from projectmodify pm WHERE pm.projectid='{0}' and reviewstatus='0'", projectID);
            DataSet ds = DbHelperMySQL.Query(sql);
            int count = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                count = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
            }
            return count > 0;
        }
        #endregion  ExtensionMethod
    }
}
