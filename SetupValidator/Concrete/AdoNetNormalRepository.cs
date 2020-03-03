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
                cmd.CommandText = "EXEC [StoredProcedureDB].[dbo].[sp_get_validator_ftsetupreport] " +
                                  "@mcNo = '" + mcNo + "'";

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        SetupData methodSetupData = new SetupData();
                        EquipmentFormat equipmentFormat = new EquipmentFormat();

                        if (!(reader["MachineId"] is DBNull)) methodSetupData.MachineId = int.Parse(reader["MachineId"].ToString());
                        if (!(reader["PackageName"] is DBNull)) methodSetupData.PackageName = reader["PackageName"].ToString();
                        if (!(reader["DeviceName"] is DBNull)) methodSetupData.DeviceName = reader["DeviceName"].ToString();
                        if (!(reader["ProgramName"] is DBNull)) methodSetupData.ProgramName = reader["ProgramName"].ToString();
                        if (!(reader["TesterType"] is DBNull)) methodSetupData.TesterType = reader["TesterType"].ToString();
                        if (!(reader["TestFlow"] is DBNull)) methodSetupData.TestFlow = reader["TestFlow"].ToString();
                        if (!(reader["PCType"] is DBNull)) methodSetupData.PCType = reader["PCType"].ToString();
                        if (!(reader["PCMain"] is DBNull)) methodSetupData.PCMain = reader["PCMain"].ToString();
                        if (!(reader["TesterNoA"] is DBNull)) methodSetupData.TesterIdList.Add(reader["TesterNoA"].ToString());
                        if (!(reader["TesterNoB"] is DBNull)) methodSetupData.TesterIdList.Add(reader["TesterNoB"].ToString());

                        if (!(reader["TestBoxA"] is DBNull)) equipmentFormat.EquipmentName = (reader["TestBoxA"].ToString());
                        if (!(reader["TestBoxAType"] is DBNull)) equipmentFormat.EquipmentTypeName = (reader["TestBoxAType"].ToString());
                        methodSetupData.EquipmentIdList.Add(equipmentFormat);

                        //if (!(reader["TestBoxB"] is DBNull)) methodSetupData.EquipmentIdList.Add(reader["TestBoxB"].ToString());
                        //if (!(reader["AdaptorA"] is DBNull)) methodSetupData.EquipmentIdList.Add(reader["AdaptorA"].ToString());
                        //if (!(reader["AdaptorB"] is DBNull)) methodSetupData.EquipmentIdList.Add(reader["AdaptorB"].ToString());
                        //if (!(reader["DutcardA"] is DBNull)) methodSetupData.EquipmentIdList.Add(reader["DutcardA"].ToString());
                        //if (!(reader["DutcardB"] is DBNull)) methodSetupData.EquipmentIdList.Add(reader["DutcardB"].ToString());
                        //if (!(reader["BridgecableA"] is DBNull)) methodSetupData.EquipmentIdList.Add(reader["BridgecableA"].ToString());
                        //if (!(reader["BridgecableB"] is DBNull)) methodSetupData.EquipmentIdList.Add(reader["BridgecableB"].ToString());
                        //if (!(reader["OptionType1"] is DBNull)) methodSetupData.EquipmentIdList.Add(reader["OptionType1"].ToString());
                        //if (!(reader["OptionType2"] is DBNull)) methodSetupData.EquipmentIdList.Add(reader["OptionType2"].ToString());
                        //if (!(reader["OptionType3"] is DBNull)) methodSetupData.EquipmentIdList.Add(reader["OptionType3"].ToString());
                        //if (!(reader["OptionType4"] is DBNull)) methodSetupData.EquipmentIdList.Add(reader["OptionType4"].ToString());
                        //if (!(reader["OptionType5"] is DBNull)) methodSetupData.EquipmentIdList.Add(reader["OptionType5"].ToString());
                        //if (!(reader["OptionType6"] is DBNull)) methodSetupData.EquipmentIdList.Add(reader["OptionType6"].ToString());
                        //if (!(reader["OptionType7"] is DBNull)) methodSetupData.EquipmentIdList.Add(reader["OptionType7"].ToString());
                        if (!(reader["QRCodesocket1"] is DBNull)) methodSetupData.JigIdList.Add(reader["QRCodesocket1"].ToString());
                        if (!(reader["QRCodesocket2"] is DBNull)) methodSetupData.JigIdList.Add(reader["QRCodesocket2"].ToString());
                        if (!(reader["QRCodesocket3"] is DBNull)) methodSetupData.JigIdList.Add(reader["QRCodesocket3"].ToString());
                        if (!(reader["QRCodesocket4"] is DBNull)) methodSetupData.JigIdList.Add(reader["QRCodesocket4"].ToString());

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
                cmd.CommandText = "EXEC [StoredProcedureDB].[dbo].[sp_get_setupchecksheet_getbom] " +
                                  "@PackageName = '"        +   packageName + "', " +
                                  "@CustomerDeviceName = '" +   deviceName  + "', " +
                                  "@TesterTypeName = '"     +   testerType  + "', " +
                                  "@TestFlowName = '"       +   testFlow    + "', " +
                                  "@PCMain = '"             +   pcMain      + "'";

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
        public IEnumerable<Equipment> EquipmentDatas (int bomId)
        {
            List<Equipment> equipmentDatas = new List<Equipment>();
            var conn = new SqlConnection(Properties.Settings.Default.SPConnecting);
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "EXEC [StoredProcedureDB].[dbo].[sp_get_setupchecksheet_getbomtestequipment] " +
                                  "@bomId = " + bomId;

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Equipment methodEquipmentData = new Equipment();

                        if (!(reader["Name"] is DBNull)) methodEquipmentData.EquipmentName = (reader["Name"].ToString());
                        if (!(reader["TypeName"] is DBNull)) methodEquipmentData.EquipmentTypeName = (reader["TypeName"].ToString());

                        equipmentDatas.Add(methodEquipmentData);
                    }
                    conn.Close();
                }
                return equipmentDatas;
            }
        }

        //Get Option
        public IEnumerable<Option> OptionDatas(int bomId)
        {
            List<Option> optionDatas = new List<Option>();
            var conn = new SqlConnection(Properties.Settings.Default.SPConnecting);
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "EXEC [StoredProcedureDB].[dbo].[sp_get_setupchecksheet_getbomoption] " +
                                  "@bomId = " + bomId;

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Option methodOptionData = new Option();

                        if (!(reader["Name"] is DBNull)) methodOptionData.OptionName = (reader["Name"].ToString());
                        if (!(reader["OptionName"] is DBNull)) methodOptionData.OptionGroupName = (reader["OptionName"].ToString());
                        if (!(reader["Quantity"] is DBNull)) methodOptionData.Quantity = int.Parse(reader["Quantity"].ToString());
                        if (!(reader["Setting"] is DBNull)) methodOptionData.Setting = (reader["Setting"].ToString());
                        if (!(reader["OptionCategory"] is DBNull)) methodOptionData.OptionCategory = (reader["OptionCategory"].ToString());

                        optionDatas.Add(methodOptionData);
                    }
                    conn.Close();
                }
                return optionDatas;
            }
        }
    }
}