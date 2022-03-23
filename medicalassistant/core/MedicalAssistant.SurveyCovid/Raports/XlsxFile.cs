namespace MedicalAssistant.SurveyCovid.Raports
{
    public class XlsxFile
    {
        public XlsxFile(byte[] byteContent, string contentType)
        {
            ByteContent = byteContent;
            ContentType = contentType;
        }

        public byte[] ByteContent { get; }

        public string ContentType { get; }
    }
}
