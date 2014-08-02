SharpValidation
===============

Sharp Validation is a very simple and extensible validation library.

Example Usage:

    public class ProductModel
    {
		public readonly Dictionary<string, string> Errors;
        public int ProductId { get; set; }
        public string Quantity { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Size { get; set; }
        public string UPC { get; set; }

		public ProductModel()
		{
			Errors = new Dictionary<string, string>();
		}

        public bool Validate()
        {
            var validator = new Validator<ProductModel>(this, Errors);

            return validator.Validate(m => m.Quantity)
                .IsNotNull()
                .WithMessage("Quantity is required")
                .AsBoolean()

                & validator.Validate(m => m.Name)
                .IsNotEmpty()
                .WithMessage("Product Name is required")
                .AsBoolean()
                ;
        }
    }