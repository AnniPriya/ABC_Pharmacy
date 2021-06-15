using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using ABC_Pharmacy.Models;
using System.Reflection;


namespace ABC_Pharmacy.Controllers
{
    public class MedicinesController : Controller
    {
        string pathData = @"C:\Users\M1049000\Desktop\ABC_Pharmacy\ABC_Pharmacy\ABC_Pharmacy\Data\MedicineData.json";
        [HttpGet]
        public IActionResult GetMedicines()
        {
            string content = System.IO.File.ReadAllText(pathData);
            var data = JsonConvert.DeserializeObject<IEnumerable<Medicine>>(content);
            return View(data);
        }

        [HttpGet]
        public IActionResult create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult create(Medicine medicine)
        {         
            var read = System.IO.File.ReadAllText(pathData);
            List<Medicine> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Medicine>>(read);
            if (lst == null)
            {
                List<Medicine> _data = new List<Medicine>();
                _data.Add(medicine);
                String json = Newtonsoft.Json.JsonConvert.SerializeObject(_data.ToArray(), Formatting.Indented);
                System.IO.File.WriteAllText(pathData, json);
            }
            else
            {
                lst.Add(medicine);
                //var obj = JObject.Parse(read);
                String json = Newtonsoft.Json.JsonConvert.SerializeObject(lst, Formatting.Indented);
                System.IO.File.WriteAllText(pathData, json);
                //obj.Add(lst);
            }
            return View("GetMedicines", lst);
        }

        [HttpGet]
        public IActionResult Buy(string name)
        {
            List<Medicine> medicine = new List<Medicine>();
            Medicine med = new Medicine();
            med = medicine.FirstOrDefault(x => x.Name == name);
            ///ViewBag.QuantityList = med.Quantity;
            return View(med);
        }

        [HttpPost]
        public IActionResult Buy(Medicine medicine)
        {
            var name = medicine.Name;
            var quatity = medicine.Quantity;
            return View();
        }

        [HttpDelete]
        public void Delete(string name)
        {
            List<Medicine> medicine = new List<Medicine>();
            Medicine med = new Medicine();
            med = medicine.FirstOrDefault(x => x.Name == name);
            medicine.Remove(med);
        }
    }
}
