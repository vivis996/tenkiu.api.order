using tenkiu.api.order.Models.Response;
using Newtonsoft.Json;
using vm.common;
using vm.common.api.Models;
using vm.common.Services.ConfigService;
using vm.common.Utils;

namespace tenkiu.api.order.App.AuthApp;

public class ExternalAuthAppService(
  IConfigService configService,
  IHttpContextAccessor httpContextAccessor
) : DisposableBase, IAuthAppService
{
  public async Task<BaseResponse<ValidateDto>> ValidateToken()
  {
    using var httpClient = new HttpClient();
    var request = new HttpRequestMessage(HttpMethod.Post, $"{this.GetBaseUrl()}/api/auth/v1/Auth/validate");
    request.Headers.Add("X-Forwarded-For", httpContextAccessor.HttpContext?.GetClientIp());
    this.AddAuthentication(httpClient);
    var response = await httpClient.SendAsync(request);
    return await this.ParseResponse<ValidateDto>(response);
  }

  private async Task<BaseResponse<T>> ParseResponse<T>(HttpResponseMessage response)
  {
    var responseData = await response.Content.ReadAsStringAsync();

    if (response.IsSuccessStatusCode)
      return JsonConvert.DeserializeObject<SuccessResponse<T>>(responseData);

    return JsonConvert.DeserializeObject<FailureResponse<T>>(responseData);
  }

  private void AddAuthentication(HttpClient httpClient)
  {
    var token = httpContextAccessor.HttpContext?.Request.Headers.Authorization.FirstOrDefault();
    if (token is null) return;
    token = token.Replace("Bearer ", string.Empty);
    httpClient.DefaultRequestHeaders.Authorization = new("Bearer", token);
  }

  private string GetBaseUrl()
  {
    return configService.GetConfig(Config.Auth.SectionName, Config.Auth.ApiBaseUrl);
  }

  protected override void DisposeResources()
  {
    configService.Dispose();
  }
}
