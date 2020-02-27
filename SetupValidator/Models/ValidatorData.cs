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
        public List<int> equipmentIdList { get; set; }
        public List<int> jigIdList { get; set; }
        public List<int> materialIdList { get; set; }
        public List<int> carrierIdList { get; set; }
    }
}