using System.Collections.Generic;
using System.Data.SqlClient;
using DiplAPI.Connection;
using DiplAPI.DistributeLock;
using DiplAPI1.Connection.Database_connection;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DiplAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly ZkConnector zk;
        private ZLock zklock;
        private DataBase db;
        private SqlConnection conn;

        public ProductController()
        {
            zk = new ZkConnector();
            zklock = new ZLock("/locks/Product", "anything");
            db = new DataBase();
            conn = DataBase.conn;
        }

        // GET: api/<controller>
        [HttpGet]
        public List<string[]> Index()
        {
            
            string query = @"SELECT * FROM dbo.Product";
            SqlCommand command = new SqlCommand(query);
            List<string[]> dataList = DataBase.GetTableRows(command);

            return dataList;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string[] Get(int id)
        {
            string query = @"SELECT * FROM dbo.Product WHERE id=@id";
            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue(@"id", id);
            string[] result = DataBase.GetRow(command);
            

            return result;
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]Newtonsoft.Json.Linq.JObject product)
        {
            string name = product.GetValue("Name").ToString();
            string type = product.GetValue("Type").ToString();
                        
            string query = @"INSERT INTO dbo.Product (Name, Type) VALUES (@name, @type)";
            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue(@"name", name);
            command.Parameters.AddWithValue(@"type", type);
            string path = zklock.Lock(command,"Product");
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]Newtonsoft.Json.Linq.JObject product)
        {
            string name = product.GetValue("Name").ToString();
            string type = product.GetValue("Type").ToString();

            string query = @"UPDATE dbo.Product SET Name=@name, Type=@type WHERE id=@id";
            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue(@"name", name);
            command.Parameters.AddWithValue(@"Type", type);
            command.Parameters.AddWithValue(@"id", id);
            string path = zklock.Lock(command, "Product");
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            string query = @"DELETE FROM dbo.Product WHERE id=@id";

            SqlConnection conn = DataBase.conn;
            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue(@"id", id);
            string path = zklock.Lock(command, "Product");
        }
    }
}
