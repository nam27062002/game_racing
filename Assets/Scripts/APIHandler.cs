using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class APIHandler : Singleton<APIHandler>
{
    
    [SerializeField] private string baseUrl;
    [SerializeField] private string getPromosAPI;
    [SerializeField] private string addPromoAPI;
    private string _userId;
    private PromoData _promoData;
    
    protected override void Awake()
    {
#if UNITY_EDITOR
        _userId = "20";
#else
        var url = Application.absoluteURL;
        if (!url.Contains("userId")) return;
        _userId = GetParameterValue(url, "userId");
        Debug.Log("userId: " + _userId);
#endif
    }

    private void Start()
    {
        StartCoroutine(GetListGamePromo(baseUrl,getPromosAPI));
    }

    public Promo GetPromoByScore(int score)
    {
        return score switch
        {
            >= 15 => GetPromoByRank(1),
            >= 10 => GetPromoByRank(2),
            >= 5 => GetPromoByRank(3),
            _ => GetPromoByRank(4)
        };
    }

    private Promo GetPromoByRank(int rank)
    {
        if (_promoData != null && _promoData.data != null)
        {
            Promo[] sortedPromos = _promoData.data.OrderByDescending(p => p.discount).ToArray();
            
            if (rank > 0 && rank <= sortedPromos.Length)
            {
                return sortedPromos[rank - 1];
            }
            Debug.LogWarning("Invalid rank. Please provide a valid rank.");
        }
        else
        {
            Debug.LogWarning("Promo data is null or data array is null.");
        }
        
        return null;
    }
    
    private IEnumerator GetListGamePromo(string url,string api)
    {
        var www = UnityWebRequest.Get(url + api);

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error calling API: " + www.error);
        }
        else
        {
            Debug.Log("API Response: " + www.downloadHandler.text);
            _promoData = JsonUtility.FromJson<PromoData>(www.downloadHandler.text);
            foreach (var promo in _promoData.data)
            {
                Debug.Log($"ID: {promo.id}, Title: {promo.title}, Discount: {promo.discount}%");
            }
        }
    }
   
    private IEnumerator AddPromoForUser(string url,string api,string promoId)
    {
        var jsonData = "{\"promoID\":\"" + promoId + "\",\"users\":[{\"userID\":\"" + _userId + "\"}]}";

        using var request = UnityWebRequest.PostWwwForm(url + api, "POST");
        request.SetRequestHeader("Content-Type", "application/json");

        var bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        
        yield return request.SendWebRequest();
        
        if (request.isNetworkError || request.isHttpError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            Debug.Log("API Response: " + request.downloadHandler.text);
        }
    }

    private static string GetParameterValue(string url, string paramName)
    {
        var index = url.IndexOf(paramName, StringComparison.Ordinal);
        if (index == -1) return null;
        var startIndex = url.IndexOf("=", index, StringComparison.Ordinal) + 1;
        var endIndex = url.IndexOf("&", startIndex, StringComparison.Ordinal);
        if (endIndex == -1)
            endIndex = url.Length;
        return url.Substring(startIndex, endIndex - startIndex);
    }
}