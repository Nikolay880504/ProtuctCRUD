using System.ComponentModel.DataAnnotations;

namespace FIrstProductCRUD.Admin.Pages
{
    public class ProductCreateViewModel
    {
        [Required(ErrorMessage = "Поле не может быть пустым")]
        [StringLength(50, ErrorMessage = "Название не должно превышать 50 символов")]
        public string Name { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Цена не может быть меньше нуля")]
        public double Price { get; set; }

        [Range(0, 1000000, ErrorMessage = "Код товара не может быть отрицательным числом")]
        public int Code { get; set; }

        [Range(1, 1000000, ErrorMessage = "Количество товара не может быть меньше единицы")]
        public int Quantity { get; set; }
    }
}
