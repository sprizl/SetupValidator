using SetupValidator.Abstract;
using SetupValidator.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SetupValidator.Concrete
{
    public class AdoNetNormalRepository : INormalRepository
    {
        //Get Lot Data Details
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

        //Get SetupData from FTSetupReport and other Table
        public IEnumerable<SetupData> SetupDatas(string mcNo)
        {
            List<SetupData> setupDatas = new List<SetupData>();
            var conn = new SqlConnection(Properties.Settings.Default.SPConnecting);
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "EXEC [StoredProcedureDB].[dbo].[sp_get_validator_ftsetupreport] @mcNo = '" + mcNo + "'";

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        SetupData methodSetupData = new SetupData();

                        if (!(reader["MachineId"] is DBNull)) methodSetupData.MachineId = int.Parse(reader["MachineId"].ToString());
                        if (!(reader["PackageName"] is DBNull)) methodSetupData.PackageName = reader["PackageName"].ToString();
                        if (!(reader["DeviceName"] is DBNull)) methodSetupData.DeviceName = reader["DeviceName"].ToString();
                        if (!(reader["ProgramName"] is DBNull)) methodSetupData.ProgramName = reader["ProgramName"].ToString();
                        if (!(reader["TesterType"] is DBNull)) methodSetupData.TesterType = reader["TesterType"].ToString();
                        if (!(reader["TestFlow"] is DBNull)) methodSetupData.TestFlow = reader["TestFlow"].ToString();
                        if (!(reader["PCType"] is DBNull)) methodSetupData.TestFlow = reader["PCType"].ToString();
                        if (!(reader["PCMain"] is DBNull)) methodSetupData.TestFlow = reader["PCMain"].ToString();
                        if (!(reader["TesterNoAQRcode"] is DBNull)) methodSetupData.TesterIdList.Add(reader["TesterNoAQRcode"].ToString());
                        if (!(reader["TesterNoBQRcode"] is DBNull)) methodSetupData.TesterIdList.Add(reader["TesterNoBQRcode"].ToString());
                        if (!(reader["TestBoxAQRcode"] is DBNull)) methodSetupData.EquipmentIdList.Add(reader["TestBoxAQRcode"].ToString());
                        if (!(reader["TestBoxBQRcode"] is DBNull)) methodSetupData.EquipmentIdList.Add(reader["TestBoxBQRcode"].ToString());
                        if (!(reader["AdaptorAQRcode"] is DBNull)) methodSetupData.EquipmentIdList.Add(reader["AdaptorAQRcode"].ToString());
                        if (!(reader["AdaptorBQRcode"] is DBNull)) methodSetupData.EquipmentIdList.Add(reader["AdaptorBQRcode"].ToString());
                        if (!(reader["DutcardAQRcode"] is DBNull)) methodSetupData.EquipmentIdList.Add(reader["DutcardAQRcode"].ToString());
                        if (!(reader["DutcardBQRcode"] is DBNull)) methodSetupData.EquipmentIdList.Add(reader["DutcardBQRcode"].ToString());
                        if (!(reader["BridgecableAQRcode"] is DBNull)) methodSetupData.EquipmentIdList.Add(reader["BridgecableAQRcode"].ToString());
                        if (!(reader["BridgecableBQRcode"] is DBNull)) methodSetupData.EquipmentIdList.Add(reader["BridgecableBQRcode"].ToString());
                        if (!(reader["OptionType1QRcode"] is DBNull)) methodSetupData.EquipmentIdList.Add(reader["OptionType1QRcode"].ToString());
                        if (!(reader["OptionType2QRcode"] is DBNull)) methodSetupData.EquipmentIdList.Add(reader["OptionType2QRcode"].ToString());
                        if (!(reader["OptionType3QRcode"] is DBNull)) methodSetupData.EquipmentIdList.Add(reader["OptionType3QRcode"].ToString());
                        if (!(reader["OptionType4QRcode"] is DBNull)) methodSetupData.EquipmentIdList.Add(reader["OptionType4QRcode"].ToString());
                        if (!(reader["OptionType5QRcode"] is DBNull)) methodSetupData.EquipmentIdList.Add(reader["OptionType5QRcode"].ToString());
                        if (!(reader["OptionType6QRcode"] is DBNull)) methodSetupData.EquipmentIdList.Add(reader["OptionType6QRcode"].ToString());
                        if (!(reader["OptionType7QRcode"] is DBNull)) methodSetupData.EquipmentIdList.Add(reader["OptionType7QRcode"].ToString());
                        if (!(reader["QRCodesocketChannel1"] is DBNull)) methodSetupData.JigIdList.Add(reader["QRCodesocketChannel1"].ToString());
                        if (!(reader["QRCodesocketChannel2"] is DBNull)) methodSetupData.JigIdList.Add(reader["QRCodesocketChannel2"].ToString());
                        if (!(reader["QRCodesocketChannel3"] is DBNull)) methodSetupData.JigIdList.Add(reader["QRCodesocketChannel3"].ToString());
                        if (!(reader["QRCodesocketChannel4"] is DBNull)) methodSetupData.JigIdList.Add(reader["QRCodesocketChannel4"].ToString());

                        setupDatas.Add(methodSetupData);
                    }
                    conn.Close();
                }
                return setupDatas;
            }
        }

        //Get BomId
        public IEnumerable<Bom> BomDatas(string packageName, string deviceName, string testerType, string testFlow, string pcMain)
        {
            List<Bom> bomId = new List<Bom>();
            var conn = new SqlConnection(Properties.Settings.Default.SPConnecting);
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "EXEC [StoredProcedureDB].[dbo].[sp_get_setupchecksheet_getbom]"
                    + "@PackageName = '" + packageName + "'"
                    + "@CustomerDeviceName = '" + deviceName + "'"
                    + "@TesterTypeName = '" + testerType + "'"
                    + "@TestFlowName = '" + testFlow + "'"
                    + "@PCMain = '" + pcMain + "'";

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Bom methodBomData = new Bom();

                        if (!(reader["ID"] is DBNull)) methodBomData.BomId = int.Parse(reader["ID"].ToString());

                        bomId.Add(methodBomData);
                    }
                    conn.Close();
                }
                return bomId;
            }
        }

        //Get Equipment
        //public IEnumerable<> (int bomId)
        //{
        //    List<Bom> bomId = new List<Bom>();
        //    var conn = new SqlConnection(Properties.Settings.Default.SPConnecting);
        //    using (var cmd = conn.CreateCommand())
        //    {
        //        cmd.CommandText = "EXEC [StoredProcedureDB].[dbo].[sp_get_setupchecksheet_getbom]"
        //            + "@PackageName = '" + packageName + "'"
        //            + "@CustomerDeviceName = '" + deviceName + "'"
        //            + "@TesterTypeName = '" + testerType + "'"
        //            + "@TestFlowName = '" + testFlow + "'"
        //            + "@PCMain = '" + pcMain + "'";

        //        conn.Open();
        //        using (var reader = cmd.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {
        //                Bom methodBomData = new Bom();

        //                if (!(reader["ID"] is DBNull)) methodBomData.BomId = int.Parse(reader["ID"].ToString());

        //                bomId.Add(methodBomData);
        //            }
        //            conn.Close();
        //        }
        //        return bomId;
        //    }
        //}
    }
}