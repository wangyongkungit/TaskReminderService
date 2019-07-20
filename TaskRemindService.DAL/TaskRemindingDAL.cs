using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace TipService.DAL
{
    public class TaskRemindingDAL
    {
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,EMPLOYEENO,FOLDER,MODIFYFOLDER,ISREMINDED,CREATEDATE,EXPIREDATE,ISFINISHED,TASKTYPE,ENTERINGPERSON,TOUSERTYPE,SENTCOUNT ");
            strSql.Append(" FROM taskreminding ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperMySQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 将消息置为已读
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="modifyFolder"></param>
        /// <param name="taskFolder"></param>
        /// <param name="taskType"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        public bool SetIsReminded(string userId, string taskFolder, string modifyFolder, string taskType, string userType)
        {
            //StringBuilder sbSql = new StringBuilder();
            //if (userType == "0")
            //{
            //    sbSql.AppendFormat(@"UPDATE TASKREMINDING SET ISREMINDED = 1 WHERE EMPLOYEENO = '{0}' and taskType = '{1}' and folder = '{2}'", userId, taskType, taskFolder);
            //}
            //else
            //{
            //    sbSql.AppendFormat(@"UPDATE TASKREMINDING SET ISREMINDED = 1 WHERE enteringPerson = '{0}' and taskType = '{1}' and folder = '{2}'", userId, taskType, taskFolder);
            //}
            //if (!string.IsNullOrEmpty(modifyFolder))
            //{
            //    sbSql.AppendFormat(" AND MODIFYFOLDER = '{0}'", modifyFolder);
            //}
            //else
            //{
            //    sbSql.Append(" AND (MODIFYFOLDER IS NULL OR MODIFYFOLDER = ''");
            //}
            ////string updateSql = string.Format(@"delete from taskreminding where ENTERINGPERSON = '{0}' and TASKTYPE = '{1}' and userType = '{2}'", userId, taskType, userType);
            //int rows = MySqlHelper.ExecuteNonQuery(sbSql.ToString());
            //return rows > 0;

            StringBuilder sbSql = new StringBuilder();
            if (userType == "2")
            {
                sbSql.AppendFormat(@"UPDATE TASKREMINDING SET ISREMINDED = 1 WHERE EMPLOYEENO = '{0}' and taskType = '{1}' and folder = '{2}' AND TOUSERTYPE = {3}", userId, taskType, taskFolder, userType);
            }
            else if (userType == "1")
            {
                sbSql.AppendFormat(@"UPDATE TASKREMINDING SET ISREMINDED = 1 WHERE enteringPerson = '{0}' and taskType = '{1}' and folder = '{2}' AND TOUSERTYPE = {3}", userId, taskType, taskFolder, userType);
            }
            else if (userType == "0")
            {
                sbSql.AppendFormat(@"UPDATE TASKREMINDING SET ISREMINDED= 1 WHERE tasktype = '{0}' AND FOLDER = '{1}' AND TOUSERTYPE = '{2}'", taskType, taskFolder, userType);
            }
            if (!string.IsNullOrEmpty(modifyFolder))
            {
                sbSql.AppendFormat(" AND MODIFYFOLDER = '{0}'", modifyFolder);
            }
            else
            {
                sbSql.Append(" AND (MODIFYFOLDER IS NULL OR MODIFYFOLDER = '')");
            }
            //using (StreamWriter sw = new StreamWriter(System.AppDomain.CurrentDomain.BaseDirectory + "\\log.txt", true, Encoding.UTF8))
            //{
            //    sw.Write(DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss] ") + sbSql.ToString());
            //}
            //string updateSql = string.Format(@"delete from taskreminding where ENTERINGPERSON = '{0}' and TASKTYPE = '{1}' and userType = '{2}'", userId, taskType, userType);
            int rows = DbHelperMySQL.ExecuteSql(sbSql.ToString());
            return rows > 0;
        }
    }
}
