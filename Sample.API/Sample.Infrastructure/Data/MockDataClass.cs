using System;
using System.Threading.Tasks;
using System.Threading;

namespace Sample.Infrastructure
{
     class MockDataClass
    {
        private String dataFromDatabase = "";

        
        int randomNumMin = 1000;

        public async void ProduceDataAsync() 
        {
            //todo: implement timeout functionality
            await Task.Run(() => GetDataFromDataStream());
        }

        public async void ProduceDataAsync(String myQuery, int taskID)
        {
            //todo: implement timeout functionality
            String results = await Task.Run(() => GetDataFromDataStream());
            stickInDatabase(results);
        }

        public string ProduceDataSynchronous(String myQuery)
        {
            return GetDataFromDataStream();
        }


        // returns binary string of whatever data; used to represent results from external data stream.
        private string GetDataFromDataStream()
        {
            
            Random rng = new Random();
            int extraTime = rng.Next(0, 1000);
            randomNumMin += extraTime;
            Thread.Sleep(randomNumMin);
            
            return "Hello World (from data layer). Waited: " + randomNumMin.ToString() + " ms" ;
        }

        private void stickInDatabase(String results, int waitID)
        {
            // update database, put in results. 
        }

    }
}
