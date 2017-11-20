using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppZoo.Model
{
    class AnimalDao
    {
        public List<Animal> BuscarAnimais()
        {
            List<Animal> listAnimal = DataBaseHelper.ConDB.Table<Animal>().OrderBy(a => a.Especie).ToList<Animal>();
            return listAnimal;
        }

        public Animal BuscaAnimal(string nome, string especie)
        {
            var animal = DataBaseHelper.ConDB.Query<Animal>("select * from Animal where lower(Nome) = '" + nome.ToLower() + "' and lower(Especie) = '" + especie.ToLower() +"'").FirstOrDefault();
            return animal;
        }

        public void InserirAnimal(Animal animal)
        {
            SQLiteConnection conDB = DataBaseHelper.ConDB;

            conDB.RunInTransaction(() =>
            {
                conDB.Insert(animal);
            });
        }

        public void EditarAnimal(Animal animal)
        {
            SQLiteConnection conDB = DataBaseHelper.ConDB;

            conDB.RunInTransaction(() =>
            {
                conDB.Update(animal);
            });
        }

        public void ExcluirAnimal(Animal animal)
        {
            SQLiteConnection conDB = DataBaseHelper.ConDB;

            conDB.RunInTransaction(() =>
            {
                conDB.Delete(animal);
            });
        }
    }
}
