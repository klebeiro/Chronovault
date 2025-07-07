namespace chronovault_api.Helpers
{
    public static class EnvironmentVariables
    {
        public static string MinioBaseUrl => Environment.GetEnvironmentVariable("MINIO_BASE_URL") ?? "http://localhost:9000/product-images/";
    }
}
