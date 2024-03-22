using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Procedures.Models;
using System.Data;

namespace Procedures.Controllers
{
    public class BookController : Controller
    {
        private readonly string connectionString = "Data Source=DESKTOP\\MSSQLSERVER1;Initial Catalog=procedures;Integrated Security=True;Trust Server Certificate=True";

        public ActionResult Index()
        {
            List<BookViewModel> books = new List<BookViewModel>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("GetBooks", connection);
                command.CommandType = CommandType.StoredProcedure;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    BookViewModel book = new BookViewModel();
                    book.BookId = Convert.ToInt32(reader["BookId"]);
                    book.Title = reader["Title"].ToString();
                    //book.AuthorId = Convert.ToInt32(reader["AuthorId"]);
                    book.AuthorName = reader["AuthorName"].ToString();
                    books.Add(book);
                }
                connection.Close();
            }
            return View(books);
        }

        [HttpPost]
        public ActionResult AddBook(BookViewModel book)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("AddBook", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Title", book.Title);
                command.Parameters.AddWithValue("@AuthorId", book.AuthorId);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            return RedirectToAction("Index");
        }

        public ActionResult DeleteBook(int bookId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("DeleteBook", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@BookId", bookId);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            return RedirectToAction("Index");
        }
    }
}
