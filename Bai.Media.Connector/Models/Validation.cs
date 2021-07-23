namespace Bai.Media.Connector.Models
{
    public class Validation<TModel>
        where TModel : class
    {
        public Validation(TModel model) =>
            Model = model;
        public Validation(string message) =>
            Message = message;

        public TModel Model { get; set; }
        public string Message { get; set; }
        public bool IsSuccess => Message == null;
    }
}
