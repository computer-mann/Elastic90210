using Nest;

namespace ElasticWeb.Services
{
    public class ElasticSearchService<T>:IElasticSearch<T> where T : class
    {
        private readonly string _index;
        private readonly IElasticClient

        public ElasticSearchService(string index)
        {
            _index = index;
        }

        public T GetItem(string key)
        {
            throw new NotImplementedException();
        }
    }

    public interface IElasticSearch<T> where T : class
    {
        public T GetItem(string key);
    }
}
