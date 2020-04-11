namespace NServiceBus
{
    using System.Threading.Tasks;

    /// <summary>
    /// Represents an endpoint that has installers that can be run
    /// </summary>
    public interface IInstallableEndpoint
    {
        /// <summary>
        /// Runs the installers for the endpoint
        /// </summary>
        Task RunInstallers();
    }
}