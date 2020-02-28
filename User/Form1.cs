using Newtonsoft.Json;
using System;
using System.Net;
using System.Windows.Forms;

namespace User
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void GetBtn_Click(object sender, EventArgs e)
        {
            //string html = string.Empty;

            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://localhost:44301/api/validator/");

            //using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            //{
            //    //length = response.ContentLength;
            //    using (var reader = new StreamReader(response.GetResponseStream()))
            //    {
            //        Console.Write(reader.ReadToEnd());
            //    }
            //    Console.Read();
            //}

            //Console.WriteLine(html);
        }

        private void PostBtn_Click(object sender, EventArgs e)
        {
            ValidateDataDto setupData = new ValidateDataDto();
            setupData.lotNo = "2003A4086V";
            setupData.machineName = "FT-M-019";

            Requester requester = new Requester();
            var result = requester.Validate(setupData);

            Console.WriteLine("Test request for validation");
            Console.WriteLine($"LotNo := {result.Source.lotNo}");
            Console.WriteLine($"MachineName := {result.Source.machineName}");
            Console.WriteLine($"IsError := {result.IsError}");
            Console.WriteLine($"Message := {result.Message}");

            Console.ReadLine();
            //ValidateDataDto validateDataDto = new ValidateDataDto();
            //validateDataDto.lotNo = "2003A4086V";
            //validateDataDto.machineName = "FT-M-019";

            //var jsonContent = JsonConvert.SerializeObject(validateDataDto);

            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://localhost:44301/api/validator/details");
            //request.Method = "POST";

            //UTF8Encoding encoding = new System.Text.UTF8Encoding();
            //Byte[] byteArray = encoding.GetBytes(jsonContent);

            ////request.ContentLength = byteArray.Length;
            //request.ContentType = @"application/json";

            //using (Stream dataStream = request.GetRequestStream())
            //{
            //    dataStream.Write(byteArray, 0, byteArray.Length);
            //}
            ////long length = 0;
            //try
            //{
            //    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            //    {
            //        //length = response.ContentLength;
            //        using (var reader = new StreamReader(response.GetResponseStream()))
            //        {
            //            Console.Write(reader.ReadToEnd());
            //        }
            //        Console.Read();
            //    }
            //}
            //catch(Exception err)
            //{
            //    Console.WriteLine(err.Message);
            //}
        }
    }
}
