using System;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        try
        {
            using (HttpClient client = new HttpClient())
            {
                string apiUrl = "https://rickandmortyapi.com/api/character/2";

                // Send GET request to the API endpoint
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    // Read the response content as a JSON string
                    string json = await response.Content.ReadAsStringAsync();

                    // Parse the JSON string into a character object
                    Character character = Newtonsoft.Json.JsonConvert.DeserializeObject<Character>(json);

                    // Display character information
                    Console.WriteLine("Character ID: " + character.Id);
                    Console.WriteLine("Name: " + character.Name);
                    Console.WriteLine("Status: " + character.Status);
                    Console.WriteLine("Species: " + character.Species);
                    Console.WriteLine("Gender: " + character.Gender);
                }
                else
                {
                    Console.WriteLine("Failed to retrieve character information. Status code: " + response.StatusCode);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }

        Console.ReadLine();
    }
}

// Class representing the character object
class Character
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Status { get; set; }
    public string Species { get; set; }
    public string Gender { get; set; }
}