using BusinessTierWebAPI.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BusinessTierWebAPI.Controllers
{
    public class GenerateController : ApiController
    {
        List<Accinfo> dataStruct = new List<Accinfo>();

        // POST: api/Generate
        public string Post()
        {
            for (int i = 0; i < 100; i++)
            {
                GenerateData dbGen = new GenerateData();
                dbGen.GetNextAccount(out int pin, out int acctNo, out string firstName, out string lastName, out int balance, out string image);
                Accinfo data = new Accinfo();
                data.pin = pin;
                data.accno = acctNo;
                data.fname = firstName;
                data.lname = lastName;
                data.balance = balance;
                data.imageurl = image;
                dataStruct.Add(data);
                Console.WriteLine(data.fname);
            }
            foreach (Accinfo data in dataStruct)
            {
                RestClient restClient = new RestClient("https://localhost:44344/");
                RestRequest restRequest = new RestRequest("api/accinfo", Method.Post);
                restRequest.AddJsonBody(JsonConvert.SerializeObject(data));
                RestResponse restResponse = restClient.Execute(restRequest);
            }

            return (dataStruct.Count.ToString()+" generated");
        }

    }
}
