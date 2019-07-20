using NAudio.Wave;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Media;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TipService;
using TipService.BLL;
using TipService.CommonHelper;
using TipService.Model;

namespace TipService
{
    /// <summary>
    /// 说明：调用钉钉消息发送接口对员工进行新任务及完成稿等相关消息的提醒
    /// 作者：Wang Yongkun
    /// 操作：2017-05-18 09-36-12 创建
    /// </summary>
    public partial class TipService : ServiceBase
    {
        public TipService()
        {
            InitializeComponent();
        }

        //定时器
        System.Timers.Timer timer;
        EmployeeBLL eBll = new EmployeeBLL();
        TaskRemindingBLL trBll = new TaskRemindingBLL();

        protected override void OnStart(string[] args)
        {
            try
            {
                LogHelper.WriteLine("服务启动");
                timer = new System.Timers.Timer();
                //间隔
                int interval = Convert.ToInt32(ConfigurationManager.AppSettings["interval"]) * 1000;
                timer.Interval = interval;
                timer.Elapsed += timer_Elapsed;
                timer.Enabled = true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// 定时器事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                ////员工编号
                //string employeeNo = ConfigurationManager.AppSettings["employeeNo"];
                ////循环执行，taskType参数分别为0和1
                //for (int i = 0; i < 2; i++)
                //{
                //    object[] arg = { (object)"{\"userNo\":\"" + employeeNo + "\",\"taskType\":\"" + i + "\"}" };
                //    InvokeWebService(arg, i.ToString());
                //}

                //需提醒的任务列表DataSet
                DataSet dsRmd = trBll.GetList("ISREMINDED = 0 AND DATE_SUB(CURDATE(), INTERVAL 100 DAY) <= date(CREATEDATE)");
                if (dsRmd != null && dsRmd.Tables.Count > 0 && dsRmd.Tables[0].Rows.Count > 0)
                {
                    //需要提醒的任务DataTable
                    DataTable dtRmd = dsRmd.Tables[0];
                    StringBuilder sbTaskNos = new StringBuilder("'',");
                    //遍历去除需要提醒的任务的编号
                    for (int i = 0; i < dtRmd.Rows.Count; i++)
                    {
                        sbTaskNos.AppendFormat("'{0}',", dtRmd.Rows[i]["FOLDER"]);
                    }
                    //LogHelper.WriteLine("需要提醒的任务：" + sbTaskNos);
                    ProjectBLL pBll = new ProjectBLL();
                    //根据任务编号获取相应的任务列表
                    DataSet dsProject = pBll.GetListSimpleUnion(string.Format(" AND TASKNO IN ({0})", sbTaskNos.ToString().TrimEnd(',')));
                    if (dsProject == null || dsProject.Tables.Count == 0 || dsProject.Tables[0].Rows.Count == 0)
                    {
                        return;
                    }
                    DataTable dtProject = dsProject.Tables[0];

                    DataSet dsEmp = eBll.GetList(string.Empty, "EMPLOYEENO");
                    //获取所有员工列表
                    DataTable dtEmployees = dsEmp.Tables[0];

                    //需要抄送的员工的钉钉UserId
                    string externalDingdingUserId = Convert.ToString(ConfigurationManager.AppSettings["externalDingdingUserId"]);
                    // 任务详细页面地址
                    string taskDetailsPageUrl = Convert.ToString(ConfigurationManager.AppSettings["taskDetailsPageUrl"]);

                    //遍历员工列表
                    for (int i = 0; i < dtEmployees.Rows.Count; i++)
                    {
                        //用户ID
                        string userId = dtEmployees.Rows[i]["ID"].ToString();
                        //用户在钉钉系统中的UserId
                        string dingtalkUserId = dtEmployees.Rows[i]["DINGTALKUSERID"].ToString();
                        //钉钉UserId为空，就没有继续执行的必要了，直接执行下一个员工
                        if (string.IsNullOrEmpty(dingtalkUserId))
                        {
                            continue;
                        }
                        //员工编号
                        string empNo = dtEmployees.Rows[i]["EMPLOYEENO"].ToString();
                        //用户类型
                        string userType = dtEmployees.Rows[i]["TYPE"].ToString();// ConfigurationManager.AppSettings["userType"];
                        //筛选条件
                        string strDtRmdSelect = string.Empty;
                        //用户类型为1，是客服，当时插入数据时录入的是员工ID
                        if (userType == "1")
                        {
                            strDtRmdSelect = string.Format(" enteringPerson = '{0}' AND TOUSERTYPE = '{1}'", userId, userType);
                        }
                        //用户类型为2，是造价员，当时插入数据时录入的是员工编号
                        else if (userType == "2")
                        {
                            strDtRmdSelect = string.Format(" employeeNo = '{0}' AND TOUSERTYPE = '{1}'", empNo, userType);
                        }
                        //用户类型为0，是针对管理员进行的提醒
                        else if (userType == "0")
                        {
                            strDtRmdSelect = string.Format(" TOUSERTYPE = '{0}'", userType);
                        }
                        //客服和造价员均不是，就不提醒了
                        else
                        {
                            continue;
                        }
                        DataRow[] drRmd = dtRmd.Select(strDtRmdSelect);
                        //对任务提醒列表进行遍历
                        for (int drEmpRmdIndex = 0 /*针对某个员工的提醒索引*/; drEmpRmdIndex < drRmd.Length; drEmpRmdIndex++)
                        {
                            string taskType = drRmd[drEmpRmdIndex]["taskType"].ToString();
                            //提醒消息文本
                            string remindMsg = string.Empty;
                            string taskNo = drRmd[drEmpRmdIndex]["folder"].ToString();
                            string finishedEmpNo = drRmd[drEmpRmdIndex]["EMPLOYEENO"].ToString();
                            string kefuEmpId = Convert.ToString(drRmd[drEmpRmdIndex]["ENTERINGPERSON"]);
                            string wangwangName = string.Empty;
                            string shopName = string.Empty;
                            string projectId = string.Empty;
                            DataRow[] drProject = dtProject.Select(string.Format(" TASKNO = '{0}'", taskNo));
                            if (drProject.Length > 0)
                            {
                                wangwangName = drProject[0]["wangwangName"].ToString();
                                shopName = drProject[0]["shop"].ToString();
                                projectId = drProject[0]["ID"].ToString();
                            }
                            drProject = null;

                            //客服。如果是客服的话，是收不到任务类型为2（即任务倒计时）提醒的。虽然在数据插入时已基本杜绝这种条件同时成立，但这里还是在条件中限制一下。
                            if (userType == "1" && taskType != "2")
                            {
                                // 客服需接收的提醒类型有：普通任务的完成提醒、售后修改任务的完成提醒、新的疑问提醒和疑问答复提醒共 4 种
                                //普通任务
                                if (taskType == "0")
                                {
                                    remindMsg = string.Format("{0}\n完成人：{1}\n客户ID：{2}\nStore ID：{3}",
                                        taskNo, finishedEmpNo, wangwangName, shopName);
                                }
                                //售后任务
                                else if (taskType == "1")
                                {
                                    string modifyFolder = drRmd[drEmpRmdIndex]["MODIFYFOLDER"].ToString();
                                    remindMsg = string.Format("{0}的{1}\n完成人：{2}\n客户ID：{3}\nStore ID：{4}",
                                        taskNo, modifyFolder, finishedEmpNo, wangwangName, shopName);

                                }
                                // 新的疑问
                                else if (taskType == "4")
                                {
                                    string questionFolder = drRmd[drEmpRmdIndex]["MODIFYFOLDER"].ToString();
                                    remindMsg = string.Format("【新疑问】\n任务：{0}的{1}\n提问人：客服",
                                        taskNo, questionFolder);
                                }
                                // 疑问答复
                                else if (taskType == "5")
                                {
                                    string questionFolder = drRmd[drEmpRmdIndex]["MODIFYFOLDER"].ToString();
                                    remindMsg = string.Format("【新答复】\n任务：{0}的{1}\n答复人：{2}",
                                        taskNo, questionFolder, finishedEmpNo);
                                }
                                //客服和造价员均不是，就不提醒了
                                else
                                {
                                    continue;
                                }
                                // 在这里，录入一条动态到客服的动态列表中
                                try
                                {
                                    if (!string.IsNullOrEmpty(remindMsg))
                                    {
                                        TaskTrendBLL ttBll = new TaskTrendBLL();
                                        TaskTrend trend = new TaskTrend();
                                        trend.ID = Guid.NewGuid().ToString();
                                        trend.PROJECTID = ttBll.GetProjectIDByTaskNo(taskNo);
                                        trend.EMPLOYEEID = kefuEmpId;// ttBll.GetEmployeeIDByEmployeeNo(empNo);
                                        trend.DESCRIPTION = remindMsg;
                                        trend.CREATEDATE = DateTime.Now;
                                        trend.READSTATUS = false;
                                        // 客服类型
                                        trend.TYPE = 2;
                                        ttBll.Add(trend);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    LogHelper.WriteLine("添加任务" + taskNo + "动态失败:" + ex.Message + ex.StackTrace);
                                }
                            }
                            //员工（造价员）
                            else if (userType == "2")
                            {
                                //LogHelper.Write("工程师");
                                // 工程师需接收的提醒有：普通任务（即新任务）提醒、售后任务（即新的修改）提醒、新的疑问和疑问答复共 4 种
                                string taskTrendMsg = string.Empty;
                                //普通任务
                                if (taskType == "0")
                                {
                                    remindMsg = string.Format("【新任务提醒】\r\n{0}，您有一个新任务：{1}", empNo, taskNo);
                                    taskTrendMsg = string.Format("【新任务】{0}", taskNo);
                                }
                                //售后任务
                                else if (taskType == "1")
                                {
                                    string modifyFolder = drRmd[drEmpRmdIndex]["MODIFYFOLDER"].ToString();
                                    remindMsg = string.Format("【修改任务提醒】\r\n{0}，您有一个新的修改任务：{1}的{2}", empNo, taskNo, modifyFolder);
                                    taskTrendMsg = string.Format("【新修改任务】{0}的{1}", taskNo, modifyFolder);
                                    if (!string.IsNullOrEmpty(Convert.ToString(drRmd[drEmpRmdIndex]["SENTCOUNT"])))
                                    {
                                        int sentCount = Convert.ToInt32(drRmd[drEmpRmdIndex]["SENTCOUNT"]);
                                        if (sentCount > 0)
                                        {
                                            remindMsg += string.Format("（客服{0}次提醒）", sentCount);
                                            taskTrendMsg += string.Format("（客服{0}次提醒）", sentCount);
                                        }
                                    }
                                }
                                else if (taskType == "2")
                                {
                                    remindMsg = string.Format("【任务预警】\r\n{0}，任务{1}距交稿时间已不足3小时", empNo, taskNo);
                                    taskTrendMsg = string.Format("【任务预警】任务{0}仅剩3小时", taskNo);
                                }
                                // 新的疑问
                                else if (taskType == "4")
                                {
                                    string questionFolder = drRmd[drEmpRmdIndex]["MODIFYFOLDER"].ToString();
                                    remindMsg = string.Format("【新疑问提醒】\r\n任务：{0}的{1}\r\n提问人：{2}",
                                        taskNo, questionFolder, finishedEmpNo);
                                    taskTrendMsg = string.Format("【新疑问】任务{0}的{1}", taskNo, questionFolder);
                                }
                                // 疑问答复
                                else if (taskType == "5")
                                {
                                    string questionFolder = drRmd[drEmpRmdIndex]["MODIFYFOLDER"].ToString();
                                    remindMsg = string.Format("【新的疑问答复】\r\n任务：{0}的{1}\r\n答复人：客服",
                                        taskNo, questionFolder);
                                    taskTrendMsg = $"【疑问答复】任务{taskNo}的{questionFolder}";
                                }
                                // 任务状态变更
                                else if (taskType == "6")
                                {
                                    string taskStatusUpdateMessage = drRmd[drEmpRmdIndex]["MODIFYFOLDER"].ToString();
                                    remindMsg = string.Format("【任务状态变更】\n任务{0} {1}", taskNo, taskStatusUpdateMessage);
                                    taskTrendMsg = remindMsg;
                                }
                                else
                                {
                                    continue;
                                }
                                // 在这里，录入一条动态到工程师主页的动态列表中
                                try
                                {
                                    if (!string.IsNullOrEmpty(taskTrendMsg))
                                    {
                                        TaskTrendBLL ttBll = new TaskTrendBLL();
                                        TaskTrend trend = new TaskTrend();
                                        trend.ID = Guid.NewGuid().ToString();
                                        trend.PROJECTID = ttBll.GetProjectIDByTaskNo(taskNo);
                                        trend.EMPLOYEEID = ttBll.GetEmployeeIDByEmployeeNo(empNo);
                                        trend.DESCRIPTION = taskTrendMsg;
                                        trend.CREATEDATE = DateTime.Now;
                                        // 工程师类型
                                        trend.TYPE = 1;
                                        ttBll.Add(trend);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    LogHelper.WriteLine("添加任务" + taskNo + "动态失败:" + ex.Message + ex.StackTrace);
                                }
                            }
                            //管理员，只接收任务待分配的提醒
                            else if (userType == "0")
                            {
                                //LogHelper.WriteLine("管理员");
                                // 3 是任务待分配的
                                if (taskType == "3")
                                {
                                    remindMsg = string.Format("【任务待分配提醒】任务{0}待分配", taskNo);
                                }
                                else
                                {
                                    continue;
                                }
                            }

                            #region 调用钉钉接口发送消息
                            //remindMsg = string.Empty;//测试时，置空
                            if (!string.IsNullOrEmpty(remindMsg))
                            {
                                //首先获取accessToken
                                string accessTokenResult = DingTalkHelper.GetAccessToken();
                                JObject jObj = JObject.Parse(accessTokenResult);
                                //返回码
                                string errcode = jObj["errcode"].ToString();
                                //accessToken
                                string accessToken = string.Empty;
                                //获取成功
                                if (errcode == "0")
                                {
                                    accessToken = jObj["access_token"].ToString();
                                    //发送消息接口的请求地址
                                    string postUrl = string.Format("https://oapi.dingtalk.com/message/send?access_token={0}", accessToken);
                                    //钉钉企业应用id，这个值代表以哪个应用的名义发送消息
                                    string agentid = ConfigurationManager.AppSettings["agentid"];

                                    //定义接收者
                                    string toUser = dingtalkUserId;
                                    //如果额外的接收者不为空
                                    if (!string.IsNullOrEmpty(externalDingdingUserId))
                                    {
                                        toUser += externalDingdingUserId;
                                    }
                                    //如果任务类型为3，说明是新任务待分配提醒
                                    if (taskType == "3")
                                    {
                                        toUser = Convert.ToString(ConfigurationManager.AppSettings["taskAllotmentTipDingdingUserId"]);
                                    }

                                    string param = "";

                                    if (userType == "2" || userType == "0")
                                    {
                                        param = "{\"touser\": \"" + toUser + "\", \"msgtype\": \"text\", \"agentid\": \"" + agentid + "\",\"text\":{" +
                                                      "\"content\": \"" + remindMsg + "\"}}";
                                    }
                                    else if (userType == "1")
                                    {
                                        param = "{\"touser\": \"" + toUser + "\", \"msgtype\": \"link\", \"agentid\": \"" + agentid + "\",\"link\":{" +
                                                     "\"messageUrl\": \""+ taskDetailsPageUrl + projectId + "\", \"picUrl\":\"@lALOACZwe2Rk\", \"title\":\"提醒\",  \"text\": \"" + remindMsg + "\"}}";
                                    }
                                    LogHelper.WriteLine(param);
                                    //接口返回结果
                                    object msgSendResult = WebServiceHelper.Post(postUrl, param);
                                    //将Object类型结果转换为JObject
                                    JObject jsonSendResult = JObject.Parse(msgSendResult.ToString());
                                    //返回码
                                    errcode = jsonSendResult["errcode"].ToString();
                                    //返回码为0，发送成功
                                    if (errcode == "0")
                                    {
                                        LogHelper.WriteLine("发送至" + empNo + "成功");
                                        string userIdOrEmpNo = string.Empty;
                                        //userType为1，是客服
                                        if (userType == "1")
                                        {
                                            userIdOrEmpNo = userId;
                                        }
                                        //userType为2，是工程师
                                        else if (userType == "2")
                                        {
                                            userIdOrEmpNo = empNo;
                                        }
                                        //将消息置为已读
                                        bool setIsReminded = trBll.SetIsReminding(userIdOrEmpNo, taskNo, drRmd[drEmpRmdIndex]["MODIFYFOLDER"].ToString(), taskType, userType);
                                        if (!setIsReminded)
                                        {
                                            LogHelper.WriteLine("数据库设置消息已读失败,任务编号:" + taskNo);
                                        }
                                    }
                                    else
                                    {
                                        LogHelper.WriteLine("发送至" + empNo + "失败。errcode：" + errcode + "，errmsg：" + jsonSendResult["errmsg"]);
                                    }
                                }
                                else
                                {
                                    string errmsg = jObj["errmsg"].ToString();
                                    LogHelper.WriteLine("access_token获取失败，errmsg：" + errmsg);
                                }
                            }
                            #endregion

                            ////循环执行，taskType参数分别为0、1和2，分别表示普通任务、售后任务和倒计时1小时这三种提醒
                            //for (int j = 0; j < 3; j++)
                            //{
                            //    //拼接参数，用户ID、任务类型和用户类型
                            //    object[] arg = { (object)"{\"userId\":\"" + userId + "\",\"taskType\":\"" + j + "\",\"userType\":\"" + userType + "\"}" };
                            //    if (userType == "0")
                            //    {
                            //        InvokeWebService(arg, j.ToString());
                            //    }
                            //    else if (userType == "1" && j < 2)//i小于2，防止客服也收到倒计时提醒
                            //    {
                            //        InvokeWebServiceCustomerRemind(arg, j.ToString());
                            //    }
                            //}
                        }
                        drRmd = null;
                    }
                    dtEmployees = null;
                    dsEmp = null;
                    dtProject = null;
                    dsProject = null;
                    dtRmd = null;
                }
                dsRmd = null;

                ////录入人ID
                //string userId = ConfigurationManager.AppSettings["userId"];
                ////用户类型
                //string userType = ConfigurationManager.AppSettings["userType"];
                ////循环执行，taskType参数分别为0、1和2，分别表示普通任务、售后任务和倒计时1小时这三种提醒
                //for (int i = 0; i < 3; i++)
                //{
                //    //拼接参数，用户ID、任务类型和用户类型
                //    object[] arg = { (object)"{\"userId\":\"" + userId + "\",\"taskType\":\"" + i + "\",\"userType\":\"" + userType + "\"}" };
                //    if (userType == "0")
                //    {
                //        InvokeWebService(arg, i.ToString());
                //    }
                //    else if (userType == "1" && i < 2)//i小于2，防止客服也收到倒计时提醒
                //    {
                //        InvokeWebServiceCustomerRemind(arg, i.ToString());
                //    }
                //}
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message + ex.StackTrace);
            }
            finally
            {
                SetWorkingSet(750000);
            }
        }

        #region 获取员工的提醒
        ///// <summary>
        ///// 调用WebService获取员工的提醒
        ///// </summary>
        ///// <param name="arg">参数</param>
        ///// <param name="taskType">任务类型</param>
        //private void InvokeWebService(object[] arg, string taskType)
        //{
        //    //可执行文件的路径
        //    string exePath = GetParentDir() + ConfigurationManager.AppSettings["remindProgramExePath"];
        //    //WebService地址
        //    string webServiceUrl = ConfigurationManager.AppSettings["webServiceUrl"];
        //    //首先调用WebService获取是否需要提醒
        //    object haveFinished = WebServiceHelper.InvokeWebService(webServiceUrl, "GetCustomerRemind", arg);
        //    JObject jObj = JObject.Parse(haveFinished.ToString());
        //    string result = jObj["result"].ToString();
        //    //result为1时，需要提醒
        //    if (result == "1")
        //    {
        //        //调用WebService获取任务列表
        //        object objTaskList = WebServiceHelper.InvokeWebService(webServiceUrl, "GetTaskRemindingByUserId", arg);
        //        string txtPath = string.Empty;
        //        //根据任务类型设置文本文件的写入路径
        //        if (taskType == "0")
        //        {
        //            txtPath = GetParentDir() + ConfigurationManager.AppSettings["taskListCommonTextPath"].ToString();
        //        }
        //        else if (taskType == "1")
        //        {
        //            txtPath = GetParentDir() + ConfigurationManager.AppSettings["taskListAssTextPath"].ToString();
        //        }
        //        else if (taskType == "2")
        //        {
        //            txtPath = GetParentDir() + ConfigurationManager.AppSettings["taskCountdown"].ToString();
        //        }
        //        //文本文件存在时先删除
        //        if (File.Exists(txtPath))
        //            File.Delete(txtPath);
        //        //写入提醒需要的信息
        //        LogHelper.WriteLineWithoutTime(txtPath, objTaskList.ToString());
        //        string parentDirectory = Path.GetDirectoryName(exePath);//可执行文件目录
        //        string paras = string.Format("{0} {1}", exePath, taskType);//参数
        //        Interop.CreateProcess(exePath, parentDirectory, paras);//调用方法执行提醒程序
        //    }
        //}
        #endregion

        ///// <summary>
        ///// 调用WebService获取客服的提醒
        ///// </summary>
        ///// <param name="arg">参数</param>
        ///// <param name="taskType">任务类型</param>
        //private void InvokeWebServiceCustomerRemind(object[] arg, string taskType)
        //{
        //    //可执行文件的路径
        //    string exePath = GetParentDir() + ConfigurationManager.AppSettings["remindProgramExePath"];
        //    //WebService地址
        //    string webServiceUrl = ConfigurationManager.AppSettings["webServiceUrl"];
        //    //首先调用WebService获取是否需要提醒
        //    object needRemind = WebServiceHelper.InvokeWebService(webServiceUrl, "GetCustomerRemind", arg);
        //    JObject jObj = JObject.Parse(needRemind.ToString());
        //    string result = jObj["result"].ToString();
        //    //result为1时，需要提醒
        //    if (result == "1")
        //    {
        //        //调用WebService获取任务列表
        //        object objTaskList = WebServiceHelper.InvokeWebService(webServiceUrl, "GetTaskRemindingByUserId", arg);
        //        string txtPath = string.Empty;
        //        //根据任务类型设置文本文件的写入路径
        //        if (taskType == "0")
        //        {
        //            txtPath = GetParentDir() + ConfigurationManager.AppSettings["taskFinalScriptPath"].ToString();
        //        }
        //        else if (taskType == "1")
        //        {
        //            txtPath = GetParentDir() + ConfigurationManager.AppSettings["taskModifyScriptPath"].ToString();
        //        }
        //        //文本文件存在时先删除
        //        if (File.Exists(txtPath))
        //            File.Delete(txtPath);
        //        //写入提醒需要的信息
        //        LogHelper.WriteLineWithoutTime(txtPath, objTaskList.ToString());
        //        string parentDirectory = Path.GetDirectoryName(exePath);//可执行文件目录
        //        string paras = string.Format("{0} {1}", exePath, taskType);//参数
        //        Interop.CreateProcess(exePath, parentDirectory, paras);//调用方法执行提醒程序
        //    }
        //}

        /// <summary>
        /// 服务停止事件
        /// </summary>
        protected override void OnStop()
        {
            LogHelper.WriteLine("服务停止");
        }

        ///// <summary>
        ///// 获取应用程序启动的父级目录
        ///// </summary>
        ///// <returns></returns>
        //private string GetParentDir()
        //{
        //    string path = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
        //    path = path.Substring(0, path.LastIndexOf("\\"));
        //    path = path.Substring(0, path.LastIndexOf("\\") + 1);
        //    return path;
        //}

        /// <summary>
        /// 设置工作内存占用
        /// </summary>
        /// <param name="maxWorkingSet"></param>
        public static void SetWorkingSet(int maxWorkingSet)
        {
            System.Diagnostics.Process.GetCurrentProcess().MaxWorkingSet = (IntPtr)maxWorkingSet;
        }
    }
}
