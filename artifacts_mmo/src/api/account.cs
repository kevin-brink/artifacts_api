using ArtifactsAPI.Models;

namespace ArtifactsAPI
{
    public partial class APIHandler
    {
        public class AccountEndpoints(APIHandler apiHandler)
        {
            private readonly APIHandler _apiHandler = apiHandler;
            private string _path => $"my";

            public async Task<BankResponse> GetBankDetails(bool wait_for_cooldown = true)
            {
                var endpoint = $"{_path}/bank";

                var response = await _apiHandler.handle_request(endpoint, HttpMethod.Get);

                BankResponse bankDetails = new(response);
                if (wait_for_cooldown)
                {
                    return bankDetails.WaitForCooldown<BankResponse>();
                }

                return bankDetails;
            }

            public async Task<BankItemResponse> GetBankItems(
                string item_code = "",
                int page = 1,
                int size = 50
            )
            {
                ArgumentOutOfRangeException.ThrowIfLessThan(page, 1);
                ArgumentOutOfRangeException.ThrowIfLessThan(size, 1);
                ArgumentOutOfRangeException.ThrowIfGreaterThan(size, 100);

                var endpoint = $"{_path}/bank/items";

                var query = new Dictionary<string, string>();
                if (item_code != "")
                    query.Add("item_code", item_code);
                if (page != 1)
                    query.Add("page", page.ToString());
                if (size != 50)
                    query.Add("size", size.ToString());

                var response = await _apiHandler.handle_request(
                    endpoint,
                    HttpMethod.Get,
                    urlParams: query
                );

                return new(response);
            }
        }
    }
}
