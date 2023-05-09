using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

/*Создайте программу для работы с информацией о журнале.
Нужно хранить такую информацию об журнале:
1. Название журнала
2. Название издательства
3. Дата выпуска
4. Количество страниц
У программы должна быть такая функциональность:
1. Ввод информации о журнале
2. Вывод информации о журнале
3. Сериализация журнала
4. Сохранение сериализованного журнала в файл
5. Загрузка сериализованного журнала из файла. После
загрузки нужно произвести десериализацию журнала.
Выбор конкретного формата сериализации необходимо сделать
вам. Обращаем ваше внимание, что выбор должен быть
обоснованным.
*/
/*Добавьте к предыдущему заданию список статей из журнала.
Нужно хранить такую информацию о каждой статье:
1. Название статьи
2. Количество символов
3. Анонс статьи
Измените функциональность из предыдущего задания таким
образом, чтобы она учитывала список статей.
Выбор конкретного формат сериализации необходимо сделать
вам. Обращаем ваше внимание, что выбор должен быть
обоснованным.*/
/*Добавьте к предыдущему заданию возможность создания
массива журналов.
Измените функциональность из второго задания таким образом,
чтобы она учитывала массив журналов.
Выбор конкретного формат сериализации необходимо сделать
вам. Обращаем ваше внимание, что выбор должен быть
обоснованным.*/




namespace Task_2_log
{
    [Serializable]
    
    
    public class Journal
    {
       public List<Article> articles=new List<Article>(); 
        public string Name { get; set; }
        public string PublishHouse { get; set; }
        public int Year { get; set; }
        public int AmountPages { get; set; }
        public int Count;
        public Journal() { }
        public Journal(string name, string publishHouse, int year, int AmountPages)
        {
            this.Name = name;
            this.PublishHouse = publishHouse;
            this.Year = year;
            this.AmountPages = AmountPages;
        }
        public void OutputInfo()
        {
            Console.WriteLine("Name : " + Name);
            Console.WriteLine("Publish House : " + PublishHouse);
            Console.WriteLine("Year : " + Year);
            Console.WriteLine("Amount Pages : " + AmountPages);
            foreach (var article in articles) 
            {
                    article.OutputInfo();
                    Console.WriteLine();
            }
            Console.WriteLine();
        }
        public void SetInfo()
        {
            Console.WriteLine("Enter info about Magazine");
            Console.WriteLine("Name : ");
            Name = Console.ReadLine();
            Console.WriteLine("Year : ");
            Year = int.Parse(Console.ReadLine());
            Console.WriteLine("Publish House : ");
            PublishHouse = Console.ReadLine();
            Console.WriteLine("Amount pages : ");
            AmountPages = int.Parse(Console.ReadLine());
        }
        public void SetArticles(List<Article> articles)
        {
            
            this.articles.AddRange(articles);
            Count += articles.Count;
        }
    }


    [Serializable]
    public class Article
    {
        public string NameArticle { get; set; }
        public int AmountLetters { get; set; }
        public int Preview { get; set; }


        public Article() { }
        public Article(string NameArticle, int AmountLetters, int Preview)
        {
            this.NameArticle = NameArticle;
            this.AmountLetters = AmountLetters;
            this.Preview = Preview;
        }
        public void OutputInfo()
        {
        Console.WriteLine("\tName Article : " + NameArticle);
        Console.WriteLine("\tAmount letters : " + AmountLetters);
        Console.WriteLine("\tPreview : " +  Preview);
        }
    }
    class Serialiser
    {

        public void SerializeXml(Journal obj)
        {
            Stream stream = new FileStream("Journal.xml", FileMode.Create);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Journal));
            xmlSerializer.Serialize(stream, obj);
            stream.Close();
            Console.WriteLine("xml Serialize OK");
        }

        public Journal DeSerializerXml(string path = "Journal.xml")
        {
            Stream stream = new FileStream(path, FileMode.Open);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Journal));
            Journal Res = (Journal)xmlSerializer.Deserialize(stream);
            Console.WriteLine("Deserialize XML Ok");

            return Res;
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Start. Initialising Journal with articles\n\n");
            
                List<Article> articles = new List<Article>();
            articles.Add(new Article("Статья 1", 700, 2011));
            articles.Add(new Article("Статья 2", 200, 2017));
            articles.Add(new Article("Статья 3", 500, 2012));
            articles.Add(new Article("Статья 4", 800, 2015));
            articles.Add(new Article("Статья 5", 100, 2014));

            Console.WriteLine("Now we saving data to File   Journal.xml\n\n");
            Journal journal = new Journal("Журнал", "Какое-то издательство", 2030, 380);
            journal.SetArticles(articles);
            journal.OutputInfo();
            Console.WriteLine("Count = " + journal.Count);


            Serialiser serialiser = new Serialiser();

            Console.WriteLine("Now we download data from File   Journal.xml\n\n");
            serialiser.SerializeXml(journal);
            Journal journal2 = serialiser.DeSerializerXml();
            journal2.OutputInfo();
            Console.WriteLine("Count = " + journal2.Count);


        }
    }
}
