using Microsoft.JSInterop;

namespace Bers.Blazor.Ext.Javascript;

public interface IJsUtil
{
  public void Alert(string message);
  public void ConsoleLog(string message);
  public Task<IJSObjectReference> GetElementById(string id);
  public Task<IJSObjectReference[]> GetElementsByClassName(string id);
  public Task<IJSObjectReference[]> GetElementsByName(string name);
  public Task<IJSObjectReference[]> GetElementsByTagName(string tagName);
  public Task<IJSObjectReference> QuerySelector(string selector);
  public Task<IJSObjectReference[]> QuerySelectorAll(string selector);

}
public class JsUtil : IJsUtil
{
  private readonly IJSRuntime _jsRuntime;

  public JsUtil(IJSRuntime jsRuntime) {
    _jsRuntime = jsRuntime;
  }
  public void Alert(string message) {
    _jsRuntime.InvokeVoidAsync("alert", message);
  }
  public void SetFocusById(string id) {
    _jsRuntime.InvokeVoidAsync("eval", $"document.getElementById('{id}').focus()");
  }
  public void ConsoleLog(string message) {
    _jsRuntime.InvokeVoidAsync("console.log", message);
  }

  public async Task<IJSObjectReference> GetElementById(string id) {
    return await _jsRuntime.InvokeAsync<IJSObjectReference>("eval", $"document.getElementById('{id}')");
  }

  public async Task<IJSObjectReference[]> GetElementsByClassName(string id) {
    return await _jsRuntime.InvokeAsync<IJSObjectReference[]>("eval", $"document.getElementsByClassName('{id}')");
  }

  public async Task<IJSObjectReference[]> GetElementsByName(string name) {
    return await _jsRuntime.InvokeAsync<IJSObjectReference[]>("eval", $"document.getElementsByName('{name}')");
  }

  public async Task<IJSObjectReference[]> GetElementsByTagName(string tagName) {
    return await _jsRuntime.InvokeAsync<IJSObjectReference[]>("eval", $"document.getElementsByTagName('{tagName}')");

  }

  public async Task<IJSObjectReference> QuerySelector(string selector) {
    return await _jsRuntime.InvokeAsync<IJSObjectReference>("document.querySelectorAll", selector);
  }

  public async Task<IJSObjectReference[]> QuerySelectorAll(string selector) {
    return await _jsRuntime.InvokeAsync<IJSObjectReference[]>("document.querySelectorAll", selector);
  }


}