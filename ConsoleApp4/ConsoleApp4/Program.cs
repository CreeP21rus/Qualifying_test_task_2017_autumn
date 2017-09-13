using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;

namespace ConsoleApp3
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Person
    {
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("sex")]
        public string sex { get; set; }
        [JsonProperty("age")]
        public int age { get; set; }
        public Person()
        {
            id = null;
            name = null;
            sex = null;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string url = "http://testlodtask20172.azurewebsites.net/task";
            List<Person> people = new List<Person>();
            WebClient Client = new WebClient();
            string Str = Client.DownloadString(url);
            Person[] persons = JsonConvert.DeserializeObject<Person[]>(Str);
            foreach (Person item in persons)
            {
                Str = Client.DownloadString("http://testlodtask20172.azurewebsites.net/task/" + item.id);
                Person person = JsonConvert.DeserializeObject<Person>(Str);
                item.age = person.age;
            }
            Person Male = new Person(), Female = new Person();
            Male.age = -1;
            Female.age = -1;
            foreach (Person item in persons)
            {
                if (item.sex == "male")
                {
                    Male = (Male.age == -1) || (Male.age > item.age) ? item : Male;
                }
                else Female = (Female.age == -1) || (Female.age > item.age) ? item : Female;
            }
            Console.WriteLine("Male id: " + Male.id + " name: " + Male.name + " age: " + Male.age);
            Console.WriteLine("Female id: " + Female.id + " name: " + Female.name + " age: " + Female.age);
            Console.ReadKey();
        }
    }
}

