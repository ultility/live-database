using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Firebase.Database;
using Firebase.Database.Query;
using System.Threading.Tasks;

namespace live_database
{
    public static class FirebaseHelper
    {
        public static FirebaseClient firebase = new FirebaseClient("https://test-e41fe-default-rtdb.europe-west1.firebasedatabase.app/");
        public static string table_name = "Games";

        public static async Task<List<GameState>> GetAll()
        {
            return(await firebase
              .Child(table_name)
              .OnceAsync<GameState>()).Select(item => new GameState
              {
                  state = item.Object.state
              }).ToList();
        }

        public static async Task Add(GameState game)
        {
            await firebase
              .Child(table_name)
              .PostAsync(game);
        }

        public static async Task<GameState> Get(int id)
        {
            var allPersons = await GetAll();
            await firebase
              .Child(table_name)
              .OnceAsync<GameState>();
            return allPersons.Where(a => a.id == id).FirstOrDefault();
        }

        public static async Task Update(GameState state)
        {
            var toUpdatePerson = (await firebase
              .Child(table_name)
              .OnceAsync<GameState>()).Where(a => a.Object.id == state.id).FirstOrDefault();

            await firebase
              .Child(table_name)
              .Child(toUpdatePerson.Key)
              .PutAsync(state);
        }

        public static async Task Delete(int id)
        {
            var toDeletePerson = (await firebase
              .Child(table_name)
              .OnceAsync<GameState>()).Where(a => a.Object.id == id).FirstOrDefault();
            await firebase.Child(table_name).Child(toDeletePerson.Key).DeleteAsync();

        }
    }
}