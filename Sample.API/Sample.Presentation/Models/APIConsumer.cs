using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sample.Domain.Abstract;
using System.Net;
using System.IO;

namespace Sample.Presentation.Models
{
    // too generic of a name; consider making this an abstract class and creating a new concrete
    public class APIConsumer : ICRUDWebApi
    {
        private string serviceLocation;
        private string webserviceResponse;
        public string asyncResultSetLocation;

        public APIConsumer() // consider taking a factory as a param here for object instantiation.
        {
            serviceLocation = "http://localhost:49446/api/BusinessObjectsController/"; // load from settings file in future.
            asyncResultSetLocation = generateAsyncResultsetLocation();
        }

        public void Auth()
        {
            throw new NotImplementedException();
        }

        public void Create()
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public void Read()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceLocation);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                webserviceResponse = reader.ReadToEnd();
            }
        }


        public string ReadRecord(int id)
        { // try/catch on failures to read.
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceLocation + id.ToString());
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    webserviceResponse = reader.ReadToEnd();
                }
            }
            catch
            {
                webserviceResponse = "Couldn't get the data you want, due to some exception. I'd provide more detail, but this gets displayed on the front end.";
            }
           
            return webserviceResponse;
        }


        public async Task<string> ReadAsync(int id)
        {
            asyncResultSetLocation = generateAsyncResultsetLocation();

            return await GetAsync(serviceLocation + id.ToString());

        }

        public async Task<string> GetAsync(string uri)
        {

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return await reader.ReadToEndAsync();
            }
        }
        

        public void Update()
        {
            throw new NotImplementedException();
        }

        public string generateAsyncResultsetLocation()
        {
            return "TempURLWithExpirationTimer";
        }
    }
}
