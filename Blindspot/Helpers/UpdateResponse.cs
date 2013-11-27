namespace Blindspot.Helpers
{
    public class UpdateResponse
    {
        public UpdateResponseVersion stable { get; set; }
        public UpdateResponseVersion beta { get; set; }
        public UpdateResponseVersion dev { get; set; }
    }

    public class UpdateResponseVersion
    {
        public string version { get; set; }
        public string filename { get; set; }
    }
}
