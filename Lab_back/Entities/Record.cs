using System;

namespace Lab_back.Entities
{
    public class Record
    {
        public string Id { get; set; } = "";
        public string UserId { get; set; } = "";
        public string CategoryId { get; set; } = "";
        public DateTime Created { get; set; }
        public double Sum { get; set; } = 0;
    }
}
