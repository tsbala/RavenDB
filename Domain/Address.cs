namespace Domain
{
    public class Address
    {
        public int Id { get; set; }
        public string HouseNumber { get; set; }
        public string HouseName { get; set; }
        public string StreetName { get; set; }
        public string Town { get; set; }
        public string County { get; set; }
        public string Country { get; set; }
        public string Postcode { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
