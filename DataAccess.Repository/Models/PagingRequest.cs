namespace DataAccess.Repository.Models
{
    /// <summary>
    /// Data for a paging request
    /// </summary>
    public class PagingRequest
    {
        /// <summary>
        /// The desired page index
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// The desired page size
        /// </summary>
        public int PageSize { get; set; }

    }
}
