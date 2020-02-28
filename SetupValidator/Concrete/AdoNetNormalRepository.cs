using SetupValidator.Abstract;
using SetupValidator.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SetupValidator.Concrete
{
    public class AdoNetNormalRepository : INormalRepository
    {
        public IEnumerable<LotData> LotDatas()
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
                return lotDatas;
            }
        }

        public IEnumerable<ValidatorData> SetupDatas(string mcNo)
        {
            List<ValidatorData> setupDatas = new List<ValidatorData>();
            var conn = new SqlConnection(Properties.Settings.Default.SPConnecting);
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "EXEC [StoredProcedureDB].[dbo].[sp_get_validator_ftsetupreport]"
                                + "@mcNo = '" + mcNo + "', ";
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ValidatorData methodSetupData = new ValidatorData();

                        if (!(reader["MachineId"] is DBNull)) methodSetupData.machineId = int.Parse(reader["MachineId"].ToString());
                        if (!(reader["TestBoxAQRcode"] is DBNull)) methodSetupData.equipmentIdList.Add(reader["TestBoxAQRcode"].ToString());
                        if (!(reader["TestBoxBQRcode"] is DBNull)) methodSetupData.equipmentIdList.Add(reader["TestBoxBQRcode"].ToString());
                        if (!(reader["QRCodesocketChannel1"] is DBNull)) methodSetupData.jigIdList.Add(reader["QRCodesocketChannel1"].ToString());
                        if (!(reader["QRCodesocketChannel2"] is DBNull)) methodSetupData.jigIdList.Add(reader["QRCodesocketChannel2"].ToString());
                        
                        setupDatas.Add(methodSetupData);
                    }
                    conn.Close();
                }
                return setupDatas;
            }
        }
    }
}