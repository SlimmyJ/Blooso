namespace Blooso.Models
{
    public class UserLocation
    {
        public UserLocation(int areaCode = 8000)
        {
            this.AreaCode = areaCode;
        }

        public int AreaCode { get; set; }
    }
}