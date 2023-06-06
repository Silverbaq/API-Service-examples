using System;
using System.Net.Http;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;

class Program
{
    static async Task Main()
    {
        try
        {
            // Create a GraphQLHttpClient instance
            var graphQLHttpClient = new GraphQLHttpClient("https://rickandmortyapi.com/graphql", new NewtonsoftJsonSerializer());

            // Create the GraphQL request with the query
            var graphQLRequest = new GraphQLHttpRequest
            {
                Query = @"
                    query {
                      character(id: 2) {
                        id
                        name
                        status
                        species
                        gender
                      }
                    }"
            };

            // Send the GraphQL request
            var graphQLResponse = await graphQLHttpClient.SendQueryAsync<dynamic>(graphQLRequest);

            if (graphQLResponse.Errors != null)
            {
                Console.WriteLine("GraphQL request failed:");
                foreach (var error in graphQLResponse.Errors)
                {
                    Console.WriteLine(error.Message);
                }
            }
            else
            {
                // Access the character data from the GraphQL response
                var character = graphQLResponse.Data.character;
                Console.WriteLine("Character ID: " + character.id);
                Console.WriteLine("Name: " + character.name);
                Console.WriteLine("Status: " + character.status);
                Console.WriteLine("Species: " + character.species);
                Console.WriteLine("Gender: " + character.gender);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }

        Console.ReadLine();
    }
}