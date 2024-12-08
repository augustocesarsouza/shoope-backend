namespace Shoope.Application.DTOs
{
    public class TimeEndPromotionFlashOfferDTO
    {
        public DateTime TimeEnd { get; set; }

        public TimeEndPromotionFlashOfferDTO()
        {
        }

        public TimeEndPromotionFlashOfferDTO(DateTime timeEnd)
        {
            TimeEnd = timeEnd;
        }
    }
}
