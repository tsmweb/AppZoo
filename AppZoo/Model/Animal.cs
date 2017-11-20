using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace AppZoo.Model
{
    public class Animal
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Especie { get; set; }

        public DateTime DataNascimento { get; set; }

        public string Tamanho { get; set; }

        public double Custo { get; set; }

        public string Sexo { get; set; }

        public string Cor { get; set; }

        public string Observacoes { get; set; }

        public string NomeImagem { get; set; }

        [Ignore]
        public string FullPath
        {
            get { return System.IO.Path.Combine(ApplicationData.Current.LocalFolder.Path, "Recursos", NomeImagem); }
        }

        public override string ToString()
        {
            return Nome;
        }

    }
}
