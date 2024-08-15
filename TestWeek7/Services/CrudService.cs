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
            var response = await _httpClient.GetAsync("https://dummyjson.com/todos");
            response.EnsureSuccessStatusCode();
            var todos = await response.Content.ReadFromJsonAsync<ListTodo>();
            return todos.Todos;
        }

        public async Task<Todos> GetPostAsync(int id)
        {
            var response = await _httpClient.GetAsync($"https://dummyjson.com/todos/{id}");
            response.EnsureSuccessStatusCode();
            var todo = await response.Content.ReadFromJsonAsync<Todos>();
            return todo;
        }

        public async Task<Todos> CreatePostAsync(Todos newTodo)
        {
            var response = await _httpClient.PostAsJsonAsync("https://dummyjson.com/todos/add", newTodo);
            response.EnsureSuccessStatusCode();
            var createdTodo = await response.Content.ReadFromJsonAsync<Todos>();
            return createdTodo;
        }

        public async Task UpdatePostAsync(int id, ListTodo updatedTodo)
        {
            var response = await _httpClient.PutAsJsonAsync($"https://dummyjson.com/todos/{id}", updatedTodo);
            response.EnsureSuccessStatusCode();
            
        }

        public async Task DeletePostAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"https://dummyjson.com/todo/{id}");
            response.EnsureSuccessStatusCode();
        }
    }

}

