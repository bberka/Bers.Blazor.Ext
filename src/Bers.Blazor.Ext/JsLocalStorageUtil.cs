using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Bers.Blazor.Ext.Javascript;
public interface IJsLocalStorageUtil
{
  public Task SetValue(string key, string value);
  public Task RemoveValue(string key);
  public Task Clear();
  public Task<string?> GetValue(string key, string defaultValue = "");
  public Task<T?> GetValue<T>(string key, T defaultValue = default);
  public Task<T?> GetValueJson<T>(string key, T defaultValue = default);
}

public class JsLocalStorageUtil : IJsLocalStorageUtil
{
  private readonly IJSRuntime _jsRuntime;

  public JsLocalStorageUtil(IJSRuntime jsRuntime) {
    _jsRuntime = jsRuntime;
  }

  public async Task SetValue(string key, string value) {
    var evalStr = $"localStorage.setItem(\"{key}\",\"{value}\")";
    await _jsRuntime.InvokeVoidAsync("eval", evalStr);
  }

  public async Task RemoveValue(string key) {
    var evalStr = $"localStorage.removeItem(\"{key}\")";
    await _jsRuntime.InvokeVoidAsync("eval", evalStr);
  }

  public async Task Clear() {
    var evalStr = "localStorage.clear()";
    await _jsRuntime.InvokeVoidAsync("eval", evalStr);
  }

  public async Task<string?> GetValue(string key, string defaultValue = "") {
    var evalStr = $"localStorage.getItem(\"{key}\")";
    return await _jsRuntime.InvokeAsync<string>("eval", evalStr);
  }

  public async Task<T?> GetValue<T>(string key, T defaultValue = default) {
    var evalStr = $"localStorage.getItem(\"{key}\")";
    var value = await _jsRuntime.InvokeAsync<string>("eval", evalStr);
    if (string.IsNullOrEmpty(value)) {
      return defaultValue;
    }
    return (T)Convert.ChangeType(value, typeof(T));
  }

  public async Task<T?> GetValueJson<T>(string key, T defaultValue = default) {
    var evalStr = $"localStorage.getItem(\"{key}\")";
    var value = await _jsRuntime.InvokeAsync<string>("eval", evalStr);
    if (string.IsNullOrEmpty(value)) {
      return defaultValue;
    }
    return JsonSerializer.Deserialize<T>(value);
    
  }
}


