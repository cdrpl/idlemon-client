using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using UnityEngine;

public class Http : MonoBehaviour
{
    public static readonly HttpClient Client = new HttpClient();

    /// <summary>
    /// Will send a GET request to the given path.
    /// </summary>
    public static async Task<T> GetRequest<T>(string url) where T : HttpResponse
    {
        HttpResponseMessage response = await Client.GetAsync(url);
        return await ParseResponse<T>(response);
    }

    public static async Task<T> PutRequest<T>(string path, string data = "") where T : HttpResponse
    {
        var content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
        HttpResponseMessage response = await Client.PutAsync(Const.HTTP_URL + path, content);
        return await ParseResponse<T>(response);
    }

    /// <summary>
    /// Will send a POST request to the given path. The string data should be valid JSON.
    /// </summary>
    public static async Task<T> PostRequest<T>(string path, string data = "") where T : HttpResponse
    {
        var content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
        HttpResponseMessage response = await Client.PostAsync(Const.HTTP_URL + path, content);
        return await ParseResponse<T>(response);
    }

    /// <summary>
    /// Will parse the given HttpResponseMessage.
    /// </summary>
    static async Task<T> ParseResponse<T>(HttpResponseMessage response) where T : HttpResponse
    {
        string body = await response.Content.ReadAsStringAsync();

        LogRequest(response, body);

        T res = JsonConvert.DeserializeObject<T>(body);
        res.httpCode = response.StatusCode;
        return res;
    }

    /// <summary>
    /// Will log the HTTP response to the console.
    /// </summary>
    static void LogRequest(HttpResponseMessage response, string body)
    {
        HttpMethod method = response.RequestMessage.Method;
        var uri = response.RequestMessage.RequestUri;
        Debug.Log(method + " " + uri + " " + response.StatusCode + " " + body);
    }
}
