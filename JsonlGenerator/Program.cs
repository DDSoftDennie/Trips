// See https://aka.ms/new-console-template for more information
using Model;
using Model.Enums;
using System.Text.Json;

Console.WriteLine("Welcome to Jsonl Generator");
Console.WriteLine("Currently Only working for 'Trips'");
Console.WriteLine("Please give filePath to Trips.csv");
string path = Console.ReadLine();
var t = ReadFromCSV(path);

Console.WriteLine("Please give filePath to save jsonl");
string savePath = Console.ReadLine();
SaveJsonl(t, savePath);
Console.WriteLine("Done");

List<Trip> ReadFromCSV(string filePath)
{
    List<Trip> trips = new List<Trip>();
    using (var reader = new StreamReader(filePath))
    {
        
        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();
            var values = line?.Split(';');
            var t = new Trip();

            if (values[0] == null)
                throw new NullReferenceException();
            
            // Process values here
            t.Date = DateOnly.Parse(values[0]);
            t.TripName = values[1];
            t.City = values[2];
            t.Country = values[3];


            //TEMP
            t.Type = TripType.UNSET;
            //  t.Type = (TripType)Enum.Parse(typeof(TripType), values[4]);
            trips.Add(t);
        }
        return trips;
    }
}

void SaveJsonl(List<Trip> trips, string filePath)
{
    using (StreamWriter writer = new StreamWriter(filePath))
    {
        foreach (Trip trip in trips)
        {
            string json = JsonSerializer.Serialize(trip);
            writer.WriteLine(json);
        }
    }
}

