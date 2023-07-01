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

            if (values == null || values[0] == null)
                throw new NullReferenceException();

			Trip t = new Trip(
                DateOnly.Parse(values[0]),
                values[1],
                values[2],
                values[3],
                values[4]);
			
			trips.Add(t);
		}
	}
	return trips;
}

void SaveJsonl(List<Trip> trips, string filePath)
{
	using (StreamWriter file = new StreamWriter(filePath))
	{
		foreach (var trip in trips)
		{
			file.WriteLine(JsonSerializer.Serialize(trip));
		}
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

