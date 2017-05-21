using System;
namespace MyCompanyInThePocket.Core.Services
{
    public interface IBackgroundTaskService
    {
        /// <summary>
        /// Gets the minimum interval in minutes
        /// </summary>
        /// <value>The minimum interval.</value>
        double MinimumInterval { get; }

        /// <summary>
        /// Register the specified intervalBetweenUpdate.
        /// </summary>
        /// <returns>The register.</returns>
        /// <param name="intervalBetweenUpdate">Interval in minutes between update.</param>
        void Register(double intervalBetweenUpdate);

        /// <summary>
        /// Uns the register.
        /// </summary>
        void UnRegister();

		/// <summary>
		/// Runs the in background.
		/// </summary>
		/// <returns>The in background.</returns>
        System.Threading.Tasks.Task RunInBackgroundAsync();
    }
}
