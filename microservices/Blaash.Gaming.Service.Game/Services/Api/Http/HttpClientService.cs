namespace Blaash.Gaming.Service.GamePlay
{
    public class HttpClientService
    {

        //public JWinnersDbResponse SaveWinnersData(WinnersDbo winnersDbo)
        //{
        //    HttpClient httpClient = new HttpClient();
        //    string json = JsonConvert.SerializeObject(winnersDbo);
        //    StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");

        //    var httpResponse = httpClient.PostAsync($"http://localhost:815/api/engt/savewinner", httpContent).GetAwaiter().GetResult();
        //    var stringResponse = httpResponse.Content.ReadAsStringAsync().Result;
        //    var response = JsonConvert.DeserializeObject<JWinnersDbResponse>(stringResponse);
        //    return response;
        //}

        //// What happens if HHTP fails?

        //public JBonusRewardDataResponse GetBonusRewardResponse(long EngtId)
        //{
        //    HttpClient httpClient = new HttpClient();
        //    var httpResponse = httpClient.GetAsync($"http://localhost:815/api/engt/bonusreward?engagement_id= {EngtId}").GetAwaiter().GetResult();
        //    var stringResponse = httpResponse.Content.ReadAsStringAsync().Result;
        //    var bonusRewards = JsonConvert.DeserializeObject<JBonusRewardDataResponse>(stringResponse);
        //    return bonusRewards;
        //}

    }
}
