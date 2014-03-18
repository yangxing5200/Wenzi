using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wz.Common
{
    /// <summary>
    /// MobilePhones:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class MobilePhones
    {
        public MobilePhones()
        { }
        #region Model
        private string _mobilephone;
        private string _companycode;
        private string _projectcode;
        private string _createby;
        private DateTime? _createdate;
        /// <summary>
        /// 
        /// </summary>
        public string MobilePhone
        {
            set { _mobilephone = value; }
            get { return _mobilephone; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CompanyCode
        {
            set { _companycode = value; }
            get { return _companycode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ProjectCode
        {
            set { _projectcode = value; }
            get { return _projectcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CreateBy
        {
            set { _createby = value; }
            get { return _createby; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        #endregion Model

    }
}
