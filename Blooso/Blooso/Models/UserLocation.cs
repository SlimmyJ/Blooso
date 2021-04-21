namespace Blooso.Models
{
    public class UserLocation
    {
        public int AreaCode { get; set; }

        public UserLocation(int areaCode = 8000)
        {
            AreaCode = areaCode;
        }
    }
}