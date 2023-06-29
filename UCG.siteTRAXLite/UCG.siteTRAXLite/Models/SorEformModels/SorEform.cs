using UCG.siteTRAXLite.ViewModels;

namespace UCG.siteTRAXLite.Models.SorEformModels
{
    public class SorEform
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Question : BindableBase
    {
        public int Id { get; set; }

        public string Description { get; set; }

        private string response;
        public string Response
        {
            get { return response; }
            set
            {
                SetProperty(ref response, value);
            }
        }

        public int SorId { get; set; }
    }
}
