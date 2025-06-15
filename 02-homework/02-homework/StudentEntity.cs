namespace _02_homework;

using Azure;
using Azure.Data.Tables;

public class StudentEntity : ITableEntity
{
    public string PartitionKey { get; set; } = "Students";
    public string RowKey { get; set; }
    public string Name { get; set; }
    public int Grade { get; set; }
    public DateTimeOffset? Timestamp { get; set; }
    public ETag ETag { get; set; }
}