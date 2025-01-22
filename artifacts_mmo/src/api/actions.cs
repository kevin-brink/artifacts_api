using ArtifactsAPI.Models;

namespace ArtifactsAPI
{
    public partial class APIHandler
    {
        public class ActionEndpoints(APIHandler apiHandler)
        {
            private readonly APIHandler _apiHandler = apiHandler;
            private string _path => $"my/{_apiHandler.character_name}/action";

            public async Task<MoveResponse> Move(int x, int y, bool wait_for_cooldown = true)
            {
                var endpoint = $"{_path}/move";
                var body = new { x, y };

                var response = await _apiHandler.handle_request(
                    endpoint,
                    HttpMethod.Post,
                    body: body
                );

                MoveResponse move = new(response);
                if (wait_for_cooldown)
                {
                    return move.WaitForCooldown<MoveResponse>();
                }

                return move;
            }

            public async Task<MoveResponse> Move(Schemas.Map map, bool wait_for_cooldown = true)
            {
                return await Move(map.x, map.y, wait_for_cooldown);
            }

            public async Task<RestResponse> Rest(bool wait_for_cooldown = true)
            {
                var endpoint = $"{_path}/rest";

                var response = await _apiHandler.handle_request(endpoint, HttpMethod.Post);

                RestResponse rest = new(response);
                if (wait_for_cooldown)
                {
                    return rest.WaitForCooldown<RestResponse>();
                }

                return rest;
            }

            public async Task<EquipResponse> EquipItem(
                string code,
                Slot slot,
                int quantity = 1,
                bool wait_for_cooldown = true
            )
            {
                var endpoint = $"{_path}/equip";

                Dictionary<string, string> body = [];
                body.Add("code", code);
                body.Add("slot", slot.ToString());
                if ((slot == Slot.utility1 || slot == Slot.utility2) && quantity != 1)
                {
                    ArgumentOutOfRangeException.ThrowIfLessThan(quantity, 1);
                    ArgumentOutOfRangeException.ThrowIfGreaterThan(quantity, 100);

                    body.Add("quantity", quantity.ToString());
                }

                var response = await _apiHandler.handle_request(
                    endpoint,
                    HttpMethod.Post,
                    body: body
                );

                EquipResponse equip = new(response);
                if (wait_for_cooldown)
                {
                    return equip.WaitForCooldown<EquipResponse>();
                }

                return equip;
            }

            public async Task<EquipResponse> UnequipItem(
                Slot slot,
                int quantity = 1,
                bool wait_for_cooldown = true
            )
            {
                var endpoint = $"{_path}/unequip";
                Dictionary<string, string> body = [];
                body.Add("slot", slot.ToString());
                if ((slot == Slot.utility1 || slot == Slot.utility2) && quantity != 1)
                {
                    ArgumentOutOfRangeException.ThrowIfLessThan(quantity, 1);
                    ArgumentOutOfRangeException.ThrowIfGreaterThan(quantity, 100);

                    body.Add("quantity", quantity.ToString());
                }

                var response = await _apiHandler.handle_request(
                    endpoint,
                    HttpMethod.Post,
                    body: body
                );

                EquipResponse unequip = new(response);
                if (wait_for_cooldown)
                {
                    return unequip.WaitForCooldown<EquipResponse>();
                }

                return unequip;
            }

            // UseItem

            public async Task<FightResponse> Fight(bool wait_for_cooldown = true)
            {
                var endpoint = $"{_path}/fight";

                var response = await _apiHandler.handle_request(endpoint, HttpMethod.Post);

                FightResponse fight = new(response);
                if (wait_for_cooldown)
                {
                    return fight.WaitForCooldown<FightResponse>();
                }

                return fight;
            }

            public async Task<GatheringResponse> Gathering(bool wait_for_cooldown = true)
            {
                var endpoint = $"{_path}/gathering";

                var response = await _apiHandler.handle_request(endpoint, HttpMethod.Post);

                GatheringResponse gather = new(response);
                if (wait_for_cooldown)
                {
                    return gather.WaitForCooldown<GatheringResponse>();
                }

                return gather;
            }

            public async Task<CraftingResponse> Crafting(
                string code,
                int quantity = 1,
                bool wait_for_cooldown = true
            )
            {
                var endpoint = $"{_path}/crafting";
                var body = new { code, quantity };

                var response = await _apiHandler.handle_request(
                    endpoint,
                    HttpMethod.Post,
                    body: body
                );

                CraftingResponse craft = new(response);
                if (wait_for_cooldown)
                {
                    return craft.WaitForCooldown<CraftingResponse>();
                }

                return craft;
            }

            public async Task<BankGoldTransactionResponse> DepositBankGold(
                int quantity,
                bool wait_for_cooldown = true
            )
            {
                ArgumentOutOfRangeException.ThrowIfLessThan(quantity, 1);

                var endpoint = $"{_path}/bank/deposit/gold";
                var body = new { quantity };

                var response = await _apiHandler.handle_request(
                    endpoint,
                    HttpMethod.Post,
                    body: body
                );

                BankGoldTransactionResponse depositGold = new(response);
                if (wait_for_cooldown)
                {
                    return depositGold.WaitForCooldown<BankGoldTransactionResponse>();
                }

                return depositGold;
            }

            public async Task<BankItemTransactionResponse> DepositBank(
                string code,
                int quantity = 1,
                bool wait_for_cooldown = true
            )
            {
                ArgumentOutOfRangeException.ThrowIfLessThan(quantity, 1);

                var endpoint = $"{_path}/bank/deposit";
                var body = new { code, quantity };

                var response = await _apiHandler.handle_request(
                    endpoint,
                    HttpMethod.Post,
                    body: body
                );

                BankItemTransactionResponse deposit = new(response);
                if (wait_for_cooldown)
                {
                    return deposit.WaitForCooldown<BankItemTransactionResponse>();
                }

                return deposit;
            }

            public async Task<BankItemTransactionResponse> WithdrawBank(
                string code,
                int quantity = 1,
                bool wait_for_cooldown = true
            )
            {
                ArgumentOutOfRangeException.ThrowIfLessThan(quantity, 1);

                var endpoint = $"{_path}/bank/withdraw";
                var body = new { code, quantity };

                var response = await _apiHandler.handle_request(
                    endpoint,
                    HttpMethod.Post,
                    body: body
                );

                BankItemTransactionResponse withdraw = new(response);
                if (wait_for_cooldown)
                {
                    return withdraw.WaitForCooldown<BankItemTransactionResponse>();
                }

                return withdraw;
            }

            public async Task<BankGoldTransactionResponse> WithdrawBankGold(
                int quantity,
                bool wait_for_cooldown = true
            )
            {
                ArgumentOutOfRangeException.ThrowIfLessThan(quantity, 1);

                var endpoint = $"{_path}/bank/withdraw/gold";
                var body = new { quantity };

                var response = await _apiHandler.handle_request(
                    endpoint,
                    HttpMethod.Post,
                    body: body
                );

                BankGoldTransactionResponse withdrawGold = new(response);
                if (wait_for_cooldown)
                {
                    return withdrawGold.WaitForCooldown<BankGoldTransactionResponse>();
                }

                return withdrawGold;
            }
        }
    }
}
