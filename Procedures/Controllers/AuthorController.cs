using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Procedures.Controllers
{
    public class AuthorController : Controller
    {
        private readonly string connectionString = "Data Source=DESKTOP\\MSSQLSERVER1;Initial Catalog=procedures;Integrated Security=True;Trust Server Certificate=True";

        public IActionResult Index()
        {
            List<Author> authors = new List<Author>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("GetAllAuthors", connection);
                command.CommandType = CommandType.StoredProcedure;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Author author = new Author();
                    author.AuthorId = Convert.ToInt32(reader["AuthorId"]);
                    author.Name = reader["Name"].ToString();
                    authors.Add(author);
                }
                connection.Close();
            }
            return View(authors);
        }

        [HttpPost]
        public IActionResult AddAuthor(Author author)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("AddAuthor", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Name", author.Name);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            return RedirectToAction("Index");
        }

        public class Author
        {
            public int AuthorId { get; set; }
            public string Name { get; set; }
        }
    }
}
