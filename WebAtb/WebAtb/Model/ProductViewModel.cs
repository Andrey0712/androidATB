namespace WebAtb.Model
{
    public class CreateProductViewModel
    {
        public string Name { get; set; }
    }

    public class ProductItemViewModel
    {
        //public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string StartPhoto { get; set; }
        public DateTime DateFinish { get; set; }
    }

    public class ProductDelViewModel
    {
        public int Id { get; set; }

    }
}
