 

namespace ChatRoomWithBot.Domain.Entities
{
    public class Stock
    {
        
        public string Symbol { get; set; }
        public DateTime  Date { get; set; }
        public DateTime Time { get; set; }
        public decimal  Open { get; set; }
        public decimal  Close { get; set; }
        public double Volume { get; set; }
    }

}
