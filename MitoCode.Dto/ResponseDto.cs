namespace mitocode.netfullstack.dto
{
    public class ResponseDto<T>
    {
        public bool Success { get; set; }
        public T Result { get; set; }
    }
}