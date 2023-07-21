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