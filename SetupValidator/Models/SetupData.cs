using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SetupValidator.Models
{
    public class SetupData
    {
        public SetupData()
        {
            TesterIdList = new List<string>();
            EquipmentIdList = new List<EquipmentFormat>();
            JigIdList = new List<string>();
            MaterialIdList = new List<string>();
            CarrierIdList = new List<string>();
        }

        //int machineId, int[] equipmentIdArray, int[] jigIdArray, int[] materialIdArray, int[] carrierIdAddray
        public int MachineId { get; set; }
        public string PackageName { get; set; }
        public string DeviceName { get; set; }
        public string ProgramName { get; set; }
        public string TesterType { get; set; }
        public string TestFlow { get; set; }
        public string PCType { get; set; }
        public string PCMain { get; set; }
        public List<string> TesterIdList { get; set; }
        public List<EquipmentFormat> EquipmentIdList { get; set; }
        public List<string> JigIdList { get; set; }
        public List<string> MaterialIdList { get; set; }
        public List<string> CarrierIdList { get; set; }        
    }

    public class EquipmentFormat
    {
        public string EquipmentName { get; set; }
        public string EquipmentTypeName { get; set; }
    }
}