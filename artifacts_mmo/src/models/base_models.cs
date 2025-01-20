using ArtifactsAPI.Schemas;

namespace ArtifactsAPI.Models
{
    public abstract class BaseResponse
    {
        public StatusCode status_code { get; protected set; }
        public HttpResponseMessage response { get; protected set; }

        public Cooldown? cooldown { get; protected set; }

        public T WaitForCooldown<T>()
            where T : BaseResponse
        {
            WaitForCooldown(cooldown);
            return (T)this;
        }

        public static void WaitForCooldown(Cooldown? cooldown)
        {
            if (cooldown is null)
            {
                Console.WriteLine("No cooldown active");
                return;
            }

            Console.WriteLine(
                $"Cooldown is active after '{cooldown.reason}' until: '{cooldown.expiration.ToLocalTime()}'"
            );

            var currentTime = DateTime.UtcNow;
            var waitTime = cooldown.expiration - currentTime;
            if (waitTime.TotalSeconds > 0)
                Task.Delay(waitTime).Wait();
        }
    }
}
