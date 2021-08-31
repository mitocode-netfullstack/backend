namespace mitocode.netfullstack.services
{
    public class MitoCodeUtils
    {
        public static int GetTotalPages(int total, int rows)
        {
            var totalPages = total / rows;
            if (total % rows > 0)
                totalPages++;

            return totalPages;
        }
    }
}