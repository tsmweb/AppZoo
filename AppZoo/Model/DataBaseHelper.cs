using SQLite.Net;
using SQLite.Net.Platform.WinRT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppZoo.Model
{
    class DataBaseHelper
    {
        private static SQLiteConnection conDB;
        public static SQLiteConnection ConDB
        {
            get
            {
                if (conDB == null)
                {
                    CreateDataBase();
                }

                return conDB;
            }
            private set
            {
                if (conDB != value)
                {
                    conDB = value;
                }
            }
        }

        public static void CreateDataBase()
        {
            var sqlPath = System.IO.Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "animal_db.sqlite");

            ConDB = new SQLiteConnection(new SQLitePlatformWinRT(), sqlPath);
            ConDB.CreateTable<Animal>();     
        }

        public static async void PopulaDataBase()
        {
            List<Animal> listAnimal = await AnimalService.GetAnimais();
            ConDB.InsertAll(listAnimal);
        }
    }
}
