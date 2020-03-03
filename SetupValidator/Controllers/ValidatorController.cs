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
    [RoutePrefix("api")]
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
            //return Ok(lotDataDtos);
        }

        [HttpPost(), Route("validate")]
        public IHttpActionResult GetLotData([FromBody] ValidateDataDto setupData)
        {
            //Get Setup Data
            var setupDB = reposity.SetupDatas(setupData.machineName);

            //Keep Setup Data into Variable
            int machineId;
            string packageName, deviceName, testerType, testFlow, pcMain;
            try
            {
                machineId = int.Parse(setupDB.Select(g => g.MachineId).FirstOrDefault().ToString());
                packageName = setupDB.Select(g => g.PackageName).FirstOrDefault().ToString();
                deviceName = setupDB.Select(g => g.DeviceName).FirstOrDefault().ToString();
                testerType = setupDB.Select(g => g.TesterType).FirstOrDefault().ToString();
                testFlow = setupDB.Select(g => g.TestFlow).FirstOrDefault().ToString();
                pcMain = setupDB.Select(g => g.PCMain).FirstOrDefault().ToString();
            }
            catch (Exception)
            {
                return Ok(new ValidationResultDto<ValidateDataDto>()
                {
                    Source = setupData,
                    IsPass = false,
                    Message = "Setup Data Error."
                });
            }
            
            //Get Bom Id for query Option and Equipment from Master Data
            var bomDB = reposity.BomDatas(packageName, deviceName, testerType, testFlow, pcMain);

            //Keep BomId into Variable
            int bomId;
            try
            {
                bomId = int.Parse(bomDB.Select(g => g.BomId).FirstOrDefault().ToString());
            }
            catch (Exception)
            {
                return Ok(new ValidationResultDto<ValidateDataDto>()
                {
                    Source = setupData,
                    IsPass = false,
                    Message = "Bom Not Found."
                });
            }

            //Get Equipment from Master Data (BOM)
            var equipmentDB = reposity.EquipmentDatas(bomId);

            //Get Option from Master Data (BOM)
            var optionDB = reposity.OptionDatas(bomId);

            if (machineId == 1001) {
                return Ok(new ValidationResultDto<ValidateDataDto>()
                {
                    Source = setupData,
                    IsPass = true,
                    Message = ""
                });
            }
            else
            {
                return Ok(new ValidationResultDto<ValidateDataDto>()
                {
                    Source = setupData,
                    IsPass = false,
                    Message = "Not Match"
                });
            }
        }

        public IEnumerable<LotData> LotDatas()
        {
            throw new NotImplementedException();
        }
    }
}
