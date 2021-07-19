using System.Collections.Generic;

namespace Core.Data
{
    public class DataResult<T>
    {
        public DataResult()
        {
        }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public IReadOnlyList<T> Data { get; set; }
    }
}