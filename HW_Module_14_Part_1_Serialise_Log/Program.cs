using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Xml.Serialization;

namespace HW_Module_14_Part_1_Serializer_Log
{
    /*Создайте программу для работы с массивом дробей (числитель
и знаменатель). Она должна иметь такую функциональность:
1. Ввод массива дробей с клавиатуры
2. Сериализация массива дробей
3. Сохранение сериализованного массива в файл
4. Загрузка сериализованного массива из файла. После
загрузки нужно произвести десериализацию
Выбор конкретного формата сериализации необходимо сделать
вам. Обращаем ваше внимание, что выбор должен быть
обоснованным.
*/
    internal class Program
    {
        [Serializable]

        
        public  class Fraction
        {
            [DataMember]
            public int x { set; get; }
            [DataMember]
            public int y { set; get; }
            public Fraction() { }
            public override string ToString()
            {
                return x.ToString() + " / " + y.ToString();
            }
            public void Output()
            {
                Console.WriteLine(x + " / " + y);
            }
            public void SetFraction()
            {
                Console.WriteLine("Enter x : ");
                x = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter y : ");
                y = int.Parse(Console.ReadLine());

            }
        }
        [Serializable]
       
        public class FractionArr
        {
            public List<Fraction> fractionList = new List<Fraction>();
            public FractionArr() { }
            public FractionArr(List<Fraction> list) =>fractionList.AddRange(list);
            public void Add(Fraction frac) => fractionList.Add(frac);
            public void Add()
            {
                Fraction fraction = new Fraction();
                fraction.SetFraction();
                fractionList.Add(fraction);

            }
            public void Output()
            {
                foreach (var frac in fractionList) frac.Output();
            }
        }


         class Serialiser
        {
            
            
            public void SerializeJSON(FractionArr obj)
            {
                Stream stream = new  FileStream("Fraction.json", FileMode.Create);
                DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(FractionArr));
                jsonFormatter.WriteObject(stream, obj);
                stream.Close();
                Console.WriteLine("Json serializer OK");
            }
            public FractionArr DeSerializerJSON(string path = "Fraction.json")
            {
                Stream stream = new FileStream(path, FileMode.Open);
                DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer (typeof(FractionArr));
                FractionArr fractionArr = (FractionArr)jsonFormatter.ReadObject(stream);
                stream.Close ();
                Console.WriteLine("Deserialize JSON Ok");
                return fractionArr;

            }

            public void SerializeXml(FractionArr obj)
            {
                Stream stream = new FileStream("Fraction.xml" , FileMode.Create);
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(FractionArr));
                xmlSerializer.Serialize(stream, obj);
                stream.Close();
                Console.WriteLine("Sml Serialize OK");
            }

            public FractionArr DeSerializerXml(string path = "Fraction.xml")
            {
                Stream stream = new FileStream(path, FileMode.Open);
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(FractionArr));
                FractionArr Res = (FractionArr)xmlSerializer.Deserialize(stream);
                Console.WriteLine("Deserialize XML Ok");

                return Res;
            }
        }
        static void Main(string[] args)
        {

            FractionArr obj = new FractionArr();
            obj.Add();
            obj.Add();
            obj.Add();
            Serialiser serialiser = new Serialiser();
            serialiser.SerializeJSON(obj);
            FractionArr objRes = serialiser.DeSerializerJSON();
            objRes.Output();
            //serialiser.SerializeXml(obj);         //НА ЭТОМ ЭТАПЕ ВЫЛЕТАЕТ ОШИБКА СВЯЗАННАЯ С общЕДОСТУПНОСТЬЮ К ТИПУ. 
            //FractionArr objRes2 = serialiser.DeSerializerXml();
            //objRes2.Output();

        }
    }
}
