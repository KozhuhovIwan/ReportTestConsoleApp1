using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using ReportTestConsoleApp.DB;

namespace ReportTestConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var parsedList = ParseXML();
            using (TestContext dbContext = new TestContext())
            {
                // создаем два объекта User
                //NatListTrain train1 = new NatListTrain() { TrainNumber = "1111112"};
                dbContext.Trains.AddRange(parsedList);
                dbContext.SaveChanges();

                var temp = dbContext.Trains.Select(t => t).ToList();
            }

        }

        public static List<NatListTrain> ParseXML()
        {
            var curDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            
            XDocument xml = XDocument.Load(curDir + @"\Data.xml");
            var Items = xml.Descendants("row").Select(x => new { TrainNumber = (string)x.Element("TrainNumber")
                                                                                                    , TrainIndexCombined = (string)x.Element("TrainIndexCombined")
                                                                                                    , FromStationName = (string)x.Element("FromStationName")
                                                                                                    , ToStationName = (string)x.Element("ToStationName")
                                                                                                    , LastStationName = (string)x.Element("LastStationName")
                                                                                                    , WhenLastOperation = (string)x.Element("WhenLastOperation")
                                                                                                    , LastOperationName = (string)x.Element("LastOperationName")
                                                                                                    , InvoiceNum = (string)x.Element("InvoiceNum")
                                                                                                    , PositionInTrain = (string)x.Element("PositionInTrain")
                                                                                                    , CarNumber = (string)x.Element("CarNumber")
                                                                                                    , FreightEtsngName = (string)x.Element("FreightEtsngName")
                                                                                                    , FreightTotalWeightKg = (string)x.Element("FreightTotalWeightKg") }).ToList();
            
            var ttt = Items.GroupBy(t => t.TrainIndexCombined).ToList();

            List<NatListTrain> List = new List<NatListTrain>();

            foreach (var item in ttt)
            {
                var firstNatListElement = item.First();

                NatListTrain natList = new NatListTrain()
                {
                    TrainNumber = firstNatListElement.TrainNumber,
                    FromStationName = firstNatListElement.FromStationName,
                    ToStationName = firstNatListElement.ToStationName,
                    TrainIndexCombined = firstNatListElement.TrainIndexCombined,
                    TrainId = firstNatListElement.TrainIndexCombined.GetHashCode(),
                    Vagons = new List<Vagon>()
                };
                
                var vagonGroups = item.GroupBy(t => t.CarNumber).ToList();

                foreach (var group in vagonGroups)    
                {
                    foreach (var vagon in group)
                    {
                        Vagon NatListVagon = new Vagon()
                        {
                            NatListTrain = natList,
                            CarNumber = int.Parse(vagon.CarNumber),
                            InvoiceNum = vagon.InvoiceNum,
                            WhenLastOperation = DateTime.Parse(vagon.WhenLastOperation),
                            PositionInTrain = int.Parse(vagon.PositionInTrain),
                            LastStationName = vagon.LastStationName,
                            FreightEtsngName = vagon.FreightEtsngName,
                            FreightTotalWeightKg = int.Parse(vagon.FreightTotalWeightKg),
                            LastOperationName = vagon.LastOperationName
                        };
                        natList.Vagons.Add(NatListVagon);
                    }

                }
                List.Add(natList);
            }

            return List;
        }
    }
}
