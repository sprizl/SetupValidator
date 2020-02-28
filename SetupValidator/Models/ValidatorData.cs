using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SetupValidator.Models
{
    public class ValidatorData
    {
        //int machineId, int[] equipmentIdArray, int[] jigIdArray, int[] materialIdArray, int[] carrierIdAddray
        public int machineId { get; set; }
        public List<string> equipmentIdList { get; set; }
        public List<string> jigIdList { get; set; }
        public List<string> materialIdList { get; set; }
        public List<string> carrierIdList { get; set; }
    }
}