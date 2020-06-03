namespace LambdaExpressionBuilder.Models
{
    /// <summary>
    /// Pagination sort parameter
    /// </summary>
    public class QuerySort
    {
        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether Desc
        /// Sort records descendent
        /// </summary>
        public bool Desc { get; set; }

        /// <summary>
        /// Gets or sets the PropertyPath
        /// Sort records by
        /// </summary>
        public string PropertyPath { get; set; }

        #endregion
    }
}
