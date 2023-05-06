namespace Services{
    using Azure.Data.Tables;
    using System.Collections.Generic;

    public class DDTableService
    {
        private TableClient _tableClient;
        
        public DDTableService(string connectionString, string tableName)
        {
            _tableClient = new TableClient(connectionString, tableName);
            _tableClient.CreateIfNotExists();
        }
 
       
        public TableEntity GetEntity(string partitionKey, string rowKey)
        {
            return _tableClient.GetEntity<TableEntity>(partitionKey, rowKey);
        }

        public IEnumerable<TableEntity> GetAllEntities()
        {
            return _tableClient.Query<TableEntity>(x => true).ToList();
        }

        public IEnumerable<TableEntity> GetEntitiesByPartitionKey(string partitionKey)
        {
            return _tableClient.Query<TableEntity>(x => x.PartitionKey == partitionKey).ToList();
        }

        public void InsertEntity(TableEntity e)
        {
            _tableClient.AddEntity(e);
        }

        
        public void DeleteEntity(string partitionKey, string rowKey)
        {
            _tableClient.DeleteEntity(partitionKey, rowKey);
        }
        public void UpdateEntity(TableEntity p)
        {
            _tableClient.UpdateEntity(p, p.ETag);
        }
    }
}