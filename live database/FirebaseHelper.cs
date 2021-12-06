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
        static FirebaseClient firebase = new FirebaseClient("https://test-e41fe-default-rtdb.europe-west1.firebasedatabase.app/");
        static string database = "Games";

        public static async Task<List<GameState>> GetAll()
        {
            return(await firebase
              .Child(database)
              .OnceAsync<GameState>()).Select(item => new GameState { 
                  state = item.Object.state,
                  id = item.Object.id}).ToList();
        }

        public static async Task Add(GameState game)
        {

            await firebase
              .Child(database)
              .PostAsync(game);
        }

        public static async Task<GameState> Get(int id)
        {
            var allPersons = await GetAll();
            await firebase
              .Child(database)
              .OnceAsync<GameState>();
            return allPersons.Where(a => a.id == id).FirstOrDefault();
        }

        public static async Task Update(GameState state)
        {
            var toUpdatePerson = (await firebase
              .Child(database)
              .OnceAsync<GameState>()).Where(a => a.Object.id == state.id).FirstOrDefault();

            await firebase
              .Child(database)
              .Child(toUpdatePerson.Key)
              .PutAsync(state);
        }

        public static async Task Delete(int personId)
        {
            var toDeletePerson = (await firebase
              .Child(database)
              .OnceAsync<GameState>()).Where(a => a.Object.id == personId).FirstOrDefault();
            await firebase.Child(database).Child(toDeletePerson.Key).DeleteAsync();

        }
    }
}