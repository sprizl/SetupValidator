using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SetupValidator.Domain
{
    public class FTEquipmentValidator : IEquipmentValidator
    {
        public ValidationResult Validate(int machineId, List<string> equipmentIdList, List<string> jigIdList, List<string> materialIdList, List<string> carrierIdList)
        {
            return new ValidationResult(true, "Mockup validate is completed");
        }
    }
}