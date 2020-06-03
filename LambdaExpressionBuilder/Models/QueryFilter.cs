using LambdaExpressionBuilder.Enums;

namespace LambdaExpressionBuilder.Models
{
    /// <summary>
    /// Defines the <see cref="QueryFilter" />
    /// </summary>
    public class QueryFilter
    {
        #region Properties

        /// <summary>
        /// Gets or sets the Operator
        /// </summary>
        public OperatorEnum Operator { get; set; }

        /// <summary>
        /// Gets or sets the PropertyPath
        /// </summary>
        public string PropertyPath { get; set; }

        /// <summary>
        /// Gets or sets the PropertyValue
        /// </summary>
        public string PropertyValue { get; set; }

        #endregion
    }
}
