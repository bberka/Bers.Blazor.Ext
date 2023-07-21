using Microsoft.JSInterop;

namespace Bers.Blazor.Ext.Javascript;

public static class JsExtensions
{
  public static async Task SetFocus(this IJSObjectReference reference) {
    await reference.InvokeVoidAsync("focus");
  }

  public static async Task SetInnerHTML(this IJSObjectReference reference, string html) {
    await reference.InvokeVoidAsync("setInnerHTML", html);
  }
  public static async Task GetInnerHTML(this IJSObjectReference reference) {
    await reference.InvokeVoidAsync("getInnerHTML");
  }

  public static async Task GetOuterHTML(this IJSObjectReference reference) {
    await reference.InvokeVoidAsync("getOuterHTML");
  }

  public static async Task AddClass(this IJSObjectReference reference, string className) {
    await reference.InvokeVoidAsync("classList.add", className);
  }

  public static async Task RemoveClass(this IJSObjectReference reference, string className) {
    await reference.InvokeVoidAsync("classList.remove", className);
  }

  public static async Task SetAttribute(this IJSObjectReference reference, string attributeName,
    string attributeValue) {
    await reference.InvokeVoidAsync("setAttribute", attributeName, attributeValue);
  }


  public static async Task<string> GetAttribute(this IJSObjectReference reference, string attributeName) {
    return await reference.InvokeAsync<string>("getAttribute", attributeName);
  }

  public static async Task RemoveAttribute(this IJSObjectReference reference, string attributeName) {
    await reference.InvokeVoidAsync("removeAttribute", attributeName);
  }
  public static async Task AppendChild(this IJSObjectReference reference, IJSObjectReference child) {
    await reference.InvokeVoidAsync("appendChild", child);
  }

  public static async Task RemoveChild(this IJSObjectReference reference, IJSObjectReference child) {
    await reference.InvokeVoidAsync("removeChild", child);
  }

  public static async Task Blur(this IJSObjectReference reference) {
    await reference.InvokeVoidAsync("blur");
  }

  public static async Task<string[]> GetClassList(this IJSObjectReference reference) {
    return (await reference.GetAttribute("class")).Split(" ");
  }
  public static async Task<string[]> GetStyle(this IJSObjectReference reference) {
    return (await reference.GetAttribute("style")).Split(";");
  }

  public static async Task<string> GetId(this IJSObjectReference reference) {
    return await reference.GetAttribute("id");
  }

  public static async Task<string> GetName(this IJSObjectReference reference) {
    return await reference.GetAttribute("name");
  }

}