using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using TipService.DAL;

namespace TipService.BLL
{
    public class TaskRemindingBLL
    {
        TaskRemindingDAL trDal = new TaskRemindingDAL();

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return trDal.GetList(strWhere);
        }

        /// <summary>
        /// 将消息置为已读
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="taskType"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        public bool SetIsReminding(string userId, string taskFolder, string modifyFolder, string taskType, string userType)
        {
            return trDal.SetIsReminded(userId, taskFolder, modifyFolder, taskType, userType);
        }
    }
}
