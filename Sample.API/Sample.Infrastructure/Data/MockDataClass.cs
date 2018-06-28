using System;
using System.Threading.Tasks;
using System.Threading;

namespace Sample.Infrastructure
{
     class MockDataClass
    {
        private String dataFromDatabase = "";

        
        int randomNumMin = 1000;

        public string fetchData()
        {
            Console.WriteLine("Getting your data..."); // todo: logger
            ProduceDataAsync();
            Console.WriteLine("Returned from Async");
            return dataFromDatabase;
        }

        public async void ProduceDataAsync() 
        {
            //todo: implement timeout functionality
            String result = await Task.Run(() => GetDataFromDataStream());
            dataFromDatabase = result;
        }

        public async void ProduceDataAsync(String myQuery)
        {
            //todo: implement timeout functionality
            String result = await Task.Run(() => GetDataFromDataStream());
            dataFromDatabase = result;
        }

        public string ProduceDataSynchronous(String myQuery)
        {
            return GetDataFromDataStream();
        }

        private string GetDataFromDataStream()
        {
            // returns binary string of whatever data; used to represent results from external data stream.
            Random rng = new Random();
            int extraTime = rng.Next(0, 1000);
            randomNumMin += extraTime;
            Thread.Sleep(randomNumMin);
            
            return "Hello World (from data layer). Waited: " + randomNumMin.ToString() + " ms" ;
        }
    }
}
