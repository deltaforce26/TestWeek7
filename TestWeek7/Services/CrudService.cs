using Microsoft.Extensions.Hosting;
using System.Net.Http;
using TestWeek7.Models;

namespace TestWeek7.Services
{
    public class CrudService
    {
        private readonly HttpClient _httpClient;

        public CrudService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public async Task<List<Todos>> GetPostsAsync()
        {
            var response = await _httpClient.GetAsync("https://dummyjson.com/todo");
            response.EnsureSuccessStatusCode();
            var todos = await response.Content.ReadFromJsonAsync<ListTodo>();
            return todos.todos;
        }

        public async Task<Todos> GetPostAsync(int id)
        {
            var response = await _httpClient.GetAsync($"https://dummyjson.com/todo/{id}");
            response.EnsureSuccessStatusCode();
            var todo = await response.Content.ReadFromJsonAsync<Todos>();
            return todo;
        }

        public async Task<Todos> CreatePostAsync(Todos newPost)
        {
            var response = await _httpClient.PostAsJsonAsync("https://dummyjson.com/todos/add", newPost);
            response.EnsureSuccessStatusCode();
            var createdTodo = await response.Content.ReadFromJsonAsync<Todos>();
            return createdTodo;
        }

        public async Task UpdatePostAsync(int id, Todos updatedTodo)
        {
            var response = await _httpClient.PutAsJsonAsync($"https://dummyjson.com/todo/{id}", updatedTodo);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeletePostAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"https://dummyjson.com/todo/{id}");
            response.EnsureSuccessStatusCode();
        }
    }

}

