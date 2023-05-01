// See https://aka.ms/new-console-template for more information
using Model;
using System.Text.Json;

Console.WriteLine("Welcome to Jsonl Generator");
Console.WriteLine("Currently Only working for 'Trips'");
Console.WriteLine("Please give folder with 'Trips.csv'");
string? path = Console.ReadLine();
if (path == null)
{
    throw new NullReferenceException();
}
string csvpath = path +  @"\Trips.csv";
string savePath = path + @"\Trips";

SaveJsonl(ReadFromCSV(csvpath), AddFileNameExtension(savePath));
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

            if (values == null || values[0] == null)
                throw new NullReferenceException();
            
            t.Date = DateOnly.Parse(values[0]);
            t.TripName = values[1];
            t.City = values[2];
            t.Country = values[3];
            t.Type = values[4];
            
            trips.Add(t);
        }
        return trips;
    }
}

string AddFileNameExtension(string? filePath)
{
    if (filePath == null)
    {
        return new FileNotFoundException(filePath).Message;
    }
    
    filePath +=$"_{DateTime.Now.Year}{DateTime.Now.Month}{DateTime.Now.Day}-{DateTime.Now.Hour}{DateTime.Now.Minute}{DateTime.Now.Second}";
    filePath += ".jsonl";
    return filePath;
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

