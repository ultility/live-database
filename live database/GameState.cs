using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace live_database
{
    public class GameState
    {
        public bool state { get; set; }
        public int id { get; set; }

        public GameState()
        {

        }

        public GameState(bool state)
        {
            this.state = state;
            id = new Random().Next();
        }

        public GameState(bool state, int id)
        {
            this.state = state;
            this.id = id;
        }
    }
}