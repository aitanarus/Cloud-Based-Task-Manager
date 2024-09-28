namespace BlazorWASM.Config
{
    // A POCO (Plain Old CLR Object) is a simple object used in .NET applications that does not depend on any specific framework or technology.
    public class ApiEndpointsOptions
    {
        public const string ApiEndpoints = "ApiEndpoints";
        public string TaskService { get; set; } = String.Empty;
    }
}