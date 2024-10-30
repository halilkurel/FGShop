namespace FGShop.WebUI.Models.CategoryModels
{
    public class ResultCategoryModel
    {


        public List<CategoryResult> Data { get; set; }
        public object ValidationErrors { get; set; }
        public string Message { get; set; }
        public int ResponseType { get; set; }


    }
    public class CategoryResult
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string CoverPhoto { get; set; }
    }
}
