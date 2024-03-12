namespace BackEndASP.ExternalAPI.GeoCoder
{
    public class results

    {

        public string formatted_address { get; set; }

        public geometry geometry { get; set; }

        public string[] types { get; set; }

        public address_component[] address_components { get; set; }

    }
}
