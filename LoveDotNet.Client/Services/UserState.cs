using Microsoft.AspNetCore.Components;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using LoveDotNet.Models;

namespace LoveDotNet
{
    public class UserState
    {
        public event EventHandler UserChanged;
        public event EventHandler UserInfoChanged;
        public User CurrentUser { get; set; } = new User();
        public bool ShowLoginDialog { get; set; }
        public bool ShowUpdateDialog { get; set; }

        private readonly HttpClient http;
        public UserState(HttpClient http)
        {
            this.http = http;
        }

        public async Task<User> GetUser(int id)
        {
            return await http.GetJsonAsync<User>("api/Users/" + id);
        }

        public async Task<bool> UpdateUser(int id, User user)
        {
            if (await http.PutJsonAsync<bool>("api/Users/" + id, user))
            {
                CurrentUser = user;
                ShowUpdateDialog = false;
                UserInfoHasChanged();
                return true;
            }
            UserInfoHasChanged();
            return false;
        }

        public async Task<bool> Login(string email, string passwd)
        {
            var result = await http.PostJsonAsync<User>("api/Users/Login", new User() { Email = email, Password = passwd });
            if (result.IsEmpty())
                return false;
            CurrentUser = result;
            ShowLoginDialog = false;
            UserHasChanged();
            return false;
        }
        public async Task<bool> Signup(string email, string passwd)
        {
            var result = await http.PostJsonAsync<User>("api/Users/Signup", new User() { Email = email, Password = passwd });
            if (result.IsEmpty())
                return false;
            CurrentUser = result;
            ShowLoginDialog = false;
            UserHasChanged();
            return true;
        }

        public void Signout()
        {
            CurrentUser = new User();
            UserHasChanged();
        }

        public async Task<bool> Apply()
        {
            if (CurrentUser.IsEmpty()) return false;
            return await http.GetJsonAsync<bool>("api/Users/Apply/" + CurrentUser.Id);
        }

        private void UserHasChanged()
        {
            UserChanged?.Invoke(this, EventArgs.Empty);
        }
        private void UserInfoHasChanged()
        {
            UserInfoChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
