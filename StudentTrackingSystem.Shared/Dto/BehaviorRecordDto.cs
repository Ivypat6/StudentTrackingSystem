using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTrackingSystem.Shared.Dto
{
    public class BehaviorRecordDto
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public DateTime Date { get; set; }
        public string BehaviorType { get; set; }
        public string Description { get; set; }
        public string ActionTaken { get; set; }
        public string StudentName { get; set; }
    }
}
