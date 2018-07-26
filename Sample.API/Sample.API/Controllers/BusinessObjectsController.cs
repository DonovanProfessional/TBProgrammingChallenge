using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sample.Infrastructure;
using Sample.Domain.BusinessObjects;
using Sample.Domain.Abstract;
using Microsoft.AspNetCore.Cors;


namespace Sample.API.Controllers
{
    [Route("api/BusinessObjectsController")]
    public class BusinessObjectsController : Controller, ICRUDWebApi
    {
        // GET api/BusinessObjectsController/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            // prepare server side objects for parsing data

            string myObject = "To be overwritten; just mock page load code";

            // await with timeout
            MockDataWrapper fetcher = new MockDataWrapper();

            fetcher.fetchData(new SampleBusinessObject());
            
            
            myObject = fetcher.ExportedValue;
            Console.WriteLine("Returning the following to a caller: " + myObject);

            return myObject;
        }

        [HttpGet("/async/")]
        public string FetchDataAsync(int id)
        {
            int waitID = -1;
            // await with timeout
            MockDataWrapper fetcher = new MockDataWrapper();
            try
            {
               waitID = fetcher.createTaskID();
            }
            catch   
            {
               //logger.log("An error has occured in connecting to the database to create a task ID");
            }

            Task.Run(() => fetcher.fetchDataAsync(new SampleBusinessObject(),  waitID));
            return waitID != -1 ? waitID.ToString() : "Our service is temporarily down, please try later.";
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

        public void Read() // todo: have GET call this
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
