using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SetupValidator.Domain
{
    public interface IEquipmentValidator
    {
        ValidationResult Validate(int machineId, List<int> equipmentIdList, List<int> jigIdList, List<int> materialIdList, List<int> carrierIdList);
    }
}