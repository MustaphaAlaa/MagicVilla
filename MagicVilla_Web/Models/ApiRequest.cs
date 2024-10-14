namespace MagicVilla_Web.Models;

public class ApiRequest
{
    public ApiType ApiType { get; set; }
    public object Data { get; set; }
    public string URL { get; set; }

}
