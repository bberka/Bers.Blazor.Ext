using Microsoft.JSInterop;

namespace Bers.Blazor.Ext.Javascript;

public interface IJsSessionUtil
{
  public Task SetValue(string key, string value);
  public Task RemoveValue(string key);
  public Task Clear();
  public Task<string> GetValue(string key, string defaultValue = "");
  public Task<int> GetLength();
}

public class JsSessionUtil : IJsSessionUtil
{
  private readonly IJSRuntime _jsRuntime;

  public JsSessionUtil(IJSRuntime jsRuntime) {
    _jsRuntime = jsRuntime;
  }

  public async Task SetValue(string key, string value) {
    await _jsRuntime.InvokeVoidAsync("eval", $"sessionStorage.setItem(\"{key}\",\"{value}\")");
  }

  public async Task RemoveValue(string key) {
    await _jsRuntime.InvokeVoidAsync("eval", $"sessionStorage.removeItem(\"{key}\")");
  }

  public async Task Clear() {
    await  _jsRuntime.InvokeVoidAsync("eval", $"sessionStorage.clear()");
  }

  public async Task<string> GetValue(string key, string defaultValue = "") {
    return await _jsRuntime.InvokeAsync<string>("eval", $"sessionStorage.getItem(\"{key}\")");
  }

  public async Task<int> GetLength() {
    return await _jsRuntime.InvokeAsync<int>("eval", $"sessionStorage.length");
  }
}