using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using TipService.DAL;

namespace TipService.BLL
{
    public class ProjectBLL
    {
        ProjectDAL pDal=new ProjectDAL();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet GetListSimpleUnion(string strWhere)
        {
            return pDal.GetListSimpleUnion(strWhere);
        }
    }
}
