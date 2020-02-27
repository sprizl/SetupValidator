using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SetupValidator.Domain
{
    public class FTEquipmentValidator : IEquipmentValidator
    {
        public ValidationResult Validate(int machineId, List<int> equipmentIdList, List<int> jigIdList, List<int> materialIdList, List<int> carrierIdList)
        {
            return new ValidationResult(true, "Mockup validate is completed");
        }
    }
}