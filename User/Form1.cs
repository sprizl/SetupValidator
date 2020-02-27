using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using User.DTOs;

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
            string html = string.Empty;
            string url = @"https://localhost:44301/api/validator/";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                //length = response.ContentLength;
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    Console.Write(reader.ReadToEnd());
                }
                Console.Read();
            }

            Console.WriteLine(html);
        }

        private void PostBtn_Click(object sender, EventArgs e)
        {
            ValidateDataDto validateDataDto = new ValidateDataDto();
            validateDataDto.lotNo = "2003A4086V";
            validateDataDto.machineName = "MAP-LA-01";

            var jsonContent = JsonConvert.SerializeObject(validateDataDto);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://localhost:44301/api/validator/details");
            request.Method = "POST";

            UTF8Encoding encoding = new System.Text.UTF8Encoding();
            Byte[] byteArray = encoding.GetBytes(jsonContent);

            //request.ContentLength = byteArray.Length;
            request.ContentType = @"application/json";

            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
            }
            //long length = 0;
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    //length = response.ContentLength;
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        Console.Write(reader.ReadToEnd());
                    }
                    Console.Read();
                }
            }
            catch(Exception err)
            {
                Console.WriteLine(err.Message);
            }
        }
    }
}
