using Library.Entities;
using Library.Models.DTO;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTestProject1.Controllers
{
    public class BookBorrowInitTest
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public BookBorrowInitTest()
        {
            _server = ServerFactory.GetServerInstance();
            _client = _server.CreateClient();


            using (var scope = _server.Host.Services.CreateScope())
            {
                var _db = scope.ServiceProvider.GetRequiredService<LibraryContext>();

                _db.BookBorrow.Add(new BookBorrow
                {
                    IdBookBorrow = 2,
                    IdBook = 5,
                    IdUser = 5,
                    Comments = "None"
                });

                _db.SaveChanges();
            }
        }

        [Fact]
        public async Task PostBookBorrow_201CreatedAtRoute()
        {
            //Arrange i Act
            var newBook = new BookBorrowDto
            {
                IdUser = 1,
                IdBook = 1,
                Comment = "None"
            };

            HttpContent stringContent = new StringContent(JsonConvert.SerializeObject(newBook), Encoding.UTF8, "application/json");
            var httpResponse = await _client.PostAsync($"{_client.BaseAddress.AbsoluteUri}api/book-borrows", stringContent);

            httpResponse.EnsureSuccessStatusCode();

            var content = await httpResponse.Content.ReadAsStringAsync();
            var responseBook = JsonConvert.DeserializeObject<BookBorrow>(content);

            Assert.True(responseBook.IdUser == 1);
            Assert.True(responseBook.IdBook == 1);
        }

        [Fact]
        public async Task PutBookBorrow_200Ok()
        {

            //Arrange i Act
            var updatedBook = new UpdateBookBorrowDto
            {
                IdBookBorrow = 2,
                IdUser = 5,
                IdBook = 5,
                DateFrom = new DateTime(),
                DateTo = new DateTime(),
                Comments ="none"
            };

            HttpContent stringContent = new StringContent(JsonConvert.SerializeObject(updatedBook), Encoding.UTF8, "application/json");
            var httpResponse = await _client.PutAsync($"{_client.BaseAddress.AbsoluteUri}api/book-borrows/{updatedBook.IdBookBorrow}", stringContent);

            httpResponse.EnsureSuccessStatusCode();

            var content = await httpResponse.Content.ReadAsStringAsync();
            var responseBook = JsonConvert.DeserializeObject<Boolean>(content);
            
            Assert.True(responseBook);

        }
    }
}
