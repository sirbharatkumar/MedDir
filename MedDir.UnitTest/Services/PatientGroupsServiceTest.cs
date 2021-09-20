using Microsoft.VisualStudio.TestTools.UnitTesting;
using MedDir.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using MedDir.Domain.ApiModels.ResponseModel;
using MedDir.Domain.ApiModels.RequestModel;
using Moq;

namespace MedDir.Domain.Services.Tests
{
    [TestClass()]
    public class PatientGroupsServiceTest
    {

        #region Testing data
        List<List<int>> peopleExample1 = new List<List<int>>();
        List<List<int>> peopleExample2 = new List<List<int>>();
        List<List<int>> peopleExample3 = new List<List<int>>();
        List<List<int>> peopleExample4 = new List<List<int>>();
        #endregion

        PatientGroupsService patientGroupsService;
        ILogger<PatientGroupsService> _logger;

        [TestInitialize]
        public void TestInitialize()
        {
            _logger = Mock.Of<ILogger<PatientGroupsService>>();
            patientGroupsService = new PatientGroupsService(_logger);

            GenerateExample1Data();
            GenerateExample2Data();
            GenerateExample3Data();
            GenerateExample4Data();

        }

        #region Test Data generation
        private void GenerateExample1Data()
        {
            int[] array1Example1 = { 1, 0, 1, 1, 1 };
            int[] array2Example1 = { 1, 0, 0, 0, 0 };
            int[] array3Example1 = { 1, 0, 0, 0, 1 };
            int[] array4Example1 = { 0, 0, 1, 0, 0 };
            int[] array5Example1 = { 0, 1, 0, 0, 0 };
            int[] array6Example1 = { 0, 1, 0, 0, 1 };

            peopleExample1.Add(new List<int>(array1Example1));
            peopleExample1.Add(new List<int>(array2Example1));
            peopleExample1.Add(new List<int>(array3Example1));
            peopleExample1.Add(new List<int>(array4Example1));
            peopleExample1.Add(new List<int>(array5Example1));
            peopleExample1.Add(new List<int>(array6Example1));
        }

        private void GenerateExample2Data()
        {
            int[] array1Example2 = { 1, 1, 1, 1, 1 };
            int[] array2Example2 = { 1, 1, 1, 1, 1 };
            int[] array3Example2 = { 1, 1, 1, 1, 1 };
            int[] array4Example2 = { 1, 1, 1, 1, 1 };
            int[] array5Example2 = { 1, 1, 1, 1, 1 };
            int[] array6Example2 = { 1, 1, 1, 1, 1 };

            peopleExample2.Add(new List<int>(array1Example2));
            peopleExample2.Add(new List<int>(array2Example2));
            peopleExample2.Add(new List<int>(array3Example2));
            peopleExample2.Add(new List<int>(array4Example2));
            peopleExample2.Add(new List<int>(array5Example2));
            peopleExample2.Add(new List<int>(array6Example2));
        }

        private void GenerateExample3Data()
        {
            int[] array1Example3 = { 0, 0, 0, 0, 0 };
            int[] array2Example3 = { 0, 0, 0, 0, 0 };
            int[] array3Example3 = { 0, 0, 0, 0, 0 };
            int[] array4Example3 = { 0, 0, 0, 0, 0 };
            int[] array5Example3 = { 0, 0, 0, 0, 0 };
            int[] array6Example3 = { 0, 0, 0, 0, 0 };

            peopleExample3.Add(new List<int>(array1Example3));
            peopleExample3.Add(new List<int>(array2Example3));
            peopleExample3.Add(new List<int>(array3Example3));
            peopleExample3.Add(new List<int>(array4Example3));
            peopleExample3.Add(new List<int>(array5Example3));
            peopleExample3.Add(new List<int>(array6Example3));
        }

