using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYS.Utilities.Data
{
    /// <summary>
	/// 
	/// </summary>
	public class OrderBy
    {
        private string _fieldName;
        private OrderByTypes _orderyByType;

        /// <summary>
        /// 
        /// </summary>
        public string FieldName
        {
            get { return _fieldName; }
            set { _fieldName = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public OrderByTypes OrderByType
        {
            get { return _orderyByType; }
            set { _orderyByType = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldName"></param>
        public OrderBy(string fieldName)
            : this(fieldName, OrderByTypes.Ascending)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="orderByTypeByType"></param>
        public OrderBy(string fieldName, OrderByTypes orderByTypeByType)
        {
            _fieldName = fieldName;
            _orderyByType = orderByTypeByType;
        }
    }
}
