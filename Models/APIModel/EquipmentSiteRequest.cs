using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.APIModel
{
  public   class EquipmentSiteRequest
    {
        [Key]
        public int? Id { get; set; }
        public string SrmNo { get; set; }

        public int X { get; set; }

        public int Y { get; set; }
    }
}
