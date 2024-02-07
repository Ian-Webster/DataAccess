namespace DataAccess.Repository.Models
{
    /// <summary>
    /// Data for a paging request
    /// </summary>
    /// <typeparam name="T">The data type being paged</typeparam>
    public class PagedResult<T> where T : class
    {
        /// <summary>
        /// The data to be paged
        /// </summary>
        public IEnumerable<T>? Data { get; set; }
        
        /// <summary>
        /// The current page index
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// The current page size
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Total count of items in the database
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// Total number of pages
        /// </summary>
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    }
}