        private void GenerateExample4Data()
        {
            int[] array1Example4 = { 1, 1, 0, 0, 0, 0 };
            int[] array2Example4 = { 0, 1, 0, 0, 0, 0 };
            int[] array3Example4 = { 1, 0, 1, 0, 0, 0 };
            int[] array4Example4 = { 0, 0, 0, 0, 1, 0 };
            int[] array5Example4 = { 0, 0, 0, 0, 0, 1 };
            int[] array6Example4 = { 1, 1, 0, 1, 0, 0 };

            peopleExample4.Add(new List<int>(array1Example4));
            peopleExample4.Add(new List<int>(array2Example4));
            peopleExample4.Add(new List<int>(array3Example4));
            peopleExample4.Add(new List<int>(array4Example4));
            peopleExample4.Add(new List<int>(array5Example4));
            peopleExample4.Add(new List<int>(array6Example4));
        }
        #endregion


        #region Test run
        [TestMethod()]
        public void CalculatePatientGroupsTestExample1()
        {
            var numberOfGroups = 5;
            PatientGroupsResponse response = patientGroupsService.CalculatePatientGroups(new PatientGroupsRequest() { PeopleMatrix = peopleExample1 });
            Assert.AreEqual(numberOfGroups, response.NumberOfPatientGroups);
        }

        [TestMethod()]
        public void CalculatePatientGroupsDetailTestExample1()
        {
            var numberOfGroups = 5;
            PatientGroupsDetailResponse response = patientGroupsService.CalculatePatientGroupsDetail(new PatientGroupsRequest() { PeopleMatrix = peopleExample1 });
            Assert.AreEqual(numberOfGroups, response.NumberOfPatientGroups);
        }


        [TestMethod()]
        public void CalculatePatientGroupsTestExample2()
        {
            var numberOfGroups = 1;
            PatientGroupsResponse response = patientGroupsService.CalculatePatientGroups(new PatientGroupsRequest() { PeopleMatrix = peopleExample2 });
            Assert.AreEqual(numberOfGroups, response.NumberOfPatientGroups);
        }

        [TestMethod()]
        public void CalculatePatientGroupsDetailTestExample2()
        {
            var numberOfGroups = 1;
            PatientGroupsDetailResponse response = patientGroupsService.CalculatePatientGroupsDetail(new PatientGroupsRequest() { PeopleMatrix = peopleExample2 });
            Assert.AreEqual(numberOfGroups, response.NumberOfPatientGroups);
        }

        [TestMethod()]
        public void CalculatePatientGroupsTestExample3()
        {
            var numberOfGroups = 0;
            PatientGroupsResponse response = patientGroupsService.CalculatePatientGroups(new PatientGroupsRequest() { PeopleMatrix = peopleExample3 });
            Assert.AreEqual(numberOfGroups, response.NumberOfPatientGroups);
        }

        [TestMethod()]
        public void CalculatePatientGroupsDetailTestExample3()
        {
            var numberOfGroups = 0;
            PatientGroupsDetailResponse response = patientGroupsService.CalculatePatientGroupsDetail(new PatientGroupsRequest() { PeopleMatrix = peopleExample3 });
            Assert.AreEqual(numberOfGroups, response.NumberOfPatientGroups);
        }

        [TestMethod()]
        public void CalculatePatientGroupsTestExample4()
        {
            var numberOfGroups = 4;
            PatientGroupsResponse response = patientGroupsService.CalculatePatientGroups(new PatientGroupsRequest() { PeopleMatrix = peopleExample4 });
            Assert.AreEqual(numberOfGroups, response.NumberOfPatientGroups);
        }

        [TestMethod()]
        public void CalculatePatientGroupsDetailTestExample4()
        {
            var numberOfGroups = 4;
            PatientGroupsDetailResponse response = patientGroupsService.CalculatePatientGroupsDetail(new PatientGroupsRequest() { PeopleMatrix = peopleExample4 });
            Assert.AreEqual(numberOfGroups, response.NumberOfPatientGroups);
        }
        #endregion
    }
}