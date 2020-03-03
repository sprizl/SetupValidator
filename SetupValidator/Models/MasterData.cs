using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SetupValidator.Models
{
    public class MasterData
    {
    }

    public class Bom
    {
        public int BomId { get; set; }
    }

    public class Equipment
    {
        public int EquipmentId { get; set; }
        public string EquipmentQRName { get; set; }
        public string EquipmentName { get; set; }
        public int EquipmentTypeId { get; set; }
        public string EquipmentType { get; set; }
    }

    public class Option
    {
        public int OptionId { get; set; }
        public string OptionQRName { get; set; }
        public string OptionName { get; set; }
        public string OptionGroupName { get; set; }
        public int Quantity { get; set; }
        public string Setting { get; set; }
        public string OptionCategory { get; set; }
    }
}