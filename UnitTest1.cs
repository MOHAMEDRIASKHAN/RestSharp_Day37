using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;

namespace RestShopTesting
{

    public class Employee
    {
        public int id { get; set; }
        public string name { get; set; }
        public string salary { get; set; }
    }


    [TestClass]
    public class RestSharpCase
    {
        RestClient client;

        [TestInitialize]
        public void Setup()
        {
            client = new RestClient("Http://localhost:4000");

        }

        private IRestResponse getEmployeeList()
        {
            //arrange
            RestRequest request = new RestRequest("/employees", Method.GET);

            //act
            IRestResponse response = client.Execute(request);
            return response;

        }

        [TestMethod]
        public void OnCallingList_ReturnEmployeeList()
        {
            IRestResponse response = getEmployeeList();
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
            List<Employee> dataResponse = JsonConvert.DeserializeObject<List<Employee>>(response.Content);
            // Assert.AreEqual(3, dataResponse.Count);
            foreach (Employee e in dataResponse)
            {
                Console.WriteLine("id : " + e.id + "Name: " + e.name + "salary : " + e.salary);

            }
        }

        public void givenEmloyee_Onpost_ShouldReturnAddEmployee()
        {
            RestRequest request = new RestRequest("/EmployeePayroll", Method.POST);
            System.Text.Json.Nodes.JsonObject jsonobject = new System.Text.Json.Nodes.JsonObject();
            jsonobject.Add("name", "Riyas");
            jsonobject.Add("Salary", "15000");

            request.AddParameter("application/json", jsonobject, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.Created);
            Employee dataresponse = JsonConvert.DeserializeObject<Employee>(response.Content);

            Assert.AreEqual("Riyas", dataresponse.name);

            Assert.AreEqual("15000", dataresponse.salary);
            System.Console.WriteLine(response.Content);
        }

        [TestMethod]
        private void updateEmloyee_OnPut_ShouldReturnupdatedEmployee()
        {
            RestRequest request = new RestRequest("/EmployeePayroll/6", Method.PUT);
            System.Text.Json.Nodes.JsonObject jsonobject = new System.Text.Json.Nodes.JsonObject();
            jsonobject.Add("name", "Uma");
            jsonobject.Add("Salary", "20000");

            request.AddParameter("application/json", jsonobject, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
            Employee dataresponse = JsonConvert.DeserializeObject<Employee>(response.Content);

            Assert.AreEqual("Uma", dataresponse.name);

            Assert.AreEqual("20000", dataresponse.salary);
            System.Console.WriteLine(response.Content);
        }





    }

}