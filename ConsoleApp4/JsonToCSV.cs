using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;
using System.Text.Json.Nodes;


namespace ConsoleApp4
{
    public class JsonToCSV
    {
        private List<List<string>> datas = new List<List<string>>();
        private string text;
        
        public void readFile(string path)
        {
            try
            {
                string text = File.ReadAllText(path);
                this.text = text;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void jsonToData()
        {
            JsonObject? data = JsonObject.Parse(text).AsObject();
            if (data == null) {Console.WriteLine("Не удалось прочитать JSON");}
            
            var elements = data.GetEnumerator();
            
            while (elements.MoveNext())
            {
                var temp = elements.Current.Value.GetType().ToString();
                switch (temp)
                {
                    case "System.Text.Json.Nodes.JsonArray":
                        ArrayCSV(elements.Current.Key ,elements.Current.Value.AsArray());
                        break;
                    case "System.Text.Json.Nodes.JsonObject":
                        ObjectCSV(elements.Current.Key,elements.Current.Value.AsObject());
                        break;
                    case "System.Text.Json.Nodes.JsonValueTrimmable`1[System.Text.Json.JsonElement]":
                        ElementCSV(elements.Current);
                        break;
                    default:
                        break;
                }
            }
        }

        public void WriteCsv(string path)
        {
            using (var writer = new StreamWriter(path))
            using (var csv = new CsvWriter(writer, CultureInfo.CurrentCulture))
            {
                foreach (var data in datas)
                {
                    foreach (var rec in data)
                    {
                        csv.WriteField(rec);
                    }
                    csv.NextRecord();
                }
            }
        }

        public void ArrayCSV(string key ,JsonArray json)
        {
            foreach (var j in json)
            {
                switch (j.GetType().ToString())
                {
                    case "System.Text.Json.Nodes.JsonObject":
                        ObjectCSV(key , j.AsObject());
                        break;
                    case "System.Text.Json.Nodes.JsonValueTrimmable`1[System.Text.Json.JsonElement]":
                        ElementCSV(key , j);
                        break;
                }
            }
        }

        private void ObjectCSV(string key, JsonObject json)
        {
            foreach (var j in json)
            {
                ElementCSV(key , j);
            }
        }

        private void ElementCSV(string key, JsonNode json)
        {
            List<string> data = new List<string>() {key, json.ToString()};
            datas.Add(data);
        }
        private void ElementCSV(string key, KeyValuePair<string, JsonNode?> json)
        {
            List<string> data = new List<string>() {key, json.Key, json.Value.ToString()};
            datas.Add(data);
        }

        public void ElementCSV(KeyValuePair<string, JsonNode?> json)
        {
            List<string> data = new List<string>() {json.Key, json.Value.ToString()};
            datas.Add(data);
        }
    }
}