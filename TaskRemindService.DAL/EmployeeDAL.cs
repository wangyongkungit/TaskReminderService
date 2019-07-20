using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TipService.DAL
{
    public class EmployeeDAL
    {
        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="strSort"></param>
        /// <returns></returns>
        public DataSet GetList(string strWhere, string strSort)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,EMPLOYEENO,TYPE,DINGTALKUSERID ");
            //PASSWORD,SEX,BIRTHDATE,NAME,NATIVEPLACE,MOBILEPHONE,ADDRESS,DEPARTMENTID,EMAIL,BANKCARD,AVAILABLE,POLITICALSTATUS,
            strSql.Append(" FROM employee ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where AIVILABLE = 1 ");
            }
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(strWhere);
            }
            if (!string.IsNullOrEmpty(strSort.Trim()))
            {
                strSql.Append(" ORDER BY " + strSort);
            }
            return DbHelperMySQL.Query(strSql.ToString());
        }

        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        ///// <param name="strWhere"></param>
        ///// <param name="strSort"></param>
        ///// <returns></returns>
        //public DataSet GetList(string strWhere, string strSort)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("select ID,EMPLOYEENO,PASSWORD,NAME,SEX,BIRTHDATE,NATIVEPLACE,MOBILEPHONE,ADDRESS,EMAIL,BANKCARD,DEPARTMENTID,POLITICALSTATUS,TYPE,AVAILABLE,DINGTALKUSERID ");
        //    strSql.Append(" FROM employee ");
        //    if (strWhere.Trim() != "")
        //    {
        //        strSql.Append(" where AIVILABLE = 1 ");
        //    }
        //    if (!string.IsNullOrEmpty(strWhere))
        //    {
        //        strSql.Append(strWhere);
        //    }
        //    if (!string.IsNullOrEmpty(strSort.Trim()))
        //    {
        //        strSql.Append(" ORDER BY " + strSort);
        //    }
        //    return DbHelperMySQL.Query(strSql.ToString());
        //}
    }
}
