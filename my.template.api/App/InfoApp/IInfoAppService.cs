using vm.common.api.Models;

namespace my.template.api.App.InfoApp;

/// <summary>
/// Interface for the InfoAppService to retrieve application version and deployment information.
/// </summary>
public interface IInfoAppService : IDisposable
{
  /// <summary>
  /// Retrieves the version and deployment information of the application.
  /// </summary>
  /// <returns>A Task representing the asynchronous operation, with a BaseResponse containing version information.</returns>
  Task<BaseResponse<AppVersion>> GetVersion();
}
