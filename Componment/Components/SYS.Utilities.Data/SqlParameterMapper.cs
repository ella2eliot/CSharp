namespace SYS.Utilities.Data
{
    /// <summary>
	/// 
	/// </summary>
	public class SqlParameterMapper
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public SqlParameterMapper(string name, object value)
        {
            Name = name;
            Value = value;
            this.DataType = DataTypes.Variant;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="size"></param>
        /// <param name="direction"></param>
        public SqlParameterMapper(string name, object value, int size, SqlParameterDirection direction)
            : this(name, value, direction)
        {
            this.Size = size;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="direction"></param>
        public SqlParameterMapper(string name, object value, SqlParameterDirection direction)
            : this(name, value)
        {
            this.Direction = direction;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="dataType"></param>
        public SqlParameterMapper(string name, object value, string dataType)
            : this(name, value)
        {
            this.DataType = dataType;
        }

        /// <summary>
        /// Parameter direction, default is Input.
        /// </summary>
        public SqlParameterDirection Direction { get; set; }

        /// <summary>
        /// Size of parameter.
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// Parameter name.
        /// </summary>
        public string Name { get; set; }

        ///<summary>
        /// Parameter value.
        ///</summary>
        public object Value { get; set; }

        /// <summary>
        /// Column data type
        /// </summary>
        public string DataType { get; set; }
    }
}