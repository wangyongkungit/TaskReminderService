using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TipService.Model
{
    /// <summary>
	/// taskreminding:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class TaskReminding
	{
        public TaskReminding()
		{}
		#region Model
		private string _id;
		private string _employeeno;
		private string _folder;
		private string _modifyfolder;
		private decimal? _isreminded;
		private DateTime? _createdate;
		private DateTime? _expiredate;
		private decimal? _isfinished;
		private decimal? _tasktype;
		private decimal? _usertype;
		private string _enteringperson;
		private decimal? _tousertype;
		/// <summary>
		/// 
		/// </summary>
		public string ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EMPLOYEENO
		{
			set{ _employeeno=value;}
			get{return _employeeno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string FOLDER
		{
			set{ _folder=value;}
			get{return _folder;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string MODIFYFOLDER
		{
			set{ _modifyfolder=value;}
			get{return _modifyfolder;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? ISREMINDED
		{
			set{ _isreminded=value;}
			get{return _isreminded;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CREATEDATE
		{
			set{ _createdate=value;}
			get{return _createdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? EXPIREDATE
		{
			set{ _expiredate=value;}
			get{return _expiredate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? ISFINISHED
		{
			set{ _isfinished=value;}
			get{return _isfinished;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? TASKTYPE
		{
			set{ _tasktype=value;}
			get{return _tasktype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? USERTYPE
		{
			set{ _usertype=value;}
			get{return _usertype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ENTERINGPERSON
		{
			set{ _enteringperson=value;}
			get{return _enteringperson;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? TOUSERTYPE
		{
			set{ _tousertype=value;}
			get{return _tousertype;}
		}
		#endregion Model
    }
}
