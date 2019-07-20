using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TipService.DAL;
using TipService.Model;

namespace TipService.BLL
{
    public class EmployeeBLL
    {
        EmployeeDAL eDal = new EmployeeDAL();

        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="strSort"></param>
        /// <returns></returns>
        public DataSet GetList(string strWhere, string strSort)
        {
            return eDal.GetList(strWhere, strSort);
        }
    }
}
