using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportTestConsoleApp.DB
{
    public class Vagon
    {
        [Key]
        [Column(Order=1)]
        public int CarNumber  { get; set; }
        
        public string InvoiceNum { get; set; }

        public int PositionInTrain { get; set; }
        
        public string LastStationName { get; set; }

        [Key]
        [Column(Order=2)]
        public DateTime WhenLastOperation { get; set; }

        public string LastOperationName { get; set; }

        public string FreightEtsngName { get; set; }

        public decimal FreightTotalWeightKg { get; set; }
        private int TrainID { get; set; }
        public virtual NatListTrain NatListTrain { get; set; }
    }
}
