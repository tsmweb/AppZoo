using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace AppZoo.Model
{
    class AnimalService
    {
        public static async Task<List<Animal>> GetAnimais()
        {
            String xml = await Utils.readFile("Resources\\xml\\animais.xml");
            List<Animal> animais = parserXML(xml);
            return animais;
        }

        private static List<Animal> parserXML(String xml)
        {
            List<Animal> listAnimal = new List<Animal>();

            if (xml != null)
            {
                try
                {
                    XDocument xdoc = XDocument.Parse(xml);
                    XElement tagAnimais = xdoc.Element("animais");
                    IEnumerable<XElement> array = tagAnimais.Elements("animal");

                    if (array != null)
                    {
                        foreach (XElement tagAnimal in array)
                        {
                            Animal a = new Animal();
                            a.Id = Convert.ToInt32(tagAnimal.Element("id").Value);
                            a.Nome = tagAnimal.Element("nome").Value;
                            a.Especie = tagAnimal.Element("especie").Value;
                            a.DataNascimento = Convert.ToDateTime(tagAnimal.Element("data_nascimento").Value);
                            a.Tamanho = tagAnimal.Element("tamanho").Value;
                            a.Custo = Convert.ToDouble(tagAnimal.Element("custo").Value);
                            a.Sexo = tagAnimal.Element("sexo").Value;
                            a.Cor = tagAnimal.Element("cor").Value;
                            a.Observacoes = tagAnimal.Element("observacoes").Value;
                            a.NomeImagem = tagAnimal.Element("nome_imagem").Value;

                            listAnimal.Add(a);
                        }
                    }

                } catch (XmlException ex)
                {
                    Debug.WriteLine("Erro de parser: " + ex.Message);
                }
            }

            return listAnimal;
        }

    }
}
