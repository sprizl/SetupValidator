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

        public ValidatorController()
        {
            reposity = new AdoNetNormalRepository();
        }

        public ValidatorController(INormalRepository repo)
        {
            reposity = repo;
        }

        [HttpGet(), Route("")]
        [ResponseType(typeof(LotDataDto))]
        public IHttpActionResult GetAllLotData()
        {
            var data = reposity.LotDatas();
            List<LotDataDto> lotDataDtos = new List<LotDataDto>();

            for (int i = 0; i < 5; i++)
            {
                LotDataDto lotData = new LotDataDto();

                lotData.LotNo = data.ElementAt(i).lotNo.Trim();
                lotData.Package = data.ElementAt(i).packageName.Trim();
                lotData.Device = data.ElementAt(i).deviceName.Trim();
                lotData.FlowName = data.ElementAt(i).flowName.Trim();

                lotDataDtos.Add(lotData);
            }

            return Ok(lotDataDtos);

            //foreach (var item in data)
            //{
            //    LotDataDto lotData = new LotDataDto();

            //    lotData.LotNo = item.lotNo.Trim();
            //    lotData.Package = item.packageName.Trim();
            //    lotData.Device = item.deviceName.Trim();
            //    lotData.FlowName = item.flowName.Trim();

            //    lotDatas.Add(lotData);
            //    //lotDataDto.LotNo = item.lotNo.Trim();
            //    //lotDataDto.Package = item.packageName.Trim();
            //    //lotDataDto.Device = item.deviceName.Trim();
            //    //lotDataDto.FlowName = item.flowName.Trim();
            //}

            //LotDataDto lotDataDto = new LotDataDto();
            //lotDataDto.LotNo = lotDatas.Select(g => g.lotNo).ToString();
            //lotDataDto.Package = lotDatas.Select(g => g.packageName).ToString();
            //lotDataDto.Device = lotDatas.Select(g => g.deviceName).ToString();
            //lotDataDto.FlowName = lotDatas.Select(g => g.flowName).ToString();
        }

        [HttpPost(), Route("details")]
        [ResponseType(typeof(LotDataDto))]
        public IHttpActionResult GetLotData([FromBody] ValidateDataDto setupData)
        {
            return Ok(new ValidationResultDto<ValidateDataDto>()
            {
                Source = setupData,
                IsError = false,
                Message = "This is a test"
            });

            //var setupDetails = reposity.SetupDatas(validateDataDto.machineName);

            //ValidatorData validatorData = new ValidatorData();

            //foreach (var item in setupDetails.Select(g => g.equipmentIdList))
            //{
            //    validatorData.equipmentIdList.Add(item.ToString());
            //}
            //foreach (var item in setupDetails.Select(g => g.jigIdList))
            //{
            //    validatorData.jigIdList.Add(item.ToString());
            //}
            //foreach (var item in setupDetails.Select(g => g.materialIdList))
            //{
            //    validatorData.materialIdList.Add(item.ToString());
            //}
            //foreach (var item in setupDetails.Select(g => g.carrierIdList))
            //{
            //    validatorData.carrierIdList.Add(item.ToString());
            //}

            ////List<int> lstEquipmentId = new List<int>();
            ////lstEquipmentId.Add(1);
            ////lstEquipmentId.Add(2);

            ////validatorData.equipmentIdList = lstEquipmentId;

            ////List<int> lstJigId = new List<int>();
            ////lstJigId.Add(2);

            ////validatorData.jigIdList = lstJigId;

            ////List<int> lstMaterialId = new List<int>();
            ////lstMaterialId.Add(3);

            ////validatorData.materialIdList = lstMaterialId;

            ////List<int> lstCarrierId = new List<int>();
            ////lstCarrierId.Add(4);

            ////validatorData.carrierIdList = lstCarrierId;

            //FTEquipmentValidator validator = new FTEquipmentValidator();

            //var result = validator.Validate(int.Parse(setupDetails.Select(g => g.machineId).ToString())
            //                              , validatorData.equipmentIdList
            //                              , validatorData.jigIdList
            //                              , validatorData.materialIdList
            //                              , validatorData.carrierIdList);

            //return Ok(result);

            ////var lotDetails = reposity.LotDatas();
            ////List<LotData> lotDetailList = new List<LotData>();
            ////lotDetailList = lotDetails.Where(g => g.LotNo.Contains(validateDataDto.LotNo)).ToList();

            ////if (lotDetailList.Count == 0)
            ////{
            ////    return NotFound();
            ////}
            ////return Ok(lotDetailList);
        }

        public IEnumerable<LotData> LotDatas()
        {
            throw new NotImplementedException();
        }
    }
}
