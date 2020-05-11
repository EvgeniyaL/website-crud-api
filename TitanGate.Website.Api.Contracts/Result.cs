namespace TitanGate.Website.Api.Contracts
{
    public class Result<TResult, TFail>
    {
        public bool IsSuccess { get; set; }

        public TResult Success { get; set; }

        public TFail Fail { get; set; }
    }
}
