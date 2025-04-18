﻿using System;
using System.Xml;
using System.Xml.Schema;
using Newtonsoft.Json;
using System.IO;



/**
 * This template file is created for ASU CSE445 Distributed SW Dev Assignment 4.
 * Please do not modify or delete any existing class/variable/method names. However, you can add more variables and functions.
 * Uploading this file directly will not pass the autograder's compilation check, resulting in a grade of 0.
 * **/


namespace ConsoleApp1
{

    public class Program
    {
        public static string xmlURL = "https://raw.githubusercontent.com/0eef0/CSE445-Assignment4/refs/heads/master/CSE445-Assignment4/Hotels.xml";
        public static string xmlErrorURL = "https://raw.githubusercontent.com/0eef0/CSE445-Assignment4/refs/heads/master/CSE445-Assignment4/HotelsErrors.xml";
        public static string xsdURL = "https://raw.githubusercontent.com/0eef0/CSE445-Assignment4/refs/heads/master/CSE445-Assignment4/Hotels.xsd";

        public static void Main(string[] args)
        {
            string result = Verification(xmlURL, xsdURL);
            Console.WriteLine(result);

            result = Verification(xmlErrorURL, xsdURL);
            Console.WriteLine(result);

            result = Xml2Json(xmlURL);
            Console.WriteLine(result);
        }

        // Q2.1
        public static string Verification(string xmlUrl, string xsdUrl)
        {
            try
            {
                XmlReaderSettings xmlSettings = new XmlReaderSettings();
                xmlSettings.Schemas.Add(null, xsdUrl);
                xmlSettings.ValidationType = ValidationType.Schema;

                string errorMessages = "";
                xmlSettings.ValidationEventHandler += (sender, args) =>
                {
                    errorMessages += args.Message + "\n";
                };
                using (XmlReader reader = XmlReader.Create(xmlUrl, xmlSettings))
                {
                    while (reader.Read()) { }
                }

                //return "No Error" if XML is valid. Otherwise, return the desired exception message.
                return string.IsNullOrEmpty(errorMessages) ? "No Error" : errorMessages;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string Xml2Json(string xmlUrl)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(xmlUrl);
                return JsonConvert.SerializeXmlNode(doc, Newtonsoft.Json.Formatting.Indented);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
