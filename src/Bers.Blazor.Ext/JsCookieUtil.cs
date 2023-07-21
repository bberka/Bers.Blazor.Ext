using Microsoft.JSInterop;

namespace Bers.Blazor.Ext.Javascript;

public interface IJsCookieUtil
{
  public Task SetValue(string key, string value, TimeSpan timeSpan);
  public Task SetValue(string key, string value);
  public Task<string> GetValue(string key, string defaultValue = "");
  public void SetDefaultExpireTime(TimeSpan timeSpan);
}

public class JsCookieUtil : IJsCookieUtil
{
  private readonly IJSRuntime _jsRuntime;
  private string _expires = "";

  public JsCookieUtil(IJSRuntime jsRuntime) {
    _jsRuntime = jsRuntime;
    SetDefaultExpireTime(TimeSpan.FromDays(365));
  }
  public void SetDefaultExpireTime(TimeSpan timeSpan) {
     _expires = TimeSpanToUtc(timeSpan);
  }

  public async Task SetValue(string key, string value, TimeSpan timeSpan) {
    await SetCookie($"{key}={value}; expires={TimeSpanToUtc(timeSpan)}; path=/");
  }


  public async Task SetValue(string key, string value) {
    await SetCookie($"{key}={value}; expires={_expires}; path=/");
  }

  public async Task<string> GetValue(string key, string defaultValue = "") {
    var cValue = await GetCookie();
    if (string.IsNullOrEmpty(cValue)) return defaultValue;
    var values = cValue.Split(';');
    foreach (var val in values)
      if (!string.IsNullOrEmpty(val) && val.IndexOf('=') > 0)
        if (val[..val.IndexOf('=')].Trim().Equals(key, StringComparison.OrdinalIgnoreCase))
          return val[(val.IndexOf('=') + 1)..];
    return defaultValue;
  }

  private async Task SetCookie(string value) {
    await _jsRuntime.InvokeVoidAsync("eval", $"document.cookie = \"{value}\"");
  }

  private async Task<string> GetCookie() {
    return await _jsRuntime.InvokeAsync<string>("eval", "document.cookie");
  }

  private static string TimeSpanToUtc(TimeSpan timeSpan) {
    var isInFuture = timeSpan.TotalSeconds > 0;
    if (!isInFuture) throw new InvalidOperationException(nameof(timeSpan) + " must be in future");
    return DateTime.Now.AddMinutes(timeSpan.TotalMinutes).ToUniversalTime().ToString("R");

  }
}
