using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace User
{
    public class Requester
    {
        public string BaseUrl { get; }

        public Requester()
        {
            BaseUrl = "https://localhost:44301/api/validator";
        }

        private TResult BaseRequest<TInput, TResult>(string method, TInput requestData)
        {
            var jsonContent = JsonConvert.SerializeObject(requestData);
            
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"{BaseUrl}/{method}");
            request.Method = "POST";

            UTF8Encoding encoding = new System.Text.UTF8Encoding();
            Byte[] byteArray = encoding.GetBytes(jsonContent);

            //request.ContentLength = byteArray.Length;
            request.ContentType = @"application/json";

            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
            }

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        string responseText = reader.ReadToEnd();

                        TResult result = (TResult)JsonConvert.DeserializeObject<TResult>(responseText);

                        return result;
                    }
                }
            }
            catch (Exception e)
            {
                TResult result = (TResult)JsonConvert.DeserializeObject<TResult>(e.Message);
                return result;
            }
            
        }

        public SetupDataValidationResultDto Validate(ValidateDataDto setupData)
        {
            var result = BaseRequest<ValidateDataDto, SetupDataValidationResultDto>(
                method: "details",
                requestData: setupData);

            return result;
        }
    }
}
