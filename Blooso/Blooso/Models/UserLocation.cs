namespace Blooso.Models
{
    public class UserLocation
    {
        public UserLocation(int areaCode = 8000)
        {
            AreaCode = areaCode;
        }

        public int AreaCode { get; set; }
    }
}