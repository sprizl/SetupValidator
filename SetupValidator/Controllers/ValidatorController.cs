using Newtonsoft.Json;
using SetupValidator.Abstract;
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
        public INormalRepository reposity;

        //public ValidatorController(INormalRepository repo)
        //{
        //    reposity = repo;
        //}

        public ValidatorController()
        {
        }

        [Route("")]
        //[ResponseType(typeof(LotDataDto))]
        [HttpGet]
        public IHttpActionResult GetAllLotData()
        {
            List<LotData> lotDatas = new List<LotData>();
            var conn = new SqlConnection(Properties.Settings.Default.SPConnecting);
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "EXEC [StoredProcedureDB].[atom].[sp_get_trans_lots]";
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        LotData methodLotData = new LotData();

                        if (!(reader["LotNo"] is DBNull)) methodLotData.lotNo = reader["LotNo"].ToString();
                        if (!(reader["PackageId"] is DBNull)) methodLotData.packageId = int.Parse(reader["PackageId"].ToString());
                        if (!(reader["Package"] is DBNull)) methodLotData.packageName = reader["Package"].ToString();
                        if (!(reader["Device"] is DBNull)) methodLotData.deviceName = reader["Device"].ToString();
                        if (!(reader["StepNo"] is DBNull)) methodLotData.stepNo = int.Parse(reader["StepNo"].ToString());
                        if (!(reader["FlowName"] is DBNull)) methodLotData.flowName = reader["FlowName"].ToString();

                        lotDatas.Add(methodLotData);
                    }
                    conn.Close();
                }
                LotDataDto lotDataDto = new LotDataDto();
                lotDataDto.LotNo = lotDatas.Where(g => g.lotNo.Contains("2003A4086V")).Select(g => g.lotNo).FirstOrDefault();
                lotDataDto.Package = lotDatas.Where(g => g.lotNo.Contains("2003A4086V")).Select(g => g.packageName).FirstOrDefault();
                lotDataDto.Device = lotDatas.Where(g => g.lotNo.Contains("2003A4086V")).Select(g => g.deviceName).FirstOrDefault();
                lotDataDto.FlowName = lotDatas.Where(g => g.lotNo.Contains("2003A4086V")).Select(g => g.flowName).FirstOrDefault();

                return Ok(lotDataDto);
            }
        }

        [Route("details")]
        [ResponseType(typeof(LotDataDto))]
        [HttpPost]
        public IHttpActionResult GetLotData([FromBody] ValidateDataDto validateDataDto)
        {
            //try
            //{
            //    var lotDetails = reposity.LotDatas();
            //    List<LotData> lotDetailList = new List<LotData>();
            //    lotDetailList = lotDetails.Where(g => g.lotNo.Contains(validateDataDto.lotNo.Trim())).ToList();
            //}
            //catch (Exception e)
            //{
            //    return Ok(e);
            //}

            //if (lotDetailList.Count <= 0)
            //{
            //    return NotFound();
            //}

            ValidatorData validatorData = new ValidatorData();
            validatorData.machineId = 1;

            List<int> lstEquipmentId = new List<int>();
            lstEquipmentId.Add(1);
            lstEquipmentId.Add(2);

            validatorData.equipmentIdList = lstEquipmentId;

            List<int> lstJigId = new List<int>();
            lstJigId.Add(2);

            validatorData.jigIdList = lstJigId;

            List<int> lstMaterialId = new List<int>();
            lstMaterialId.Add(3);

            validatorData.materialIdList = lstMaterialId;

            List<int> lstCarrierId = new List<int>();
            lstCarrierId.Add(4);

            validatorData.carrierIdList = lstCarrierId;

            FTEquipmentValidator validator = new FTEquipmentValidator();

            var result = validator.Validate(validatorData.machineId, validatorData.equipmentIdList, validatorData.jigIdList, validatorData.materialIdList, validatorData.carrierIdList);

            return Ok(result);

            //var lotDetails = reposity.LotDatas();
            //List<LotData> lotDetailList = new List<LotData>();
            //lotDetailList = lotDetails.Where(g => g.LotNo.Contains(validateDataDto.LotNo)).ToList();

            //if (lotDetailList.Count == 0)
            //{
            //    return NotFound();
            //}
            //return Ok(lotDetailList);
        }

        public IEnumerable<LotData> LotDatas()
        {
            throw new NotImplementedException();
        }
    }
}
