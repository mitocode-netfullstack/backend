using System.Collections.Generic;

namespace mitocode.netfullstack.dto
{
    public class CollectionBaseResponse<T> where T : class
    {
        public ICollection<T> Collection { get; set; }
        public int TotalPages { get; set; }
    }
}