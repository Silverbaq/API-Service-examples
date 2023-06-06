using System;
using System.Net;
using System.IO;
using System.Xml;

class Program
{
    static void Main()
    {
        try
        {
            // Create a web request to the SOAP service
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://www.w3schools.com/xml/tempconvert.asmx");
            request.Headers.Add("SOAPAction", "https://www.w3schools.com/xml/FahrenheitToCelsius"); // SOAP action for FahrenheitToCelsius method
            request.ContentType = "text/xml; charset=utf-8";
            request.Method = "POST";

            // Create the SOAP envelope for the FahrenheitToCelsius request
            string soapEnvelope = @"<?xml version=""1.0"" encoding=""utf-8""?>
                <soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
                    <soap:Body>
                        <FahrenheitToCelsius xmlns=""https://www.w3schools.com/xml/"">
                            <Fahrenheit>50</Fahrenheit>
                        </FahrenheitToCelsius>
                    </soap:Body>
                </soap:Envelope>";

            // Convert the SOAP envelope string to a byte array
            byte[] requestBodyBytes = System.Text.Encoding.UTF8.GetBytes(soapEnvelope);
            request.ContentLength = requestBodyBytes.Length;

            // Send the SOAP request
            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(requestBodyBytes, 0, requestBodyBytes.Length);
            }

            // Get the SOAP response
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream responseStream = response.GetResponseStream())
            {
                // Load the SOAP response into an XML document
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(responseStream);

                // Find the Celsius temperature in the SOAP response XML
                XmlNamespaceManager nsManager = new XmlNamespaceManager(xmlDoc.NameTable);
                nsManager.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
                nsManager.AddNamespace("m", "https://www.w3schools.com/xml/");
                XmlNode celsiusNode = xmlDoc.SelectSingleNode("//m:FahrenheitToCelsiusResult", nsManager);

                // Display the converted temperature
                if (celsiusNode != null)
                {
                    Console.WriteLine("50 degrees Fahrenheit is equal to {0} degrees Celsius.", celsiusNode.InnerText);
                }
                else
                {
                    Console.WriteLine("Failed to retrieve the Celsius temperature from the SOAP response.");
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
