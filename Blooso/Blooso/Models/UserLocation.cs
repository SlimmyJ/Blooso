namespace Blooso.Models
{
    public class UserLocation
    {
        public int Id { get; set; }

        public int AreaCode { get; set; }

        public UserLocation(int areaCode = 8000) => this.AreaCode = areaCode;
    }
}