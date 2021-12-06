namespace ConsoleApp4
{
    class Program
    {
        static void Main(string[] args)
        {
            string pathJson = "Json.json";
            string pathCSV = "CSV.csv";
            JsonToCSV jsonToCsv = new JsonToCSV();
            jsonToCsv.readFile(pathJson);
            jsonToCsv.jsonToData();
            jsonToCsv.WriteCsv(pathCSV);
        }
    }
}