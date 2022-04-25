using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportTestConsoleApp.DB
{
    public class NatListTrain
    {
        
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int TrainId { get; set; }
        
        public string TrainIndexCombined { get; set; }

        public string TrainNumber { get; set; }

        public string FromStationName { get; set; }

        public string ToStationName { get; set; }

        public virtual List<Vagon> Vagons { get; set; }
        
    }
}
