using Nest;

namespace ElasticWeb.Services
{
    public class ElasticSearchService<T>:IElasticSearch<T> where T : class
    {
        private readonly string _index;
        private readonly IElasticClient _elasticClient;

        public ElasticSearchService(string index, IElasticClient elasticClient)
        {
            _index = index;
            _elasticClient = elasticClient;
        }
        

        public T GetItem(string key)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddorUpdate(T item)
        {
            throw new NotImplementedException(); 
        }

        public bool CreateIndexIfNotExist(string key) 
        {
            throw new NotImplementedException(); 
        }

        public Task<bool> AddorUpdate(string key)
        {
            throw new NotImplementedException();
        }

        public bool DeleteItem(string key)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> GetItems()
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateKey(string key)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddMultiple(string key, IEnumerable<T> items)
        {
            throw new NotImplementedException();
        }
    }

    public interface IElasticSearch<T> where T : class
    {
        public Task<bool> CreateKey(string key);
        public T GetItem(string key);
        public Task<bool> AddorUpdate(string key); 
        public Task<bool> AddMultiple(string key, IEnumerable<T> items);
        public bool DeleteItem(string key);
        public Task<List<T>> GetItems();
    }
}


/*https://yamannasser.medium.com/simplifying-elasticsearch-crud-with-net-core-a-step-by-step-guide-25c86a12ae15*/