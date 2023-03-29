using System;

namespace SYS.Utilities.Data
{
    /// <summary>
	/// 
	/// </summary>
	public class FieldCondition
    {
        /// <summary>
        /// 
        /// </summary>
        internal string DatabaseType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldName">field name of condition, ex where arg1 = @arg2. arg1 is field name.</param>
        public FieldCondition(string fieldName)
            : this(fieldName, FieldConditionTypes.Equal)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldName">field name of condition, ex where arg1 = @arg2. arg1 is field name.</param>
        /// <param name="conditionType"></param>
        public FieldCondition(string fieldName, FieldConditionTypes conditionType)
            : this(fieldName, conditionType, fieldName)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldName">field name of condition, ex where arg1 = @arg2. arg1 is field name.</param>
        /// <param name="conditionType"></param>
        /// <param name="argumentName">name of argument, ex: @arg1. arg1 is argument name.</param>
        public FieldCondition(string fieldName, FieldConditionTypes conditionType, string argumentName)
            : this(fieldName, fieldName, conditionType, argumentName)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldName">field name of condition, ex where arg1 = @arg2. arg1 is field name.</param>
        /// <param name="replaceName">replace the field name.</param>
        /// <param name="conditionType"></param>
        /// <param name="argumentName">name of argument, ex: @arg1. arg1 is argument name.</param>
        public FieldCondition(string fieldName, string replaceName, FieldConditionTypes conditionType, string argumentName)
            : this(fieldName, replaceName, conditionType, argumentName, DataTypes.Variant, null)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldName">field name of condition, ex where arg1 = @arg2. arg1 is field name.</param>
        /// <param name="replaceName">replace the field name.</param>
        /// <param name="conditionType"></param>
        /// <param name="argumentName">name of argument, ex: @arg1. arg1 is argument name.</param>
        /// <param name="dataType"></param>
        /// <param name="argumentValue"></param>
        public FieldCondition(string fieldName, string replaceName, FieldConditionTypes conditionType, string argumentName, string dataType, object argumentValue)
        {
            this.ConditionType = conditionType;
            this.FieldName = fieldName;
            this.ReplaceName = replaceName;
            this.ArgumentName = argumentName;
            this.DataType = dataType;
            this.ArgumentValue = argumentValue;
        }


        /// <summary>
        /// 
        /// </summary>
        public object ArgumentValue { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ReplaceName { get; set; }

        ///<summary>
        ///</summary>
        public string FieldName { get; set; }

        ///<summary>
        ///</summary>
        public FieldConditionTypes ConditionType { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public string DataType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ArgumentName { get; set; }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            string result;

            switch (ConditionType)
            {
                case FieldConditionTypes.Equal:
                    result = string.Format("{0} = @{1}", this.FixFieldName(this.ReplaceName), this.ArgumentName);
                    break;
                case FieldConditionTypes.NotEqual:
                    result = string.Format("{0} <> @{1}", this.FixFieldName(this.ReplaceName), this.ArgumentName);
                    break;
                case FieldConditionTypes.Like:
                    result = string.Format("{0} like @{1}", this.FixFieldName(this.ReplaceName), this.ArgumentName);
                    break;
                case FieldConditionTypes.GreateThan:
                    result = string.Format("{0} > @{1}", this.FixFieldName(this.ReplaceName), this.ArgumentName);
                    break;
                case FieldConditionTypes.GreateEqual:
                    result = string.Format("{0} >= @{1}", this.FixFieldName(this.ReplaceName), this.ArgumentName);
                    break;
                case FieldConditionTypes.LessThan:
                    result = string.Format("{0} < @{1}", this.FixFieldName(this.ReplaceName), this.ArgumentName);
                    break;
                case FieldConditionTypes.LessEqual:
                    result = string.Format("{0} <= @{1}", this.FixFieldName(this.ReplaceName), this.ArgumentName);
                    break;
                case FieldConditionTypes.In:
                    result = string.Format("{0} in (@{1})", this.FixFieldName(this.ReplaceName), this.ArgumentName);
                    break;
                case FieldConditionTypes.NotIn:
                    result = string.Format("{0} not in (@{1})", this.FixFieldName(this.ReplaceName), this.ArgumentName);
                    break;
                case FieldConditionTypes.IsNull:
                    result = string.Format("{0} is null", this.FixFieldName(this.ReplaceName));
                    break;
                case FieldConditionTypes.IsNotNull:
                    result = string.Format("{0} is not null", this.FixFieldName(this.ReplaceName));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return result;
        }

        private string FixFieldName(string name)
        {
            if (this.DatabaseType == DatabaseTypes.SQL)
            {
                return string.Format("[{0}]", name);
            }

            return name;
        }
    }
}