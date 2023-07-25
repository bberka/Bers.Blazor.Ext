using Microsoft.Extensions.DependencyInjection;

namespace Bers.Blazor.Ext.Javascript;

public static class BlazorExtInjectorExtensions
{
  /// <summary>
  /// Adds <see cref="IJsUtil"/> and <see cref="IJsCookieUtil"/> to the service collection
  /// </summary>
  /// <param name="services"></param>
  /// <returns></returns>
  public static IServiceCollection AddBlazorExt(this IServiceCollection services) {
    services.AddScoped<IJsUtil,JsUtil>();
    services.AddScoped<IJsCookieUtil, JsCookieUtil>();
    services.AddScoped<IJsSessionUtil, JsSessionUtil>();
    services.AddScoped<IJsLocalStorageUtil, JsLocalStorageUtil>();
    return services;
  }

}