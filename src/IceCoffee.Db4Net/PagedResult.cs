namespace IceCoffee.Db4Net
{
    /// <summary>
    /// Represents a paginated result set containing a collection of items and the total count of items.
    /// </summary>
    /// <remarks>This class is commonly used to return paginated data from APIs or database queries. It
    /// provides both the items for the current page and the total number of items across all pages.</remarks>
    /// <typeparam name="TEntity">The type of the items in the result set.</typeparam>
    public class PagedResult<TEntity>
    {
        /// <summary>
        /// Items
        /// </summary>
        public required IEnumerable<TEntity> Items { get; set; }

        /// <summary>
        /// Total
        /// </summary>
        public required long Total { get; set; }
    }
}
