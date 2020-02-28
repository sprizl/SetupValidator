using Newtonsoft.Json;
using SetupValidator.Abstract;
using SetupValidator.Concrete;
using SetupValidator.Domain;
using SetupValidator.DTOs;
using SetupValidator.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace SetupValidator.Controllers
{
    [RoutePrefix("api/validator")]
    public class ValidatorController : ApiController
    {
        //Called Data from DB through GetData();
        public INormalRepository reposity { get; }

        public ValidatorController(INormalRepository repo)
        {
            reposity = repo;
        }

        [HttpGet(), Route("")]
        public IHttpActionResult GetAllLotData()
        {
            List<LotDataDto> lotDataDtos = new List<LotDataDto>();

            for (int i = 0; i < 5; i++)
            {
                LotDataDto lotData = new LotDataDto();

                lotData.LotNo = "LotNo" + i.ToString();
                lotData.Package = "Package" + i.ToString();
                lotData.Device = "Device" + i.ToString();
                lotData.FlowName = "FlowName" + i.ToString();

                lotDataDtos.Add(lotData);
            }

            return Ok(new LotDataDto()
            {
                LotNo = lotDataDtos.FirstOrDefault().LotNo.ToString(),
                Package = lotDataDtos.FirstOrDefault().Package.ToString(),
                Device = lotDataDtos.FirstOrDefault().Device.ToString(),
                FlowName = lotDataDtos.FirstOrDefault().FlowName.ToString()
            });

            //var data = reposity.LotDatas();
            //return Ok(lotDataDtos);
        }

        [HttpPost(), Route("details")]
        public IHttpActionResult GetLotData([FromBody] ValidateDataDto setupData)
        {
            return Ok(new ValidationResultDto<ValidateDataDto>()
            {
                Source = setupData,
                IsError = false,
                Message = "This is a test"
            });
        }

        public IEnumerable<LotData> LotDatas()
        {
            throw new NotImplementedException();
        }
    }
}
