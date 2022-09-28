using BooksDemo.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;

namespace BooksDemo.Api.Test
{
    [TestClass]
    public class BooksApiTests
    {
        private HttpClient _httpClient;
        public BooksApiTests()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            _httpClient = webAppFactory.CreateDefaultClient();
        }

        [TestMethod]
        public void GetAllBooks()
        {
            var response = GetBooks();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void GetBookById_NotFound()
        {
            Guid bookId = Guid.NewGuid();
            var response = _httpClient.GetAsync("/api/Books/GetBookById?bookId=" + bookId).Result;
            var data = response.Content.ReadAsStringAsync();
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public void GetBookById_success()
        {
            var allBookResponse = GetBooks();
            var data = allBookResponse.Content.ReadAsStringAsync();
            var bookResponse = JsonConvert.DeserializeObject<List<Book>>(data.Result.ToString());
            var bookId = bookResponse.First().Id;
            var response = _httpClient.GetAsync("/api/Books/GetBookById/" + bookId).Result;
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void CreateBook_Success()
        {
            BookRequest bookRequest = new BookRequest()
            {
                Name = "Test Book Name",
                AuthorName = "Test Author Name"
            };
            var response = PostAsync(bookRequest);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void CreateBook_ValidationError()
        {
            BookRequest bookRequest = new BookRequest()
            {
                Name = "N",
                AuthorName = "A"
            };
            var response = PostAsync(bookRequest);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        private HttpResponseMessage PostAsync(BookRequest bookRequest)
        {
            var myContent = JsonConvert.SerializeObject(bookRequest);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = _httpClient.PostAsync("/api/Books/Createbook", byteContent).Result;
            var data = response.Content.ReadAsStringAsync();
            return response;
        }

        private HttpResponseMessage GetBooks()
        {
            var response = _httpClient.GetAsync("/api/Books/GetAllBooks").Result;            
            return response;
        }
    }
}