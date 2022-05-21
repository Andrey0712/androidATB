using System.ComponentModel.DataAnnotations;

namespace WebAtb.Model
{
    /// <summary>
    /// Модель для створення товару
    /// </summary>
    public class ProductAddViewModel
    {
        [Display(Name = "Назва"), Required(ErrorMessage = "Поле 'Назва' не може бути пустим!")]
        public string Name { get; set; }
        [Display(Name = "Ціна"), Required(ErrorMessage = "Поле 'Ціна' не може бути пустим!")]
        public int Price { get; set; }
        [Display(Name = "Опис товару"), Required(ErrorMessage = "Поле 'Опис товару' не може бути пустим!")]
        public string Description { get; set; }
        [Display(Name = "Категория")]
        public int ProductCategoryId { get; set; }
        [Display(Name = "Титульна фотографія")]
        public IFormFile StartPhoto { get; set; }
        [Display(Name = "Дата створення")]
        public DateTime DateCreate { get; set; }
        [Display(Name = "Дата закінчення")]
        public DateTime DateFinish { get; set; }
        [Display(Name = "Фотографії")]
        public List<IFormFile> Images { get; set; }
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
        //public string Name { get; set; }

    }

    public class ProductViewModelImages
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public string StartPhoto { get; set; }
        public string DateCreate { get; set; }
        public string DateFinish { get; set; }
        public List<ProductImageItemViewModel> Images { get; set; }
    }

    public class ProductImageItemViewModel
    {
        public string Path { get; set; }
    }

    public class ProductRateViewModel
    {
        [Display(Name = "Номер лота"), Required(ErrorMessage = "Поле не може бути пустим!")]
        public int Id { get; set; }
        public int Price { get; set; }
    }
}
