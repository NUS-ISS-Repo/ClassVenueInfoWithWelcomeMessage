﻿using ClassVenueInfo.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Data;
//History
//Date          Changed By    Description
//2018-11-27    isska         for reading the text from a file and serialize it to json.The json string is extracted from database and the e-Poster Solution is used for this.

namespace ClassVenueInfo.Controllers
{
   
    public class VenueController : Controller
    {
        bool DACourseExists;
        public ActionResult Info()
        {
            VenueModels objgridmodel = new VenueModels();
            List<VenueModel> objgrid = new List<VenueModel>();
            objgrid = GetGridDetails();
            objgridmodel.VenueModel = objgrid;
            return View("Info", objgridmodel);          
        }

        public ActionResult Info2()
        {
            VenueModels objgridmodel = new VenueModels();
            List<VenueModel> objgrid = new List<VenueModel>();
            objgrid = GetGridDetails();
            objgridmodel.VenueModel = objgrid;
            return View("Info2", objgridmodel);
        }
        //for level 1 hd TVs
        public ActionResult Info3()
        {
            VenueModels objgridmodel = new VenueModels();
            List<VenueModel> objgrid = new List<VenueModel>();
            objgrid = GetGridDetails();
            objgridmodel.VenueModel = objgrid;
            return View("Info3", objgridmodel);
        }

        //To test class venue info issue of display looping in these pages
        public ActionResult Info4()
        {
            VenueModels objgridmodel = new VenueModels();
            List<VenueModel> objgrid = new List<VenueModel>();
            objgrid = GetGridDetails();
            objgridmodel.VenueModel = objgrid;
            return View("Info4", objgridmodel);
        }

        //To test class venue info issue of display looping in these pages
        public ActionResult L2New()
        {
            VenueModels objgridmodel = new VenueModels();
            List<VenueModel> objgrid = new List<VenueModel>();
            objgrid = GetGridDetails();
            objgridmodel.VenueModel = objgrid;
            return View("L2New", objgridmodel);
        }

        public ActionResult Index()
        {
            VenueModels objgridmodel = new VenueModels();
            List<VenueModel> objgrid = new List<VenueModel>();
            objgrid = GetGridDetails();
            objgridmodel.VenueModel = objgrid;
            return View("Index", objgridmodel);
        }

        //To display welcome message for TDA if TDA courses are there
        public ActionResult Info6()
        {
            VenueModels objgridmodel = new VenueModels();
            List<VenueModel> objgrid = new List<VenueModel>();
            objgrid = GetGridDetails();
            objgridmodel.VenueModel = objgrid;
            return View("Info6", objgridmodel);
        }


        public List<VenueModel> GetGridDetails()
        {
            var CourseCode = "";
            var CourseDesc = "";
            var ClassTime = "";
            var ClassDate = "";
            var RoomCode = "";
            var RoomType = "";
            var ClassTimeCode = "";
            var Department = "";
            //var data = "N";

            List<VenueModel> objgrid = new List<VenueModel>();

            //isska 27-22-2018, start
            var filepath = System.Configuration.ConfigurationManager.AppSettings["filepath"];
            var result_execute = "";
            try
            {
                result_execute = System.IO.File.ReadAllText(@filepath);
            }
            catch (FileNotFoundException) { }

            if (result_execute != null & result_execute != "")
            {
                List<Dictionary<string, string>> obj = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(result_execute);

                foreach (Dictionary<string, string> lst in obj)
                {
                    foreach (KeyValuePair<string, string> item in lst)
                    {
                        Console.WriteLine(string.Format("Key: {0} Value: {1}", item.Key, item.Value));
                        if (item.Key.Equals("CourseCode"))
                        {
                            CourseCode = item.Value.ToString();
                        }
                        if (item.Key.Equals("CourseDesc"))
                        {
                            CourseDesc = item.Value.ToString();
                        }
                        if (item.Key.Equals("ClassTime"))
                        {
                            ClassTime = item.Value.ToString();
                        }
                        if (item.Key.Equals("ClassTimeCode"))
                        {
                            ClassTimeCode = item.Value.ToString();
                        }
                        if (item.Key.Equals("ClasDate"))
                        {
                            ClassDate = item.Value.ToString();
                        }
                        if (item.Key.Equals("RoomCode"))
                        {
                            RoomCode = item.Value.ToString();
                        }
                        if (item.Key.Equals("RoomType"))
                        {
                            RoomType = item.Value.ToString();
                        }
                        if (RoomType.Equals("C"))
                        {
                            RoomType = "Classroom " + RoomCode;
                        }
                        if (item.Key.Equals("Department"))
                        {
                            Department = item.Value.ToString();
                            if (DACourseExists == false && Department == "TDA")
                            {
                                DACourseExists = true;
                            }
                            ViewBag.DACourseExists = DACourseExists;
                        }

                    }
                    objgrid.Add(new VenueModel { CourseCode = CourseCode, CourseDesc = CourseDesc, ClassTime = ClassTime, ClassTimeCode = ClassTimeCode, ClassDate = ClassDate, RoomCode = RoomCode, RoomType = RoomType ,Department = Department });
                }            
            }
            return objgrid;

        }


        //public List<VenueModel> GetGridDetails()
        //{
        //    var CourseCode = "";
        //    var CourseDesc = "";
        //    var ClassTime = "";
        //    var ClassDate = "";
        //    var RoomCode = "";
        //    var RoomType = "";
        //    var ClassTimeCode = "";
        //    var data = "N";

        //    List<VenueModel> objgrid = new List<VenueModel>();

        //    //isska 27-22-2018, start
        //    var filepath = System.Configuration.ConfigurationManager.AppSettings["filepath"];
        //    var result_execute = "";
        //    try
        //    {
        //        result_execute = System.IO.File.ReadAllText(@filepath);               
        //    }
        //    catch (FileNotFoundException) { }

        //    List<Dictionary<string, string>> obj = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(result_execute);

        //    foreach (Dictionary<string, string> lst in obj)
        //    {
        //        foreach (KeyValuePair<string, string> item in lst)
        //        {
        //                Console.WriteLine(string.Format("Key: {0} Value: {1}", item.Key, item.Value));
        //                if (item.Key.Equals("CourseCode"))
        //                {
        //                    CourseCode = item.Value.ToString();
        //                }
        //                if (item.Key.Equals("CourseDesc"))
        //                {
        //                    CourseDesc = item.Value.ToString();
        //                }
        //                if (item.Key.Equals("ClassTime"))
        //                {
        //                    ClassTime = item.Value.ToString();
        //                }
        //                if (item.Key.Equals("ClassTimeCode"))
        //                {
        //                    ClassTimeCode = item.Value.ToString();
        //                }
        //                if (item.Key.Equals("ClasDate"))
        //                {
        //                    ClassDate = item.Value.ToString();
        //                }
        //                if (item.Key.Equals("RoomCode"))
        //                {
        //                    RoomCode = item.Value.ToString();
        //                }
        //                if (item.Key.Equals("RoomType"))
        //                {
        //                    RoomType = item.Value.ToString();
        //                }
        //                if (RoomType.Equals("C"))
        //                {
        //                    RoomType = "Classroom " + RoomCode;
        //                }

        //            }
        //        objgrid.Add(new VenueModel { CourseCode = CourseCode, CourseDesc = CourseDesc, ClassTime = ClassTime, ClassTimeCode = ClassTimeCode, ClassDate = ClassDate, RoomCode = RoomCode, RoomType = RoomType });
        //    }             
        //    return objgrid;
        //}
        //isska 27-22-2018, end


    }
}
